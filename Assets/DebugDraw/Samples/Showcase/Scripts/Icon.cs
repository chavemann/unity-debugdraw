using DebugDrawItems;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Scripts
{

	[ExecuteAlways]
	public class Icon : BaseComponent
	{

		public Color iconColor = Color.white;
		public bool iconCircle = true;
		public float iconSize = 0.2f;
		public float axesSize;

		protected Dot icon;
		protected Axes axes;

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
			icon?.Remove();
			icon = null;
		}

		private void CreateIcon()
		{
			if (iconSize <= 0 || !tr)
			{
				ClearIcon();
				return;
			}

			icon = DebugDraw.Dot(tr.position, iconSize, iconColor, 0, -1) ;
		}

		private void UpdateIcon()
		{
			if (!icon) return;

			icon.color = iconColor;
			icon.radius = iconSize;
			icon.segments = iconCircle ? 24 : 0;
		}

		private void ClearAxes()
		{
			axes?.Remove();
			axes = null;
		}

		private void CreateAxes()
		{
			if (axesSize == 0 || !tr)
			{
				ClearAxes();
				return;
			}
			
			axes = DebugDraw.Axes(tr.position, tr.rotation, default, false, -1);
		}

		private void UpdateAxes()
		{
			if (!axes)
				return;
			
			float size = Mathf.Abs(axesSize);
			axes.size = new Vector3(size, size, size);
			axes.doubleSided = axesSize < 0;
		}

		private void OnDisable()
		{
			ClearIcon();
			ClearAxes();
		}

		protected virtual void LateUpdate()
		{
			if (icon)
			{
				icon.position = tr.position;
			}

			if (axes)
			{
				axes.position = tr.position;
				axes.rotation = tr.rotation;
			}
		}

		protected virtual void OnValidate()
		{
			CreateIcon();
			UpdateIcon();

			CreateAxes();
			UpdateAxes();
		}

	}

}