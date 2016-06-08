using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class ProjectRootView
    {
        private readonly ILogger _logger;

        public ProjectRootView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~ProjectRootView()
        {
            _logger.LogDisposable(this);
        }
    }
}