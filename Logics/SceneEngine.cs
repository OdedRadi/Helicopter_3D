using Graphics;
using OpenGL;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Logics
{
	public class SceneEngine
	{
		private bool wKeyPressed = false;
		private bool sKeyPressed = false;
		private bool dKeyPressed = false;
		private bool aKeyPressed = false;
		private bool upKeyPressed = false;
		private bool downKeyPressed = false;
		private bool rightKeyPressed = false;
		private bool leftKeyPressed = false;

		private Timer timer = new Timer();
		private Scene m_scene;

		public SceneEngine()
		{
			timer.Interval = 20;
			timer.Tick += timer_Elapsed;
		}

		public void Init(Size sceneSize, uint sceneWindowId)
		{
			m_scene = new Scene();
			m_scene.Init(sceneSize, sceneWindowId);
		}

		public void Start()
		{
			timer.Start();
		}

		private void timer_Elapsed(object sender, EventArgs e)
		{
			m_scene.Draw();
		}

		public void HandleMouseWheel(object sender, MouseEventArgs e)
		{
			m_scene.ChangeLookDistance(e.Delta > 0 ? eLookDistance.Closer: eLookDistance.Further);		
		}

		public void HandleKeyDown(object sender, KeyEventArgs e)
		{
			eThrottleStick throttleStickState = eThrottleStick.None;
			eDirectionStick directionStickState = eDirectionStick.None;

			switch (e.KeyCode)
			{
				case Keys.W:
					if (!wKeyPressed)
					{
						throttleStickState = eThrottleStick.Ascend;
						wKeyPressed = true;
					}
					break;
				case Keys.S:
					if (!sKeyPressed)
					{
						throttleStickState = eThrottleStick.Descend;
						sKeyPressed = true;
					}
					break;
				case Keys.D:
					if (!dKeyPressed)
					{
						throttleStickState = eThrottleStick.Right;
						dKeyPressed = true;
					}
					break;
				case Keys.A:
					if (!aKeyPressed)
					{
						throttleStickState = eThrottleStick.Left;
						aKeyPressed = true;
					}
					break;
				case Keys.Up:
					if (!upKeyPressed)
					{
						directionStickState = eDirectionStick.Forward;
						upKeyPressed = true;
					}
					break;
				case Keys.Down:
					if (!downKeyPressed)
					{
						directionStickState = eDirectionStick.Backward;
						downKeyPressed = true;
					}
					break;
				case Keys.Right:
					if (!rightKeyPressed)
					{
						directionStickState = eDirectionStick.Right;
						rightKeyPressed = true;
					}
					break;
				case Keys.Left:
					if (!leftKeyPressed)
					{
						directionStickState = eDirectionStick.Left;
						leftKeyPressed = true;
					}
					break;
			}

			if (throttleStickState != eThrottleStick.None)
			{ 
				m_scene.ThrottleStickActivate(throttleStickState);
			}

			if (directionStickState != eDirectionStick.None)
			{
				m_scene.DirectionStickActivate(directionStickState);
			}
		}

		public void HandleKeyUp(object sender, KeyEventArgs e)
		{
			eThrottleStick throttleStickState = eThrottleStick.None;
			eDirectionStick directionStickState = eDirectionStick.None;
			
			switch (e.KeyCode)
			{
				case Keys.W:
					wKeyPressed = false;
					throttleStickState = eThrottleStick.Ascend;
					break;
				case Keys.S:
					sKeyPressed = false;
					throttleStickState = eThrottleStick.Descend;
					break;
				case Keys.D:
					dKeyPressed = false;
					throttleStickState = eThrottleStick.Right;
					break;
				case Keys.A:
					aKeyPressed = false;
					throttleStickState = eThrottleStick.Left;
					break;
				case Keys.Up:
					upKeyPressed = false;
					directionStickState = eDirectionStick.Forward;
					break;
				case Keys.Down:
					downKeyPressed = false;
					directionStickState = eDirectionStick.Backward;
					break;
				case Keys.Right:
					rightKeyPressed = false;
					directionStickState = eDirectionStick.Right;
					break;
				case Keys.Left:
					leftKeyPressed = false;
					directionStickState = eDirectionStick.Left;
					break;
			}

			if (throttleStickState != eThrottleStick.None)
			{
				m_scene.ThrottleStickDeactivate(throttleStickState);
			}

			if (directionStickState != eDirectionStick.None)
			{
				m_scene.DirectionStickDeactivate(directionStickState);
			}
		}

		public void LightSourceDrawingChanged(bool draw)
		{
			m_scene.LightSourceDrawing = draw;
		}

		public void LightPositionChanged(eAxis axis, float value)
		{
			switch (axis)
			{
				case eAxis.X:
					m_scene.LightPositionX = value;
					break;
				case eAxis.Y:
					m_scene.LightPositionY = value;
					break;
				case eAxis.Z:
					m_scene.LightPositionZ = value;
					break;
			}
		}

		public void LookAtPositionChanged(eLookAtParam lookAtParam, eAxis axis, float value)
		{
			switch (lookAtParam)
			{
				case eLookAtParam.Eye:
					setLookAtEyeValue(axis, value);
					break;
				case eLookAtParam.Center:
					setLookAtCenterValue(axis, value);
					break;
				case eLookAtParam.Up:
					setLookAtUpValue(axis, value);
					break;
			}
		}

		private void setLookAtEyeValue(eAxis axis, float value)
		{
			switch (axis)
			{
				case eAxis.X:
					m_scene.LookAtEyeX = value;
					break;
				case eAxis.Y:
					m_scene.LookAtEyeY = value;
					break;
				case eAxis.Z:
					m_scene.LookAtEyeZ = value;
					break;
			}
		}

		private void setLookAtCenterValue(eAxis axis, float value)
		{
			switch (axis)
			{
				case eAxis.X:
					m_scene.LookAtCenterX = value;
					break;
				case eAxis.Y:
					m_scene.LookAtCenterY = value;
					break;
				case eAxis.Z:
					m_scene.LookAtCenterZ = value;
					break;
			}
		}

		private void setLookAtUpValue(eAxis axis, float value)
		{
			switch (axis)
			{
				case eAxis.X:
					m_scene.LookAtUpX = value;
					break;
				case eAxis.Y:
					m_scene.LookAtUpY = value;
					break;
				case eAxis.Z:
					m_scene.LookAtUpZ = value;
					break;
			}
		}
	}
}
