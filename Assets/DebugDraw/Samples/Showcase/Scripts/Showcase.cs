using System;
using System.Collections.Generic;
using System.Globalization;
using DebugDrawAttachments;
using DebugDrawItems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Scripts
{

	[ExecuteAlways]
	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : Icon
	{

		[Header("Line Attachment")]
		public GameObject lineStart;
		public GameObject lineEnd;
		public Vector3 startOffset;
		public Vector3 endOffset;
		public Vector2 headOffset;
		public bool arrowAutoSize;

		[Header("Arc")]
		public float arcRotation = 0;
		public float arcStart = 0;
		public float arcEnd = 360;
		public Vector2 arcSize = new Vector2(2, 1);
		public DrawEllipseAxes arcAxes = DrawEllipseAxes.InsideArc;
		public DrawArcSegments arcSegments = DrawArcSegments.OpenOnly;
		public int arcRes = 32;

		[Header("Line3D")]
		public float line3DSize = 0.1f;
		public bool line3DFaceCam;
		public bool line3DAutoSize;
		public float line3DLength = 1;
		
		private float delayedInit = -1;
		private int frame;
		private LineAttachment attachment;
		private Arrow arrow;
		
		private readonly List<Vector3> positions = new List<Vector3>();
		private readonly List<Color> colors = new List<Color>();
		private readonly List<float> sizes = new List<float>();

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
				attachment = DebugDraw.Arrow(default, default, Color.red, Color.green, 0.5f, true, arrowAutoSize, -1)
					.AttachTo(lineStart, lineEnd)
					.start.SetLocalOffset(startOffset)
					.end.SetLocalOffset(endOffset);
				arrow = ((Arrow) attachment.line)
					.startHead.SetOffset(headOffset.x)
					.endHead.SetOffset(headOffset.y);
			}
			
			Random.InitState(5);
			for (int i = 0; i < 20; i++)
			{
				positions.Add(Vector3.Scale(Random.insideUnitSphere, new Vector3(1, 1, 1)));
				colors.Add(Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f));
				sizes.Add(Random.Range(0.25f, 0.75f));
			}
			
			// DebugDraw.Text(
			// 		default, "Hello",
			// 		Color.white, TextAnchor.LowerCenter, 1f, -1)
			// 	.SetUseWorldSize()
			// 	.AttachTo(this)
			// 		.obj.SetLocalOffset(Vector3.up * 0.1f);

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
			
			// DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);
			DebugDraw.Dots(positions, sizes, colors, 24)
				.SetAutoSize();
			DebugDraw.WireEllipse(Vector3.zero, arcSize, Vector3.forward, Color.cyan, arcRes)
				.SetArc(arcStart, arcEnd, arcSegments)
				.SetAxes(arcAxes)
				.SetRotation(arcRotation)
				.SetAutoResolution();
			// DebugDraw.Ellipse(Vector3.zero + Vector3.up * 3, arcSize, Vector3.forward, Color.cyan, arcRes)
			// 	.SetArc(arcStart, arcEnd, arcSegments)
			// 	.SetAxes(arcAxes)
			// 	.SetRotation(arcRotation);
			
			DebugDraw.transform = Matrix4x4.identity;
			
			// DebugDraw.WireEllipse(Vector3.zero, arcSize, tr.forward, Color.yellow, arcRes)
			// 	.SetArc(arcStart, arcEnd, arcSegments)
			// 	.SetAxes(arcAxes)
			// 	.SetRotation(arcRotation);
			// DebugDraw.Ellipse(Vector3.zero + Vector3.up * 3, arcSize, tr.forward, Color.yellow, arcRes)
			// 	.SetArc(arcStart, arcEnd, arcSegments)
			// 	.SetAxes(arcAxes)
			// 	.SetRotation(arcRotation);

			// DebugDraw.Line3D(tr.position - tr.right*line3DLength, tr.position + tr.right*line3DLength, line3DSize, tr.forward, Color.red, Color.green)
			// 	.SetAutoSize(line3DAutoSize)
			// 	.SetFaceCamera(line3DFaceCam);

			// DebugDraw.Text(
			// 	tr.position + Vector3.up * 0.1f, "Hello",
			// 	Color.white, TextAnchor.LowerCenter, 1f)
			// 	.SetUseWorldSize();

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

			if (arrow)
			{
				arrow
					.startHead.SetOffset(headOffset.x)
					.endHead.SetOffset(headOffset.y);
				arrow.autoSize = arrowAutoSize;
			}
		}

	}

}