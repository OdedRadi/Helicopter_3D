using Logics;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
	class Skyscraper : IGraphicComponent
	{
		public eThrottleStick ThrottleStickState { set => throw new NotImplementedException(); }
		public eDirectionStick DirectionStickState { set => throw new NotImplementedException(); }

		private uint m_textureId;
		private const float m_height = 1000;
		private const float m_width = 300;

		public void Init()
		{
			initTexture();
		}

		private void initTexture()
		{
			string[] bmpFilesPath = new string[1];

			bmpFilesPath[0] = "../../Resources/skyscraper.bmp";

			uint[] textureList = TextureLoader.LoadTextures(bmpFilesPath);

			m_textureId = textureList[0];
		}

		public void Draw()
		{
			// front

			GL.glEnable(GL.GL_TEXTURE_2D);

			GL.glColor3f(1,1,1);
			GL.glBindTexture(GL.GL_TEXTURE_2D, m_textureId);
			GL.glBegin(GL.GL_QUADS);
			GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0, 0, 0);
			GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(m_width, 0, 0);
			GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(m_width, m_height, 0);
			GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0, m_height, 0);
			GL.glEnd();

			GL.glDisable(GL.GL_TEXTURE_2D);
		}
	}
}
