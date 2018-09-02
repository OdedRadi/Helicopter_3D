using Engine;
using System.Windows.Forms;

namespace Forms
{
	class Program
	{
		static void Main(string[] args)
		{
			SceneEngine sceneEngine = new SceneEngine();
			MainWindow mainWindow = new MainWindow(sceneEngine);

			sceneEngine.Start();
			mainWindow.ShowDialog();
		}
	}
}
