using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Multiple points that have no size.
	/// </summary>
	public class Points : BaseItem
	{
		
		/* mesh: point */
		
		/// <summary>
		/// The positions of the points.
		/// </summary>
		public List<Vector3> positions;
		
		/// <summary>
		/// The colors of the points.
		/// </summary>
		public List<Color> colors;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Batch draw multiple points from a positions and colors array.
		/// Both arrays must be non-null and of the same size.
		/// </summary>
		/// <param name="positions">The positions of the start and end points of each line.</param>
		/// <param name="colors">The colors of the start and end points of each line.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Points Get(List<Vector3> positions, List<Color> colors, EndTime? duration = null)
		{
			Points item = ItemPool<Points>.Get(duration);
			
			item.positions = positions;
			item.colors = colors;
			
			return item;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddPoints(this, positions, colors);
		}
		
		internal override void Release()
		{
			ItemPool<Points>.Release(this);
		}
		
	}

}
