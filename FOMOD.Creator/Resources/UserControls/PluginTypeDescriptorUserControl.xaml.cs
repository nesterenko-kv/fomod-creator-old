namespace FOMOD.Creator.Resources.UserControls
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class PluginTypeDescriptorUserControl : INotifyPropertyChanged
    {
        public static readonly DependencyProperty DescriptorProperty = DependencyProperty.Register("Descriptor", typeof(PluginTypeDescriptor), typeof(PluginTypeDescriptorUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true,
            PropertyChangedCallback = TypeDescriptorPropertyChanged
        });

        private ICommand _changeTypeCommand;

        private object _pluginType;

        private object _previewPluginType;

        public PluginTypeDescriptorUserControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                            var dependency = Descriptor.DependencyType.DefaultType;
                            Descriptor.DependencyType = null;
                            Descriptor.Type = dependency;
                        }
                        else
                        {
                            if (_previewPluginType is DependencyPluginType)
                            {
                                var dependency = Descriptor.Type;
                                Descriptor.Type = null;
                                Descriptor.DependencyType = (DependencyPluginType) _previewPluginType;
                                Descriptor.DependencyType.DefaultType = dependency;
                            }
                            else
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        if (PluginTypeData is PluginType)
                        {
                            var dependency = DependencyPluginType.Create();
                            dependency.DefaultType = Descriptor.Type;
                            Descriptor.Type = null;
                            Descriptor.DependencyType = dependency;
                            Descriptor.DependencyType.Patterns = new ObservableCollection<DependencyPattern>();
                        }
                        else
                        {
                            if (PluginTypeData is DependencyPluginType)
                            {
                                var dependency = Descriptor.DependencyType.DefaultType;
                                Descriptor.DependencyType = null;
                                Descriptor.Type = dependency;
                            }
                        }
                    }
                    _previewPluginType = temp;
                }));
            }
        }

        public PluginTypeDescriptor Descriptor
        {
            get
            {
                return (PluginTypeDescriptor) GetValue(DescriptorProperty);
            }
            set
            {
                SetValue(DescriptorProperty, value);
            }
        }

        public object PluginTypeData
        {
            get
            {
                return _pluginType;
            }
            set
            {
                _pluginType = value;
                OnPropertyChanged(nameof(PluginTypeData));
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private static void TypeDescriptorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (PluginTypeDescriptorUserControl) d;

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
            else if (sender.DependencyType != null && sender.Type == null)
                PluginTypeData = sender.DependencyType;
        }
    }
}
