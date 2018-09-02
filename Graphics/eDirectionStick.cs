using System;

namespace Graphics
{
	[Flags]
	public enum eDirectionStick
	{
		None = 0,
		Forward = 1,
		Backward = 2,
		Right = 4,
		Left = 8
	}
}
