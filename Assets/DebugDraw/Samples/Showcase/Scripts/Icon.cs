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

		protected Dot icon;

		protected override void OnEnable()
		{
			base.OnEnable();

			if (icon)
			{
				icon.Remove();
			}
			
			icon = DebugDraw.Dot(tr.position, iconSize, iconColor, iconCircle ? 24 : 0, -1)
				.SetAutoSize();
		}

		private void OnDisable()
		{
			icon.Remove();
			icon = null;
		}

		protected virtual void LateUpdate()
		{
			icon.position = tr.position;
		}

		protected virtual void OnValidate()
		{
			if (icon)
			{
				icon.color = iconColor;
				icon.radius = iconSize;
				icon.segments = iconCircle ? 24 : 0;
			}
		}

	}

}