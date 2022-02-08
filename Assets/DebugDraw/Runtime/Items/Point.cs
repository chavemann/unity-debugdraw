using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// A point that has no size.
	/// </summary>
	public class Point : BaseItem
	{
		/* mesh: point */

		/// <summary>
		/// The position of the point.
		/// </summary>
		public Vector3 position;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a point that has no size.
		/// </summary>
		/// <param name="position">The position of the point.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color">The point's.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point Get(ref Vector3 position, ref Color color, float duration = 0)
		{
			Point item = ItemPool<Point>.Get(duration);
			
			item.position = position;
			item.color = color;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddColor(this, ref color);
			mesh.AddVertex(this, ref position);
			mesh.AddIndex();
		}

		internal override void Release()
		{
			ItemPool<Point>.Release(this);
		}

	}

}