using DebugDrawItems;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class EllipsesSection : BaseSection
	{

		public Transform row1;
		public float row1Spacing = 1;
		public float row1Radius = 1;
		public float row1SizeSpeed = 1;
		public Vector3 row1Speed = new Vector3(-30, 44, -50);
		
		public Transform row2;
		public float row2Spacing = 1;
		public float row2Radius = 1;
		public float row2InnerRadius = 0.5f;
		
		public Transform row3;
		public float row3Spacing = 1;
		public float row3Radius = 1;
		public float row3InnerRadius = 0.5f;
		public float row3AngleMin = -45f;
		public float row3AngleMax = -45f;
		public float row3AngleSpeed = 90;

		private Vector3 row1Angle;
		private Vector3 row3Angle;
		private float row3ArcAngle;
		
		private readonly Color[] colors = new Color[9];

		protected override void Init()
		{
			Showcase.NiceColors(colors);
		}

		private void Update()
		{
			Vector3 forward = tr.forward;
			Vector3 right = tr.right;
			
			// Row 1
			row1Angle += row1Speed * Time.deltaTime;
			Vector3 facing = Quaternion.Euler(row1Angle) * forward;
			
			Vector3 p = row1 ? row1.position : tr.position;
			Vector3 r = right * row1Spacing;
			DebugDraw.Ellipse(
				p - r * 1,
				row1Radius,
				forward,
				colors[0]);
			DebugDraw.Ellipse(
				p + r * 0,
				new Vector2(
					row1Radius * (Mathf.Cos(Time.time * row1SizeSpeed) * 0.5f + 0.5f),
					row1Radius * (Mathf.Sin(Time.time * row1SizeSpeed) * 0.5f + 0.5f)),
				forward,
				colors[1]);
			DebugDraw.FillEllipse(
				p + r * 1,
				row1Radius,
				forward,
				colors[2]);
			
			// Row 2
			p = row2 ? row2.position : tr.position;
			r = right * row2Spacing;
			DebugDraw.Ellipse(
				p - r * 1,
				row2Radius,
				forward,
				colors[3], 8);
			DebugDraw.Ellipse(
				p + r * 0,
				row2Radius,
				forward,
				colors[4], 32, DrawEllipseAxes.Always);
			DebugDraw.Ellipse(
				p + r * 1,
				row2Radius,
				forward,
				colors[5])
				.SetInnerRadius(row2InnerRadius);
			
			// Row 3
			row3ArcAngle = Showcase.SmoothPingPong(0, 180, row3AngleSpeed);
			
			p = row3 ? row3.position : tr.position;
			r = right * row3Spacing;
			DebugDraw.Ellipse(
				p - r * 1,
				row3Radius,
				forward,
				colors[6])
				.SetArcAngles(row3AngleMin, row3AngleMax);
			DebugDraw.Ellipse(
				p + r * 0,
				row3Radius,
				forward,
				colors[7])
				.SetArc(-row3ArcAngle, row3ArcAngle, DrawArcSegments.OpenOnly)
				.SetInnerRadius(row3InnerRadius);
			DebugDraw.FillEllipse(
				p + r * 1,
				row3Radius,
				facing,
				colors[8])
				.SetArc(-row3ArcAngle, row3ArcAngle, DrawArcSegments.OpenOnly)
				.SetInnerRadius(row3InnerRadius);
		}

	}

}
