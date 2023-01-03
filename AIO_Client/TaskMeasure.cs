using System.Threading;

namespace AIO_Client
{

	public class TaskMeasure : ITask
	{
		private MainForm owner;

		private MeasureDelegate callBack;

		public TaskMeasure(MainForm owner, MeasureDelegate callBack)
		{
			this.owner = owner;
			this.callBack = callBack;
		}

		public void Execute()
		{
			Thread.Sleep(2000);
			owner.Invoke(callBack);
			Thread.Sleep(2000);
		}
	}
}
