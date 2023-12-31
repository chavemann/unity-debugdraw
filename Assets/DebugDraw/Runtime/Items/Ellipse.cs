using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Renders various types of ellipse - open, closed, arcs, filled, or wire.
	/// </summary>
	public class Ellipse : BasePointItem
	{
		/* mesh: line */

		/// <summary>
		/// The size/radius of the ellipse.
		/// </summary>
		public Vector2 size;
		/// <summary>
		/// If non-zero, defines the radius of the inner ring turning this ellipse into a 2D donut.
		/// </summary>
		public float innerRadius;
		/// <summary>
		/// The normal or direction the front of the ellipse is facing.
		/// </summary>
		public Vector3 facing;
		/// <summary>
		/// The rotation of the ellipse. Mostly useful for arcs.
		/// </summary>
		public float rotation;
		/// <summary>
		/// The start angle in degrees of the arc.
		/// </summary>
		public float startAngle;
		/// <summary>
		/// The end angle in degrees of the arc.
		/// </summary>
		public float endAngle;
		/// <summary>
		/// Options for connecting the centre of the ellipse and the arc end points.
		/// Only relevant when <see cref="wireframe"/> is false.
		/// </summary>
		public DrawArcSegments drawArcSegments;
		/// <summary>
		/// Options for drawing an X and Y axis inside the ellipse.
		/// Only relevant when <see cref="wireframe"/> is false.
		/// </summary>
		public DrawEllipseAxes drawAxes;
		/// <summary>
		/// The resolution of the ellipse. If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;
		/// <summary>
		/// The forward vector used to orient the ellipse.
		/// If null, an arbitrary axis will be chosen.
		/// </summary>
		public Vector3? forward;
		/// <summary>
		/// True for a filled ellipse made up from triangles, otherwise a wire ellipse.
		/// It's important that this Ellipse item is added to a mesh with the right topology, either lines or triangles,
		/// based on this setting.
		/// </summary>
		public bool wireframe;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		public static Ellipse Get(ref Vector3 position, float radius, ref Vector3 facing, ref Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float? duration = null)
		{
			Vector2 size = new Vector2(radius, radius);
			return Get(ref position, ref size, ref facing, ref color, segments, drawAxes, duration);
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
		public static Ellipse GetFill(ref Vector3 position, float radius, ref Vector3 facing, ref Color color, int segments = 32, float? duration = null)
		{
			Vector2 size = new Vector2(radius, radius);
			return GetFill(ref position, ref size, ref facing, ref color, segments, duration);
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
		public static Ellipse GetArc(
			ref Vector3 position, float radius, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never,
			float? duration = null)
		{
			Vector2 size = new Vector2(radius, radius);
			return GetArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, drawArcSegments, drawAxes, duration);
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
		public static Ellipse GetFillArc(
			ref Vector3 position, float radius, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			float? duration = null)
		{
			Vector2 size = new Vector2(radius, radius);
			return GetFillArc(ref position, ref size, ref facing, startAngle, endAngle, ref color, segments, duration);
		}

		//

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
		public static Ellipse Get(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, int segments = 32, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never, float? duration = null)
		{
			Ellipse item = ItemPool<Ellipse>.Get(duration);

			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.rotation = 0;
			item.startAngle = 0;
			item.endAngle = 360;
			item.drawAxes = drawAxes;
			item.drawArcSegments = DrawArcSegments.Never;
			item.forward = null;
			item.wireframe = true;
			item.innerRadius = 0;

			return item;
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
		public static Ellipse GetFill(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, int segments = 32, float? duration = null)
		{
			Ellipse item = ItemPool<Ellipse>.Get(duration);

			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.rotation = 0;
			item.startAngle = 0;
			item.endAngle = 360;
			item.drawAxes = DrawEllipseAxes.Never;
			item.drawArcSegments = DrawArcSegments.Never;
			item.forward = null;
			item.wireframe = false;
			item.innerRadius = 0;

			return item;
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
		public static Ellipse GetArc(
			ref Vector3 position, ref Vector2 size, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never,
			float? duration = null)
		{
			Ellipse item = ItemPool<Ellipse>.Get(duration);

			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.rotation = 0;
			item.startAngle = startAngle;
			item.endAngle = endAngle;
			item.drawArcSegments = drawArcSegments;
			item.drawAxes = drawAxes;
			item.wireframe = true;
			item.forward = null;
			item.innerRadius = 0;

			return item;
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
		public static Ellipse GetFillArc(
			ref Vector3 position, ref Vector2 size, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			float? duration = null)
		{
			Ellipse item = ItemPool<Ellipse>.Get(duration);

			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.rotation = 0;
			item.startAngle = startAngle;
			item.endAngle = endAngle;
			item.drawAxes = DrawEllipseAxes.Never;
			item.drawArcSegments = DrawArcSegments.Never;
			item.wireframe = false;
			item.forward = null;
			item.innerRadius = 0;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Sets the forward vector used to orient the ellipse.
		/// </summary>
		/// <param name="forward"></param>
		/// <returns></returns>
		public Ellipse SetForward(Vector3? forward)
		{
			this.forward = forward;

			return this;
		}

		public Ellipse SetInnerRadius(float innerRadius)
		{
			this.innerRadius = innerRadius;

			return this;
		}

		/// <summary>
		/// Sets the angles for this arc.
		/// </summary>
		/// <param name="startAngle">The start angle of the arc.</param>
		/// <param name="endAngle">The end angle of the arc.</param>
		/// <returns></returns>
		public Ellipse SetArcAngles(float startAngle, float endAngle)
		{
			this.startAngle	= startAngle;
			this.endAngle = endAngle;

			return this;
		}

		/// <summary>
		/// Sets the options for this arc.
		/// </summary>
		/// <param name="startAngle">The start angle of the arc.</param>
		/// <param name="endAngle">The end angle of the arc.</param>
		/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.
		/// Only relevant when <see cref="wireframe"/> is true.</param>
		/// <returns></returns>
		public Ellipse SetArc(float startAngle, float endAngle, DrawArcSegments drawArcSegments)
		{
			this.startAngle	= startAngle;
			this.endAngle = endAngle;
			this.drawArcSegments = drawArcSegments;

			return this;
		}

		/// <summary>
		/// Sets the drawAxes option for this ellipse.
		/// </summary>
		/// <param name="drawAxes">Options for drawing an X and Y axis inside the ellipse.
		/// Only relevant when <see cref="wireframe"/> is true.</param>
		/// <returns></returns>
		public Ellipse SetAxes(DrawEllipseAxes drawAxes = DrawEllipseAxes.InsideArc)
		{
			this.drawAxes = drawAxes;

			return this;
		}

		/// <summary>
		/// Sets the drawAxes option for this ellipse.
		/// </summary>
		/// <param name="drawArcSegments">Options for connecting the centre of the ellipse and the arc end points.
		/// Only relevant when <see cref="wireframe"/> is true.</param>
		/// <returns></returns>
		public Ellipse SetDrawArcSegments(DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly)
		{
			this.drawArcSegments = drawArcSegments;

			return this;
		}

		public Ellipse SetRotation(float rotation)
		{
			this.rotation = rotation;

			return this;
		}

		/// <summary>
		/// Sets <see cref="segments"/> to zero so that it will be calculated dynamically based
		/// on the distance to the camera.
		/// </summary>
		/// <returns></returns>
		public Ellipse SetAutoResolution()
		{
			segments = 0;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 position = this.position;
			Vector3 up, right;

			if (forward.HasValue)
			{
				Vector3 f = forward.GetValueOrDefault();
				DebugDraw.FindAxisVectors(ref facing, ref f, out up, out right);
			}
			else
			{
				DebugDraw.FindAxisVectors(ref facing, ref DebugDraw.forward, out up, out right);
			}

			if (hasStateTransform)
			{
				position = stateTransform.MultiplyPoint3x4(position);
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}

			Color clr = GetColor(ref color);

			BuildArc(
				mesh, ref position, ref right, ref up, ref size, innerRadius, rotation,
				startAngle, endAngle, segments, drawArcSegments, drawAxes, ref clr, wireframe);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void BuildArc(
			DebugDrawMesh mesh, ref Vector3 worldPos, ref Vector3 right, ref Vector3 up, ref Vector2 size, float innerRadius, float rotation,
			float startAngle, float endAngle, int segments,
			DrawArcSegments drawArcSegments, DrawEllipseAxes drawAxes,
			ref Color color, bool wireframe)
		{
			float angle1 = Mathf.Min(startAngle, endAngle);
			float angle2 = Mathf.Clamp(Mathf.Max(startAngle, endAngle) - angle1, 0, 360);
			bool isOpen = angle2 < 360;
			angle1 = Mathf.Repeat(angle1, 360);
			angle2 = angle1 + angle2;
			angle2 = (rotation + angle2) * Mathf.Deg2Rad;
			angle1 = (rotation + angle1) * Mathf.Deg2Rad;

			int finalSegments = segments <= 0
				? DefaultAutoResolution(
					Mathf.Max(DebugDraw.DistanceFromCamera(ref worldPos), 0),
					Mathf.Max(size.x, size.y))
				: segments;

			float innerRadiusT = Mathf.Clamp01(Mathf.Abs(innerRadius) / Mathf.Max(size.x, size.y));
			bool hasInner = innerRadiusT > 0;

			int centreVertexIndex = -1;

			if (!wireframe && !hasInner)
			{
				mesh.AddVertex(ref worldPos);
				mesh.AddColor(ref color);
				centreVertexIndex = mesh.vertexIndex++;
			}

			int arcStartVertexIndex = mesh.vertexIndex;

			if (angle1 < angle2)
			{
				float deltaAngle = (Mathf.PI * 2) / Mathf.Max(finalSegments, 3);
				float angle = angle1;

				float c = Mathf.Cos(angle);
				float s = Mathf.Sin(angle);
				Vector2 p = new Vector2(
					c * size.x,
					s * size.y);
				mesh.AddVertex(
					worldPos.x + right.x * p.x + up.x * p.y,
					worldPos.y + right.y * p.x + up.y * p.y,
					worldPos.z + right.z * p.x + up.z * p.y);
				mesh.AddColor(ref color);
				mesh.vertexIndex++;

				if (hasInner)
				{
					p.x = c * size.x * innerRadiusT;
					p.y = s * size.y * innerRadiusT;
					mesh.AddVertex(
						worldPos.x + right.x * p.x + up.x * p.y,
						worldPos.y + right.y * p.x + up.y * p.y,
						worldPos.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref color);
					mesh.vertexIndex++;
				}

				while (angle < angle2)
				{
					angle += deltaAngle;

					if (angle > angle2)
					{
						angle = angle2;
					}

					c = Mathf.Cos(angle);
					s = Mathf.Sin(angle);
					p.x = c * size.x;
					p.y = s * size.y;
					mesh.AddVertex(
						worldPos.x + right.x * p.x + up.x * p.y,
						worldPos.y + right.y * p.x + up.y * p.y,
						worldPos.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref color);

					if (hasInner)
					{
						p.x = c * size.x * innerRadiusT;
						p.y = s * size.y * innerRadiusT;
						mesh.AddVertex(
							worldPos.x + right.x * p.x + up.x * p.y,
							worldPos.y + right.y * p.x + up.y * p.y,
							worldPos.z + right.z * p.x + up.z * p.y);
						mesh.AddColor(ref color);

						if (!wireframe)
						{
							mesh.AddIndices(
								mesh.vertexIndex++,
								mesh.vertexIndex - 2,
								mesh.vertexIndex - 3,
								mesh.vertexIndex++,
								mesh.vertexIndex - 3,
								mesh.vertexIndex - 2);
						}
						else
						{
							mesh.AddIndices(
								mesh.vertexIndex - 2,
								mesh.vertexIndex++,
								mesh.vertexIndex++,
								mesh.vertexIndex - 3);
						}
					}
					else
					{
						if (!wireframe)
						{
							mesh.AddIndices(
								centreVertexIndex,
								mesh.vertexIndex - 1,
								mesh.vertexIndex++);
						}
						else
						{
							mesh.AddIndices(
								mesh.vertexIndex - 1,
								mesh.vertexIndex++);
						}
					}
				}
			}

			if (!wireframe)
				return;

			int loopVertexCount = mesh.vertexIndex - arcStartVertexIndex;

			// Arc segments
			if (drawArcSegments == DrawArcSegments.Always || isOpen && drawArcSegments == DrawArcSegments.OpenOnly)
			{
				if (loopVertexCount > 0)
				{
					if (!hasInner)
					{
						centreVertexIndex = mesh.vertexIndex;
						mesh.AddVertex(ref worldPos);
						mesh.AddColor(ref color);
						mesh.vertexIndex++;

						mesh.AddIndices(
							centreVertexIndex,
							arcStartVertexIndex,
							centreVertexIndex,
							mesh.vertexIndex - 2);
					}
					else
					{
						mesh.AddIndices(
							arcStartVertexIndex,
							arcStartVertexIndex + 1,
							mesh.vertexIndex - 2,
							mesh.vertexIndex - 1);
					}
				}
				// The start and end angles are the same so no vertices were added.
				else
				{
					Vector2 p = new Vector2(
						Mathf.Cos(angle1) * size.x,
						Mathf.Sin(angle1) * size.y);
					mesh.AddVertex(
						worldPos.x + right.x * p.x + up.x * p.y,
						worldPos.y + right.y * p.x + up.y * p.y,
						worldPos.z + right.z * p.x + up.z * p.y);
					mesh.vertexIndex++;

					if (!hasInner)
					{
						mesh.AddVertex(ref worldPos);
					}
					else
					{
						p.x = Mathf.Cos(angle1) * size.x * innerRadiusT;
						p.y = Mathf.Sin(angle1) * size.y * innerRadiusT;
						mesh.AddVertex(
							worldPos.x + right.x * p.x + up.x * p.y,
							worldPos.y + right.y * p.x + up.y * p.y,
							worldPos.z + right.z * p.x + up.z * p.y);
					}

					mesh.AddColorX2(ref color);
					mesh.AddIndices(
						mesh.vertexIndex - 1,
						mesh.vertexIndex++);
				}
			}

			// Axes
			if ((loopVertexCount > 0 || drawAxes == DrawEllipseAxes.Always) && drawAxes != DrawEllipseAxes.Never)
			{
				for (int i = 0; i < 4; i++)
				{
					float angle = rotation * Mathf.Deg2Rad + Mathf.PI * 0.5f * i;

					if (!CheckAngle(angle, angle1, angle2, drawAxes))
						continue;

					Vector2 p;

					if (!hasInner)
					{
						mesh.AddVertex(ref worldPos);
						mesh.vertexIndex++;
					}
					else
					{
						p.x = Mathf.Cos(angle) * size.x * innerRadiusT;
						p.y = Mathf.Sin(angle) * size.y * innerRadiusT;
						mesh.AddVertex(
							worldPos.x + right.x * p.x + up.x * p.y,
							worldPos.y + right.y * p.x + up.y * p.y,
							worldPos.z + right.z * p.x + up.z * p.y);
						mesh.vertexIndex++;
					}

					p.x = Mathf.Cos(angle) * size.x;
					p.y = Mathf.Sin(angle) * size.y;
					mesh.AddVertex(
						worldPos.x + right.x * p.x + up.x * p.y,
						worldPos.y + right.y * p.x + up.y * p.y,
						worldPos.z + right.z * p.x + up.z * p.y);
					mesh.AddColorX2(ref color);
					mesh.AddIndices(
						mesh.vertexIndex - 1,
						mesh.vertexIndex++);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CheckAngle(float angle, float min, float max, DrawEllipseAxes drawAxes)
		{
			const float epsilon = 0.001f;
			angle = Mathf.Repeat(angle - min, Mathf.PI * 2);

			return drawAxes == DrawEllipseAxes.Always || angle >= -epsilon && angle <= max - min + epsilon;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int DefaultAutoResolution(float distance, float radius)
		{
			return DebugDraw.AutoResolution(distance, radius, 4, 50, 96);
		}

		internal override void Release()
		{
			ItemPool<Ellipse>.Release(this);
		}

	}

	/// <summary>
	/// Options for how to draw axes inside of an Ellipse.
	/// </summary>
	public enum DrawEllipseAxes
	{
		/// <summary>
		/// Don't draw.
		/// </summary>
		Never,
		/// <summary>
		/// Always draw.
		/// </summary>
		Always,
		/// <summary>
		/// Only draw an axis if it falls within the arc's range.
		/// </summary>
		InsideArc,
	}

	/// <summary>
	/// Options for how to draw the opening and closing segments (connecting the ends of the arc and the centre) of an arc.
	/// </summary>
	public enum DrawArcSegments
	{
		/// <summary>
		/// Don't connect the centre and arc end points.
		/// </summary>
		Never,
		/// <summary>
		/// Always connect the centre and arc end points.
		/// </summary>
		Always,
		/// <summary>
		/// Only connect the centre and arc end points when the arc is open, i.e. when the start and end angles are not the same.
		/// </summary>
		OpenOnly,
	}

}
