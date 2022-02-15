using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DebugDrawAttachments;
using DebugDrawItems;
using DebugDrawUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Scripts
{

	[AddComponentMenu("DebugDraw/Samples/Showcase")]
	public class Showcase : Icon
	{

		public float crossHairSize = 0;

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
		public Vector2 arcSize1 = new Vector2(0.5f, 0.5f);
		public Vector2 arcSize2 = new Vector2(1f, 1f);
		public float arcInnerRadius = 0;
		public DrawEllipseAxes arcAxes = DrawEllipseAxes.InsideArc;
		public DrawArcSegments arcSegments = DrawArcSegments.OpenOnly;
		public int arcRes = 32;
		public Color arcColor = Color.cyan;

		[Header("Cone")]
		public float coneLength = 2;
		public float coneAngle = 90;
		public int coneRes = 12;
		public bool coneCaps = true;
		public bool shell = false;
		public int coneType = 0;

		[Header("Line3D")]
		public float line3DSize = 0.1f;
		public bool line3DFaceCam;
		public bool line3DAutoSize;
		public float line3DLength = 1;
		public Vector3 boxSize = Vector3.one;

		public Mesh mesh;
		private MeshItem m;
		
		private float delayedInit = 0;
		private int frame;
		private LineAttachment attachment;
		private Arrow arrow;
		
		private readonly List<Vector3> positions = new List<Vector3>();
		private readonly List<Color> colors = new List<Color>();
		private readonly List<float> sizes = new List<float>();

		static Showcase()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		private void Start()
		{
			Application.targetFrameRate = 60;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			
			// Log.Print("Showcase.OnEnable", DebugDraw.isActive); 
			
			if (!DebugDraw.isActive)
				return;
			
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
				arrow = ((Arrow) attachment.lineItem)
					.startHead.SetOffset(headOffset.x)
					.endHead.SetOffset(headOffset.y);
			}

			if (mesh)
			{
				m = DebugDraw.Mesh(mesh, Color.red, -1);
			}
			
			// Cylinder c;
			// if (shell)
			// {
			// 	c = DebugDraw.Cylinder(
			// 		tr.position + tr.up,
			// 		tr.position - tr.up,
			// 		arcSize1, arcSize2, Color.cyan, arcRes, -1);
			// }
			// else
			// {
			// 	c = DebugDraw.WireCylinder(
			// 		tr.position + tr.up,
			// 		tr.position - tr.up,
			// 		arcSize1, arcSize2, Color.cyan, arcRes, -1);
			// }
			//
			// c.AttachTo(lineStart, lineEnd);
			// 	.SetDistances(arcSize.x, arcSize.x);

			Random.InitState(5);
			for (int i = 0; i < 20; i++)
			{
				positions.Add(Vector3.Scale(Random.insideUnitSphere, new Vector3(1, 1, 1)));
				colors.Add(Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f));
				sizes.Add(Random.Range(0.25f, 0.75f));
			}
			
			DebugDraw.Text(
					default, "Hello",
					Color.white, TextAnchor.LowerCenter, 1f, -1)
				.SetUseWorldSize()
				.AttachTo(this)
					.obj.SetLocalOffset(Vector3.up * 0.1f);

			delayedInit = Time.time;
		}

		private void Update()
		{
			// Log.Print("Update", DebugDraw.isActive);

			if (!Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}

			if (Input.GetKeyDown(KeyCode.BackQuote))
			{
				DebugDrawCamera.Toggle();
				DebugDrawCamera.UpdateCamera();
			}

			if (DebugDrawCamera.active && Input.GetKeyDown(KeyCode.T))
			{
				DebugDrawCamera.TrackObject(!DebugDrawCamera.isTrackingObj
					? FindObjectOfType<PlayerMovement>()
					: null, true);
			}
			
			if (!DebugDraw.isActive)
				return;

			// if (Application.isPlaying)
			// {
				// if (delayedInit >= 0)
				// {
				// 	float diff = Time.time - delayedInit;
				//
				// 	if (diff > 1)
				// 	{
				// 		
				// 		
				// 		// SceneManager.LoadScene("Test");
				// 		
				// 		delayedInit = Time.time;
				// 	}
				// }
			// }

			if (frame == 0 || frame % (Application.isPlaying ? 12 : 4) == 0)
			{
				// Log.nextMessageColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 1f, 1f);
				// Log.Show(0, 1.0f, $" This is the time: <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");
			}
			
			// Log.Show(1, 1.0f, $"<color=#66ffff>Persistent</color> message <b>XX</b> <i>{DebugDraw.GetTime().ToString(CultureInfo.InvariantCulture)}</i>");

			if (m)
			{
				m.SetGlobalTransform(tr.localToWorldMatrix);
			}
			
			DebugDraw.transform = tr.localToWorldMatrix;
			
			// DebugDraw.Line(Vector3.zero, Vector3.forward, Color.cyan);
			
			// DebugDraw.Dots(positions, sizes, colors, 24)
			// 	.SetAutoSize();
			
			// DebugDraw.Ellipse(Vector3.zero, arcSize, Vector3.forward, Color.cyan, arcRes)
			// 	.SetArc(arcStart, arcEnd, arcSegments)
			// 	.SetAxes(arcAxes)
			// 	.SetRotation(arcRotation)
			// 	.SetAutoResolution();
			// DebugDraw.FillEllipse(Vector3.zero + Vector3.up * 3, arcSize, Vector3.forward, Color.cyan, arcRes)
			// 	.SetArc(arcStart, arcEnd, arcSegments)
			// 	.SetAxes(arcAxes)
			// 	.SetRotation(arcRotation);
			
			// DebugDraw.Quad(
			// 	Vector3.up + Vector3.left,
			// 	Vector3.up + Vector3.right,
			// 	Vector3.up + Vector3.right * 0.75f + Vector3.up,
			// 	Vector3.up + Vector3.left * 0.75f + Vector3.up,
			// 	Color.red, Color.green, Color.blue, Color.yellow);
			// DebugDraw.FillQuad(
			// 	Vector3.forward + Vector3.left,
			// 	Vector3.forward + Vector3.right,
			// 	Vector3.forward + Vector3.right * 0.75f + Vector3.up,
			// 	Vector3.forward + Vector3.left * 0.75f + Vector3.up,
			// 	Color.red, Color.green, Color.blue, Color.yellow);
			
			// DebugDraw.Sphere(Vector3.zero, boxSize, tr.rotation, Color.red, arcRes);
			// DebugDraw.Ball(Vector3.zero + Vector3.up * 3, boxSize, Quaternion.identity, Color.green, arcRes);
			
			DebugDraw.transform = Matrix4x4.identity;
			
			// DebugDraw.WireSphere(tr.position + Vector3.down, boxSize, tr.rotation, Color.red, arcRes);
			// DebugDraw.Sphere(tr.position + Vector3.down + Vector3.forward * 3, boxSize, tr.rotation, Color.green, arcRes);

			// DebugDraw.Box(tr.position, boxSize, tr.rotation, Color.red);

			// for (int i = -1; i < 2; i++)
			// {
			// 	if (i == 0 || i == 1 && shell)
			// 		continue;
			//
			// 	DebugDrawMesh mesh = i == -1 ? DebugDraw.lineMesh : DebugDraw.triangleMesh;
			// 	Vector3 pos = tr.position + tr.up * i;
			// 	Vector3 facing = tr.up;
			// 	Color clr = Color.yellow;
			// 	Ellipse e = mesh.Add(Ellipse.Get(
			// 		ref pos, ref arcSize1, ref facing,
			// 		ref clr, arcRes, arcAxes, 0)
			// 	)
			// 		.SetForward(tr.forward)
			// 		.SetArc(arcStart, arcEnd, arcSegments)
			// 		.SetRotation(arcRotation)
			// 		.SetInnerRadius(arcInnerRadius);
			//
			// 	e.wireframe = i == -1;
			// }
			
			// if (shell)
			// {
			// 	DebugDraw.Cylinder(
			// 		tr.position + tr.up,
			// 		tr.position - tr.up,
			// 		arcSize1, arcSize2, Color.cyan, arcRes, true, 0);
			// }
			// else
			// {
			// 	DebugDraw.WireCylinder(
			// 		tr.position + tr.up,
			// 		tr.position - tr.up,
			// 		arcSize1, arcSize2, Color.cyan, arcRes, true, 0);
			// }
			
			// DebugDraw.FillEllipse(tr.position + tr.forward * 4 + Vector3.up * 3, arcSize, tr.forward, Color.yellow, arcRes)
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

			// for (int i = -2; i < 3; i++)
			// {
			// 	if (i == 0)
			// 		continue;
			//
			// 	float s = Mathf.Sign(i);
			// 	Vector3 p1 = tr.position + tr.forward * s + tr.up * ((Mathf.Abs(i) - 1) * 4);
			// 	Vector3 dir = tr.forward * s;
			// 	Color clr = i < 0 ? Color.cyan : Color.magenta;
			// 	Cone c = i == -1 || i == 1
			// 		? DebugDraw.Cone(
			// 			p1, dir, coneLength,
			// 			coneAngle, clr, coneRes,
			// 			i < 0, coneCaps)
			// 		: DebugDraw.WireCone(
			// 			p1, dir, coneLength,
			// 			coneAngle, clr, coneRes,
			// 			i < 0, coneCaps);
			//
			// 	if (arcRotation != 0)
			// 		c.SetUp(tr.up);
			// }

			// for (int i = -2; i < 3; i++)
			// {
			// 	if (i == 0 || (coneType != 0 && i != coneType))
			// 		continue;
			//
			// 	float s = Mathf.Sign(i);
			// 	Vector3 localOffset = Vector3.forward * s + Vector3.up * ((Mathf.Abs(i) - 1) * 4);
			// 	Vector3 p1 = Vector3.zero + localOffset;
			// 	Vector3 dir = Vector3.forward * s;
			// 	Color clr = i < 0 ? Color.cyan : Color.magenta;
			// 	Cone c = i == -1 || i == 1
			// 		? DebugDraw.Cone(
			// 			p1, dir, coneLength,
			// 			coneAngle, clr, coneRes,
			// 			i < 0, coneCaps)
			// 		: DebugDraw.WireCone(
			// 			p1, dir, coneLength,
			// 			coneAngle, clr, coneRes,
			// 			i < 0, coneCaps);
			//
			// 	if (shell || coneType == 0)
			// 	{
			// 		c.AttachTo(tr)
			// 			.obj.SetLocalOffset(localOffset);
			// 	}
			// 	else
			// 	{
			// 		c.AttachTo(lineStart, lineEnd);
			// 	}
			// }

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

			DebugDrawCamera.crossHairSize = crossHairSize;
		}

	}

}