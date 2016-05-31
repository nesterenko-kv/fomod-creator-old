using EditorNew.View;
using EditorNew.ViewModel;
using FomodInfrastructure;
using FomodInfrastructure.Interface;
using FomodModel.Base;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditorNew
{
    /// <summary>
    /// Класс отвечающий за открытие проектов
    /// </summary>
    public class Watcher
    {
        #region Services

        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        #endregion

        public Watcher(IRegionManager regionManager, IServiceLocator serviceLocator, IEventAggregator eventAggregator, ILogger logger)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _logger = logger;

            _logger.LogCreate(this);
            _eventAggregator.GetEvent<PubSubEvent<IRepository<ProjectRoot>>>().Subscribe(OpenProject);
        }
        ~Watcher()
        {
            _logger.LogDisposable(this);
        }

        private void OpenProject(IRepository<ProjectRoot> dataObj)
        {
            _logger.Log($"[Open Project Subscribe Event] " + dataObj.GetData().FolderPath);

            _logger.Log($"[Find open view]");


            var reg = _regionManager.Regions[Names.MainContentRegion];
            var views = reg.Views;

            var openView = _regionManager.Regions[Names.MainContentRegion].Views.Cast<FrameworkElement>().FirstOrDefault(view =>
            {
                return 
                view.DataContext is MainEditorViewModel
                &&
                (view.DataContext as MainEditorViewModel).Data.FolderPath == dataObj.GetData().FolderPath;
            });

            if (openView != null)
            {
                _logger.Log($"[Activate old View] {openView.GetHashCode()}" + openView.GetHashCode());
                _regionManager.Regions[Names.MainContentRegion].Activate(openView);
            }
            else
            {
                _logger.Log($"[Create new View]");
                var view = CreateNewView(dataObj);
                _logger.LogCreate(view);
                _regionManager.Regions[Names.MainContentRegion].Activate(view);
                _logger.Log($"[Activate View] " + view.GetHashCode());
            }

        }

        private MainEditorView CreateNewView(IRepository<ProjectRoot> dataObj)
        {
            var view = _serviceLocator.GetInstance<object>(nameof(MainEditorView)) as MainEditorView;
            var viewmodel = view.DataContext as MainEditorViewModel;
            var detailsRegion = _regionManager.Regions[Names.MainContentRegion];
            var detailsRegionManager = detailsRegion.Add(view, null, true);
            viewmodel.ConfigurateViewModel(detailsRegionManager, dataObj);
            return view;
        }
    }
}
