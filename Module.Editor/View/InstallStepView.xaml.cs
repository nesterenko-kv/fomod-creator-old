using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class InstallStepView
    {
        private readonly ILogger _logger;

        public InstallStepView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~InstallStepView()
        {
            _logger.LogDisposable(this);
        }
    }
}