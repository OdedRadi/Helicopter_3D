﻿using OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
	class Scene
	{
		private static Scene m_instace;

		private const double m_maxFovyAngle = 90;
		private const double m_minFovyAngle = 45;
		private double m_fovyAngle = 75;

		private eThrottleStick m_throttleStickState = eThrottleStick.None;
		private eDirectionStick m_directionStickState = eDirectionStick.None;

		private float m_xTranslate;
		private float m_yTranslate;
		private float m_zTranslate;

		//private float m_xRotate;
		private float m_yRotate;
		//private float m_zRotate;

		private int m_width = 0;
		private int m_height = 0;

		private uint m_uint_DC = 0;
		private uint m_uint_RC = 0;

		private SkyBox m_skyBox = new SkyBox();
		private Helicopter m_helicopter = new Helicopter();
		private Skyscraper m_skyscraper = new Skyscraper();
		private Light m_light = new Light();

		private Scene()
		{
		}

		~Scene()
		{
			WGL.wglDeleteContext(m_uint_RC);
		}

		public static Scene GetInstance()
		{
			if (m_instace == null)
			{
				m_instace = new Scene();
			}

			return m_instace;
		}

		#region init functions

		public void Init(Size sceneSize, uint sceneWindowId)
		{
			m_width = sceneSize.Width;
			m_height = sceneSize.Height;
			InitializeGL(sceneWindowId);
		}

		private void InitializeGL(uint sceneWindowId)
		{
			m_uint_DC = WGL.GetDC(sceneWindowId);
			WGL.wglSwapBuffers(m_uint_DC);

			initPixelFormat();
			initRenderingContext();
			initRenderingGL();
			initPerspective();

			m_zTranslate = -10;
			m_yTranslate = -1;

			m_helicopter.Init();
			m_skyBox.Init();
			m_skyscraper.Init();
			m_light.Init(0, -10, 20, 5);
		}

		private void initPixelFormat()
		{
			int pixelFormatIndex = 0;
			WGL.PIXELFORMATDESCRIPTOR pixelFormatDescriptor = new WGL.PIXELFORMATDESCRIPTOR();

			WGL.ZeroPixelDescriptor(ref pixelFormatDescriptor);
			pixelFormatDescriptor.nVersion = 1;
			pixelFormatDescriptor.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
			pixelFormatDescriptor.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
			pixelFormatDescriptor.cColorBits = 32;
			pixelFormatDescriptor.cDepthBits = 32;
			pixelFormatDescriptor.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);
			pixelFormatDescriptor.cStencilBits = 32;

			pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pixelFormatDescriptor);
			if (pixelFormatIndex == 0)
			{
				MessageBox.Show("Unable to retrieve pixel format");
				return;
			}

			if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pixelFormatDescriptor) == 0)
			{
				MessageBox.Show("Unable to set pixel format");
				return;
			}
		}

		private void initRenderingContext()
		{
			m_uint_RC = WGL.wglCreateContext(m_uint_DC);

			if (m_uint_RC == 0)
			{
				MessageBox.Show("Unable to get rendering context");
				return;
			}
			if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
			{
				MessageBox.Show("Unable to make rendering context current");
				return;
			}
		}

		private void initRenderingGL()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;

			GL.glEnable(GL.GL_DEPTH_TEST);
			GL.glDepthFunc(GL.GL_LEQUAL);
			GL.glViewport(0, 0, m_width, m_height);
			GL.glClearColor(0, 0, 0, 0);
		}

		private void initPerspective()
		{
			GL.glMatrixMode(GL.GL_PROJECTION);
			GL.glLoadIdentity();

			GLU.gluPerspective(m_fovyAngle, m_width / m_height, 1, 3000);

			GL.glMatrixMode(GL.GL_MODELVIEW);
			GL.glLoadIdentity();
		}
		#endregion

		public float xTranslate { get { return m_xTranslate; } }
		public float yTranslate { get { return m_yTranslate; } }
		public float zTranslate { get { return m_zTranslate; } }

		public void Draw()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;

			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
			GL.glLoadIdentity();
			GL.glTranslated(0, 0, -5);
			
			checkSticksState();

			// the world is moving (the helicopter always on 0,0,0), its looks like the helicopter is moving
			GL.glRotatef(m_yRotate, 0, 1, 0);
			GL.glTranslatef(m_xTranslate, m_yTranslate, m_zTranslate);

			m_light.SetEnable(true);
			drawReflectedScene();
			m_skyscraper.Draw();

			m_light.DrawLightSource();

			// the skybox always keep the same distance from the helicopter, so it is not affected by translation
			GL.glTranslatef(-m_xTranslate, -m_yTranslate, -m_zTranslate);
			m_skyBox.Draw();

			// the helicopter is not affected by translation or rotation (first person view)
			GL.glRotatef(m_yRotate, 0, -1, 0);
			m_helicopter.Draw();

			drawShadows();

			GL.glFlush();
			WGL.wglSwapBuffers(m_uint_DC);
		}

		private void drawReflectedScene()
		{
			m_skyscraper.BeginStencil(Skyscraper.eStencils.Reflection);

			GL.glPushMatrix();
			GL.glScalef(1, 1, -1);

			m_skyBox.Draw();

			if (m_zTranslate < -1) // the helicopter drawing only on the front wall of the skyscraper
			{
				GL.glTranslatef(-m_xTranslate, -m_yTranslate, -m_zTranslate); // the real helicopter is always at 0,0,0, so we go back to 0,0,0
				GL.glRotatef(m_yRotate, 0, -1, 0); // to enable reflcted helicopter rotating
				m_helicopter.Draw();

				// drawing shadows reflections, moving to the skyscraper origins to set correct reflected floor stencil
				GL.glRotatef(m_yRotate, 0, 1, 0);
				GL.glTranslatef(m_xTranslate, m_yTranslate, m_zTranslate);
				m_skyscraper.BeginReflectedFloorStencil();
				GL.glTranslatef(-m_xTranslate, -m_yTranslate, -m_zTranslate);

				drawFloorShadows();
			}

			GL.glPopMatrix();

			m_skyscraper.StopStencil();
		}


		private void drawShadows()
		{
			GL.glPushMatrix();
			GL.glDisable(GL.GL_LIGHTING);
			GL.glColor3f(0.15f, 0.15f, 0.15f);

			// move to skyscraper origins
			GL.glRotatef(m_yRotate, 0, 1, 0);
			GL.glTranslatef(m_xTranslate, m_yTranslate, m_zTranslate);

			// walls shadows
			drawWallsShadows();

			// floor shadow
			m_skyscraper.BeginStencil(Skyscraper.eStencils.Floor);
			GL.glTranslatef(-m_xTranslate, -m_yTranslate, -m_zTranslate);
			drawFloorShadows();
			m_skyscraper.StopStencil();

			GL.glEnable(GL.GL_LIGHTING);
			GL.glPopMatrix();
		}

		private void drawWallsShadows()
		{
			float[] lightPos = new float[4];

			lightPos[0] = m_light.X;
			lightPos[1] = m_light.Y;
			lightPos[2] = m_light.Z;
			lightPos[3] = 1;

			float[] matrix = null;

			// back wall shadow
			m_skyscraper.BeginStencil(Skyscraper.eStencils.BackWall);
			matrix = m_skyscraper.GetShadowMatrix(Skyscraper.eShadowMatrices.BackWall, lightPos);

			GL.glPushMatrix();
			GL.glTranslatef(0, 0, -0.01f);
			multiplyMatrixAndDrawHelicotperShadow(matrix);
			GL.glPopMatrix();

			// left wall shadow
			m_skyscraper.BeginStencil(Skyscraper.eStencils.LeftWall);
			matrix = m_skyscraper.GetShadowMatrix(Skyscraper.eShadowMatrices.LeftWall, lightPos);

			GL.glPushMatrix();
			GL.glTranslatef(-0.01f, 0, 0);
			multiplyMatrixAndDrawHelicotperShadow(matrix);
			GL.glPopMatrix();

			// right wall shadow
			m_skyscraper.BeginStencil(Skyscraper.eStencils.RightWall);
			matrix = m_skyscraper.GetShadowMatrix(Skyscraper.eShadowMatrices.RightWall, lightPos);

			GL.glPushMatrix();
			GL.glTranslatef(0.01f, 0, 0);
			multiplyMatrixAndDrawHelicotperShadow(matrix);
			GL.glPopMatrix();

			// disable stencil
			m_skyscraper.StopStencil();
		}

		private void multiplyMatrixAndDrawHelicotperShadow(float[] matrix)
		{
			GL.glMultMatrixf(matrix);
			GL.glTranslatef(-m_xTranslate, -m_yTranslate, -m_zTranslate);
			GL.glRotatef(m_yRotate, 0, -1, 0);
			m_helicopter.DrawShadow();
		}

		private void drawFloorShadows()
		{
			GL.glPushMatrix();
			GL.glDisable(GL.GL_LIGHTING);

			GL.glColor3f(0.15f, 0.15f, 0.15f);

			// creating floor shadow matrix
			float[] lightPos = new float[4];

			lightPos[0] = m_light.X + m_xTranslate;
			lightPos[1] = m_light.Y + m_yTranslate;
			lightPos[2] = m_light.Z + m_zTranslate;
			lightPos[3] = 1;

			float[] mat = m_skyscraper.GetShadowMatrix(Skyscraper.eShadowMatrices.Floor, lightPos);
			
			GL.glTranslatef(0, 0.01f, 0);
			GL.glMultMatrixf(mat);

			// drawing helicopter shadow
			GL.glPushMatrix();
			GL.glRotatef(m_yRotate, 0, -1, 0);
			m_helicopter.DrawShadow();
			GL.glPopMatrix();

			// drawing skyscraper shadow
			GL.glPushMatrix();
			GL.glTranslatef(m_xTranslate, m_yTranslate, m_zTranslate);
			m_skyscraper.DrawShadow();
			GL.glPopMatrix();
			
			GL.glEnable(GL.GL_LIGHTING);
			GL.glPopMatrix();
		}

		public void ChangeLookDistance(eLookDistance lookDistance)
		{
			switch (lookDistance)
			{
				case eLookDistance.Further:
					if (m_fovyAngle < m_maxFovyAngle)
					{
						m_fovyAngle++;
					}
					break;
				case eLookDistance.Closer:
					if (m_fovyAngle > m_minFovyAngle)
					{
						m_fovyAngle--;
					}
					break;
			}

			initPerspective();
		}

		#region sticks handlers
		public void ThrottleStickActivate(eThrottleStick throttleStickState)
		{
			m_helicopter.ThrottleStickState |= throttleStickState;
			m_throttleStickState |= throttleStickState;
		}

		public void ThrottleStickDeactivate(eThrottleStick throttleStickState)
		{
			m_helicopter.ThrottleStickState &= ~throttleStickState;
			m_throttleStickState &= ~throttleStickState;
		}

		public void DirectionStickActivate(eDirectionStick directionStickState)
		{
			m_helicopter.DirectionStickState |= directionStickState;
			m_directionStickState |= directionStickState;
		}

		public void DirectionStickDeactivate(eDirectionStick directionStickState)
		{
			m_helicopter.DirectionStickState &= ~directionStickState;
			m_directionStickState &= ~directionStickState;
		}

		private void checkSticksState()
		{
			checkThrottleStickState();
			checkDirectionStickState();
		}

		private void checkThrottleStickState()
		{
			if (m_throttleStickState.HasFlag(eThrottleStick.Ascend))
			{
				m_yTranslate -= 0.2f;
			}
			if (m_throttleStickState.HasFlag(eThrottleStick.Descend))
			{
				m_yTranslate += 0.2f;
			}
			if (m_throttleStickState.HasFlag(eThrottleStick.Right))
			{
				m_yRotate += 1;
			}
			if (m_throttleStickState.HasFlag(eThrottleStick.Left))
			{
				m_yRotate -= 1;
			}
		}

		private void checkDirectionStickState()
		{
			if (m_directionStickState.HasFlag(eDirectionStick.Forward))
			{
				m_xTranslate -= (float)Math.Sin((Math.PI / 180) * m_yRotate) * 0.2f;
				m_zTranslate += (float)Math.Cos((Math.PI / 180) * m_yRotate) * 0.2f;
			}
			if (m_directionStickState.HasFlag(eDirectionStick.Backward))
			{
				m_xTranslate += (float)Math.Sin((Math.PI / 180) * m_yRotate) * 0.2f;
				m_zTranslate -= (float)Math.Cos((Math.PI / 180) * m_yRotate) * 0.2f;
			}
			if (m_directionStickState.HasFlag(eDirectionStick.Right))
			{
				m_xTranslate -= (float)Math.Cos((Math.PI / 180) * m_yRotate) * 0.2f;
				m_zTranslate -= (float)Math.Sin((Math.PI / 180) * m_yRotate) * 0.2f;
			}
			if (m_directionStickState.HasFlag(eDirectionStick.Left))
			{
				m_xTranslate += (float)Math.Cos((Math.PI / 180) * m_yRotate) * 0.2f;
				m_zTranslate += (float)Math.Sin((Math.PI / 180) * m_yRotate) * 0.2f;
			}
		}
		#endregion
		
		public void drawAxes()
		{
			GL.glBegin(GL.GL_LINES);
			//x  RED
			GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(300.0f, 0.0f, 0.0f);
			//y  GREEN 
			GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 300.0f, 0.0f);
			//z  BLUE
			GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 300.0f);

			GL.glEnd();
		}
	}
}
