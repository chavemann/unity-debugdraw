using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Visuals;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Scripts
{

	[ExecuteAlways]
	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : MonoBehaviour
	{

		public GameObject lineStart;
		public GameObject lineEnd;
		public Vector3 startOffset;
		public Vector3 endOffset;

		private new Transform transform;
		private float delayedInit = -1;
		
		private int frame;

		private void OnEnable()
		{
			// if (Application.isPlaying != EditorApplication.isPlayingOrWillChangePlaymode)
				// return;
			
			transform = base.transform;
			// Log.Print("Showcase.OnEnable", Application.isPlaying, EditorApplication.isPlayingOrWillChangePlaymode); 

			// Log.Print("-- Log.Print ----------------");
			// Log.Print("    Args:", 0, "test", 4.5f);
			// Log.Print("    Arrays:", new int[] {0, 1, 2}, new List<int> {3, 4, 5});
			// init = false;

			if (lineStart || lineEnd)
			{
				LineVisual.Line(
					lineStart, lineEnd, startOffset, endOffset,
					default, default, Color.red, Color.green);
			}

			delayedInit = Time.time;
		}

		private void Update()
		{
			// Log.Print("  Showcase.Update");
			if (Application.isPlaying)
			{
				if (delayedInit >= 0)
				{
					float diff = Time.time - delayedInit;
			
					if (diff > 2)
					{
						SceneManager.LoadScene("Test");
					}
				}
			}

			if (frame == 0 || frame % (Application.isPlaying ? 8 : 4) == 0)
			{
				Log.nextMessageColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
				Log.Show(0, 1.0f, $" This is the time: <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");
			}
			
			Log.Show(1, 1.0f, $"<color=#66ffff>Persistent</color> message <b>XX</b> <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");
			
			DebugDraw.transform = transform.localToWorldMatrix;
			DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);
			DebugDraw.transform = Matrix4x4.identity;

			DebugDraw.Text(
				transform.position + Vector3.up * 0.1f, "Hello",
				Color.white, TextAnchor.LowerCenter, 1f)
				.SetUseWorldSize();

			frame++;
		}

	}

}