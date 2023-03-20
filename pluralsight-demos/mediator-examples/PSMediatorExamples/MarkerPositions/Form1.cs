namespace MarkerPositions
{
    public partial class Form1 : Form
    {

        private MarkerMediator mediator = new MarkerMediator();
        private Button addButton;

        public Form1()
        {
            InitializeComponent();
            addButton = new Button();
            addButton.Click += OnAddClick;
            addButton.Text = "Add Marker";
            addButton.Dock = DockStyle.Bottom;
            Controls.Add(addButton);
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            var m = mediator.CreateMarker();
            Controls.Add(m);
        }
    }
}