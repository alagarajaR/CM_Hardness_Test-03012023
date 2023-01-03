using System;
using System.Collections.Generic;
using System.Threading;

namespace AIO_Client
{

	public class TaskManager : Queue<ITask>
	{
		private Thread threadExecuteTask;

		private volatile bool canPause = false;

		private volatile bool canAbort = false;

		private volatile bool isPaused = false;

		private volatile bool isRunning = false;

		private string backupTaskName;

		private Queue<ITask> backupQueue;

		public bool IsRunning => isRunning;

		public bool IsPaused => isPaused;

		public event EventHandler OnTaskStarted;

		public event EventHandler OnTaskFinished;

		public event EventHandler OnSingleTaskDone;

		public event EventHandler OnTaskPaused;

		public event EventHandler OnTaskAborted;

		public event TaskFailedEventHandler OnTaskFailed;

		public TaskManager()
		{
			backupQueue = new Queue<ITask>();
		}

		public void Pause()
		{
			canPause = true;
		}

		public void Resume()
		{
			if (!isRunning && backupQueue.Count > 0)
			{
				while (backupQueue.Count > 0)
				{
					ITask taskToBeBackup = backupQueue.Dequeue();
					Enqueue(taskToBeBackup);
				}
				canPause = false;
				canAbort = false;
				isPaused = false;
				threadExecuteTask = new Thread(ExecuteTask);
				threadExecuteTask.Start(backupTaskName);
				if (this.OnTaskStarted != null)
				{
					this.OnTaskStarted(backupTaskName, new EventArgs());
				}
			}
		}

		public void Abort()
		{
			if (isRunning)
			{
				canAbort = true;
				return;
			}
			Clear();
			if (this.OnTaskAborted != null)
			{
				this.OnTaskAborted(backupTaskName, new EventArgs());
			}
			backupQueue.Clear();
			isPaused = false;
		}

		public void Run(string taskName = "")
		{
			if (!isRunning && base.Count > 0)
			{
				canPause = false;
				canAbort = false;
				threadExecuteTask = new Thread(ExecuteTask);
				threadExecuteTask.Start(taskName);
				if (this.OnTaskStarted != null)
				{
					this.OnTaskStarted(taskName, new EventArgs());
				}
			}
		}

		private void ExecuteTask(object obj)
		{
			isRunning = true;
			while (base.Count > 0)
			{
				if (canAbort)
				{
					Clear();
					if (this.OnTaskAborted != null)
					{
						this.OnTaskAborted(obj, new EventArgs());
					}
					backupQueue.Clear();
					isRunning = false;
					isPaused = false;
					return;
				}
				if (canPause)
				{
					backupQueue.Clear();
					backupTaskName = obj as string;
					while (base.Count > 0)
					{
						ITask taskToBeBackup = Dequeue();
						backupQueue.Enqueue(taskToBeBackup);
					}
					if (this.OnTaskPaused != null)
					{
						this.OnTaskPaused(obj, new EventArgs());
					}
					isPaused = true;
					isRunning = false;
					return;
				}
				ITask task = null;
				try
				{
					Thread.Sleep(100);
					task = Dequeue();
					task.Execute();
					if (this.OnSingleTaskDone != null)
					{
						this.OnSingleTaskDone(task, new EventArgs());
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
					if (this.OnTaskFailed != null)
					{
						this.OnTaskFailed(task, ex);
					}
					canPause = true;
				}
			}
			if (this.OnTaskFinished != null)
			{
				this.OnTaskFinished(obj, new EventArgs());
			}
			isRunning = false;
		}
	}
}
