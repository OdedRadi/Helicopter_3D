using Logics;
using OpenGL;

namespace Graphics
{
	class Skyscraper : IShadowingGraphicComponent
	{
		private enum eStencils
		{
			Reflection,
			ReflectedFloor,
			Floor,
			Roof,
			UpperFrontWall,
			UpperBackWall,
			UpperLeftWall,
			UpperRightWall,
			BottomFrontWall,
			BottomBackWall,
			BottomLeftWall,
			BottomRightWall
		}

		private enum eShadowMatrices
		{
			Floor,
			Roof,
			UpperFrontWall,
			UpperBackWall,
			UpperLeftWall,
			UpperRightWall,
			BottomFrontWall,
			BottomBackWall,
			BottomLeftWall,
			BottomRightWall
		}

		private uint m_wallsTexture = 0;
		private uint m_floorTexture = 0;
		private uint m_roofTexture = 0;

		private const float m_windowHeight = 1;
		private const float m_windowWidth = 0.5f;
		private const float m_floors = 40;
		private const float m_windowsInFloor = 30;
		private const float m_windowsDistance = 0.025f;
		private const float m_width = m_windowsInFloor * m_windowWidth + m_windowsDistance * m_windowsInFloor;
		private const float m_height = m_floors * m_windowHeight + m_windowsDistance * m_floors;

		private const float m_floorWidth = 40;

		#region init methods
		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			string[] bmpFilesNames = new string[3];

			bmpFilesNames[0] = "../../Resources/building_texture.bmp";
			bmpFilesNames[1] = "../../Resources/stone_floor.bmp";
			bmpFilesNames[2] = "../../Resources/roof_texture.bmp";

			uint[] texture = TextureLoader.LoadTextures(bmpFilesNames);

			m_wallsTexture = texture[0];
			m_floorTexture = texture[1];
			m_roofTexture = texture[2];
		}
		#endregion

		#region skyscarper drawing
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
			GL.glPushMatrix();

			GL.glTranslatef(0, m_height, 0);

			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_roofTexture);

			GL.glBegin(GL.GL_QUADS);
			GL.glNormal3f(0, 1, 0);

			GL.glTexCoord2f(0, 0); GL.glVertex3f(0, 0, 0);
			GL.glTexCoord2f(10, 0); GL.glVertex3f(0, 0, -m_width);
			GL.glTexCoord2f(10, 10); GL.glVertex3f(m_width, 0, -m_width);
			GL.glTexCoord2f(0, 10); GL.glVertex3f(m_width, 0, 0);

			GL.glEnd();
			GL.glDisable(GL.GL_TEXTURE_2D);
			GL.glPopMatrix();
		}

		private void drawFloor()
		{
			GL.glPushMatrix();

			GL.glTranslatef(m_width / 2, 0, -m_width / 2);
			GL.glTranslatef(-m_floorWidth / 2, 0, m_floorWidth / 2);

			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
			GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
			GL.glEnable(GL.GL_TEXTURE_2D);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_floorTexture);

			GL.glBegin(GL.GL_QUADS);
			GL.glNormal3f(0, 1, 0);

			GL.glTexCoord2f(0, 0);   GL.glVertex3f(0, 0, 0);
			GL.glTexCoord2f(10, 0);  GL.glVertex3f(m_floorWidth, 0, 0);
			GL.glTexCoord2f(10, 10); GL.glVertex3f(m_floorWidth, 0, -m_floorWidth);
			GL.glTexCoord2f(0, 10);  GL.glVertex3f(0, 0, -m_floorWidth);

			GL.glEnd();
			GL.glDisable(GL.GL_TEXTURE_2D);

			GL.glPopMatrix();
		}

		private void drawBottomPart()
		{
			GL.glPushMatrix();

			GL.glTranslatef(m_width / 2.0f, 0, -m_width / 2.0f);
			GL.glTranslatef(-m_floorWidth / 2, 0, m_floorWidth / 2);

			// front wall
			drawWall(m_floorWidth, -100);

			// left wall
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth, -100);

			// right wall
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_floorWidth, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth, -100);

			// back wall
			GL.glTranslatef(m_floorWidth, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawWall(m_floorWidth, -100);

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

			GL.glTexCoord2f(0, 0); GL.glVertex3f(0, 0, 0);
			GL.glTexCoord2f(10, 0); GL.glVertex3f(width, 0, 0);
			GL.glTexCoord2f(10, 10); GL.glVertex3f(width, height, 0);
			GL.glTexCoord2f(0, 10); GL.glVertex3f(0, height, 0);

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
			drawSolidWall(m_width, m_height);

			// left wall
			GL.glRotatef(90, 0, 1, 0);
			GL.glTranslatef(0, 0.1f, 0);
			drawSolidWall(m_width, m_height);

			// right wall
			GL.glRotatef(-90, 0, 1, 0);
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawSolidWall(m_width, m_height);

			// back wall
			GL.glTranslatef(m_width, 0, 0);
			GL.glRotatef(90, 0, 1, 0);
			drawSolidWall(m_width, m_height);

			GL.glPopMatrix();
		}

		private void drawSolidWall(float width, float height)
		{
			GL.glBegin(GL.GL_QUADS);
			GL.glVertex3f(0, 0, 0);
			GL.glVertex3f(width, 0, 0);
			GL.glVertex3f(width, height, 0);
			GL.glVertex3f(0, height, 0);
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
		#endregion

		#region reflection drawing
		public void DrawReflection(IGraphicComponent component, float[] drawingTranslation, float[] drawingRotation, float[] lightPosition)
		{
			GL.glPushMatrix();

			if (component is IShadowingGraphicComponent)
			{
				drawReflectedShadow(component as IShadowingGraphicComponent, drawingTranslation, drawingRotation, lightPosition);
			}

			beginStencil(eStencils.Reflection);

			GL.glScalef(1, 1, -1);

			// drawing skyscraper floor reflection anyway
			drawFloor();

			GL.glTranslatef(drawingTranslation[0], drawingTranslation[1], drawingTranslation[2]);
			GL.glRotatef(drawingRotation[0], 1, 0, 0);
			GL.glRotatef(drawingRotation[1], 0, 1, 0);
			GL.glRotatef(drawingRotation[2], 0, 0, 1);

			GL.glColor3f(1, 1, 1);
			component.Draw();

			stopStencil();

			GL.glPopMatrix();
		}

		private void drawReflectedShadow(IShadowingGraphicComponent component, float[] drawingTranslation, float[] drawingRotation, float[] lightPosition)
		{
			GL.glDisable(GL.GL_LIGHTING);
			GL.glPushMatrix();

			GL.glScalef(1, 1, -1);
			beginStencil(eStencils.ReflectedFloor);

			float[] matrix = getShadowMatrix(eShadowMatrices.Floor, lightPosition);

			GL.glTranslatef(0, 0.01f, 0);
			GL.glMultMatrixf(matrix);

			GL.glTranslatef(drawingTranslation[0], drawingTranslation[1], drawingTranslation[2]);
			GL.glRotatef(drawingRotation[0], 1, 0, 0);
			GL.glRotatef(drawingRotation[1], 0, 1, 0);
			GL.glRotatef(drawingRotation[2], 0, 0, 1);

			GL.glColor3f(0.15f, 0.15f, 0.15f);
			component.DrawShadow();

			stopStencil();
			GL.glPopMatrix();
			GL.glEnable(GL.GL_LIGHTING);
		}
		#endregion

		#region components shadows drawing
		public void DrawComponentShadows(float[] lightPosition, IShadowingGraphicComponent component, float[] drawingTranslation, float[] drawingRotation)
		{
			GL.glDisable(GL.GL_LIGHTING);

			GL.glColor3f(0.15f, 0.15f, 0.15f);

			// floor shadow
			beginStencil(eStencils.Floor);
			float[] matrix = getShadowMatrix(eShadowMatrices.Floor, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0.01f, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			multiplyMatrixAndDrawComponentShadow(matrix, this, new float[] { 0, 0, 0 }, new float[] { 0, 0, 0 }); // drawing skyscraper shadow on the floor
			GL.glPopMatrix();

			// roof shadow
			beginStencil(eStencils.Roof);
			matrix = getShadowMatrix(eShadowMatrices.Roof, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0.01f, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			GL.glColor3f(0.20f, 0.20f, 0.20f);

			// upper back wall shadow
			beginStencil(eStencils.UpperBackWall);
			matrix = getShadowMatrix(eShadowMatrices.UpperBackWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0, -0.01f);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			// upper left wall shadow
			beginStencil(eStencils.UpperLeftWall);
			matrix = getShadowMatrix(eShadowMatrices.UpperLeftWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(-0.01f, 0, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			// upper right wall shadow
			beginStencil(eStencils.UpperRightWall);
			matrix = getShadowMatrix(eShadowMatrices.UpperRightWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0.01f, 0, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			// bottom front wall shadow
			beginStencil(eStencils.BottomFrontWall);
			matrix = getShadowMatrix(eShadowMatrices.BottomFrontWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0, 0.01f);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();
			
			// bottom back wall shadow
			beginStencil(eStencils.BottomBackWall);
			matrix = getShadowMatrix(eShadowMatrices.BottomBackWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0, -0.01f);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			// bottom left wall shadow
			beginStencil(eStencils.BottomLeftWall);
			matrix = getShadowMatrix(eShadowMatrices.BottomLeftWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(-0.01f, 0, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();

			// bottom right wall shadow
			beginStencil(eStencils.BottomRightWall);
			matrix = getShadowMatrix(eShadowMatrices.BottomRightWall, lightPosition);

			GL.glPushMatrix();
			GL.glTranslatef(0.01f, 0, 0);
			multiplyMatrixAndDrawComponentShadow(matrix, component, drawingTranslation, drawingRotation);
			GL.glPopMatrix();
			
			// disable stencil
			stopStencil();

			GL.glEnable(GL.GL_LIGHTING);
		}

		private float[] getShadowMatrix(eShadowMatrices requiredMatrix, float[] lightPosition)
		{
			float[] normal = null;
			float[] pointOnThePlane = null;

			switch (requiredMatrix)
			{
				case eShadowMatrices.Floor:
					pointOnThePlane = new float[] { 0, 0, 0 };
					normal = new float[] { 0, 1, 0 };
					break;
				case eShadowMatrices.Roof:
					pointOnThePlane = new float[] { 0, m_height, 0 };
					normal = new float[] { 0, 1, 0 };
					break;
				case eShadowMatrices.UpperFrontWall:
					pointOnThePlane = new float[] { 0, 0, 0 };
					normal = new float[] { 0, 0, 1 };
					break;
				case eShadowMatrices.UpperBackWall:
					pointOnThePlane = new float[] { 0, 0, -m_width };
					normal = new float[] { 0, 0, -1 };
					break;
				case eShadowMatrices.UpperLeftWall:
					pointOnThePlane = new float[] { 0, 0, 0 };
					normal = new float[] { -1, 0, 0 };
					break;
				case eShadowMatrices.UpperRightWall:
					pointOnThePlane = new float[] { m_width, 0, 0 };
					normal = new float[] { 1, 0, 0 };
					break;
				case eShadowMatrices.BottomFrontWall:
					pointOnThePlane = new float[] { 0, 0, -(m_width / 2) + (m_floorWidth / 2) };
					normal = new float[] { 0, 0, 1 };
					break;
				case eShadowMatrices.BottomBackWall:
					pointOnThePlane = new float[] { 0, 0, -(m_width / 2) - (m_floorWidth / 2) };
					normal = new float[] { 0, 0, -1 };
					break;
				case eShadowMatrices.BottomLeftWall:
					pointOnThePlane = new float[] { m_width / 2 - (m_floorWidth / 2), 0, 0 };
					normal = new float[] { -1, 0, 0 };
					break;
				case eShadowMatrices.BottomRightWall:
					pointOnThePlane = new float[] { m_width / 2 + (m_floorWidth / 2), 0, 0 };
					normal = new float[] { 1, 0, 0 };
					break;
			}

			return ShadowMatrixGenerator.GenerateShadowMatrix(normal, pointOnThePlane, lightPosition);
		}

		private void multiplyMatrixAndDrawComponentShadow(float[] matrix, IShadowingGraphicComponent component, float[] drawingTranslation, float[] drawingRotation)
		{
			GL.glPushMatrix();

			GL.glMultMatrixf(matrix);
			GL.glTranslatef(drawingTranslation[0], drawingTranslation[1], drawingTranslation[2]);
			GL.glRotatef(drawingRotation[0], 1, 0, 0);
			GL.glRotatef(drawingRotation[1], 0, 1, 0);
			GL.glRotatef(drawingRotation[2], 0, 0, 1);
			component.DrawShadow();

			GL.glPopMatrix();
		}
		#endregion

		#region stencils methods
		private void beginStencil(eStencils stencil)
		{
			GL.glPushMatrix();
			beginStencilBufferDrawing();

			switch (stencil)
			{
				case eStencils.Reflection:
					drawWindows();
					break;
				case eStencils.ReflectedFloor:
					drawReflectedFloorStencil();
					break;
				case eStencils.Floor:
					drawFloor();
					break;
				case eStencils.Roof:
					drawRoof();
					break;
				case eStencils.UpperFrontWall:
					drawSolidWall(m_width, m_height);
					break;
				case eStencils.UpperBackWall:
					GL.glTranslatef(0, 0, -m_width);
					drawSolidWall(m_width, m_height);
					break;
				case eStencils.UpperLeftWall:
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall(m_width, m_height);
					break;
				case eStencils.UpperRightWall:
					GL.glTranslatef(m_width, 0, 0);
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall(m_width, m_height);
					break;
				case eStencils.BottomFrontWall:
					GL.glTranslatef(m_width / 2 - m_floorWidth / 2, 0, -(m_width / 2) + m_floorWidth / 2);
					drawSolidWall(m_floorWidth, -100);
					break;
				case eStencils.BottomBackWall:
					GL.glTranslatef(m_width / 2 - m_floorWidth / 2, 0, -(m_width / 2) - m_floorWidth / 2);
					drawSolidWall(m_floorWidth, -100);
					break;
				case eStencils.BottomLeftWall:
					GL.glTranslatef(m_width / 2 - m_floorWidth / 2, 0, -(m_width / 2) + m_floorWidth / 2);
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall(m_floorWidth, -100);
					break;
				case eStencils.BottomRightWall:
					GL.glTranslatef(m_width / 2 + m_floorWidth / 2, 0, -(m_width / 2) + m_floorWidth / 2);
					GL.glRotatef(90, 0, 1, 0);
					drawSolidWall(m_floorWidth, -100);
					break;
			}

			endStencilBufferDrawing();

			GL.glPopMatrix();
		}

		private void stopStencil()
		{
			GL.glDisable(GL.GL_STENCIL_TEST);
		}

		private void drawReflectedFloorStencil()
		{
			// first drawing the windows area with value of 2
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_REPLACE);
			GL.glStencilFunc(GL.GL_ALWAYS, 2, 0xFFFFFFFF);
			drawWindows();

			// then decrment the floor area by 1, so where is windows and floor togther will be 1
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_DECR);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			drawFloor();
		}

		private void beginStencilBufferDrawing()
		{
			GL.glClear(GL.GL_STENCIL_BUFFER_BIT);

			GL.glEnable(GL.GL_STENCIL_TEST);
			GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
			GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
			GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
			GL.glDisable(GL.GL_DEPTH_TEST);
		}

		private void endStencilBufferDrawing()
		{
			// restore regular settings
			GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
			GL.glEnable(GL.GL_DEPTH_TEST);

			GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
			GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);
		}
		#endregion
	}
}
