using System.Windows.Forms;
using Logics;

namespace Forms
{
	public partial class MainWindow : Form
	{
		private LookAtForm lookAtForm;
		private LightPositionForm lightPositionForm;

		public MainWindow(SceneEngine engine)
		{
			InitializeComponent();

			// engine init
			engine.Init(drawPanel.Size, (uint)drawPanel.Handle.ToInt32());
			KeyDown += engine.HandleKeyDown;
			KeyUp += engine.HandleKeyUp;
			MouseWheel += engine.HandleMouseWheel;

			// settings forms init
			lookAtForm = new LookAtForm(engine);
			lightPositionForm = new LightPositionForm(engine);
        }

		private void menuItemLookAt_Click(object sender, System.EventArgs e)
		{
			lookAtForm.Show();
		}

		private void menuItemLight_Click(object sender, System.EventArgs e)
		{
			lightPositionForm.Show();
		}
	}
}
