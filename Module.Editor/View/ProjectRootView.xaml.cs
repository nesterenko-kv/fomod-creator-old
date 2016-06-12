using FomodInfrastructure.Interfaces;

namespace Module.Editor.View
{
    public partial class ProjectView
    {
        private readonly ILogger _logger;

        public ProjectView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~ProjectView()
        {
            _logger.LogDisposable(this);
        }
    }
}