using OpenGL;

namespace Graphics
{
	class Light
	{
		private float[] m_position = { 5, 5, 5, 1 };

		public void Init()
		{
			GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, m_position);
		}

		public void SetEnable(bool enable)
		{

			drawLightSource();
			if (enable == true)
			{
				GL.glEnable(GL.GL_LIGHTING);
				GL.glEnable(GL.GL_LIGHT0);
				GL.glEnable(GL.GL_COLOR_MATERIAL);
			}
			else
			{
				GL.glDisable(GL.GL_LIGHTING);
				GL.glDisable(GL.GL_LIGHT0);
				GL.glDisable(GL.GL_COLOR_MATERIAL);
			}
		}

		private void drawLightSource()
		{
			GL.glPushMatrix();

			GL.glTranslatef(0,0,-10);
			

			GL.glColor4d(1, 0, 0, 0.5);
			GL.glTranslatef(m_position[0], m_position[1], m_position[2]);
			GLUT.glutSolidSphere(1, 16, 16);

			GL.glPopMatrix();
		}
	}
}
