using System.Collections.Generic;

namespace AIO_Client
{

	public class PatternSet
	{
		public int Index { get; set; }

		public string Identifier { get; set; }

		public string PatternName { get; set; }

		public int PointCount { get; set; }

		public bool Checked { get; set; }

		public List<PointAndGraphicsPair> PointAndGraphicsPairList { get; set; }
	}
}
