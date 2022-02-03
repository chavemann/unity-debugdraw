using System.Collections.Generic;
using Attachments;
using UnityEngine;

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
		private bool init;

		private void OnEnable()
		{
			transform = base.transform;

			// Log.Print("-- Log.Print ----------------");
			// Log.Print("    Args:", 0, "test", 4.5f);
			// Log.Print("    Arrays:", new int[] {0, 1, 2}, new List<int> {3, 4, 5});
			init = false;
		}

		private void Update()
		{
			Log.Print("Showcase.Update ----------------------------------------", init);
			if (!init)
			{
				if (lineStart || lineEnd)
				{
					Log.Print("== Adding Line attachment "); 
					DebugDraw.Add(LineAttachment.Line(
						lineStart, lineEnd, startOffset, endOffset, default, default, Color.red, Color.green));
				}
				
				init = true;
			}
			// DebugDraw.transform = transform.localToWorldMatrix;
			// DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);
			// DebugDraw.transform = Matrix4x4.identity;
		}

	}

}