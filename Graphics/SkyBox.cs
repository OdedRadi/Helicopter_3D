using Logics;
using OpenGL;

namespace Graphics
{
	public class SkyBox : IGraphicComponent
	{
		private const float m_textureSize = 256;
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
		
		public void Init()
		{
			initTexture();
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
			//GL.glTranslatef(0, m_textureSize-10, 0);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glColor3f(1, 1, 1);

			// front
			GL.glNormal3f(0, 0, -1);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_frontTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, m_textureSize);
			GL.glEnd();

			// back
			GL.glNormal3f(0, 0, 1);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_backTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glEnd();

			// top
			GL.glNormal3f(0, -1, 0);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_topTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, m_textureSize, -m_textureSize);
			GL.glEnd();

			// bottom
			GL.glNormal3f(0, 1, 0);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_bottomTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-m_textureSize, -m_textureSize, m_textureSize);
			GL.glEnd();

			// right
			GL.glNormal3f(-1, 0, 0);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_rightTexture);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, m_textureSize);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_textureSize, -m_textureSize, -m_textureSize);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, -m_textureSize);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(m_textureSize, m_textureSize, m_textureSize);
			GL.glEnd();

			// left
			GL.glNormal3f(1, 0, 0);
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
	}
}
