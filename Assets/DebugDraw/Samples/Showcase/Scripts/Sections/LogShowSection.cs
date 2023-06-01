using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class LogShowSection : BaseSection
	{

		public bool isPersistent;

		private void OnTriggerStay(Collider other)
		{
			Transform tr = other.transform;
			Camera cam = tr.GetComponentInChildren<Camera>();

			if (!cam)
				return;

			if (isPersistent)
			{
				tr = cam.transform;
				Vector3 p = tr.position;
				Vector3 f = tr.forward;
				Log.Show(99, 2,
					$"<color=#bba0ffff><b>Position</b></color>: {p.x:f2}, {p.y:f2}, {p.z:f2}\n" +
					$"<color=#bba0ffff><b>Looking</b></color>: {f.x:f2}, {f.y:f2}, {f.z:f2}\n" +
					$"<color=#bba0ffff><b>Debug Items</b></color>: {DebugDraw.itemCount}\n" +
					$"<color=#bba0ffff><b>Debug Vertices</b></color>: {DebugDraw.vertexCount}");
			}
			else
			{
				Log.Show(0, 1, $"Current Game Time: <color=#FF9571FF>{Time.time:f2}</color>");
			}
		}

	}

}
