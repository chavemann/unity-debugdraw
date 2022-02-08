using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugDrawItems;
using UnityEngine;

public static partial class DebugDraw
{

	/*
	 * These methods are generated automatically from the Item Get methods.
	 */
	/* <StaticGenMethods> */
	
	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="startSize">The size of the arrow head at the start of the line.</param>
	/// <param name="endSize">The size of the arrow head at the end of the line.</param>
	/// <param name="startShape">The shape of the head at the start of the line.</param>
	/// <param name="endShape">The shape of the head at the end of the line.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, ArrowShape startShape = ArrowShape.Arrow, ArrowShape endShape = ArrowShape.Arrow, bool faceCamera = false, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, startShape, endShape, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="startSize">The size of the arrow head at the start of the line.</param>
	/// <param name="endSize">The size of the arrow head at the end of the line.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, bool faceCamera = false, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="size">The size of the arrow head.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float size, bool faceCamera = false, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, size, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws lines along the x, y, and z axes.
	/// </summary>
	/// <param name="position">The axes origin.</param>
	/// <param name="rotation">The orientation of the axes.</param>
	/// <param name="size">The size of each axis. Set to zero to not draw an axis.</param>
	/// <param name="doubleSided">If true the axis line extends in both directions, other only in the positive.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Axes object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Axes Axes(Vector3 position, Quaternion rotation, Vector3 size, bool doubleSided = false, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Axes.Get(ref position, ref rotation, ref size, doubleSided, duration));
	}
	
	/// <summary>
	/// Draws a 3D dot that automatically faces the camera.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dot Dot(Vector3 position, float radius, Color color, int segments = 0, float duration = 0)
	{
		return triangleMeshInstance.Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a 3D dot.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="facing">The forward direction of the dot. Automatically update if faceCamera is true.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 = square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dot Dot(Vector3 position, float radius, Color color, Vector3 facing, int segments = 0, float duration = 0)
	{
		return triangleMeshInstance.Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, ref facing, segments, duration));
	}
	
	/// <summary>
	/// Batch draws 3D dots that automatically faces the camera.
	/// </summary>
	/// <param name="positions">The positions of each dot.</param>
	/// <param name="sizes">The sizes each dot.</param>
	/// <param name="colors">The colors each dot.</param>
	/// <param name="segments">The shape/resolution of the dots. 0 or 4 = square, >= 3 = circle.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dots Dots(List<Vector3> positions, List<float> sizes, List<Color> colors, int segments = 0, float duration = 0)
	{
		return triangleMeshInstance.Add(DebugDrawItems.Dots.Get(positions, sizes, colors, segments, duration));
	}
	
	/// <summary>
	/// Batch draws 3D dots.
	/// </summary>
	/// <param name="positions">The positions of each dot.</param>
	/// <param name="sizes">The sizes each dot.</param>
	/// <param name="facing">The forward direction of the dot. Automatically update if faceCamera is true.</param>
	/// <param name="colors">The colors each dot.</param>
	/// <param name="segments">The shape/resolution of the dots. 0 = square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dots object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dots Dots(List<Vector3> positions, List<float> sizes, List<Color> colors, Vector3 facing, int segments = 0, float duration = 0)
	{
		return triangleMeshInstance.Add(DebugDrawItems.Dots.Get(positions, sizes, colors, ref facing, segments, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/// <summary>
	/// Batch draw multiple lines from a positions and colors array.
	/// Both arrays must be non-null, of the same size, and multiples of two - two entries for each line.
	/// </summary>
	/// <param name="positions">The positions of the start and end points of each line.</param>
	/// <param name="colors">The colors of the start and end points of each line.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Lines Lines(List<Vector3> positions, List<Color> colors, float duration = 0)
	{
		return lineMeshInstance.Add(DebugDrawItems.Lines.Get(positions, colors, duration));
	}
	
	/// <summary>
	/// Draws a point that has no size.
	/// </summary>
	/// <param name="position">The position of the point.</param>
	/// <param name="color">The point's.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Point Point(Vector3 position, Color color, float duration = 0)
	{
		return pointMeshInstance.Add(DebugDrawItems.Point.Get(ref position, ref color, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text.</param>
	/// <param name="text">The text to display.</param>
	/// <param name="color">The text color.</param>
	/// <param name="align">Where to anchor the text.</param>
	/// <param name="scale">The text scale. Set to 1 for default.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Text Text(Vector3 position, string text, Color color, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return textMeshInstance.Add(DebugDrawItems.Text.Get(ref position, text, ref color, align, scale, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text.</param>
	/// <param name="text">The text to display.</param>
	/// <param name="align">Where to anchor the text.</param>
	/// <param name="scale">The text scale. Set to 1 for default.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Text Text(Vector3 position, string text, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return textMeshInstance.Add(DebugDrawItems.Text.Get(ref position, text, ref DebugDraw.colorIdentity, align, scale, duration));
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
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="startSize">The size of the arrow head at the start of the line.</param>
	/// <param name="endSize">The size of the arrow head at the end of the line.</param>
	/// <param name="startShape">The shape of the head at the start of the line.</param>
	/// <param name="endShape">The shape of the head at the end of the line.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, ArrowShape startShape = ArrowShape.Arrow, ArrowShape endShape = ArrowShape.Arrow, bool faceCamera = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, startShape, endShape, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="startSize">The size of the arrow head at the start of the line.</param>
	/// <param name="endSize">The size of the arrow head at the end of the line.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, bool faceCamera = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="size">The size of the arrow head.</param>
	/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float size, bool faceCamera = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, size, faceCamera, duration));
	}
	
	/// <summary>
	/// Draws lines along the x, y, and z axes.
	/// </summary>
	/// <param name="position">The axes origin.</param>
	/// <param name="rotation">The orientation of the axes.</param>
	/// <param name="size">The size of each axis. Set to zero to not draw an axis.</param>
	/// <param name="doubleSided">If true the axis line extends in both directions, other only in the positive.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Axes object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Axes Axes(Vector3 position, Quaternion rotation, Vector3 size, bool doubleSided = false, float duration = 0)
	{
		return Add(DebugDrawItems.Axes.Get(ref position, ref rotation, ref size, doubleSided, duration));
	}
	
	/// <summary>
	/// Draws a 3D dot that automatically faces the camera.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Dot Dot(Vector3 position, float radius, Color color, int segments = 0, float duration = 0)
	{
		return Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a 3D dot.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="facing">The forward direction of the dot. Automatically update if faceCamera is true.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 = square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Dot Dot(Vector3 position, float radius, Color color, Vector3 facing, int segments = 0, float duration = 0)
	{
		return Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, ref facing, segments, duration));
	}
	
	/// <summary>
	/// Batch draws 3D dots that automatically faces the camera.
	/// </summary>
	/// <param name="positions">The positions of each dot.</param>
	/// <param name="sizes">The sizes each dot.</param>
	/// <param name="colors">The colors each dot.</param>
	/// <param name="segments">The shape/resolution of the dots. 0 or 4 = square, >= 3 = circle.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Dots Dots(List<Vector3> positions, List<float> sizes, List<Color> colors, int segments = 0, float duration = 0)
	{
		return Add(DebugDrawItems.Dots.Get(positions, sizes, colors, segments, duration));
	}
	
	/// <summary>
	/// Batch draws 3D dots.
	/// </summary>
	/// <param name="positions">The positions of each dot.</param>
	/// <param name="sizes">The sizes each dot.</param>
	/// <param name="facing">The forward direction of the dot. Automatically update if faceCamera is true.</param>
	/// <param name="colors">The colors each dot.</param>
	/// <param name="segments">The shape/resolution of the dots. 0 = square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dots object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Dots Dots(List<Vector3> positions, List<float> sizes, List<Color> colors, Vector3 facing, int segments = 0, float duration = 0)
	{
		return Add(DebugDrawItems.Dots.Get(positions, sizes, colors, ref facing, segments, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float duration = 0)
	{
		return Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line Line(Vector3 p1, Vector3 p2, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color, ref color, duration));
	}
	
	/// <summary>
	/// Batch draw multiple lines from a positions and colors array.
	/// Both arrays must be non-null, of the same size, and multiples of two - two entries for each line.
	/// </summary>
	/// <param name="positions">The positions of the start and end points of each line.</param>
	/// <param name="colors">The colors of the start and end points of each line.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Lines Lines(List<Vector3> positions, List<Color> colors, float duration = 0)
	{
		return Add(DebugDrawItems.Lines.Get(positions, colors, duration));
	}
	
	/// <summary>
	/// Draws a point that has no size.
	/// </summary>
	/// <param name="position">The position of the point.</param>
	/// <param name="color">The point's.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Point Point(Vector3 position, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Point.Get(ref position, ref color, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text.</param>
	/// <param name="text">The text to display.</param>
	/// <param name="color">The text color.</param>
	/// <param name="align">Where to anchor the text.</param>
	/// <param name="scale">The text scale. Set to 1 for default.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Text Text(Vector3 position, string text, Color color, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return Add(DebugDrawItems.Text.Get(ref position, text, ref color, align, scale, duration));
	}
	
	/// <summary>
	/// Draws a line.
	/// </summary>
	/// <param name="position">The world space position of the text.</param>
	/// <param name="text">The text to display.</param>
	/// <param name="align">Where to anchor the text.</param>
	/// <param name="scale">The text scale. Set to 1 for default.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Text object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Text Text(Vector3 position, string text, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
	{
		return Add(DebugDrawItems.Text.Get(ref position, text, ref DebugDraw.colorIdentity, align, scale, duration));
	}
	
	/* </InstanceGenMethods> */
	
}