using OpenGL;

namespace Graphics
{
	class Light
	{
		private uint m_glLightId;

		public Light(uint lightID)
		{
			m_glLightId = 0x4000 + lightID;
		}

		public void SetPosition(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;

			GL.glLightfv(m_glLightId, GL.GL_AMBIENT, new float[] { 0.3f, 0.3f, 0.3f, 1f });
		}

		public float X { get; private set; }
		public float Y { get; private set; }
		public float Z { get; private set; }
		public float[] Position { get { return new float[] { X, Y, Z, 1 }; } }

		public void SetEnable(bool enable)
		{
			if (enable == true)
			{
				float[] position = { X, Y, Z, 1 };

				GL.glLightfv(m_glLightId, GL.GL_POSITION, position);

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
			GL.glColor3f(1, 1, 1);
			GL.glTranslatef(this.X, this.Y, this.Z);
			GLUT.glutSolidSphere(1, 64, 64);
			GL.glEnable(GL.GL_LIGHTING);

			GL.glPopMatrix();
		}
	}
}
