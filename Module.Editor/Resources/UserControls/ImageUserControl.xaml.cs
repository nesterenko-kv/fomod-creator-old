using FomodModel.Base.ModuleCofiguration;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Data;
using System.Globalization;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ImageUserControl.xaml
    /// </summary>
    public partial class ImageUserControl : INotifyPropertyChanged
    {
        #region DependencyProperties

        public static readonly DependencyProperty BrowseImageCommandProperty =
              DependencyProperty.Register("BrowseImageCommand", typeof(ICommand), typeof(ImageUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ProjectFolderPathProperty =
           DependencyProperty.Register("ProjectFolderPath", typeof(string), typeof(ImageUserControl), 
               new FrameworkPropertyMetadata
               {
                   DefaultValue = null,
                   BindsTwoWayByDefault = true,
                   PropertyChangedCallback = ChangeImageSource
               });

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Image), typeof(ImageUserControl), 
                new FrameworkPropertyMetadata
                {
                    DefaultValue = null,
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = ChangeImageSource
                });

        private static void ChangeImageSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (ImageUserControl)d;
            var oldValue = (e.OldValue as INotifyPropertyChanged);
            var newValue = (e.NewValue as INotifyPropertyChanged);
            if (e.Property.Name == nameof(ImageUserControl.ProjectFolderPath))
                sender.ChangeImagePaths();
            else if(e.Property.Name == nameof(ImageUserControl.ImageSource))
            {
                if (oldValue != null)
                    oldValue.PropertyChanged -= sender.Image_PropertyChanged;
                if (newValue != null)
                {
                    newValue.PropertyChanged += sender.Image_PropertyChanged;
                    sender.ChangeImagePaths();
                }
            }
            else
                throw new ArgumentException();
        }

        private void Image_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(FomodModel.Base.ModuleCofiguration.Image.Path))
                this.ChangeImagePaths();
        }


        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion

        public ImageUserControl()
        {
            InitializeComponent();
        }
        
        #region Properties

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            private set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public Image ImageSource
        {
            get { return (Image)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public string ProjectFolderPath
        {
            get { return (string)GetValue(ProjectFolderPathProperty); }
            set { SetValue(ProjectFolderPathProperty, value); }
        }

        public ICommand BrowseImageCommand
        {
            get { return (ICommand)GetValue(BrowseImageCommandProperty); }
            set { SetValue(BrowseImageCommandProperty, value); }
        }


        #endregion


        private void ChangeImagePaths()
        {
            var folderParh = this.ProjectFolderPath;
            var imageSubPath = this.ImageSource?.Path;
            if (string.IsNullOrWhiteSpace(folderParh) || string.IsNullOrWhiteSpace(imageSubPath))
            {
                Image = null;
                return;
            }

            var fullpath = Path.Combine(folderParh, imageSubPath);
            if (Directory.Exists(folderParh) &&
                File.Exists(fullpath))
            {
                var bitmap = new BitmapImage();
                using (var stream = File.OpenRead(fullpath))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                Image = bitmap;
            }
            else
                Image = null;
        }
    }
}
