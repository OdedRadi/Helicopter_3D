using System;
using System.Drawing;
using System.Drawing.Imaging;
using Milkshape;
using OpenGL;

namespace Graphics
{
	public class Quadocopter : IGraphicComponent
	{
		private uint m_heliTexture;
		private Character m_heliBody = new Character("../../Resources/HeliBody.ms3d");
		private Character m_heliSmallRotor = new Character("../../Resources/HeliSmallRotor.ms3d");
		private Character m_heliMainRotor = new Character("../../Resources/HeliMainRotor.ms3d");
	
		// Small rotor params
		private const float m_smallRotorYRotate = 5f;
		private const float m_smallRotorZRotate = -35f;
		private const float m_smallRotorRotateSpeed = 10;
		private float m_smallRotorAngle = 0;

		// Main rotor params
		private const float m_mainRotorXRotate = -2;
		private const float m_mainRotorRotateSpeed = 10;
		private const float m_mainRotorZRotate = 9.2f;
		private float m_mainRotateAngle = 0;

		private float m_forwardPitchingAngle = 0;
		private float m_backwardPitchingAngle = 0;
		private float m_rightRollingAngle = 0;
		private float m_leftRollingAngle = 0;

		private const float m_maxPitchingAngle = 25.0f;
		private const float m_maxRollingAngle = 45.0f;

		public eThrottleStick ThrottleStickState { get; set; }
		public eDirectionStick DirectionStickState { get; set; }

		~Quadocopter()
		{
		}

		#region Init
		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			uint[] texture = new uint[1];
			string bmpFilePath = "../../Resources/leviathnbody8bit256.bmp";

			GL.glGenTextures(texture.Length, texture);

			Bitmap image = new Bitmap(bmpFilePath);
			image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards

			Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
			BitmapData bitmapData = image.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			GL.glBindTexture(GL.GL_TEXTURE_2D, texture[0]);
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

			m_heliTexture = texture[0];
		}
		#endregion

		#region Draw
		public void Draw()
		{
			GL.glPushMatrix();
			GL.glColor3d(1, 1, 1);
			GL.glTranslated(0, 0, -50);
			GL.glScaled(0.8, 0.8, -0.8);
			GL.glRotated(-2, 0, 1, 0);

			rotateAccordingToDirectionStick();

			m_heliBody.DrawModel();
			drawAndMoveSmallRotor();
			drawAndMoveMainRotor();

			GL.glPopMatrix();
		}
		
		private void drawAndMoveSmallRotor()
		{
			GL.glPushMatrix();
			GL.glTranslated(0, m_smallRotorYRotate, m_smallRotorZRotate);
			GL.glRotatef(m_smallRotorAngle, 1, 0, 0);
			m_smallRotorAngle += m_smallRotorRotateSpeed;
			GL.glTranslated(0, -m_smallRotorYRotate, -m_smallRotorZRotate);
			m_heliSmallRotor.DrawModel();
			GL.glPopMatrix();
		}

		private void drawAndMoveMainRotor()
		{
			GL.glPushMatrix();
			GL.glTranslated(m_mainRotorXRotate, 0, m_mainRotorZRotate);
			GL.glRotatef(m_mainRotateAngle, 0, -1, 0);
			m_mainRotateAngle += m_mainRotorRotateSpeed;
			GL.glTranslated(-m_mainRotorXRotate, 0, -m_mainRotorZRotate);
			m_heliMainRotor.DrawModel();
			GL.glPopMatrix();
		}

		private void rotateAccordingToDirectionStick()
		{
			checkOneDirection(eDirectionStick.Forward, ref m_forwardPitchingAngle, m_maxPitchingAngle);
			checkOneDirection(eDirectionStick.Backward, ref m_backwardPitchingAngle, m_maxPitchingAngle);
			checkOneDirection(eDirectionStick.Right, ref m_rightRollingAngle, m_maxRollingAngle);
			checkOneDirection(eDirectionStick.Left, ref m_leftRollingAngle, m_maxRollingAngle);

			GL.glRotatef(-m_rightRollingAngle, 0, 0, 1);
			GL.glRotatef(m_forwardPitchingAngle, 1, 0, 0);
			GL.glRotatef(m_leftRollingAngle, 0, 0, 1);
			GL.glRotatef(-m_backwardPitchingAngle, 1, 0, 0);
		}

		private void checkOneDirection(eDirectionStick state, ref float value, float maxValue)
		{
			if (DirectionStickState.HasFlag(state))
			{
				if (value < maxValue)
				{
					value += 2;
				}
			}
			else
			{
				if (value > 0)
				{
					value -= 2;
				}
			}
		}
		#endregion
	}
}
