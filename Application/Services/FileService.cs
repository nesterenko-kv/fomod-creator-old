using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MainApplication.Services
{
    public class FileService
    {
        IRepository<XmlDataProvider> _repository;
        public FileService(IRepository<XmlDataProvider> repository)
        {
            _repository = repository;
        }


        private void CreateSubFolder(string subpath)
        {
            var path = $"{_repository.CurrentPath}\\{subpath}";
            Directory.CreateDirectory(path);
        }
        private void RenameSubFolder(string oldSubpath, string newSubpath)
        {
            var oldpath = $"{_repository.CurrentPath}\\{oldSubpath}";
            var newpath = $"{_repository.CurrentPath}\\{newSubpath}";
            Directory.Move(oldSubpath, newSubpath);
        }


    }
}
