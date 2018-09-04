using OpenGL;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
	class Scene
	{
		private const double m_maxFovyAngle = 90;
		private const double m_minFovyAngle = 45;
		private double m_fovyAngle = 75;

		public float xSceneRotate { get; set; }
		public float ySceneRotate { get; set; }

		private int m_width = 0;
		private int m_height = 0;

		private uint m_uint_DC = 0;
		private uint m_uint_RC = 0;
		
		private SkyBox m_skyBox = new SkyBox();
		private Quadocopter m_quadocopter = new Quadocopter();

		public Scene(Size sceneSize, uint sceneWindowId)
		{
			m_width = sceneSize.Width;
			m_height = sceneSize.Height;
			InitializeGL(sceneWindowId);
		}

		~Scene()
		{
			WGL.wglDeleteContext(m_uint_RC);
		}

		#region init functions
		private void InitializeGL(uint sceneWindowId)
		{
			m_uint_DC = WGL.GetDC(sceneWindowId);
			WGL.wglSwapBuffers(m_uint_DC);

			initPixelFormat();
			initRenderingContext();
			initRenderingGL();
			initPerspective();

			m_quadocopter.Init();
			m_skyBox.Init();
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
		
		public void Draw()
		{
			if (m_uint_DC == 0 || m_uint_RC == 0)
				return;
			
			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
			GL.glLoadIdentity();
			GL.glClearColor(1, 1, 1,1);
			GL.glRotatef(xSceneRotate, 1, 0, 0);
			GL.glRotatef(ySceneRotate, 0, 1, 0);
			
			createLightning();
			m_quadocopter.Draw();
			GL.glDisable(GL.GL_LIGHTING);
			m_skyBox.Draw();
			
			GL.glFlush();
			WGL.wglSwapBuffers(m_uint_DC);
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
			m_quadocopter.ThrottleStickState |= throttleStickState;
			m_skyBox.ThrottleStickState |= throttleStickState;
		}

		public void ThrottleStickDeactivate(eThrottleStick throttleStickState)
		{
			m_quadocopter.ThrottleStickState &= ~throttleStickState;
			m_skyBox.ThrottleStickState &= ~throttleStickState;
		}
		
		public void DirectionStickActivate(eDirectionStick directionStickState)
		{
			m_quadocopter.DirectionStickState |= directionStickState;
			m_skyBox.DirectionStickState |= directionStickState;
		}

		public void DirectionStickDeactivate(eDirectionStick directionStickState)
		{
			m_quadocopter.DirectionStickState &= ~directionStickState;
			m_skyBox.DirectionStickState &= ~directionStickState;
		}
		#endregion

		#region gyro private funcs
		private void createLightning()
		{
			GL.glPushMatrix();

			float[] lightPos = new float[4];
			lightPos[0] = 5; 
			lightPos[1] = 5; 
			lightPos[2] = 5; 
			lightPos[3] = 1;

			// draw light source as solid sphere
			GL.glColor3f(1, 0, 0);
			GL.glTranslatef(lightPos[0], lightPos[1], lightPos[2]);
			GLUT.glutSolidSphere(1, 16, 16);

			GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, lightPos);
			GL.glEnable(GL.GL_LIGHTING);
			GL.glEnable(GL.GL_LIGHT0);
			GL.glEnable(GL.GL_COLOR_MATERIAL);

			GL.glPopMatrix();
		}

		/*private void drawAxes()
		{
			GL.glBegin(GL.GL_LINES);
			//x  RED
			GL.glColor3f(1.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(3.0f, 0.0f, 0.0f);
			//y  GREEN 
			GL.glColor3f(0.0f, 1.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 3.0f, 0.0f);
			//z  BLUE
			GL.glColor3f(0.0f, 0.0f, 1.0f);
			GL.glVertex3f(0.0f, 0.0f, 0.0f);
			GL.glVertex3f(0.0f, 0.0f, 3.0f);

			GL.glEnd();
		}*/

		/*private void drawSquareSurface(float width, float height, float depth, eAxis axis)
		{
			GL.glBegin(GL.GL_QUADS);

			// if there is no texture bind, the glTexCoord2f do nothing
			switch (axis)
			{
				case eAxis.X:
					GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(width, 0, 0);
					GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(width, 0, depth);
					GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(width, height, depth);
					GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(width, height, 0);
					break;
				case eAxis.Y:
					GL.glVertex3f(0, height, 0);
					GL.glVertex3f(0, height, depth);
					GL.glVertex3f(width, height, depth);
					GL.glVertex3f(width, height, 0);
					break;
				case eAxis.Z:
					GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0, 0, depth);
					GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0, height, depth);
					GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(width, height, depth);
					GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(width, 0, depth);
					break;
			}

			GL.glEnd();
		}*/
		#endregion
	}
}
