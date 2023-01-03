namespace AIO_Client
{

	public class TaskAutoFocus : ITask
	{
		private MainForm owner;

		private AutoFocusDelegate callBack;

		public TaskAutoFocus(MainForm owner, AutoFocusDelegate callBack)
		{
			this.owner = owner;
			this.callBack = callBack;
		}

		public void Execute()
		{
			callBack();
		}
	}
}