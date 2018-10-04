using OpenGL;

namespace Graphics
{
	class Light
	{
		private uint m_glLightId;

		public void Init(uint lightID, float x, float y, float z)
		{
			m_glLightId = 0x4000 + lightID;
			X = x;
			Y = y;
			Z = z;
		}

		public float X { get; private set; }
		public float Y { get; private set; }
		public float Z { get; private set; }

		public void SetEnable(bool enable)
		{
			if (enable == true)
			{
				float[] position = { X, Y, Z, 1 };

				GL.glLightfv(m_glLightId, GL.GL_POSITION, position);

				GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, new float[] { 0.3f, 0.3f, 0.3f, 1f });

				GL.glEnable(GL.GL_LIGHTING);
				GL.glEnable(m_glLightId);
				GL.glEnable(GL.GL_COLOR_MATERIAL);
			}
			else
			{
				GL.glDisable(GL.GL_LIGHTING);
				GL.glDisable(m_glLightId);
				GL.glDisable(GL.GL_COLOR_MATERIAL);
			}
		}

		public void DrawLightSource()
		{
			GL.glPushMatrix();

			GL.glDisable(GL.GL_LIGHTING);
			GL.glTranslatef(this.X, this.Y, this.Z);
			GLUT.glutSolidSphere(1, 16, 16);

			GL.glPopMatrix();
		}
	}
}
