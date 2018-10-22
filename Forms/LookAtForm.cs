using Graphics;
using Logics;
using System.Windows.Forms;

namespace Forms
{
	public partial class LookAtForm : Form
	{
		private SceneEngine m_engine;

		public LookAtForm(SceneEngine engine)
		{
			InitializeComponent();

			m_engine = engine;

			// init first look at
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.X, valueReduce(scrollBarEyeX.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.Y, valueReduce(scrollBarEyeY.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.Z, valueReduce(scrollBarEyeZ.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.X, valueReduce(scrollBarCenterX.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.Y, valueReduce(scrollBarCenterY.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.Z, valueReduce(scrollBarCenterZ.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.X, valueReduce(scrollBarUpX.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.Y, valueReduce(scrollBarUpY.Value));
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.Z, valueReduce(scrollBarUpZ.Value));
		}

		private void scrollBarEyeX_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.X, valueReduce(e.NewValue));
		}

		private void scrollBarEyeY_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.Y, valueReduce(e.NewValue));
		}

		private void scrollBarEyeZ_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Eye, eAxis.Z, valueReduce(e.NewValue));
		}

		private void scrollBarCenterX_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.X, valueReduce(e.NewValue));
		}

		private void scrollBarCenterY_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.Y, valueReduce(e.NewValue));
		}

		private void scrollBarCenterZ_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Center, eAxis.Z, valueReduce(e.NewValue));
		}

		private void scrollBarUpX_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.X, valueReduce(e.NewValue));
		}

		private void scrollBarUpY_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.Y, valueReduce(e.NewValue));
		}

		private void scrollBarUpZ_Scroll(object sender, ScrollEventArgs e)
		{
			m_engine.LookAtPositionChanged(eLookAtParam.Up, eAxis.Z, valueReduce(e.NewValue));
		}

		private float valueReduce(int value)
		{
			return (value - 100) / 10.0f;
		}
	}
}
