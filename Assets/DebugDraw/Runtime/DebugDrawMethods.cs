using System.Runtime.CompilerServices;
using Items;
using UnityEngine;

public static partial class DebugDraw
{

	/*
	 * These methods are generated automatically from the Item Get methods.
	 */
	/* <StaticGenMethods> */
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line</param>
	/// <param name="p2">The end of the line</param>
	/// <param name="color1">The line's colour at the start</param>
	/// <param name="color2">The line's colour at the end</param>
	/// <param name="duration">How long the line will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float duration = 0)
	{
		return lineMeshInstance.Add(Items.Line.Get(ref p1, ref p2, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line</param>
	/// <param name="p2">The end of the line</param>
	/// <param name="color">The line's colour</param>
	/// <param name="duration">How long the line will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return lineMeshInstance.Add(Items.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/* </StaticGenMethods> */

}

public partial class DebugDrawMesh
{
	
	/*
	 * These methods are generated automatically from the Item Get methods.
	 */
	/* <InstanceGenMethods> */
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line</param>
	/// <param name="p2">The end of the line</param>
	/// <param name="color1">The line's colour at the start</param>
	/// <param name="color2">The line's colour at the end</param>
	/// <param name="duration">How long the line will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float duration = 0)
	{
		return Add(Items.Line.Get(ref p1, ref p2, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line</param>
	/// <param name="p2">The end of the line</param>
	/// <param name="color">The line's colour</param>
	/// <param name="duration">How long the line will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return Add(Items.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/* </InstanceGenMethods> */
	
}