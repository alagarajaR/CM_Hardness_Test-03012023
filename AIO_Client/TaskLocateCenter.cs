using System.Threading;

namespace AIO_Client
{

	public class TaskLocateCenter : ITask
	{
		private MainForm owner;

		private LocateCenterDelegate callBack;

		public TaskLocateCenter(MainForm owner, LocateCenterDelegate callBack)
		{
			this.owner = owner;
			this.callBack = callBack;
		}

		public void Execute()
		{
			callBack();
			Thread.Sleep(500);
		}
	}
}
