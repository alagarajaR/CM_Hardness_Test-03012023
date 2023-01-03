using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AIO_Client
{

	public class MeasureRecordFlowLayout : FlowLayoutPanel
	{
		private BindingList<MeasureRecord> records = null;

		public event EventHandler OnRecordButtonClick;

		public void ReloadContents()
		{
			for (int i = base.Controls.Count - 1; i >= 0; i--)
			{
				if (base.Controls[i] is MeasureRecordUnit unit)
				{
					unit.OnButtonClick -= unit_OnButtonClick;
				}
				base.Controls[i].Dispose();
			}
			foreach (MeasureRecord record in records)
			{
				Add(record);
			}
			Invalidate();
		}

		public void SetRecordSource(BindingList<MeasureRecord> records)
		{
			this.records = records;
			ReloadContents();
		}

		public void Add(MeasureRecord record)
		{
			MeasureRecordUnit unit = new MeasureRecordUnit(record);
			unit.Font = Font;
			unit.OnButtonClick += unit_OnButtonClick;
			base.Controls.Add(unit);
		}

		public void Remove(MeasureRecord record)
		{
			foreach (MeasureRecordUnit unit in base.Controls)
			{
				if (unit.Record == record)
				{
					unit.OnButtonClick -= unit_OnButtonClick;
					unit.Dispose();
					Invalidate();
					break;
				}
			}
		}

		public void Clear()
		{
			for (int i = base.Controls.Count - 1; i >= 0; i--)
			{
				if (base.Controls[i] is MeasureRecordUnit unit)
				{
					unit.OnButtonClick -= unit_OnButtonClick;
				}
				base.Controls[i].Dispose();
			}
		}

		public void UpdateRecord(MeasureRecord record)
		{
			MeasureRecordUnit foundUnit = null;
			foreach (MeasureRecordUnit unit in base.Controls)
			{
				if (unit.Record == record)
				{
					foundUnit = unit;
					break;
				}
			}
			if (foundUnit == null)
			{
				if (records.Contains(record))
				{
					Add(record);
				}
			}
			else
			{
				foundUnit.Refresh();
			}
		}

		public void RefreshRecord()
		{
			foreach (MeasureRecordUnit unit in base.Controls)
			{
				unit.Refresh();
			}
		}

		public void EnabledGoButton(bool enabled)
		{
			foreach (MeasureRecordUnit unit in base.Controls)
			{
				unit.EnalbleGoButton(enabled);
			}
		}

		private void unit_OnButtonClick(object sender, EventArgs e)
		{
			if (this.OnRecordButtonClick != null)
			{
				this.OnRecordButtonClick(sender, e);
			}
		}
	}
}
