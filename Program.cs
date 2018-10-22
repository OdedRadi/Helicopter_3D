using Logics;
using Forms;
using System.Windows.Forms;

namespace Helicopter
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SceneEngine sceneEngine = new SceneEngine();
			MainWindow mainWindow = new MainWindow(sceneEngine);

			sceneEngine.Start();

			Application.Run(mainWindow);
		}
	}
}
