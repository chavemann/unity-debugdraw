using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class RectanglesSection : BaseSection
	{

		public float spacing = 1;
		public float rectSize = 0.5f;
		public Vector2 rect2Size = new Vector2(0.65f, 0.3f);
		public Vector3 speed = new Vector3(34, 29, 54);

		public readonly Color[] colors = new Color[4];

		protected Vector3 angles;

		protected override void Init()
		{
			Showcase.NiceColors(colors);
		}

		private void Update()
		{
			angles += speed * Time.deltaTime;
			
			Vector3 p = tr.position;
			Vector3 u = DebugDraw.up * spacing;
			Vector3 r = DebugDraw.right * spacing;

			DebugDraw.Rectangle(p + u - r, rectSize, DebugDraw.forward, colors[0]);
			DebugDraw.Rectangle(p + u + r, rect2Size, DebugDraw.forward, colors[1]);
			DebugDraw.Rectangle(p - u - r, rectSize, Quaternion.Euler(angles) * DebugDraw.forward, colors[2]);
			DebugDraw.FillRectangle(p - u + r, rectSize, DebugDraw.forward, colors[3]);
		}

	}

}