using FomodInfrastructure.Interface;

namespace Module.Editor.View
{
    public partial class MainEditorView
    {
        private readonly ILogger _logger;

        public MainEditorView(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            _logger.LogCreate(this);
        }

        ~MainEditorView()
        {
            _logger.LogDisposable(this);
        }
    }
}