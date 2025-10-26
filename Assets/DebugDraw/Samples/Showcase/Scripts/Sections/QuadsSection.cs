using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawShowcase.Sections
{

public class QuadsSection : BaseSection
{
	
	public float spacing = 1;
	public float size = 0.5f;
	
	private readonly Color[] colors = new Color[9];
	
	protected override void Init()
	{
		Showcase.NiceColors(colors);
	}
	
	private void Update()
	{
		Vector3 o = tr.position;
		Vector3 up = tr.up;
		Vector3 right = tr.right;
		Vector3 r1 = right * spacing;
		Vector3 u1 = up * spacing;
		Vector3 r2 = right * size;
		Vector3 u2 = up * (size * 2);
		
		Vector3 p = o - r1 * spacing + u1 - u2 * 0.5f;
		DebugDraw.Triangle(p - r2, p + u2, p + r2, colors[0], colors[0], colors[0]);
		p = o + r1 * spacing + u1 - u2 * 0.5f;
		DebugDraw.FillTriangle(p - r2, p + u2, p + r2, colors[1], colors[2], colors[3]);
		p = o - r1 * spacing - u1 - u2 * 0.5f;
		DebugDraw.Quad(p - r2, p - r2 * 0.5f + u2, p + r2 * 0.5f + u2, p + r2, colors[4], colors[4], colors[4], colors[4]);
		p = o + r1 * spacing - u1 - u2 * 0.5f;
		DebugDraw.FillQuad(p - r2 * 0.5f, p - r2 + u2, p + r2 + u2, p + r2 * 0.5f, colors[5], colors[6], colors[7], colors[8]);
	}
	
}

}
