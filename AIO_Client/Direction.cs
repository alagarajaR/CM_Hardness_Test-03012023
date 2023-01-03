using System;

namespace AIO_Client
{

	[Flags]
	public enum Direction
	{
		None = 0,
		Left = 1,
		Front = 2,
		Right = 4,
		Back = 8,
		Up = 0x16,
		Bottom = 0x32
	}
}
