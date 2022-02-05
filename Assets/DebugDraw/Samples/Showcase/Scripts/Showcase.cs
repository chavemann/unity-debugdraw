using System;
using System.Collections;
using System.Collections.Generic;
using Visuals;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			
			DebugDraw.transform = transform.localToWorldMatrix;
			DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);//, 0.1f);
			DebugDraw.transform = Matrix4x4.identity;
		}

	}

}