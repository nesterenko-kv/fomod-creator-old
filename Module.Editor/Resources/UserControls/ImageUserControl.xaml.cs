using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        public static readonly DependencyProperty ImageDataSourceProperty =
            DependencyProperty.Register("ImageDataSource", typeof(object), typeof(ImageUserControl), new FrameworkPropertyMetadata(null, ImageSourceChanged)
            {
                BindsTwoWayByDefault = false
            });

        public static readonly DependencyProperty ProjectFolderPathProperty =
           DependencyProperty.Register("ProjectFolderPath", typeof(string), typeof(ImageUserControl), new FrameworkPropertyMetadata (null, ChangePath));

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

        public object ImageDataSource
        {
            get { return GetValue(ImageDataSourceProperty); }
            set { SetValue(ImageDataSourceProperty, value); }
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

        private static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue is INotifyPropertyChanged)
                ((INotifyPropertyChanged)args.OldValue).PropertyChanged -= (s, a) => ChangePath(sender, args);
            if (args.NewValue is INotifyPropertyChanged)
                ((INotifyPropertyChanged)args.NewValue).PropertyChanged += (s, a) => ChangePath(sender, args);
        }
        
        private static void ChangePath(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as ImageUserControl;
            if (control == null) return;
            var folderParh = control.ProjectFolderPath;
            var imageSubPath = (control.ImageDataSource as dynamic).Path.Trim('\\', '/');
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
                control.Image = bitmap;
            }
            else
                control.Image = null;
        }
    }
}
