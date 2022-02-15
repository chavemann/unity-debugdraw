#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugDrawItems;
using UnityEngine;

public static partial class DebugDraw
{
	
	public static Color rayHitColor = Color.green;
	public static Color rayMissColor = Color.red;
	public static Color rayNormalColor = Color.cyan;
	
	/// <summary>
	/// Draws a ray.
	/// </summary>
	/// <param name="origin">The ray's starting position.</param>
	/// <param name="direction">The ray's direction.</param>
	/// <param name="maxDistance">The ray's max distance.</param>
	/// <param name="hitResult">The hit result.</param>
	/// <param name="normalSize">If larger than zero draws the hit normal</param>
	/// <param name="arrowSize">The size of the arrow head. Set to zero to just draw a line.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Ray(Vector3 origin, Vector3 direction, float maxDistance, RaycastHit hitResult, float normalSize = 0, float arrowSize = 0, float duration = 0)
	{
		#if DEBUG_DRAW
		bool hit = hitResult.collider;
		Vector3 p2 = hit ? hitResult.point : new Vector3(
			origin.x + direction.x * maxDistance,
			origin.y + direction.y * maxDistance,
			origin.z + direction.z * maxDistance);
		Color clr = hit ? rayHitColor : rayMissColor;
		
		lineMeshInstance.Add(DebugDrawItems.Arrow.Get(
			ref origin, ref p2, ref clr, ref clr, arrowSize, arrowSize,
			ArrowShape.None, arrowSize > 0 ? ArrowShape.Arrow : ArrowShape.None,
			true, false, duration));

		if (hit && normalSize > 0)
		{
			Vector3 p1 = hitResult.point;
			Vector3 normal = hitResult.normal;
			p2 = new Vector3(
				p1.x + normal.x * normalSize,
				p1.y + normal.y * normalSize,
				p1.z + normal.z * normalSize);
			lineMeshInstance.Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref rayNormalColor, duration));
		}
		#endif
	}

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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, ArrowShape startShape = ArrowShape.Arrow, ArrowShape endShape = ArrowShape.Arrow, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, startShape, endShape, faceCamera, autoSize, duration));
		#else
		return DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, startShape, endShape, faceCamera, autoSize, duration);
		#endif
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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, faceCamera, autoSize, duration));
		#else
		return DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, faceCamera, autoSize, duration);
		#endif
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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float size, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, size, faceCamera, autoSize, duration));
		#else
		return DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, size, faceCamera, autoSize, duration);
		#endif
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
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Axes.Get(ref position, ref rotation, ref size, doubleSided, duration));
		#else
		return DebugDrawItems.Axes.Get(ref position, ref rotation, ref size, doubleSided, duration);
		#endif
	}
	
	/// <summary>
	/// Draws an axis aligned box.
	/// </summary>
	/// <param name="position">The centre of the box.</param>
	/// <param name="size">The half size of the box.</param>
	/// <param name="color">The color of the box.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Box object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Box Box(Vector3 position, Vector3 size, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Box.Get(ref position, ref size, ref color, duration));
		#else
		return DebugDrawItems.Box.Get(ref position, ref size, ref color, duration);
		#endif
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
	public static Box Box(Vector3 position, Vector3 size, Quaternion orientation, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Box.Get(ref position, ref size, ref orientation, ref color, duration));
		#else
		return DebugDrawItems.Box.Get(ref position, ref size, ref orientation, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a capsule shell.
	/// </summary>
	/// <param name="p1">The center of the capsule at the start of the capsule.</param>
	/// <param name="p2">The center of the capsule at the end of the capsule.</param>
	/// <param name="radius">The radius of the capsule.</param>
	/// <param name="color">The color of the capsule.</param>
	/// <param name="segments">The resolution of the capsule. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Capsule object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Capsule Capsule(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Capsule.Get(ref p1, ref p2, radius, ref color, segments, duration));
		#else
		return DebugDrawItems.Capsule.Get(ref p1, ref p2, radius, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe capsule.
	/// </summary>
	/// <param name="p1">The center of the capsule at the start of the capsule.</param>
	/// <param name="p2">The center of the capsule at the end of the capsule.</param>
	/// <param name="radius">The radius of the capsule.</param>
	/// <param name="color">The color of the capsule.</param>
	/// <param name="segments">The resolution of the capsule. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Capsule object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Capsule WireCapsule(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Capsule.GetWire(ref p1, ref p2, radius, ref color, segments, duration));
		#else
		return DebugDrawItems.Capsule.GetWire(ref p1, ref p2, radius, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a cone shell.
	/// </summary>
	/// <param name="origin">The origin point of the cone.</param>
	/// <param name="direction">The direction the cone.</param>
	/// <param name="length">The length of the cone.</param>
	/// <param name="angle">The angle of the cone.</param>
	/// <param name="color">The color of the cone.</param>
	/// <param name="segments">The resolution of the cone. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="round">If true, the length is treated as the radius of the cone.
	/// If false the length is the distance from the origin to the centre of the cones cap.</param>
	/// <param name="drawCap">Should a cap be drawn on the cone.
	/// When <see cref="round"/> is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cone Cone(Vector3 origin, Vector3 direction, float length, float angle, Color color, int segments = 32, bool round = false, bool drawCap = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cone.Get(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration));
		#else
		return DebugDrawItems.Cone.Get(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe cone.
	/// </summary>
	/// <param name="origin">The origin point of the cone.</param>
	/// <param name="direction">The direction the cone.</param>
	/// <param name="length">The length of the cone.</param>
	/// <param name="angle">The angle of the cone.</param>
	/// <param name="color">The color of the cone.</param>
	/// <param name="segments">The resolution of the cone. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="round">If true, the length is treated as the radius of the cone.
	/// If false the length is the distance from the origin to the centre of the cones cap.</param>
	/// <param name="drawCap">Should a cap be drawn on the cone.
	/// When <see cref="round"/> is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cone WireCone(Vector3 origin, Vector3 direction, float length, float angle, Color color, int segments = 32, bool round = false, bool drawCap = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cone.GetWire(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration));
		#else
		return DebugDrawItems.Cone.GetWire(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a cylinder shell.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder Cylinder(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius, radius, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius, radius, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder Cylinder(Vector3 p1, Vector3 p2, float radius1, float radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder Cylinder(Vector3 p1, Vector3 p2, Vector2 radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius, ref radius, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius, ref radius, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder Cylinder(Vector3 p1, Vector3 p2, Vector2 radius1, Vector2 radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius1, ref radius2, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius1, ref radius2, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder WireCylinder(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder WireCylinder(Vector3 p1, Vector3 p2, float radius1, float radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder WireCylinder(Vector3 p1, Vector3 p2, Vector2 radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Cylinder WireCylinder(Vector3 p1, Vector3 p2, Vector2 radius1, Vector2 radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
		#else
		return DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D dot that automatically faces the camera.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.
	/// If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dot Dot(Vector3 position, float radius, Color color, int segments = 0, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, segments, duration));
		#else
		return DebugDrawItems.Dot.Get(ref position, radius, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D dot.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="facing">The forward direction of the dot. Automatically update if faceCamera is true.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 = square.
	/// If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Dot object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Dot Dot(Vector3 position, float radius, Color color, Vector3 facing, int segments = 0, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Dot.Get(ref position, radius, ref color, ref facing, segments, duration));
		#else
		return DebugDrawItems.Dot.Get(ref position, radius, ref color, ref facing, segments, duration);
		#endif
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
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Dots.Get(positions, sizes, colors, segments, duration));
		#else
		return DebugDrawItems.Dots.Get(positions, sizes, colors, segments, duration);
		#endif
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
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Dots.Get(positions, sizes, colors, ref facing, segments, duration));
		#else
		return DebugDrawItems.Dots.Get(positions, sizes, colors, ref facing, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse Ellipse(Vector3 position, float radius, Vector3 facing, Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Ellipse.Get(ref position, radius, ref facing, ref color, segments, drawAxes, duration));
		#else
		return DebugDrawItems.Ellipse.Get(ref position, radius, ref facing, ref color, segments, drawAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse FillEllipse(Vector3 position, float radius, Vector3 facing, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Ellipse.GetFill(ref position, radius, ref facing, ref color, segments, duration));
		#else
		return DebugDrawItems.Ellipse.GetFill(ref position, radius, ref facing, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse Arc(Vector3 position, float radius, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Ellipse.GetArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration));
		#else
		return DebugDrawItems.Ellipse.GetArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse FillArc(Vector3 position, float radius, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Ellipse.GetFillArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, duration));
		#else
		return DebugDrawItems.Ellipse.GetFillArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse Ellipse(Vector3 position, Vector2 size, Vector3 facing, Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Ellipse.Get(ref position, ref size, ref facing, ref color, segments, drawAxes, duration));
		#else
		return DebugDrawItems.Ellipse.Get(ref position, ref size, ref facing, ref color, segments, drawAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse FillEllipse(Vector3 position, Vector2 size, Vector3 facing, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Ellipse.GetFill(ref position, ref size, ref facing, ref color, segments, duration));
		#else
		return DebugDrawItems.Ellipse.GetFill(ref position, ref size, ref facing, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse Arc(Vector3 position, Vector2 size, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Ellipse.GetArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration));
		#else
		return DebugDrawItems.Ellipse.GetArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ellipse FillArc(Vector3 position, Vector2 size, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Ellipse.GetFillArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, duration));
		#else
		return DebugDrawItems.Ellipse.GetFillArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, duration);
		#endif
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
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color1, ref color2, duration));
		#else
		return DebugDrawItems.Line.Get(ref p1, ref p2, ref color1, ref color2, duration);
		#endif
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
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Line.Get(ref p1, ref p2, ref color, ref color, duration));
		#else
		return DebugDrawItems.Line.Get(ref p1, ref p2, ref color, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line3D Line3D(Vector3 p1, Vector3 p2, float size, Color color1, Color color2, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color1, ref color2, duration));
		#else
		return DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color1, ref color2, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line3D object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line3D Line3D(Vector3 p1, Vector3 p2, float size, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color, ref color, duration));
		#else
		return DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="facing">The forward direction of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line3D Line3D(Vector3 p1, Vector3 p2, float size, Vector3 facing, Color color1, Color color2, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color1, ref color2, duration));
		#else
		return DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color1, ref color2, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="facing">The forward direction of the line.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line3D object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Line3D Line3D(Vector3 p1, Vector3 p2, float size, Vector3 facing, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color, ref color, duration));
		#else
		return DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color, ref color, duration);
		#endif
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
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Lines.Get(positions, colors, duration));
		#else
		return DebugDrawItems.Lines.Get(positions, colors, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wireframe mesh.
	/// </summary>
	/// <param name="vertices">The list of vertices.</param>
	/// <param name="colors">The list of colors.</param>
	/// <param name="indices">The list of triangle indices.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MeshItem Mesh(List<Vector3> vertices, List<Color> colors, List<int> indices, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.MeshItem.Get(vertices, colors, indices, duration));
		#else
		return DebugDrawItems.MeshItem.Get(vertices, colors, indices, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wireframe mesh with a single color.
	/// </summary>
	/// <param name="vertices">The list of vertices.</param>
	/// <param name="indices">The list of triangle indices.</param>
	/// <param name="color">The color of the mesh.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MeshItem Mesh(List<Vector3> vertices, List<int> indices, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.MeshItem.Get(vertices, indices, ref color, duration));
		#else
		return DebugDrawItems.MeshItem.Get(vertices, indices, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wireframe mesh. This will allocate new lists and fetch the mesh data so it's advisable to
	/// not call this every frame and instead create it once keep a reference to it.
	/// </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="color">The color of the mesh. If null the mesh must have color data associated with it</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MeshItem Mesh(Mesh mesh, Color? color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.MeshItem.Get(mesh, color, duration));
		#else
		return DebugDrawItems.MeshItem.Get(mesh, color, duration);
		#endif
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
		#if DEBUG_DRAW
		return pointMeshInstance.Add(DebugDrawItems.Point.Get(ref position, ref color, duration));
		#else
		return DebugDrawItems.Point.Get(ref position, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The third point of the quad.</param>
	/// <param name="color">The colour of the quad.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quad Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color, duration));
		#else
		return DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color1">The color of the quad's first point..</param>
	/// <param name="color2">The color of the quad's second point.</param>
	/// <param name="color3">The color of the quad's third point.</param>
	/// <param name="color4">The color of the quad's fourth point.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quad Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color1, Color color2, Color color3, Color color4, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration));
		#else
		return DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color">The colour of the quad.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quad FillQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color, duration));
		#else
		return DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color1">The color of the quad's first point..</param>
	/// <param name="color2">The color of the quad's second point.</param>
	/// <param name="color3">The color of the quad's third point.</param>
	/// <param name="color4">The color of the quad's fourth point.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quad FillQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color1, Color color2, Color color3, Color color4, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration));
		#else
		return DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a sphere comprised of a circle for each axis.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sphere Sphere(Vector3 position, Vector3 radius, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Sphere.Get(ref position, ref radius, ref color, segments, duration));
		#else
		return DebugDrawItems.Sphere.Get(ref position, ref radius, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a sphere comprised of a circle for each axis.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="orientation">The orientation of the sphere.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sphere Sphere(Vector3 position, Vector3 radius, Quaternion orientation, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Sphere.Get(ref position, ref radius, ref orientation, ref color, segments, duration));
		#else
		return DebugDrawItems.Sphere.Get(ref position, ref radius, ref orientation, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe sphere.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sphere WireSphere(Vector3 position, Vector3 radius, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref color, segments, duration));
		#else
		return DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a full wireframe sphere.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="orientation">The orientation of the sphere.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Sphere WireSphere(Vector3 position, Vector3 radius, Quaternion orientation, Color color, int segments = 32, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref orientation, ref color, segments, duration));
		#else
		return DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref orientation, ref color, segments, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a filled square.
	/// </summary>
	/// <param name="position">The centre of the square.</param>
	/// <param name="size">The half size of the square.</param>
	/// <param name="facing">The normal or direction the front of the square is facing.</param>
	/// <param name="color">The colour of the square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Square Square(Vector3 position, Vector2 size, Vector3 facing, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Square.Get(ref position, ref size, ref facing, ref color, duration));
		#else
		return DebugDrawItems.Square.Get(ref position, ref size, ref facing, ref color, duration);
		#endif
	}
	
	/// <summary>
	/// Draws a wire square.
	/// </summary>
	/// <param name="position">The centre of the square.</param>
	/// <param name="size">The half size of the square.</param>
	/// <param name="facing">The normal or direction the front of the square is facing.</param>
	/// <param name="color">The colour of the square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Square WireSquare(Vector3 position, Vector2 size, Vector3 facing, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Square.GetWire(ref position, ref size, ref facing, ref color, duration));
		#else
		return DebugDrawItems.Square.GetWire(ref position, ref size, ref facing, ref color, duration);
		#endif
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
		#if DEBUG_DRAW
		return textMeshInstance.Add(DebugDrawItems.Text.Get(ref position, text, ref color, align, scale, duration));
		#else
		return DebugDrawItems.Text.Get(ref position, text, ref color, align, scale, duration);
		#endif
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
		#if DEBUG_DRAW
		return textMeshInstance.Add(DebugDrawItems.Text.Get(ref position, text, ref DebugDraw.colorIdentity, align, scale, duration));
		#else
		return DebugDrawItems.Text.Get(ref position, text, ref DebugDraw.colorIdentity, align, scale, duration);
		#endif
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
	public static Triangle Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color, duration));
		#else
		return DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color, duration);
		#endif
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
	public static Triangle Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color1, Color color2, Color color3, float duration = 0)
	{
		#if DEBUG_DRAW
		return lineMeshInstance.Add(DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration));
		#else
		return DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration);
		#endif
	}
	
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
	public static Triangle FillTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color, duration));
		#else
		return DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color, duration);
		#endif
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
	public static Triangle FillTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color1, Color color2, Color color3, float duration = 0)
	{
		#if DEBUG_DRAW
		return triangleMeshInstance.Add(DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration));
		#else
		return DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration);
		#endif
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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, ArrowShape startShape = ArrowShape.Arrow, ArrowShape endShape = ArrowShape.Arrow, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, startShape, endShape, faceCamera, autoSize, duration));
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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float startSize, float endSize, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, startSize, endSize, faceCamera, autoSize, duration));
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
	/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Arrow object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Arrow Arrow(Vector3 p1, Vector3 p2, Color color1, Color color2, float size, bool faceCamera = false, bool autoSize = false, float duration = 0)
	{
		return Add(DebugDrawItems.Arrow.Get(ref p1, ref p2, ref color1, ref color2, size, faceCamera, autoSize, duration));
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
	/// Draws an axis aligned box.
	/// </summary>
	/// <param name="position">The centre of the box.</param>
	/// <param name="size">The half size of the box.</param>
	/// <param name="color">The color of the box.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Box object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Box Box(Vector3 position, Vector3 size, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Box.Get(ref position, ref size, ref color, duration));
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
	public Box Box(Vector3 position, Vector3 size, Quaternion orientation, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Box.Get(ref position, ref size, ref orientation, ref color, duration));
	}
	
	/// <summary>
	/// Draws a capsule shell.
	/// </summary>
	/// <param name="p1">The center of the capsule at the start of the capsule.</param>
	/// <param name="p2">The center of the capsule at the end of the capsule.</param>
	/// <param name="radius">The radius of the capsule.</param>
	/// <param name="color">The color of the capsule.</param>
	/// <param name="segments">The resolution of the capsule. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Capsule object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Capsule Capsule(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Capsule.Get(ref p1, ref p2, radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe capsule.
	/// </summary>
	/// <param name="p1">The center of the capsule at the start of the capsule.</param>
	/// <param name="p2">The center of the capsule at the end of the capsule.</param>
	/// <param name="radius">The radius of the capsule.</param>
	/// <param name="color">The color of the capsule.</param>
	/// <param name="segments">The resolution of the capsule. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Capsule object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Capsule WireCapsule(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Capsule.GetWire(ref p1, ref p2, radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a cone shell.
	/// </summary>
	/// <param name="origin">The origin point of the cone.</param>
	/// <param name="direction">The direction the cone.</param>
	/// <param name="length">The length of the cone.</param>
	/// <param name="angle">The angle of the cone.</param>
	/// <param name="color">The color of the cone.</param>
	/// <param name="segments">The resolution of the cone. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="round">If true, the length is treated as the radius of the cone.
	/// If false the length is the distance from the origin to the centre of the cones cap.</param>
	/// <param name="drawCap">Should a cap be drawn on the cone.
	/// When <see cref="round"/> is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cone Cone(Vector3 origin, Vector3 direction, float length, float angle, Color color, int segments = 32, bool round = false, bool drawCap = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cone.Get(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe cone.
	/// </summary>
	/// <param name="origin">The origin point of the cone.</param>
	/// <param name="direction">The direction the cone.</param>
	/// <param name="length">The length of the cone.</param>
	/// <param name="angle">The angle of the cone.</param>
	/// <param name="color">The color of the cone.</param>
	/// <param name="segments">The resolution of the cone. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="round">If true, the length is treated as the radius of the cone.
	/// If false the length is the distance from the origin to the centre of the cones cap.</param>
	/// <param name="drawCap">Should a cap be drawn on the cone.
	/// When <see cref="round"/> is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cone WireCone(Vector3 origin, Vector3 direction, float length, float angle, Color color, int segments = 32, bool round = false, bool drawCap = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cone.GetWire(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration));
	}
	
	/// <summary>
	/// Draws a cylinder shell.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder Cylinder(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius, radius, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder Cylinder(Vector3 p1, Vector3 p2, float radius1, float radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder Cylinder(Vector3 p1, Vector3 p2, Vector2 radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius, ref radius, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a cylinder shell with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder Cylinder(Vector3 p1, Vector3 p2, Vector2 radius1, Vector2 radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.Get(ref p1, ref p2, ref radius1, ref radius2, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder WireCylinder(Vector3 p1, Vector3 p2, float radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder WireCylinder(Vector3 p1, Vector3 p2, float radius1, float radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius">The radius of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder WireCylinder(Vector3 p1, Vector3 p2, Vector2 radius, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe cylinder with a different start and end radius.
	/// </summary>
	/// <param name="p1">The start point of the cylinder.</param>
	/// <param name="p2">The end point of the cylinder.</param>
	/// <param name="radius1">The radius at the start point of the cylinder.</param>
	/// <param name="radius2">The radius at the end point of the cylinder.</param>
	/// <param name="color">The color of the cylinder.</param>
	/// <param name="segments">The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="drawEndAxes">Draw axis at each end of the cylinder.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Cylinder object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Cylinder WireCylinder(Vector3 p1, Vector3 p2, Vector2 radius1, Vector2 radius2, Color color, int segments = 32, bool drawEndAxes = false, float duration = 0)
	{
		return Add(DebugDrawItems.Cylinder.GetWire(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration));
	}
	
	/// <summary>
	/// Draws a 3D dot that automatically faces the camera.
	/// </summary>
	/// <param name="position">The position of the dot.</param>
	/// <param name="radius">The size of the dot.</param>
	/// <param name="color">The color of the dot.</param>
	/// <param name="segments">The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.
	/// If set to zero will be adjusted based on the distance to the camera.</param>
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
	/// <param name="segments">The shape/resolution of the dot. 0 = square.
	/// If set to zero will be adjusted based on the distance to the camera.</param>
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
	/// Draws a wire ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse Ellipse(Vector3 position, float radius, Vector3 facing, Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.Get(ref position, radius, ref facing, ref color, segments, drawAxes, duration));
	}
	
	/// <summary>
	/// Draws a filled ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse FillEllipse(Vector3 position, float radius, Vector3 facing, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetFill(ref position, radius, ref facing, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a wire arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse Arc(Vector3 position, float radius, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration));
	}
	
	/// <summary>
	/// Draws a filled arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="radius">The radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse FillArc(Vector3 position, float radius, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetFillArc(ref position, radius, ref facing, startAngle, endAngle, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a wire ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse Ellipse(Vector3 position, Vector2 size, Vector3 facing, Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.Get(ref position, ref size, ref facing, ref color, segments, drawAxes, duration));
	}
	
	/// <summary>
	/// Draws a filled ellipse.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse FillEllipse(Vector3 position, Vector2 size, Vector3 facing, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetFill(ref position, ref size, ref facing, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a wire arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.</param>
	/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse Arc(Vector3 position, Vector2 size, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration));
	}
	
	/// <summary>
	/// Draws a filled arc.
	/// </summary>
	/// <param name="position">The centre of the ellipse.</param>
	/// <param name="size">The size/radius of the ellipse.</param>
	/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
	/// <param name="startAngle">The start angle in degrees of the arc.</param>
	/// <param name="endAngle">The end angle in degrees of the arc.</param>
	/// <param name="color">The colour of the ellipse.</param>
	/// <param name="segments">The resolution of the ellipse.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ellipse FillArc(Vector3 position, Vector2 size, Vector3 facing, float startAngle, float endAngle, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Ellipse.GetFillArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, duration));
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
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line3D Line3D(Vector3 p1, Vector3 p2, float size, Color color1, Color color2, float duration = 0)
	{
		return Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line3D object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line3D Line3D(Vector3 p1, Vector3 p2, float size, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref color, ref color, duration));
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="facing">The forward direction of the line.</param>
	/// <param name="color1">The line's colour at the start.</param>
	/// <param name="color2">The line's colour at the end.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line3D Line3D(Vector3 p1, Vector3 p2, float size, Vector3 facing, Color color1, Color color2, float duration = 0)
	{
		return Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color1, ref color2, duration));
	}
	
	/// <summary>
	/// Draws a 3D line that orients itself towards the camera.
	/// </summary>
	/// <param name="p1">The start of the line.</param>
	/// <param name="p2">The end of the line.</param>
	/// <param name="size">The line thickness.</param>
	/// <param name="facing">The forward direction of the line.</param>
	/// <param name="color">The line's colour.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
	/// <returns>The Line3D object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Line3D Line3D(Vector3 p1, Vector3 p2, float size, Vector3 facing, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Line3D.Get(ref p1, ref p2, size, ref facing, ref color, ref color, duration));
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
	/// Draws a wireframe mesh.
	/// </summary>
	/// <param name="vertices">The list of vertices.</param>
	/// <param name="colors">The list of colors.</param>
	/// <param name="indices">The list of triangle indices.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public MeshItem Mesh(List<Vector3> vertices, List<Color> colors, List<int> indices, float duration = 0)
	{
		return Add(DebugDrawItems.MeshItem.Get(vertices, colors, indices, duration));
	}
	
	/// <summary>
	/// Draws a wireframe mesh with a single color.
	/// </summary>
	/// <param name="vertices">The list of vertices.</param>
	/// <param name="indices">The list of triangle indices.</param>
	/// <param name="color">The color of the mesh.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public MeshItem Mesh(List<Vector3> vertices, List<int> indices, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.MeshItem.Get(vertices, indices, ref color, duration));
	}
	
	/// <summary>
	/// Draws a wireframe mesh. This will allocate new lists and fetch the mesh data so it's advisable to
	/// not call this every frame and instead create it once keep a reference to it.
	/// </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="color">The color of the mesh. If null the mesh must have color data associated with it</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Line object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public MeshItem Mesh(Mesh mesh, Color? color, float duration = 0)
	{
		return Add(DebugDrawItems.MeshItem.Get(mesh, color, duration));
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
	/// Draws a wire quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The third point of the quad.</param>
	/// <param name="color">The colour of the quad.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Quad Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color, duration));
	}
	
	/// <summary>
	/// Draws a wire quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color1">The color of the quad's first point..</param>
	/// <param name="color2">The color of the quad's second point.</param>
	/// <param name="color3">The color of the quad's third point.</param>
	/// <param name="color4">The color of the quad's fourth point.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Quad Quad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color1, Color color2, Color color3, Color color4, float duration = 0)
	{
		return Add(DebugDrawItems.Quad.Get(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration));
	}
	
	/// <summary>
	/// Draws a filled quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color">The colour of the quad.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Quad FillQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color, duration));
	}
	
	/// <summary>
	/// Draws a filled quad.
	/// </summary>
	/// <param name="p1">The first point of the quad.</param>
	/// <param name="p2">The second point of the quad.</param>
	/// <param name="p3">The third point of the quad.</param>
	/// <param name="p4">The fourth point of the quad.</param>
	/// <param name="color1">The color of the quad's first point..</param>
	/// <param name="color2">The color of the quad's second point.</param>
	/// <param name="color3">The color of the quad's third point.</param>
	/// <param name="color4">The color of the quad's fourth point.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Quad FillQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Color color1, Color color2, Color color3, Color color4, float duration = 0)
	{
		return Add(DebugDrawItems.Quad.GetFill(ref p1, ref p2, ref p3, ref p4, ref color1, ref color2, ref color3, ref color4, duration));
	}
	
	/// <summary>
	/// Draws a sphere comprised of a circle for each axis.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Sphere Sphere(Vector3 position, Vector3 radius, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Sphere.Get(ref position, ref radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a sphere comprised of a circle for each axis.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="orientation">The orientation of the sphere.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Sphere Sphere(Vector3 position, Vector3 radius, Quaternion orientation, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Sphere.Get(ref position, ref radius, ref orientation, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe sphere.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Sphere WireSphere(Vector3 position, Vector3 radius, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a full wireframe sphere.
	/// </summary>
	/// <param name="position">The centre of the sphere.</param>
	/// <param name="radius">The radius of the sphere in each axis.</param>
	/// <param name="orientation">The orientation of the sphere.</param>
	/// <param name="color">The color of the sphere.</param>
	/// <param name="segments">The resolution of the sphere.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The Sphere object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Sphere WireSphere(Vector3 position, Vector3 radius, Quaternion orientation, Color color, int segments = 32, float duration = 0)
	{
		return Add(DebugDrawItems.Sphere.GetWire(ref position, ref radius, ref orientation, ref color, segments, duration));
	}
	
	/// <summary>
	/// Draws a filled square.
	/// </summary>
	/// <param name="position">The centre of the square.</param>
	/// <param name="size">The half size of the square.</param>
	/// <param name="facing">The normal or direction the front of the square is facing.</param>
	/// <param name="color">The colour of the square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Square Square(Vector3 position, Vector2 size, Vector3 facing, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Square.Get(ref position, ref size, ref facing, ref color, duration));
	}
	
	/// <summary>
	/// Draws a wire square.
	/// </summary>
	/// <param name="position">The centre of the square.</param>
	/// <param name="size">The half size of the square.</param>
	/// <param name="facing">The normal or direction the front of the square is facing.</param>
	/// <param name="color">The colour of the square.</param>
	/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
	/// <returns>The ellipse object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Square WireSquare(Vector3 position, Vector2 size, Vector3 facing, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Square.GetWire(ref position, ref size, ref facing, ref color, duration));
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
	public Triangle Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color, duration));
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
	public Triangle Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color1, Color color2, Color color3, float duration = 0)
	{
		return Add(DebugDrawItems.Triangle.Get(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration));
	}
	
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
	public Triangle FillTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color, float duration = 0)
	{
		return Add(DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color, duration));
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
	public Triangle FillTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color1, Color color2, Color color3, float duration = 0)
	{
		return Add(DebugDrawItems.Triangle.GetFill(ref p1, ref p2, ref p3, ref color1, ref color2, ref color3, duration));
	}
	
	/* </InstanceGenMethods> */
	
}