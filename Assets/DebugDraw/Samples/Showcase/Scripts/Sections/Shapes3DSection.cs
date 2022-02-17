using System;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class Shapes3DSection : BaseSection
	{

		public float radius = 0.5f;
		public float height = 1;
		public float spacing = 1;
		public float coneAngle = 45;
		public int segmentsMin = 4;
		public int segmentsMax = 32;
		public float segmentsSpeed = 1;
		public Transform shellsTr;
		public Transform wireframeTr;

		public Transform cylinderTr;
		public Vector2 cylinderRadius1 = new Vector2(0.15f, 0.15f);
		public Vector2 cylinderRadius2 = new Vector2(0.15f, 0.35f);
		
		public Transform conesTr;
		public float conesAngleMin = 25;
		public float conesAngleMax = 55;
		public float conesAngleSpeed = 1;
		
		public Transform boxesTr;
		public float boxSize = 0.5f;
		public Vector3 box2Size = new Vector3(0.25f, 0.5f, 0.6f);
		public Vector3 box2Speed = new Vector3(15, 25, -30);
		
		private readonly Color[] colors = new Color[10];
		private Vector3 box2Angle;

		protected override void Init()
		{
			Showcase.NiceColors(colors);
		}

		private void Update()
		{
			Vector3 shellsP = shellsTr ? shellsTr.position : tr.position;
			Vector3 wireP = wireframeTr ? wireframeTr.position : tr.position;
			Vector3 r = tr.right * spacing;
			Vector3 up = tr.up;
			Vector3 forward = tr.forward;
			Vector3 u2 = up * height;
			Vector3 u3 = up * (height - radius);

			// Shell
			Vector3 o = shellsP;
			Vector3 p = o - r;
			DebugDraw.Cylinder(p - u2, p + u2, radius, colors[0]);
			p = o + r * 0;
			DebugDraw.Capsule(p - u3, p + u3, radius, colors[1]);
			p = o + r * 1;
			DebugDraw.Cone(p - u2, up, height * 2, coneAngle, colors[2]);
			p = o + r * 2;
			DebugDraw.Sphere(p, radius, colors[3]);

			// Wireframe
			int segments = Mathf.RoundToInt(Showcase.SmoothPingPong(segmentsMin, segmentsMax, segmentsSpeed));
			o = wireP;
			p = o - r;
			DebugDraw.WireCylinder(p - u2, p + u2, radius, colors[0], segments);
			p = o + r * 0;
			DebugDraw.WireCapsule(p - u3, p + u3, radius, colors[1], segments);
			p = o + r * 1;
			DebugDraw.WireCone(p - u2, up, height * 2, coneAngle, colors[2], segments);
			p = o + r * 2;
			DebugDraw.WireSphere(p, radius, colors[3], segments);
			
			// Cylinders
			o = cylinderTr ? cylinderTr.position : tr.position;
			p = o;
			p.y = shellsP.y;
			DebugDraw.Cylinder(p - forward * height, p + forward * height, cylinderRadius1, cylinderRadius2, colors[4]);
			p = o;
			p.y = wireP.y;
			DebugDraw.Cylinder(p - forward * height, p + forward * height, radius, colors[5], 32, true);
			
			// Cones
			float a = Showcase.SmoothPingPong(conesAngleMin, conesAngleMax, conesAngleSpeed);
			o = (conesTr ? conesTr.position : tr.position) + forward * height;
			p = o;
			p.y = shellsP.y;
			DebugDraw.Cone(p, -forward, height * 2, a, colors[6], 32,  false, true);
			a = Showcase.SmoothPingPong(conesAngleMin, conesAngleMax, conesAngleSpeed, 1);
			p = o;
			p.y = wireP.y;
			DebugDraw.Cone(p, -forward, height * 2, a, colors[7], 32,  true, true);
			
			// Boxes
			box2Angle += box2Speed * Time.deltaTime;
			o = boxesTr ? boxesTr.position : tr.position;
			p = o;
			p.y = shellsP.y;
			DebugDraw.Box(p, boxSize, tr.rotation, colors[8]);
			p = o;
			p.y = wireP.y;
			DebugDraw.Box(p, box2Size, Quaternion.Euler(box2Angle), colors[9]);
		}

	}

}