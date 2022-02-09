using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Triangle : BasePointItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The second point of the triangle.
		/// </summary>
		public Vector3 p2;
		/// <summary>
		/// The third point of the triangle.
		/// </summary>
		public Vector3 p3;
		/// <summary>
		/// The color of the triangle's second point.
		/// </summary>
		public Color color2;
		/// <summary>
		/// The color of the triangle's third point.
		/// </summary>
		public Color color3;
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
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color;
			item.color2 = color;
			item.color3 = color;
			item.filled = true;

			return item;
		}
		
		/// <summary>
		/// Draws a filled triangle.
		/// </summary>
		/// <param name="p1">The first point of the triangle.</param>
		/// <param name="p2">The second point of the triangle.</param>
		/// <param name="p3">The third point of the triangle.</param>
		/// <param name="color1">The color of the triangle's first point..</param>
		/// <param name="color2">The color of the triangle's second point.</param>
		/// <param name="color3">The color of the triangle's third point.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Triangle Get(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Color color1, ref Color color2, ref Color color3, float duration = 0)
		{
			Triangle item = ItemPool<Triangle>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color1;
			item.color2 = color2;
			item.color3 = color3;
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
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color;
			item.color2 = color;
			item.color3 = color;
			item.filled = false;

			return item;
		}
		
		/// <summary>
		/// Draws a wire triangle.
		/// </summary>
		/// <param name="p1">The first point of the triangle.</param>
		/// <param name="p2">The second point of the triangle.</param>
		/// <param name="p3">The third point of the triangle.</param>
		/// <param name="color1">The color of the triangle's first point..</param>
		/// <param name="color2">The color of the triangle's second point.</param>
		/// <param name="color3">The color of the triangle's third point.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Triangle GetWire(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Color color1, ref Color color2, ref Color color3, float duration = 0)
		{
			Triangle item = ItemPool<Triangle>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.color = color1;
			item.color2 = color2;
			item.color3 = color3;
			item.filled = true;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddVertices(this, ref position, ref p2, ref p3);
			mesh.AddColors(this, ref color, ref color2, ref color3);

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