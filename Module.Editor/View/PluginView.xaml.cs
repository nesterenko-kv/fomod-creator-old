using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class PluginView
    {
        private readonly ILogger _logger;

        public PluginView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~PluginView()
        {
            _logger.LogDisposable(this);
        }
    }

}