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
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
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
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return lineMeshInstance.Add(Items.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text</param>
	/// <param name="text">The text to display</param>
	/// <param name="color">The text color</param>
	/// <param name="align">Where to anchor the text</param>
	/// <param name="scale">The text scale. Set to 1 for default</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Text Text(Vector3 position, string text, Color color, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return textMeshInstance.Add(Items.Text.Get(ref position, text, ref color, align, scale, duration));
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
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
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
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return Add(Items.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text</param>
	/// <param name="text">The text to display</param>
	/// <param name="color">The text color</param>
	/// <param name="align">Where to anchor the text</param>
	/// <param name="scale">The text scale. Set to 1 for default</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Text Text(Vector3 position, string text, Color color, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return Add(Items.Text.Get(ref position, text, ref color, align, scale, duration));
	}
	
	/* </InstanceGenMethods> */
	
}