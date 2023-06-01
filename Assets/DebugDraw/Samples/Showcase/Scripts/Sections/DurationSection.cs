using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	[RequireComponent(typeof(Icon))]
	public class DurationSection : BaseSection
	{

		public float duration = 0.5f;
		public float length = 1;
		public float spawnGap = 0.1f;

		private float t;

		private void Update()
		{
			t += Time.deltaTime;

			if (t > spawnGap)
			{
				t -= spawnGap;

				Vector3 p = tr.position;
				Vector3 u = tr.up * (length * 0.5f);
				DebugDraw.Line(p - u, p + u, Showcase.NiceColor(), duration);
			}
		}

	}

}
