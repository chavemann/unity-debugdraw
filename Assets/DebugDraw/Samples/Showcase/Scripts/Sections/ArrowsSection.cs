using DebugDrawUtils;
using DebugDrawUtils.DebugDrawItems;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

[ExecuteAlways]
public class ArrowsSection : BaseSection
{
	
	public float spacing = 1;
	public float gap = 1;
	public float height = 1;
	public float size;
	public Icon a, b, c;
	public float offset = 0.1f;
	public Icon origin;
	public float rayDist = 1;
	public float rayNormal = 0.1f;
	
	private readonly Color[] colors = new Color[4];
	private float minDist, maxDist;
	
	protected override void Init()
	{
		Showcase.NiceColors(colors);
		
		if (b && c)
		{
			float dist = (c.tr.position - b.tr.position).magnitude;
			minDist = dist * 0.75f;
			maxDist = dist * 1.25f;
		}
	}
	
	private void Update()
	{
		Vector3 p = tr.position;
		Vector3 r = tr.right;
		Vector3 u = tr.up * height;
		float x = 0;
		
		DebugDraw.Arrow(
			p + r * x - u,
			p + r * x + u, colors[0], size);
		x += spacing;
		DebugDraw.Arrow(
				p + r * x - u,
				p + r * x + u, colors[1], size, size)
			.endHead.SetSize(size * 1.5f, size * 3);
		x += spacing;
		DebugDraw.Arrow(
			p + r * x - u,
			p + r * x + u, colors[2], colors[2],
			size, size, ArrowShape.Line, ArrowShape.Line);
		
		x += gap;
		DebugDraw.Arrow(
			p + r * x - u,
			p + r * x + u, colors[3], size * 4, true, true);
		
		if (a && b)
		{
			DebugDraw.Arrow(a.tr.position, b.tr.position, a.iconColor, a.iconColor, size, size, ArrowShape.Line, ArrowShape.Line, true)
				.startHead.SetOffset(offset)
				.endHead.SetOffset(offset);
		}
		
		if (b && c)
		{
			DebugDraw.Arrow(c.tr.position, b.tr.position, c.iconColor, size, true)
				.SetLimits(minDist, maxDist);
		}
		
		if (origin)
		{
			p = origin.tr.position;
			Vector3 dir = DebugDraw.down;
			Physics.Raycast(p, dir, out RaycastHit hit, rayDist);
			DebugDraw.Ray(p, dir, rayDist, hit, rayNormal, size * 0.5f);
		}
	}
	
}

}
