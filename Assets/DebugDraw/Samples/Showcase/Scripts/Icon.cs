using DebugDrawUtils;
using DebugDrawUtils.Items;
using UnityEngine;

namespace DebugDrawShowcase
{

[ExecuteAlways]
public class Icon : BaseComponent
{
	
	[Header("Icon")]
	public Color iconColor = Color.white;
	
	public bool iconCircle = true;
	public float iconSize = 0.2f;
	public bool iconAutoSize;
	public float axesSize;
	
	public Dot Dot { get; protected set; }
	public Axes Axes { get; protected set; }
	
	protected override void OnEnable()
	{
		base.OnEnable();
		
		ClearIcon();
		CreateIcon();
		UpdateIcon();
		
		ClearAxes();
		CreateAxes();
		UpdateAxes();
	}
	
	private void ClearIcon()
	{
		Dot?.Remove();
		Dot = null;
	}
	
	private void CreateIcon()
	{
		if (!DebugDraw.IsActive || iconSize <= 0 || !Transform)
		{
			ClearIcon();
			return;
		}
		
		if (!Dot)
		{
			Dot = DebugDraw.Dot(Transform.position, iconSize, iconColor, 0, -1);
		}
	}
	
	private void UpdateIcon()
	{
		if (!Dot)
			return;
		
		Dot.color = iconColor;
		Dot.radius = Mathf.Max(iconSize, 0);
		Dot.segments = iconCircle ? (iconAutoSize ? 24 : 0) : 1;
		Dot.autoSize = iconAutoSize;
	}
	
	private void ClearAxes()
	{
		Axes?.Remove();
		Axes = null;
	}
	
	private void CreateAxes()
	{
		if (!DebugDraw.IsActive || axesSize == 0 || !Transform)
		{
			ClearAxes();
			return;
		}
		
		if (!Axes)
		{
			Axes = DebugDraw.Axes(Transform.position, Transform.rotation, 0, false, -1);
		}
	}
	
	private void UpdateAxes()
	{
		if (!Axes)
			return;
		
		float size = Mathf.Abs(axesSize);
		Axes.size = new Vector3(size, size, size);
		Axes.doubleSided = axesSize < 0;
	}
	
	private void OnDisable()
	{
		ClearIcon();
		ClearAxes();
	}
	
	protected void LateUpdate()
	{
		if (Dot)
		{
			Dot.position = Transform.position;
		}
		
		if (Axes)
		{
			Axes.position = Transform.position;
			Axes.rotation = Transform.rotation;
		}
	}
	
	protected void OnValidate()
	{
		OnEnable();
	}
	
}

}
