namespace FOMOD.Creator.Resources.UserControls
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class ImageUserControl : INotifyPropertyChanged
    {
        public static readonly DependencyProperty BrowseImageCommandProperty = DependencyProperty.Register(nameof(BrowseImageCommand), typeof(ICommand), typeof(ImageUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(Image), typeof(ImageUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true,
            PropertyChangedCallback = ChangeImageSource
        });

        public static readonly DependencyProperty ProjectFolderPathProperty = DependencyProperty.Register(nameof(ProjectFolderPath), typeof(string), typeof(ImageUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true,
            PropertyChangedCallback = ChangeImageSource
        });

        private BitmapImage _image;

        public ImageUserControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BrowseImageCommand
        {
            get
            {
                return (ICommand) GetValue(BrowseImageCommandProperty);
            }
            set
            {
                SetValue(BrowseImageCommandProperty, value);
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            private set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public Image ImageSource
        {
            get
            {
                return (Image) GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public string ProjectFolderPath
        {
            get
            {
                return (string) GetValue(ProjectFolderPathProperty);
            }
            set
            {
                SetValue(ProjectFolderPathProperty, value);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void ChangeImageSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (ImageUserControl) d;
            var oldValue = e.OldValue as INotifyPropertyChanged;
            var newValue = e.NewValue as INotifyPropertyChanged;
            switch (e.Property.Name)
            {
                case nameof(ProjectFolderPath):
                    sender.ChangeImagePaths();
                    break;
                case nameof(ImageSource):
                    if (oldValue != null)
                        oldValue.PropertyChanged -= sender.ImagePropertyChanged;
                    if (newValue != null)
                    {
                        newValue.PropertyChanged += sender.ImagePropertyChanged;
                        sender.ChangeImagePaths();
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private void ChangeImagePaths()
        {
            var folderPath = ProjectFolderPath;
            var imageSubPath = ImageSource?.Path;
            if (!string.IsNullOrWhiteSpace(folderPath) && !string.IsNullOrWhiteSpace(imageSubPath))
            {
                var fullPath = Path.Combine(folderPath, imageSubPath);
                if (File.Exists(fullPath))
                {
                    var bitmap = new BitmapImage();
                    using (var stream = File.OpenRead(fullPath))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                    }
                    Image = bitmap;
                    return;
                }
            }
            Image = null;
        }

        private void ImagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Domain.Models.ModuleCofiguration.Image.Path))
                ChangeImagePaths();
        }
    }
}
