using System;
using System.Globalization;
using DebugDrawAttachments;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Scripts
{

	[ExecuteAlways]
	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : Icon
	{

		public GameObject lineStart;
		public GameObject lineEnd;
		public Vector3 startOffset;
		public Vector3 endOffset;

		private float delayedInit = -1;
		private int frame;
		private LineAttachment attachment;

		protected override void OnEnable()
		{
			base.OnEnable();
			
			// if (Application.isPlaying != EditorApplication.isPlayingOrWillChangePlaymode)
				// return;
			
			// Log.Print("Showcase.OnEnable", Application.isPlaying); 

			// Log.Print("-- Log.Print ----------------");
			// Log.Print("    Args:", 0, "test", 4.5f);
			// Log.Print("    Arrays:", new int[] {0, 1, 2}, new List<int> {3, 4, 5});
			// init = false;

			if (lineStart || lineEnd)
			{
				attachment = DebugDraw.Line(default, default, Color.red, Color.green, -1)
					.AttachTo(lineStart, lineEnd)
					.start.SetLocalOffset(startOffset)
					.end.SetLocalOffset(endOffset);
			}

			delayedInit = Time.time;
		}

		private void Update()
		{
			// Log.Print("  Showcase.Update");
			// if (Application.isPlaying)
			// {
			// 	if (delayedInit >= 0)
			// 	{
			// 		float diff = Time.time - delayedInit;
			//
			// 		if (diff > 2)
			// 		{
			// 			SceneManager.LoadScene("Test");
			// 		}
			// 	}
			// }

			if (frame == 0 || frame % (Application.isPlaying ? 12 : 4) == 0)
			{
				Log.nextMessageColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
				Log.Show(0, 1.0f, $" This is the time: <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");
			}
			
			Log.Show(1, 1.0f, $"<color=#66ffff>Persistent</color> message <b>XX</b> <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");
			
			DebugDraw.transform = tr.localToWorldMatrix;
			DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);
			DebugDraw.transform = Matrix4x4.identity;

			DebugDraw.Text(
				tr.position + Vector3.up * 0.1f, "Hello",
				Color.white, TextAnchor.LowerCenter, 1f)
				.SetUseWorldSize();

			Random.State state = Random.state;
			Random.InitState(5);
			DebugDraw.transform = tr.localToWorldMatrix;
			for (int i = 0; i < 20; i++)
			{
				DebugDraw.Point(Random.insideUnitSphere, Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f));
			}
			DebugDraw.transform = Matrix4x4.identity;
			Random.state = state;

			frame++;
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			if (attachment)
			{
				attachment
					.start.SetLocalOffset(startOffset)
					.end.SetLocalOffset(endOffset);
			}
		}

	}

}