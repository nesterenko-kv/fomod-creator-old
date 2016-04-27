using System.IO;
using System.Xml.Serialization;
using FomodInfrastructure.Interface;
using Gat.Controls;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using FomodModel.Base.ModuleConfiguration;

namespace MainApplication.Services
{
    public class Repository: IRepository<ProjectRoot>
    {
        private string _infoSubPath = @"\fomod\info.xml";
        private string _configurationSubPath = @"\fomod\ModuleConfig.xml";
        private ProjectRoot ProjectRoot;

        private readonly IServiceLocator ServiceLocator;

        public Repository(IServiceLocator ServiceLocator)
        {
            this.ServiceLocator = ServiceLocator;
        }


        #region IRepository

        public ProjectRoot LoadData(string path = null)
        {
            return this.ProjectRoot = (path != null ? this.LoadProjectFromPath(path) : this.LoadProjectIfPathNull());
        }

        public bool SaveData(string path = null)
        {
            return (path != null ? this.SaveProjectFromPath(path) : this.SaveProjectIfPathNull());
        }

        public ProjectRoot GetData()
        {
            return ProjectRoot;
        }

        #endregion



        #region Private methmods

        private bool SerializeObject<T>(T data, string path)
        {
            //if (data == null || !File.Exists(path)) return false;
            using (var fs = File.Create(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(fs, data);
            }
            return true;
        }
        private T DeserializeObject<T>(string path)
        {
            //if (!File.Exists(path)) return default(T);
            using (var fs = File.OpenRead(path))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(fs);
            }
        }

        private bool CheckFiles(string FolderPath)
        {
            if (File.Exists(FolderPath + _infoSubPath) && File.Exists(FolderPath + _configurationSubPath))
            {
                return true;
            }
            return false;
        }

        private ProjectRoot LoadProjectIfPathNull()
        {
            var _folderPath = this.GetFolderPath();

            if (_folderPath != null)
            {
                return this.LoadProjectFromPath(_folderPath);
            }
            return null;
        }
        private ProjectRoot LoadProjectFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();

            if (CheckFiles(path))
            {
                var ProjectRoot = this.ServiceLocator.GetInstance<ProjectRoot>();

                try
                {
                    ProjectRoot.ModuleInformation = this.DeserializeObject<ModuleInformation>(path + _infoSubPath);
                    ProjectRoot.ModuleConfiguration = this.DeserializeObject<ModuleConfiguration>(path + _configurationSubPath);

                    return ProjectRoot;
                }
                catch (System.Exception)
                {
                    //если ошибка то позже сделаем сервис оповещения об ошибках
                }
            }

            throw new FileNotFoundException();
        }

        private bool SaveProjectIfPathNull()
        {
            if (this.ProjectRoot == null)
                throw new FileNotFoundException();

            var _folderPath = this.GetFolderPath();

            if (_folderPath != null)
            {
                return this.SaveProjectFromPath(_folderPath);
            }

            return false;
        }
        private bool SaveProjectFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new FileNotFoundException();

            Directory.CreateDirectory(path + @"\fomod\");

            try
            {
                this.SerializeObject<ModuleInformation>(ProjectRoot.ModuleInformation, path + _infoSubPath);
                this.SerializeObject<ModuleConfiguration>(ProjectRoot.ModuleConfiguration, path + _configurationSubPath);

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        private string GetFolderPath()
        {
            OpenDialogView openDialog = new OpenDialogView();
            OpenDialogViewModel vm = (OpenDialogViewModel)openDialog.DataContext;
            vm.IsDirectoryChooser = true;

            bool? result = vm.Show();
            if (result == true)
            {
                return vm.SelectedFolder.Path;
            }
            return null;
        }

        #endregion
    }
}