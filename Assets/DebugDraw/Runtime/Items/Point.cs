using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

/// <summary>
/// A point that has no size.
/// </summary>
public class Point : BasePointItem
	{
		
		/* mesh: point */
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a point that has no size.
		/// </summary>
		/// <param name="position">The position of the point.</param>
		/// <param name="color">The point's.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Point Get(ref Vector3 position, ref Color color, EndTime? duration = null)
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
