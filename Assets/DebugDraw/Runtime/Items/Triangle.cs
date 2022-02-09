using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Triangle : BasePointItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The first point of the triangle.
		/// </summary>
		public Vector3 p1;
		/// <summary>
		/// The second point of the triangle.
		/// </summary>
		public Vector3 p2;
		/// <summary>
		/// The third point of the triangle.
		/// </summary>
		public Vector3 p3;
		/// <summary>
		/// True for a filled triangle, otherwise a wire triangle.
		/// It's important that this Triangle item is added to a mesh with the right topology, either lines or triangles,
		/// based on this setting.
		/// </summary>
		public bool filled;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a filled triangle.
		/// </summary>
		/// <param name="p1">The first point of the triangle.</param>
		/// <param name="p2">The second point of the triangle.</param>
		/// <param name="p3">The third point of the triangle.</param>
		/// <param name="color">The colour of the triangle.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Triangle Get(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Color color, float duration = 0)
		{
			Triangle item = ItemPool<Triangle>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color;
			item.filled = true;

			return item;
		}
		
		/// <summary>
		/// Draws a wire triangle.
		/// </summary>
		/// <param name="p1">The first point of the triangle.</param>
		/// <param name="p2">The second point of the triangle.</param>
		/// <param name="p3">The third point of the triangle.</param>
		/// <param name="color">The colour of the triangle.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Triangle GetWire(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Color color, float duration = 0)
		{
			Triangle item = ItemPool<Triangle>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color;
			item.filled = false;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddVertices(this, ref p1, ref p2, ref p3);
			mesh.AddColorX3(this, ref color);

			if (filled)
			{
				mesh.AddIndexX3();
			}
			else
			{
				mesh.AddIndices(
					// Line 1
					mesh.vertexIndex++,
					mesh.vertexIndex,
					// Line 2
					mesh.vertexIndex++,
					mesh.vertexIndex,
					// Line 3
					mesh.vertexIndex++,
					mesh.vertexIndex - 3);
			}
		}

		internal override void Release()
		{
			ItemPool<Triangle>.Release(this);
		}

	}

}