using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class AxesSection : BaseSection
	{

		public Color xAxisColor;
		public Color yAxisColor;
		public Color zAxisColor;
		public Transform axes1;
		public Transform axes2;
		public Transform axes3;
		public Transform axes4;
		public float axes1Size = -1;
		public float axes2Size = -1;
		public Vector3 axes3Size = new Vector3(0.4f, 0.1f, 0.5f);
		public float axes4Size = -1;

		protected override void Init()
		{
			if (xAxisColor == default)
			{
				xAxisColor = Showcase.NiceColor();
				yAxisColor = Showcase.NiceColor();
				zAxisColor = Showcase.NiceColor();
			}
		}

		private void Update()
		{
			if (axes1)
			{
				DebugDraw.Axes(axes1.position, axes1.rotation, Mathf.Abs(axes1Size), axes1Size < 0);
			}

			if (axes2)
			{
				DebugDraw.Axes(axes2.position, axes2.rotation, Mathf.Abs(axes2Size), axes2Size < 0);
			}

			if (axes3)
			{
				DebugDraw.Axes(axes3.position, axes3.rotation,
					new Vector3(Mathf.Abs(axes3Size.x), Mathf.Abs(axes3Size.y), Mathf.Abs(axes3Size.z)),
					axes3Size.x < 0);
			}

			if (axes4)
			{
				DebugDraw.Axes(axes4.position, axes4.rotation, Mathf.Abs(axes4Size), axes4Size < 0)
					.SetColours(xAxisColor, yAxisColor, zAxisColor);
			}
		}

	}

}
