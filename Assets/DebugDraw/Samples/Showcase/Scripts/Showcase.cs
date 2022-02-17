using System.Collections.Generic;
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
			Log.MessageStyle.fontSize = 20;
		}

		private void Update()
		{
			if (!Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}

			if (Input.GetKeyDown(KeyCode.Return))
			{
				ToggleDebugCamera();
			}

			if (DebugDrawCamera.isActive && Input.GetKeyDown(KeyCode.T))
			{
				DebugDrawCamera.TrackObject(!DebugDrawCamera.isTrackingObj
					? FindObjectOfType<PlayerMovement>()
					: null, true);
			}
		}

		public static void ToggleDebugCamera()
		{
			DebugDrawCamera.TrackObject(null);
			DebugDrawCamera.Toggle();
			DebugDrawCamera.UpdateCamera();
		}

		public static void InitRandom(Transform tr, int seed = 0)
		{
			Vector3 p = tr.position;
			Random.InitState(Mathf.FloorToInt(p.x * 101.2f + p.y * 312.2f + p.z + 32.65f) + seed);
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

		public static void NiceColors(List<Color> colors)
		{
			for (int i = 0; i < colors.Count; i++)
			{
				colors[i] = NiceColor();
			}
		}

		public static float SmoothPingPong(float min, float max, float speed, float offset = 0)
		{
			float diff = max - min;
			return Mathf.SmoothStep(min, max, Mathf.PingPong(Time.time * speed + offset * diff, diff) / diff);
		}

	}

}