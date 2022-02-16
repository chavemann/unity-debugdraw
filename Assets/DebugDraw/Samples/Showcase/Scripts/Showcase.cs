using System.Globalization;
using System.Threading;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase
{

	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : MonoBehaviour
	{

		static Showcase()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		private void Awake()
		{
			Application.targetFrameRate = 60;

			DebugDrawCamera.onInitCamera += cam => cam.cullingMask = int.MaxValue;
		}

		private void Update()
		{
			if (!Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}

			if (Input.GetKeyDown(KeyCode.BackQuote))
			{
				DebugDrawCamera.TrackObject(null);
				DebugDrawCamera.Toggle();
				DebugDrawCamera.UpdateCamera();
			}

			if (DebugDrawCamera.isActive && Input.GetKeyDown(KeyCode.T))
			{
				DebugDrawCamera.TrackObject(!DebugDrawCamera.isTrackingObj
					? FindObjectOfType<PlayerMovement>()
					: null, true);
			}
		}

	}

}