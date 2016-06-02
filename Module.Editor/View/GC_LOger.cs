using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Editor.View
{
    public partial class MainEditorView
    {
        ILogger _logger;

        public MainEditorView(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }
        ~MainEditorView()
        {
            _logger.LogDisposable(this);
        }
    }


    public partial class GroupView
    {
        ILogger _logger;

        public GroupView(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }
        ~GroupView()
        {
            _logger.LogDisposable(this);
        }
    }

    public partial class InstallStepView
    {
        ILogger _logger;

        public InstallStepView(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }
        ~InstallStepView()
        {
            _logger.LogDisposable(this);
        }
    }
    public partial class PluginView
    {
        ILogger _logger;

        public PluginView(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }
        ~PluginView()
        {
            _logger.LogDisposable(this);
        }
    }


    public partial class ProjectRootView
    {
        ILogger _logger;

        public ProjectRootView(ILogger logger)
        {
            _logger = logger;
            _logger.LogCreate(this);
        }
        ~ProjectRootView()
        {
            _logger.LogDisposable(this);
        }
    }
}
