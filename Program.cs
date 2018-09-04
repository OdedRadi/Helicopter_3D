using Engine;
using Forms;

namespace Helicopter
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
