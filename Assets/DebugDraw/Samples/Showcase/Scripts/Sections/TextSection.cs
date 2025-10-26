using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class TextSection : BaseSection
	{
		
		public Transform player;
		public float playerDistance = 10;
		public Transform tr1;
		public Transform tr2;
		public Transform tr3;
		public Transform autoTr;
		public float scale = 2;
		public float autoScale = 2;
		
		private readonly Color[] colors = new Color[4];
		
		protected override void Init()
		{
			Showcase.NiceColors(colors);
			colors[0] = DebugDraw.colorIdentity;
		}
		
		private void Update()
		{
			Vector3 p = Position(autoTr);
			DebugDraw.Text(p, "Hello World", colors[3], TextAnchor.LowerCenter, autoScale)
				.SetUseWorldSize();
			
			if (player)
			{
				float dist = (player.position - tr.position).magnitude;
				
				if (dist > playerDistance)
					return;
			}
			
			p = Position(tr1);
			DebugDraw.Text(p, "Lorem ipsum dolor sit amet.\nLorem ipsum dolor sit amet.", colors[0], TextAnchor.MiddleCenter);
			p = Position(tr2);
			DebugDraw.Text(p, "Lorem ipsum dolor sit amet.\nLorem ipsum dolor sit amet.", colors[1], TextAnchor.LowerLeft);
			p = Position(tr3);
			DebugDraw.Text(p, "Lorem ipsum dolor sit amet.", colors[2], TextAnchor.UpperCenter, scale);
		}
		
	}

}
