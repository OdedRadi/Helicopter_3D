using System;
using Logics;
using OpenGL;

namespace Graphics
{
	class Skyscraper : IGraphicComponent
	{
		public enum eStencils
		{
			Reflection,
			Floor,
			BackWall,
			LeftWall,
			RightWall
		}

		public enum eShadowMatrices
		{
			Floor,
			BackWall,
			LeftWall,
			RightWall
		}

		private uint m_wallsTexture = 0;
		private uint m_floorTexture = 0;

		private const float m_windowHeight = 1;
		private const float m_windowWidth = 0.5f;
		private const float m_floors = 40;
		private const float m_windowsInFloor = 30;
		private const float m_windowsDistance = 0.025f;
		private const float m_width = m_windowsInFloor * m_windowWidth + m_windowsDistance * m_windowsInFloor;
		private const float m_height = m_floors * m_windowHeight + m_windowsDistance * m_floors;

		private const float m_floorWidth = 30;

		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			string[] bmpFilesNames = new string[2];

			bmpFilesNames[0] = "../../Resources/building_texture.bmp";
			bmpFilesNames[1] = "../../Resources/stone_floor.bmp";

			uint[] texture = TextureLoader.LoadTextures(bmpFilesNames);

			m_wallsTexture = texture[0];
			m_floorTexture = texture[1];
		}
		
		public void BeginStencil(eStencils stencil)
		{
			GL.glPushMatrix();
			beginStencilDrawing();

			switch (stencil)
			{
				case eStencils.Reflection:
					drawWindows();
					break;
				case eStencils.Floor:
					drawFloor();
					break;
				case eStencils.BackWall:
					GL.glTranslatef(0, 0, -m_width);
					drawSolidWall();
					break;
				case eStencils.LeftWall:
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall();
					break;
				case eStencils.RightWall:
					GL.glTranslatef(m_width, 0, 0);
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall();
					break;
			}

			endStencilDrawing();

			if (stencil == eStencils.Reflection)
			{
				drawReflectedFloor();
			}

			GL.glPopMatrix();
		}
		
		public void StopStencil()
		{
			GL.glDisable(GL.GL_STENCIL_TEST);
		}

		private void drawReflectedFloor()
		{
			GL.glPushMatrix();
			GL.glScalef(1, 1, -1);
			drawFloor();
			GL.glPopMatrix();
		}

		public void BeginReflectedFloorStencil()
		{
			beginStencilDrawing();

			// first drawing the windows area
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_REPLACE);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			drawWindows();

			// then increment the floor area, so where is windows and floor togther will be 2
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_INCR);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			drawFloor();

			endStencilDrawing();

			// drawing only where both windows and floor area
			GL.glStencilFunc(GL.GL_EQUAL, 2, 0xFFFFFFFF);
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);
		}

		private void beginStencilDrawing()
		{
			GL.glClear(GL.GL_STENCIL_BUFFER_BIT);

			GL.glEnable(GL.GL_STENCIL_TEST);
			GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
			GL.glDisable(GL.GL_DEPTH_TEST);
		}

		private void endStencilDrawing()
		{
			// restore regular settings
			GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
			GL.glEnable(GL.GL_DEPTH_TEST);

			GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);
		}

		public float[] GetShadowMatrix(eShadowMatrices requiredMatrix, float[] lightPosition)
		{
			float[] normal = null;
			float[] pointOnThePlane = null;

			switch (requiredMatrix)
			{
				case eShadowMatrices.Floor:
					pointOnThePlane = new float[] { 0, Scene.GetInstance().yTranslate, 0 };
					normal = new float[] { 0, 1, 0 };
					break;
				case eShadowMatrices.BackWall:
					pointOnThePlane = new float[] { 0, 0, -m_width };
					normal = new float[] { 0, 0, -1 };
					break;
				case eShadowMatrices.LeftWall:
					pointOnThePlane = new float[] { 0, 0, 0 };
					normal = new float[] { -1, 0, 0 };
					break;
				case eShadowMatrices.RightWall:
					pointOnThePlane = new float[] {m_width, 0, 0 };
					normal = new float[] { 1, 0, 0 };
					break;
			}
			
			return ShadowMatrixGenerator.GenerateShadowMatrix(normal, pointOnThePlane, lightPosition);
		}

		public void Draw()
		{
			GL.glPushMatrix();
			GL.glEnable(GL.GL_BLEND);
			GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

			drawUpperPart();
			drawRoof();
			drawFloor();
			drawBottomPart();

			GL.glPopMatrix();
		}

		private void drawUpperPart()
		{
			GL.glPushMatrix();

			// front wall has windows
			GL.glNormal3f(0, 0, 1);
			drawWindowsWall();

			// left wall
			GL.glRotatef(90, 0, 1, 0);
			GL.glNormal3f(0, 0, -1);
			drawWall(m_width, m_height);

			// right wall
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			GL.glNormal3f(0, 0, 1);
			drawWall(m_width, m_height);

			// back wall
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			GL.glNormal3f(0, 0, 1);
			drawWall(m_width, m_height);

			GL.glPopMatrix();
		}
		
		private void drawRoof()
		{
		}

		private void drawFloor()
		{
			GL.glPushMatrix();

			GL.glTranslatef(m_width / 2.0f, 0, -m_width / 2.0f);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_floorTexture);

			GL.glColor3f(1, 1, 1);
			GL.glBegin(GL.GL_QUADS);
			GL.glNormal3f(0, 1, 0);

			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-m_floorWidth, 0, -m_floorWidth);
			GL.glTexCoord2f(m_floorWidth / 2, 0.0f); GL.glVertex3f(m_floorWidth, 0, -(m_floorWidth));
			GL.glTexCoord2f(m_floorWidth / 2, m_floorWidth / 2); GL.glVertex3f(m_floorWidth, 0, m_floorWidth);
			GL.glTexCoord2f(0.0f, m_floorWidth / 2); GL.glVertex3f(-m_floorWidth, 0, m_floorWidth);

			GL.glEnd();
			GL.glDisable(GL.GL_TEXTURE_2D);

			GL.glPopMatrix();
		}
		
		private void drawBottomPart()
		{
			GL.glPushMatrix();

			GL.glTranslatef(m_width / 2.0f, 0, -m_width / 2.0f);
			GL.glTranslatef(-m_floorWidth, 0, m_floorWidth);

			// front wall
			drawWall(m_floorWidth * 2, -100);

			// left wall
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth * 2, -100);

			// right wall
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_floorWidth * 2, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth * 2, -100);

			// back wall
			GL.glTranslatef(m_floorWidth * 2, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth * 2, -100);

			GL.glPopMatrix();
		}

		private void drawWall(float width, float height)
		{
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_wallsTexture);

			GL.glColor3f(1, 1, 1);
			GL.glBegin(GL.GL_QUADS);

			GL.glTexCoord2f(0,  0);  GL.glVertex3f(0, 0, 0);
			GL.glTexCoord2f(10, 0);  GL.glVertex3f(width, 0, 0);
			GL.glTexCoord2f(10, 10); GL.glVertex3f(width, height, 0);
			GL.glTexCoord2f(0,  10); GL.glVertex3f(0, height, 0);

			GL.glEnd();
			GL.glDisable(GL.GL_TEXTURE_2D);
		}

		private void drawWindowsWall()
		{
			drawHorizontalSeperators();
			drawVerticalSeperators();
			drawWindows();
		}

		public void DrawShadow()
		{
			GL.glPushMatrix();

			// front wall
			drawSolidWall();

			// left wall
			GL.glRotatef(90, 0, 1, 0);
			GL.glTranslatef(0, 0.1f, 0);
			drawSolidWall();

			// right wall
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawSolidWall();

			// back wall
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawSolidWall();

			GL.glPopMatrix();
		}

		private void drawSolidWall()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glVertex3f(0, 0, 0);
			GL.glVertex3f(m_width, 0, 0);
			GL.glVertex3f(m_width, m_height, 0);
			GL.glVertex3f(0, m_height, 0);
			GL.glEnd();
		}

		private void drawHorizontalSeperators()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glColor3f(0.2f, 0.2f, 0.2f);

			for (int i = 0; i <= m_floors; i++)
			{
				float currY = (i * m_windowsDistance) + i * m_windowHeight;

				GL.glVertex3f(-m_windowsDistance, currY - m_windowsDistance, 0);
				GL.glVertex3f(m_width, currY - m_windowsDistance, 0);
				GL.glVertex3f(m_width, currY, 0);
				GL.glVertex3f(-m_windowsDistance, currY, 0);
			}

			GL.glEnd();
		}

		private void drawVerticalSeperators()
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glColor3f(0.2f, 0.2f, 0.2f);

			for (int i = 0; i <= m_windowsInFloor; i++)
			{
				float currX = (i * m_windowsDistance) + i * m_windowWidth;

				GL.glVertex3f(currX - m_windowsDistance, -m_windowsDistance, 0);
				GL.glVertex3f(currX, -m_windowsDistance, 0);
				GL.glVertex3f(currX, m_height, 0);
				GL.glVertex3f(currX - m_windowsDistance, m_height, 0);
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
	}
}
