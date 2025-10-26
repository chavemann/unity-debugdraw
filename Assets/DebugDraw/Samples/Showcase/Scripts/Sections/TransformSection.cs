using System;
using DebugDrawUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase.Sections
{

public class TransformSection : BaseSection
{
	
	public float colorSpeed = 1;
	public float length = 1;
	public float lineSize = 0.1f;
	public float spacing;
	public float boxSizeMax = 0.75f;
	
	private readonly Color[] colors = new Color[5];
	private Vector3 boxSize;
	
	protected override void Init()
	{
		Showcase.NiceColors(colors);
		colors[1].a = 0.25f;
		boxSize = new Vector3(
			Random.Range(boxSizeMax * 0.25f, boxSizeMax),
			Random.Range(boxSizeMax * 0.25f, boxSizeMax),
			Random.Range(boxSizeMax * 0.25f, boxSizeMax));
	}
	
	private void Update()
	{
		float t = Showcase.SmoothPingPong(0, 1, colorSpeed);
		DebugDraw.PushState();
		DebugDraw.transform = tr.localToWorldMatrix;
		DebugDraw.color = Color.Lerp(colors[0], colors[1], t);
		
		DebugDraw.Box(DebugDraw.left * spacing, boxSize, colors[2]);
		DebugDraw.Line3D(
			DebugDraw.right * spacing + DebugDraw.up * length,
			DebugDraw.right * spacing - DebugDraw.up * length, lineSize, colors[3], colors[4]);
		
		DebugDraw.PopState();
	}
	
}

}