namespace Graphics
{
	interface IGraphicComponent
	{
		eThrottleStick ThrottleStickState { set; }
		eDirectionStick DirectionStickState { set; }

		void Init();
		void Draw();
	}
}