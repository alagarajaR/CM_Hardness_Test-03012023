namespace AIO_Client
{

	public class TaskImpress : ITask
	{
		private MainForm owner;

		private IndentationDelegate callBack;

		private string scaleName;

		private int loadTime;

		private bool turretAfterImpress;

		public TaskImpress(MainForm owner, IndentationDelegate callBack, string scaleName, int loadTime, bool turretAfterImpress = false)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.scaleName = scaleName;
			this.loadTime = loadTime;
			this.turretAfterImpress = turretAfterImpress;
		}

		public void Execute()
		{
			callBack(scaleName, loadTime, turretAfterImpress);
		}
	}
}
