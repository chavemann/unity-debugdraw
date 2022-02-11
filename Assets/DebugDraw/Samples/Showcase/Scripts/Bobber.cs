using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Scripts
{

	public class Bobber : BaseComponent
	{

		public float startY;
		public float height = 2;
		public float speed = 4;
		public float phase;

		private void Reset()
		{
			startY = transform.localPosition.y;
			phase = Random.value * Mathf.PI;
		}

		private void Update()
		{
			Vector3 pos = tr.localPosition;
			pos.y = startY + (Mathf.Cos((Time.time + phase) * speed) + 1) * 0.5f * height;

			tr.localPosition = pos;
		}

	}

}