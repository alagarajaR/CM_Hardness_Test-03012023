using System.Threading;

namespace AIO_Client
{

	public class TaskMovePlatformByRecord : ITask
	{
		private MainForm owner;

		private MoveByRecordDelegate callBack;

		private MeasureRecord targetRecord;

		public TaskMovePlatformByRecord(MainForm owner, MoveByRecordDelegate callBack, MeasureRecord targetRecord)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.targetRecord = targetRecord;
		}

		public void Execute()
		{
			callBack(targetRecord);
			Thread.Sleep(500);
		}
	}

}