namespace AIO_Client
{

	public class TaskUpdateForce : ITask
	{
		private MainForm owner;

		private UpdateForceDelegate callBack;

		private string forceName;

		public TaskUpdateForce(MainForm owner, UpdateForceDelegate callBack, string forceName)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.forceName = forceName;
		}

		public void Execute()
		{
			callBack(forceName);
		}
	}
}
