namespace AIO_Client
{

	public class TaskUpdateLoadTime : ITask
	{
		private MainForm owner;

		private UpdateLoadTimeDelegate callBack;

		private int loadTime;

		public TaskUpdateLoadTime(MainForm owner, UpdateLoadTimeDelegate callBack, int loadTime)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.loadTime = loadTime;
		}

		public void Execute()
		{
			callBack(loadTime);
		}
	}
}
