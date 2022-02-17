using System;
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
					$"<b>Position</b>: {p.x:f2}, {p.y:f2}, {p.z:f2}\n" +
					$"<b>Looking</b>: {f.x:f2}, {f.y:f2}, {f.z:f2}");
			}
			else
			{
				Log.Show(0, 1, $"Current Game Time: <color=#FF9571FF>{Time.time:f2}</color>");
			}
		}

	}

}