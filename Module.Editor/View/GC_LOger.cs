using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class MainEditorView
    {
        private readonly ILogger _logger;

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
        private readonly ILogger _logger;

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
        private readonly ILogger _logger;

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
        private readonly ILogger _logger;

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
        private readonly ILogger _logger;

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