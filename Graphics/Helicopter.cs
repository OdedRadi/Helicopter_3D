using Logics;
using Milkshape;
using OpenGL;

namespace Graphics
{
	public class Helicopter : IGraphicComponent
	{
		private uint m_heliTexture;
		private Character m_heliBody = new Character("../../Resources/HeliBody.ms3d");
		private Character m_heliSmallRotor = new Character("../../Resources/HeliSmallRotor.ms3d");
		private Character m_heliMainRotor = new Character("../../Resources/HeliMainRotor.ms3d");

		// Small rotor params
		private const float m_smallRotorYTranslate = 5.248f; // According to MilkShape data
		private const float m_smallRotorZTranslate = -34.847f; // According to MilkShape data
		private const float m_smallRotorRotateSpeed = 20;
		private float m_smallRotorAngle = 0;

		// Main rotor params
		private const float m_mainRotorXTranslate = -2.392f; // According to MilkShape data
		private const float m_mainRotorZTranslate = 9.346f; // According to MilkShape data
		private const float m_mainRotorRotateSpeed = 20;
		private float m_mainRotateAngle = 0;

		private float m_forwardPitchingAngle = 0;
		private float m_backwardPitchingAngle = 0;
		private float m_rightRollingAngle = 0;
		private float m_leftRollingAngle = 0;

		private const float m_maxPitchingAngle = 20.0f;
		private const float m_maxRollingAngle = 60.0f;
		private const float m_rollingSpeed = 2.0f;

		public eThrottleStick ThrottleStickState { get; set; }
		public eDirectionStick DirectionStickState { get; set; }

		~Helicopter()
		{
		}

		#region Init
		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			string[] bmpFilePath = new string[1];

			bmpFilePath[0] = "../../Resources/leviathnbody8bit256.bmp";

			uint[] texture = TextureLoader.LoadTextures(bmpFilePath);

			m_heliTexture = texture[0];
		}
		#endregion

		#region Draw
		public void Draw()
		{
			GL.glPushMatrix();
			GL.glColor3d(1, 1, 1);
			GL.glTranslated(0, 0, -10);
			GL.glScalef(0.1f, 0.1f, -0.1f);
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
			GL.glTranslated(0, m_smallRotorYTranslate, m_smallRotorZTranslate);
			GL.glRotatef(m_smallRotorAngle, 1, 0, 0);
			m_smallRotorAngle += m_smallRotorRotateSpeed;
			GL.glTranslated(0, -m_smallRotorYTranslate, -m_smallRotorZTranslate);
			m_heliSmallRotor.DrawModel();
			GL.glPopMatrix();
		}

		private void drawAndMoveMainRotor()
		{
			GL.glPushMatrix();
			GL.glTranslated(m_mainRotorXTranslate, 0, m_mainRotorZTranslate);
			GL.glRotatef(m_mainRotateAngle, 0, -1, 0);
			m_mainRotateAngle += m_mainRotorRotateSpeed;
			GL.glTranslated(-m_mainRotorXTranslate, 0, -m_mainRotorZTranslate);
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
					value += m_rotatingSpeed;
				}
			}
			else
			{
				if (value > 0)
				{
					value -= m_rotatingSpeed;
				}
			}
		}
		#endregion
	}
}
