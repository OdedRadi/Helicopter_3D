using System;
using System.Diagnostics;
using System.Windows.Forms;
using Engine;

namespace Forms
{
	public partial class MainWindow : Form
	{
		public MainWindow(SceneEngine engine)
		{
			InitializeComponent();

            engine.Init(drawPanel.Size, (uint)drawPanel.Handle.ToInt32());
            KeyDown += engine.HandleKeyDown;
			KeyUp += engine.HandleKeyUp;
			MouseWheel += engine.HandleMousheWheel;
        }
	}
}
