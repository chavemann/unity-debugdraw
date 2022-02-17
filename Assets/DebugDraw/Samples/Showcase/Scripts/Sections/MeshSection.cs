using DebugDrawItems;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class MeshSection : BaseSection
	{

		public Mesh mesh;
		public float spacing = 1;

		private Color color;
		private MeshItem mesh1;
		private MeshItem mesh2;
		
		protected override void Init()
		{
			color = Showcase.NiceColor();

			if (mesh)
			{
				if (!mesh1)
				{
					mesh1 = DebugDraw.Mesh(mesh, null, -1);
					mesh2 = DebugDraw.Mesh(mesh, color, -1);
				}
				
				Showcase.NiceColors(mesh1.colors);
				mesh2.color = color;
			}
			else if (mesh1)
			{
				mesh1.Remove();
				mesh2.Remove();
				mesh1 = null;
				mesh2 = null;
			}
		}

		private void Update()
		{
			if (mesh1)
			{
				Vector3 p = tr.position;
				Vector3 r = tr.right * spacing;
				Quaternion rot = tr.rotation;
				mesh1.SetGlobalTransform(Matrix4x4.TRS(p - r, rot, DebugDraw.scaleIdentity));
				mesh2.SetGlobalTransform(Matrix4x4.TRS(p + r, rot, DebugDraw.scaleIdentity));
			}
		}

	}

}