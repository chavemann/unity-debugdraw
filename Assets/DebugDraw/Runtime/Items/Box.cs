using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Box : BasePointItem
	{
		/* mesh: line */

		/// <summary>
		/// The half size of the box.
		/// </summary>
		public Vector3 size;
		/// <summary>
		/// The orientation of the box.
		/// </summary>
		public Quaternion orientation;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, ref Vector3 size, ref Color color, float duration = 0)
		{
			Box item = ItemPool<Box>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.color = color;
			item.orientation = DebugDraw.rotationIdentity;

			return item;
		}
		
		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="orientation">The orientation of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, ref Vector3 size, ref Quaternion orientation, ref Color color, float duration = 0)
		{
			Box item = ItemPool<Box>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.color = color;
			item.orientation = orientation;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			Matrix4x4 m = Matrix4x4.TRS(position, orientation, DebugDraw.scaleIdentity);
			
			// Top vertices
			Vector3 v1 = m.MultiplyPoint3x4(new Vector3(-size.x, -size.y, -size.z));
			Vector3 v2 = m.MultiplyPoint3x4(new Vector3(+size.x, -size.y, -size.z));
			Vector3 v3 = m.MultiplyPoint3x4(new Vector3(+size.x, -size.y, +size.z));
			Vector3 v4 = m.MultiplyPoint3x4(new Vector3(-size.x, -size.y, +size.z));
			mesh.AddVertices(this, ref v1, ref v2, ref v3, ref v4);
			// Bottom vertices
			v1 = m.MultiplyPoint3x4(new Vector3(-size.x, +size.y, -size.z));
			v2 = m.MultiplyPoint3x4(new Vector3(+size.x, +size.y, -size.z));
			v3 = m.MultiplyPoint3x4(new Vector3(+size.x, +size.y, +size.z));
			v4 = m.MultiplyPoint3x4(new Vector3(-size.x, +size.y, +size.z));
			mesh.AddVertices(this, ref v1, ref v2, ref v3, ref v4);
			
			Color clr = GetColor(ref color);
			mesh.AddColorX4(ref clr);
			mesh.AddColorX4(ref clr);

			int i = mesh.vertexIndex;
			
			// Top edges
			mesh.AddQuadLineIndices();
			// Bottom edges
			mesh.AddQuadLineIndices();
			// Sides
			mesh.AddIndices(
				i + 0, i + 4,
				i + 1, i + 5,
				i + 2, i + 6,
				i + 3, i + 7);
		}

		internal override void Release()
		{
			ItemPool<Box>.Release(this);
		}

	}

}