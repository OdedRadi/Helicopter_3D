using OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Graphics
{
	public class SkyBox : IGraphicComponent
	{
		private const float m_textureSize = 1024.0f;
		private const float m_acsendingDecsendingSpeed = 4;
		private const float m_rotatingSpeed = 1;
		private const float m_forwardBackwardSpeed = 4;
		private const float m_sideFlyingSpeed = 4;

		private uint m_frontTexture;
		private uint m_backTexture;
		private uint m_topTexture;
		private uint m_bottomTexture;
		private uint m_rightTexture;
		private uint m_leftTexture;
		
		private float m_xTranslate;
		private float m_yTranslate;
		private float m_zTranslate;
		private float m_yRotate;

		public eThrottleStick ThrottleStickState{ get; set; }
		public eDirectionStick DirectionStickState { get; set; }

		public Action<eDirectionStick> DirectionLimitsEvent;

		public void Init()
		{
			initTexture();
			m_yTranslate = m_textureSize - 1;
			m_zTranslate = -m_textureSize + 300;
		}

		private void initTexture()
		{
			uint[] textureList = new uint[6];
			string[] bmpFilesPath = new string[textureList.Length];

			bmpFilesPath[0] = "../../Resources/environment_front.bmp";
			bmpFilesPath[1] = "../../Resources/environment_back.bmp";
			bmpFilesPath[2] = "../../Resources/environment_top.bmp";
			bmpFilesPath[3] = "../../Resources/environment_bottom.bmp";
			bmpFilesPath[4] = "../../Resources/environment_right.bmp";
			bmpFilesPath[5] = "../../Resources/environment_left.bmp";

			GL.glGenTextures(textureList.Length, textureList);
			for (int i = 0; i < textureList.Length; i++)
			{
				Bitmap image = new Bitmap(bmpFilesPath[i]);
				image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards

				/*if (i == 0 || i == 3) // instead of drawing the texture in different way in drawSquareSurface, we flip here the texture
				{
					image.RotateFlip(RotateFlipType.RotateNoneFlipX);
				}*/

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

			m_frontTexture= textureList[0];
			m_backTexture = textureList[1];
			m_topTexture = textureList[2];
			m_bottomTexture = textureList[3];
			m_rightTexture = textureList[4];
			m_leftTexture = textureList[5];
		}
		
		public void Draw()
		{
			GL.glPushMatrix();
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glColor3f(1, 1, 1);

			checkThrottleStickState();
			checkDirectionStickState();
			GL.glTranslatef(m_xTranslate, m_yTranslate, m_zTranslate);
			GL.glRotatef(m_yRotate, 0, 1, 0);
			
			// front
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_frontTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, m_textureSize);
			GL.glEnd();

			// back
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_backTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glEnd();

			// top
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_topTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, -m_textureSize);
			GL.glEnd();

			// bottom
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_bottomTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, m_textureSize);
			GL.glEnd();

			// right
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_rightTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glEnd();

			// left
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_leftTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, -m_textureSize);
			GL.glEnd();

			GL.glDisable(GL.GL_TEXTURE_2D);
			GL.glPopMatrix();
		}


		private void checkThrottleStickState()
		{
			if (ThrottleStickState.HasFlag(eThrottleStick.Ascend))
			{
				m_yTranslate -= m_acsendingDecsendingSpeed;
			}
			if (ThrottleStickState.HasFlag(eThrottleStick.Descend))
			{
				m_yTranslate += m_acsendingDecsendingSpeed;
			}
			if (ThrottleStickState.HasFlag(eThrottleStick.Right))
			{
				m_yRotate += m_rotatingSpeed;
			}
			if (ThrottleStickState.HasFlag(eThrottleStick.Left))
			{
				m_yRotate -= m_rotatingSpeed;
			}
		}
		
		private void checkDirectionStickState()
		{
			if (DirectionStickState.HasFlag(eDirectionStick.Forward))
			{
				m_zTranslate += m_forwardBackwardSpeed;
			}
			if (DirectionStickState.HasFlag(eDirectionStick.Backward))
			{
				m_zTranslate -= m_forwardBackwardSpeed;
			}
			if (DirectionStickState.HasFlag(eDirectionStick.Right))
			{
				m_xTranslate -= m_sideFlyingSpeed;
			}
			if (DirectionStickState.HasFlag(eDirectionStick.Left))
			{
				m_xTranslate += m_sideFlyingSpeed;
			}
		}
	}
}
