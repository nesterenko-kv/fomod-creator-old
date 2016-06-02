using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    public partial class PluginTypeDescriptorUserControl : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion

        public PluginTypeDescriptorUserControl()
        {
            InitializeComponent();
        }

        public PluginTypeDescriptor Descriptor
        {
            get { return (PluginTypeDescriptor)GetValue(DescriptorProperty); }
            set { SetValue(DescriptorProperty, value); }
        }

        public static readonly DependencyProperty DescriptorProperty =
            DependencyProperty.Register("Descriptor", typeof(PluginTypeDescriptor), typeof(PluginTypeDescriptorUserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = null,
                BindsTwoWayByDefault = true,
                PropertyChangedCallback = TypeDescriptorPropertyChanged
            });

        private static void TypeDescriptorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var sender = (PluginTypeDescriptorUserControl)d;

            if (e.OldValue != null)
            {
                var inpch = (e.OldValue as INotifyPropertyChanged);
                if (inpch != null)
                    inpch.PropertyChanged -= sender.TypeDescriptor_PropertyChanged;
            }
            if (e.NewValue != null)
            {
                var inpch = (e.NewValue as INotifyPropertyChanged);
                if (inpch != null)
                    inpch.PropertyChanged += sender.TypeDescriptor_PropertyChanged;
            }

            sender.TypeDescriptor_PropertyChanged(e.NewValue, new PropertyChangedEventArgs(string.Empty));
        }

        private void TypeDescriptor_PropertyChanged(object s, PropertyChangedEventArgs e)
        {
            var sender = s as PluginTypeDescriptor;
            if (sender == null) return;

            if (sender.DependencyType == null && sender.Type != null)
                PluginTypeData = sender.Type;
            else if (sender.DependencyType != null && sender.Type == null)
                PluginTypeData = sender.DependencyType;
            //else if (sender.DependencyType == null && sender.Type == null)
            //    sender.Type = PluginType.Create();
            //else if (sender.DependencyType != null && sender.Type != null)
            //    sender.Type = null;
        }

        private object _pluginType;
        public object PluginTypeData
        {
            get { return _pluginType; }
            set
            {
                _pluginType = value;
                OnPropertyChanged(nameof(PluginTypeData));
            }
        }

        private object _previewPluginType;
        private ICommand _changeTypeCommand;
        public ICommand ChangeTypeCommand
        {
            get
            {
                return _changeTypeCommand ?? (_changeTypeCommand = new RelayCommand(() =>
                {
                    var temp = PluginTypeData;

                    if (_previewPluginType != null)
                        if (_previewPluginType is PluginType)
                        {
                            Descriptor.DependencyType = null;
                            Descriptor.Type = (PluginType)_previewPluginType;
                        }
                        else if (_previewPluginType is DependencyPluginType)
                        {
                            Descriptor.Type = null;
                            Descriptor.DependencyType = (DependencyPluginType)_previewPluginType;
                        }
                        else
                            throw new ArgumentException("при смене типа произошла ошибка (ChangeTypeCommand)");
                    else if (PluginTypeData is PluginType)
                    {
                        Descriptor.Type = null;
                        Descriptor.DependencyType = DependencyPluginType.Create();
                    }
                    else if (PluginTypeData is DependencyPluginType)
                    {
                        Descriptor.DependencyType = null;
                        Descriptor.Type = PluginType.Create();
                    }
                    _previewPluginType = temp;
                }));
            }
        }
    }
}
