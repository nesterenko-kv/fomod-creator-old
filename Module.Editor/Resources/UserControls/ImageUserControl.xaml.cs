using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ImageUserControl.xaml
    /// </summary>
    public partial class ImageUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ImageUserControl()
        {
            InitializeComponent();
        }




        public object ImageDataSource
        {
            get { return (object)GetValue(ImageDataSourceProperty); }
            set { SetValue(ImageDataSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageDataSourceProperty =
            DependencyProperty.Register("ImageDataSource", typeof(object), typeof(ImageUserControl), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = false,
                DefaultValue = null,
                PropertyChangedCallback = ImageSourceChanged
            });

        private static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue is INotifyPropertyChanged)
                ((INotifyPropertyChanged)args.OldValue).PropertyChanged -= (s, a) => ChangePath(sender, args);
            if (args.NewValue is INotifyPropertyChanged)
                ((INotifyPropertyChanged)args.NewValue).PropertyChanged += (s, a) => ChangePath(sender, args);
        }


        public string ProjectFolderPath
        {
            get { return (string)GetValue(ProjectFolderPathProperty); }
            set { SetValue(ProjectFolderPathProperty, value); }
        }

        public static readonly DependencyProperty ProjectFolderPathProperty =
            DependencyProperty.Register("ProjectFolderPath", typeof(string), typeof(ImageUserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = null,
                PropertyChangedCallback = ChangePath
            });


        private static void ChangePath(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as ImageUserControl;
            var folderParh = control.ProjectFolderPath;
            var imageSubPath = (control.ImageDataSource as dynamic).Path.Trim('\\', '/');
            var fullpath = System.IO.Path.Combine(folderParh, imageSubPath);

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
            {
                control.Image = null;
            }
        }

        BitmapImage _image;
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


        public ICommand BrowseImageCommand
        {
            get { return (ICommand)GetValue(BrowseImageCommandProperty); }
            set { SetValue(BrowseImageCommandProperty, value); }
        }

        public static readonly DependencyProperty BrowseImageCommandProperty =
            DependencyProperty.Register("BrowseImageCommand", typeof(ICommand), typeof(ImageUserControl), new PropertyMetadata(null));


    }
}
