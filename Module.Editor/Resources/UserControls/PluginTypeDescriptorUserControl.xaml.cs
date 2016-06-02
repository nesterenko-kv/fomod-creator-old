using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class PluginTypeDescriptorUserControl : INotifyPropertyChanged
    {
        public static readonly DependencyProperty DescriptorProperty = DependencyProperty.Register("Descriptor", typeof(PluginTypeDescriptor), typeof(PluginTypeDescriptorUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true, PropertyChangedCallback = TypeDescriptorPropertyChanged });

        private ICommand _changeTypeCommand;

        private object _pluginType;

        private object _previewPluginType;

        public PluginTypeDescriptorUserControl()
        {
            InitializeComponent();
        }

        public PluginTypeDescriptor Descriptor
        {
            get { return (PluginTypeDescriptor)GetValue(DescriptorProperty); }
            set { SetValue(DescriptorProperty, value); }
        }

        public object PluginTypeData
        {
            get { return _pluginType; }
            set
            {
                _pluginType = value;
                OnPropertyChanged(nameof(PluginTypeData));
            }
        }

        public ICommand ChangeTypeCommand
        {
            get
            {
                return _changeTypeCommand ?? (_changeTypeCommand = new RelayCommand(() =>
                {
                    var temp = PluginTypeData;

                    if (_previewPluginType != null)
                    {
                        if (_previewPluginType is PluginType)
                        {
                            Descriptor.DependencyType = null;
                            Descriptor.Type = (PluginType)_previewPluginType;
                        }
                        else
                        {
                            if (_previewPluginType is DependencyPluginType)
                            {
                                Descriptor.Type = null;
                                Descriptor.DependencyType = (DependencyPluginType)_previewPluginType;
                            }
                            else
                                throw new ArgumentException("при смене типа произошла ошибка (ChangeTypeCommand)");
                        }
                    }
                    else
                    {
                        if (PluginTypeData is PluginType)
                        {
                            Descriptor.Type = null;
                            Descriptor.DependencyType = DependencyPluginType.Create();
                        }
                        else
                        {
                            if (PluginTypeData is DependencyPluginType)
                            {
                                Descriptor.DependencyType = null;
                                Descriptor.Type = PluginType.Create();
                            }
                        }
                    }
                    _previewPluginType = temp;
                }));
            }
        }

        private static void TypeDescriptorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (PluginTypeDescriptorUserControl)d;

            if (e.OldValue != null)
            {
                var inpch = e.OldValue as INotifyPropertyChanged;
                if (inpch != null)
                    inpch.PropertyChanged -= sender.TypeDescriptorPropertyChanged;
            }
            if (e.NewValue != null)
            {
                var inpch = e.NewValue as INotifyPropertyChanged;
                if (inpch != null)
                    inpch.PropertyChanged += sender.TypeDescriptorPropertyChanged;
            }

            sender.TypeDescriptorPropertyChanged(e.NewValue, new PropertyChangedEventArgs(string.Empty));
        }

        private void TypeDescriptorPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            var sender = s as PluginTypeDescriptor;
            if (sender == null)
                return;

            if (sender.DependencyType == null && sender.Type != null)
                PluginTypeData = sender.Type;
            else
            {
                if (sender.DependencyType != null && sender.Type == null)
                    PluginTypeData = sender.DependencyType;
            }
            //else if (sender.DependencyType == null && sender.Type == null)
            //    sender.Type = PluginType.Create();
            //else if (sender.DependencyType != null && sender.Type != null)
            //    sender.Type = null;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}