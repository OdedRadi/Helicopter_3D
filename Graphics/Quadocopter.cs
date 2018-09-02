using System;
using OpenGL;

namespace Graphics
{
	public class Quadocopter : IGraphicComponent
	{
		private GLUquadric m_quadric;

		private float m_forwardPitchingAngle = 0;
		private float m_backwardPitchingAngle = 0;
		private float m_rightRollingAngle = 0;
		private float m_leftRollingAngle = 0;

		private const float m_maxRollingAngle = 45.0f;
		
		public eThrottleStick ThrottleStickState { get; set; }
		public eDirectionStick DirectionStickState { get; set; }

		~Quadocopter()
		{
			GLU.gluDeleteQuadric(m_quadric);
		}
		
		public void Init()
		{
			m_quadric = GLU.gluNewQuadric();
		}

		public void Draw()
		{
			GL.glPushMatrix();
			GL.glColor3d(0.75164, 0.60648, 0.22648);
			GL.glTranslated(0, 0, -4);
			GL.glRotated(10, 1, 0, 0);

			rotateAccordingToDirectionStick();

			drawQuadocopter();

			GL.glPopMatrix();
		}

		private void drawQuadocopter()
		{
			GL.glTranslated(0, 0, -1);

			GL.glPushMatrix();
			GL.glRotated(45, 0, 1, 0);
			GL.glTranslated(-1, 0, 0);
			GLU.gluCylinder(m_quadric, 0.2, 0.2, 2, 16, 16);
			GL.glPopMatrix();

			GL.glPushMatrix();
			GL.glRotated(-45, 0, 1, 0);
			GL.glTranslated(1, 0, 0);
			GLU.gluCylinder(m_quadric, 0.2, 0.2, 2, 16, 16);
			GL.glPopMatrix();
		}

		private void rotateAccordingToDirectionStick()
		{
			checkOneDirection(eDirectionStick.Forward, ref m_forwardPitchingAngle);
			checkOneDirection(eDirectionStick.Backward, ref m_backwardPitchingAngle);
			checkOneDirection(eDirectionStick.Right, ref m_rightRollingAngle);
			checkOneDirection(eDirectionStick.Left, ref m_leftRollingAngle);

			GL.glRotatef(-m_rightRollingAngle, 0, 0, 1);
			GL.glRotatef(-m_forwardPitchingAngle, 1, 0, 0);
			GL.glRotatef(m_leftRollingAngle, 0, 0, 1);
			GL.glRotatef(m_backwardPitchingAngle, 1, 0, 0);
		}

		private void checkOneDirection(eDirectionStick state, ref float value)
		{
			if (DirectionStickState.HasFlag(state))
			{
				if (value < m_maxRollingAngle)
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
	}
}
