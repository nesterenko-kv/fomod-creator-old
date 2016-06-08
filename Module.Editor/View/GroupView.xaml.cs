using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class GroupView
    {
        private readonly ILogger _logger;

        public GroupView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~GroupView()
        {
            _logger.LogDisposable(this);
        }
    }
}