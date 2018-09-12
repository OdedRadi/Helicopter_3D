using OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Logics
{
	class TextureLoader
	{
		public static uint[] LoadTextures(string[] bmpFilesPath)
		{
			uint[] textureList = new uint[bmpFilesPath.Length];

			GL.glGenTextures(textureList.Length, textureList);

			for (int i = 0; i < textureList.Length; i++)
			{
				Bitmap image = new Bitmap(bmpFilesPath[i]);
				image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards

				Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
				BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

				GL.glBindTexture(GL.GL_TEXTURE_2D, textureList[i]);
				//  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
				//  OpenGL.cs
				//  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
				//  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
				GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
					0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapData.Scan0);
				GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);      // Linear Filtering
				GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);      // Linear Filtering

				image.UnlockBits(bitmapData);
				image.Dispose();
			}

			return textureList;
		}
	}
}
