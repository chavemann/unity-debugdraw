using System.Collections.Generic;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawShowcase.Sections
{

public class DotsSection : BaseSection
{
	
	public int pointCount;
	public float pointsWidth = 1;
	public float pointsHeight = 1;
	public float pointsSpeed = 1;
	public float pointsFrequency = 1;
	public Transform pointsObj;
	
	public float dotSize = 0.2f;
	public float dotSpacing = 0.3f;
	public Transform dotsObj;
	
	public Transform facingObj;
	
	public float batchSizeMin = 0.075f;
	public float batchSizeMax = 0.15f;
	public Transform batchObj;
	
	private readonly List<Vector3> points = new();
	private readonly List<Color> pointColors = new();
	private readonly List<Vector3> dotPositions = new();
	private readonly List<float> sizes = new();
	
	private readonly Color[] colors = new Color[8];
	
	protected override void Init()
	{
		Showcase.NiceColors(colors);
		
		if (pointCount < 1)
		{
			pointCount = 1;
		}
		
		CreateDots();
	}
	
	private void CreateDots()
	{
		dotPositions.Clear();
		points.Clear();
		pointColors.Clear();
		sizes.Clear();
		
		for (int i = pointCount * 2 - 1; i >= 0; i--)
		{
			dotPositions.Add(default);
			points.Add(default);
			pointColors.Add(Showcase.NiceColor());
			sizes.Add(Random.Range(batchSizeMin, batchSizeMax));
		}
	}
	
	private void Update()
	{
		Vector3 o = pointsObj ? pointsObj.position : tr.position;
		Vector3 o2 = batchObj ? batchObj.position : tr.position;
		
		for (int i = 0; i < pointCount; i++)
		{
			float t = i / (float) (pointCount - 1);
			float y = pointsHeight * (t * 2 - 1);
			float a = y * pointsFrequency + Time.time * pointsSpeed;
			Vector3 p1 = new(
				Mathf.Cos(a) * pointsWidth, y,
				Mathf.Sin(a) * pointsWidth);
			Vector3 p2 = new(
				Mathf.Cos(a + Mathf.PI) * pointsWidth, y,
				Mathf.Sin(a + Mathf.PI) * pointsWidth);
			points[i] = o + p1;
			points[i + pointCount] = o + p2;
			dotPositions[i] = o2 + p1;
			dotPositions[i + pointCount] = o2 + p2;
		}
		
		DebugDraw.Points(points, pointColors);
		DebugDraw.Dots(dotPositions, sizes, pointColors);
		
		if (HasChanged(dotsObj) || HasChanged(facingObj) || HasChanged(tr))
		{
			CreateDots();
		}
		
		Vector3 up = tr.up * dotSpacing;
		Vector3 right = tr.right * dotSpacing;
		
		if (dotsObj)
		{
			o = dotsObj.position;
			DebugDraw.Dot(o - right + up, dotSize * 0.5f, colors[0]);
			DebugDraw.Dot(o + right + up, dotSize, colors[1]);
			DebugDraw.Dot(o + right - up, dotSize, colors[2], 4);
			DebugDraw.Dot(o - right - up, dotSize, colors[3], 8);
		}
		
		if (facingObj)
		{
			o = facingObj.position;
			DebugDraw.Dot(o - right + up, dotSize, colors[4]);
			DebugDraw.Dot(o + right + up, dotSize, colors[5], new Vector3(-1, -1, 0).normalized);
			DebugDraw.Dot(o + right - up, dotSize, colors[6], DebugDraw.right);
			DebugDraw.Dot(o - right - up, dotSize * 3, colors[7])
				.SetAutoSize();
		}
	}
	
}

}
