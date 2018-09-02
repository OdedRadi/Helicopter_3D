using System;

namespace Graphics
{
	[Flags]
	public enum eThrottleStick
	{
		None = 0,
		Ascend = 1,
		Descend = 2,
		Right = 4,
		Left = 8		
	}
}
