using System.Globalization;
using System.Threading;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase
{

	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : MonoBehaviour
	{

		public float crossHairSize = 1;

		static Showcase()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		private void Awake()
		{
			Application.targetFrameRate = 60;

			DebugDrawCamera.onInitCamera += cam => cam.cullingMask = int.MaxValue;
			DebugDrawCamera.crossHairSize = crossHairSize;
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

		public static void InitRandom(Transform tr)
		{
			Vector3 p = tr.position;
			Random.InitState(Mathf.FloorToInt(p.x * 101.2f + p.y * 312.2f + p.z + 32.65f));
		}

		public static Color NiceColor()
		{
			return Random.ColorHSV(0, 1, 0.5f, 0.85f, 0.8f, 1);
		}

		public static void NiceColors(Color[] colors)
		{
			for (int i = 0; i < colors.Length; i++)
			{
				colors[i] = NiceColor();
			}
		}

	}

}