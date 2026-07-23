namespace ShoulderCam;

internal static class ShoulderPositionExtensions
{
	public static float GetOffsetValue(this ShoulderPosition shoulderPosition)
	{
		return shoulderPosition switch
		{
			ShoulderPosition.Right => 1f,
			ShoulderPosition.Left => -1f,
			_ => 0f,
		};
	}
}
