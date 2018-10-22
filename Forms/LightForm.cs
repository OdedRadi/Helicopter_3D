using Graphics;
using Logics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Forms
{
	public partial class LightPositionForm : Form
	{
		private SceneEngine m_engine;

		public LightPositionForm(SceneEngine engine)
		{
			InitializeComponent();
			m_engine = engine;

			// init first light position and drawing
			m_engine.LightPositionChanged(eAxis.X, scrollBarX.Value);
			m_engine.LightPositionChanged(eAxis.Y, scrollBarY.Value);
			m_engine.LightPositionChanged(eAxis.Z, scrollBarZ.Value);
			m_engine.LightSourceDrawingChanged(false);
		}

		// instead of closing the form, hide him
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void scrollBarX_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LightPositionChanged(eAxis.X, e.NewValue);
		}

		private void scrollBarY_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LightPositionChanged(eAxis.Y, e.NewValue);
		}

		private void scrollBarZ_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LightPositionChanged(eAxis.Z, e.NewValue);
		}

		private void checkBoxDrawLightSource_CheckedChanged(object sender, EventArgs e)
		{
			m_engine.LightSourceDrawingChanged(checkBoxDrawLightSource.Checked);
		}
	}
}
