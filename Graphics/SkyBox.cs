using Logics;
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
		
		public Action<eDirectionStick> DirectionLimitsEvent;

		public void Init()
		{
			initTexture();
			m_yTranslate = m_textureSize - 300;
			m_zTranslate = -m_textureSize + 300;
		}

		private void initTexture()
		{
			string[] bmpFilesPath = new string[6];

			bmpFilesPath[0] = "../../Resources/environment_front.bmp";
			bmpFilesPath[1] = "../../Resources/environment_back.bmp";
			bmpFilesPath[2] = "../../Resources/environment_top.bmp";
			bmpFilesPath[3] = "../../Resources/environment_bottom.bmp";
			bmpFilesPath[4] = "../../Resources/environment_right.bmp";
			bmpFilesPath[5] = "../../Resources/environment_left.bmp";

			uint[] textureList = TextureLoader.LoadTextures(bmpFilesPath);

			m_frontTexture = textureList[0];
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
