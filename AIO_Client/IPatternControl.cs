using System.Collections.Generic;
using System.Drawing;

namespace AIO_Client
{

	public interface IPatternControl
	{
		void Initialize(MainForm owner);

		void BringToFront();

		void SetGraphicsVisible(bool visible);

		void Reset();

		List<PointF> GeneratePoints();

		void GeneratePointsAndDepth(out List<PointF> pointList, out List<float> depthList);
	}
}
