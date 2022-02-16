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
		public float linesPosition = 1;
		
		private readonly List<Vector3> linePositions = new List<Vector3>();
		private readonly List<Color> lineColors = new List<Color>();
		
		private Color color;
		private Color colorFade;
		private Color color1;
		private Color color2;

		protected override void Init()
		{
			CreateLines();

			color = Showcase.NiceColor();
			colorFade = Showcase.NiceColor();
			color1 = Showcase.NiceColor();
			color2 = Showcase.NiceColor();
		}

		private void CreateLines()
		{
			Vector3 p = tr.position + tr.right * linesPosition;
			
			linePositions.Clear();
			lineColors.Clear();
			
			for (int i = 0; i < lineCount; i++)
			{
				linePositions.Add(p + Random.insideUnitSphere * linesRadius);
				linePositions.Add(p + Random.insideUnitSphere * linesRadius);
				lineColors.Add(Showcase.NiceColor());
				lineColors.Add(Showcase.NiceColor());
			}
		}

		private void Update()
		{
			Vector3 p = tr.position;
			Vector3 r = tr.right * spacing;
			Vector3 u = tr.up * height;

			DebugDraw.Line(
				p - r - u,
				p - r + u, Color.Lerp(color, colorFade, Mathf.Cos(Time.time * fadeSpeed) * 0.5f + 0.5f));
			DebugDraw.Line(
				p + r - u,
				p + r + u, color1, color2);

			DebugDraw.Lines(linePositions, lineColors);
		}

	}
	
}