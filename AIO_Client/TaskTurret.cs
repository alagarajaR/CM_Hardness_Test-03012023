using Labtt.Communication.MasterControl;

namespace AIO_Client
{

	public class TaskTurret : ITask
	{
		private MainForm owner;

		private TurretDelegate callBack;

		private TurretDirection turretDirection;

		public TaskTurret(MainForm owner, TurretDelegate callBack, TurretDirection turretDirection)
		{
			this.owner = owner;
			this.callBack = callBack;
			this.turretDirection = turretDirection;
		}

		public void Execute()
		{
			callBack(turretDirection);
		}
	}
}
