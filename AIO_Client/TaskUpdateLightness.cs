namespace AIO_Client
{

	public class TaskUpdateLightness : ITask
	{
		private MainForm owner;

		private UpdateLightnessDelegate callBack;

		private int lightness;

		public TaskUpdateLightness(MainForm owner, UpdateLightnessDelegate callBack, int lightness)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.lightness = lightness;
		}

		public void Execute()
		{
			callBack(lightness);
		}
	}
}
