using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Batch draw multiple lines.
	/// Both the positions and colors list must be non null and the same size.
	/// Each line must have two entries, one for the the start point and one for the end.
	/// </summary>
	public class Lines : BaseItem
	{
		/* mesh: line */

		/// <summary>
		/// The positions of the start and end points of each line.
		/// </summary>
		public List<Vector3> positions;
		/// <summary>
		/// The colors of the start and end points of each line.
		/// </summary>
		public List<Color> colors;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Batch draw multiple lines from a positions and colors array.
		/// Both arrays must be non-null, of the same size, and multiples of two - two entries for each line.
		/// </summary>
		/// <param name="positions">The positions of the start and end points of each line.</param>
		/// <param name="colors">The colors of the start and end points of each line.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Lines Get(List<Vector3> positions, List<Color> colors, EndTime duration = default)
		{
			Lines item = ItemPool<Lines>.Get(duration);

			item.positions = positions;
			item.colors = colors;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddLines(this, positions, colors);
		}

		internal override void Release()
		{
			positions = null;
			colors = null;
		}

	}

}
