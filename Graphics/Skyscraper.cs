using System;
using OpenGL;

namespace Graphics
{
	class Skyscraper : IGraphicComponent
	{
		private uint m_textureID = 0;

		private const float m_height = 1000;
		private const float m_width = 300;
		private const float m_windowHeight = 2;
		private const float m_windowWidth = 1;
		private const float m_floors = 15;
		private const float m_windowsInFloor = 10;
		private const float m_windowsDistance = 0.05f;
		private const float m_allWindowsWidth = m_windowsInFloor * m_windowWidth + m_windowsDistance * m_windowsInFloor;
		private const float m_allWindowsHeight = m_floors * m_windowHeight + m_windowsDistance * m_floors;

		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			string[] bmpFilesNames = new string[1];

			bmpFilesNames[0] = "../../Resources/building_texture.bmp";

			uint[] texture = Logics.TextureLoader.LoadTextures(bmpFilesNames);

			m_textureID = texture[0];
		}

		public void BeginDrawReflection()
		{
			GL.glEnable(GL.GL_STENCIL_TEST);
			GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
			GL.glDisable(GL.GL_DEPTH_TEST);
			
			drawWindows(); // drawing windows on the front wall to the stencil buffer

			// restore regular settings
			GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
			GL.glEnable(GL.GL_DEPTH_TEST);

			GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);
		}

		public void StopDrawReflection()
		{
			GL.glDisable(GL.GL_STENCIL_TEST);
		}

		public void Draw()
		{
			GL.glPushMatrix();
			GL.glEnable(GL.GL_BLEND);
			GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

			// front wall has windows
			drawWindowsWall();

			// left wall
			GL.glTranslatef(-m_windowsDistance, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			GL.glTranslatef(m_windowsDistance, 0, 0);
			drawWall();

			// right wall
			GL.glTranslatef(-m_windowsDistance, 0, 0);
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_allWindowsWidth, 0, 0);
			GL.glTranslatef(m_windowsDistance, 0, -m_windowsDistance);
			GL.glRotatef(90, 0, 1, 0);
			drawWall(); 
			
			// back wall
			GL.glTranslatef(m_allWindowsWidth, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			GL.glTranslatef(m_windowsDistance, 0, 0);
			drawWall();

			GL.glPopMatrix();
		}

		private void drawWall()
		{
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureID);

			GL.glColor3f(1, 1, 1);
			GL.glBegin(GL.GL_QUADS);

			GL.glTexCoord2f(0.0f, 0.0f);   GL.glVertex3f(-m_windowsDistance, -m_windowsDistance, 0);
			GL.glTexCoord2f(10.0f, 0.0f);  GL.glVertex3f(m_allWindowsWidth, -m_windowsDistance, 0);
			GL.glTexCoord2f(10.0f, 10.0f); GL.glVertex3f(m_allWindowsWidth, m_allWindowsHeight, 0);
			GL.glTexCoord2f(0.0f, 10.0f);  GL.glVertex3f(-m_windowsDistance, m_allWindowsHeight, 0);

			GL.glEnd();
			GL.glDisable(GL.GL_TEXTURE_2D);
		}

		private void drawWindowsWall()
		{
			drawVerticalSeperators();
			drawHorizontalSeperators();
			drawWindows();
		}

		private void drawVerticalSeperators()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glColor3f(0.2f, 0.2f, 0.2f);

			for (int i = 0; i <= m_floors; i++)
			{
				float currY = (i * m_windowsDistance) + i * m_windowHeight;

				GL.glVertex3f(-m_windowsDistance, currY - m_windowsDistance, 0);
				GL.glVertex3f(m_allWindowsWidth, currY - m_windowsDistance, 0);
				GL.glVertex3f(m_allWindowsWidth, currY, 0);
				GL.glVertex3f(-m_windowsDistance, currY, 0);
			}

			GL.glEnd();
		}

		private void drawHorizontalSeperators()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glColor3f(0.2f, 0.2f, 0.2f);

			for (int i = 0; i <= m_windowsInFloor; i++)
			{
				float currX = (i * m_windowsDistance) + i * m_windowWidth;

				GL.glVertex3f(currX - m_windowsDistance, -m_windowsDistance, 0);
				GL.glVertex3f(currX, -m_windowsDistance, 0);
				GL.glVertex3f(currX, m_allWindowsHeight, 0);
				GL.glVertex3f(currX - m_windowsDistance, m_allWindowsHeight, 0);
			}

			GL.glEnd();
		}

		private void drawWindows()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glColor4f(0, 0.4f, 0.66f, 0.5f);

			for (int i = 0; i < m_floors; i++)
			{
				for (int j = 0; j < m_windowsInFloor; j++)
				{
					float currWindowHeightStart = (i * m_windowsDistance) + i * m_windowHeight;
					float currWindowHeightStop = (i * m_windowsDistance) + (i + 1) * m_windowHeight;
					float currWindowWidthStart = (j * m_windowsDistance) + j * m_windowWidth;
					float currWindowWidthStop = (j * m_windowsDistance) + (j + 1) * m_windowWidth;

					GL.glVertex3f(currWindowWidthStart, currWindowHeightStart, 0);
					GL.glVertex3f(currWindowWidthStop, currWindowHeightStart, 0);
					GL.glVertex3f(currWindowWidthStop, currWindowHeightStop, 0);
					GL.glVertex3f(currWindowWidthStart, currWindowHeightStop, 0);
				}
			}

			GL.glEnd();
		}

		int m_firstwall = 0;

		internal void SetFirstWallIndex(int v)
		{
			m_firstwall = v;
		}
		
	}
}
