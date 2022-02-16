using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Sections
{

	public class LinesSection : BaseSection
	{

		public float spacing = 0.5f;
		public float height = 1;
		public float fadeSpeed = 1;
		public int lineCount = 10;
		public float linesRadius = 0.5f;
		public Transform linesOrigin;

		public Transform line3DOrigin;
		public float line3DSize = 0.05f;
		public float lines3DSpacing = 1;
		public float lines3DLength = 1;

		public Transform lines3DOrigin;
		public int numLines3D = 10;
		public float lines3DSizeMin = 0.025f;
		public float lines3DSizeMax = 0.05f;
		public float lines3DRadius = 1;
		
		private readonly List<Vector3> linePositions = new List<Vector3>();
		private readonly List<Color> lineColors = new List<Color>();
		private readonly List<Vector3> line3DPositions = new List<Vector3>();
		private readonly List<float> line3DSizes = new List<float>();
		private readonly List<Color> line3DColors = new List<Color>();
		
		private readonly Color[] colors = new Color[7];

		protected override void Init()
		{
			Showcase.NiceColors(colors);
			
			CreateLines();
		}

		private void CreateLines()
		{
			Vector3 p = linesOrigin ? linesOrigin.position : tr.position;
			
			linePositions.Clear();
			lineColors.Clear();
			
			for (int i = 0; i < lineCount; i++)
			{
				linePositions.Add(p + Random.insideUnitSphere * linesRadius);
				linePositions.Add(p + Random.insideUnitSphere * linesRadius);
				lineColors.Add(Showcase.NiceColor());
				lineColors.Add(Showcase.NiceColor());
			}
			
			p = lines3DOrigin ? lines3DOrigin.position : tr.position;
			
			line3DPositions.Clear();
			line3DSizes.Clear();
			line3DColors.Clear();
			
			for (int i = 0; i < numLines3D; i++)
			{
				line3DPositions.Add(p + Random.insideUnitSphere * lines3DRadius);
				line3DPositions.Add(p + Random.insideUnitSphere * lines3DRadius);
				line3DSizes.Add(Random.Range(lines3DSizeMin, lines3DSizeMax));
				line3DColors.Add(Showcase.NiceColor());
				line3DColors.Add(Showcase.NiceColor());
			}
		}

		private void Update()
		{
			Vector3 p = tr.position;
			Vector3 r = tr.right;
			Vector3 u = tr.up;

			DebugDraw.Line(
				p - r * spacing - u * height,
				p - r * spacing + u * height, Color.Lerp(colors[0], colors[1], Mathf.Cos(Time.time * fadeSpeed) * 0.5f + 0.5f));
			DebugDraw.Line(
				p + r * spacing - u * height,
				p + r * spacing + u * height, colors[2], colors[3]);

			if (HasChanged(linesOrigin) || HasChanged(lines3DOrigin))
			{
				CreateLines();
			}
			
			DebugDraw.Lines(linePositions, lineColors);
			DebugDraw.Lines3D(line3DPositions, line3DSizes, line3DColors);

			if (line3DOrigin)
			{
				p = line3DOrigin.position;

				DebugDraw.Line3D(
					p + u * lines3DSpacing - r * lines3DLength,
					p + u * lines3DSpacing + r * lines3DLength, line3DSize, colors[4]);
				DebugDraw.Line3D(
					p - u * lines3DSpacing - r * lines3DLength,
					p - u * lines3DSpacing + r * lines3DLength, line3DSize * 3, colors[5], colors[6])
					.SetAutoSize();
			}
		}

	}
	
}