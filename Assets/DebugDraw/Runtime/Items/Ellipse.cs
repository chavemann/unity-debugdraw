using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
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
		/// Only relevant when <see cref="filled"/> is false.
		/// </summary>
		public DrawArcSegments drawArcSegments;
		/// <summary>
		/// Options for drawing an X and Y axis inside the ellipse.
		/// Only relevant when <see cref="filled"/> is false.
		/// </summary>
		public DrawEllipseAxes drawAxes;
		/// <summary>
		/// The resolution of the ellipse.
		/// </summary>
		public int segments;
		/// <summary>
		/// If true the ellipse resolution (segments) will be adjusted based on the distance to the camera
		/// so that it will always appear smooth.
		/// </summary>
		public bool autoResolution;
		/// <summary>
		/// True for a filled ellipse made up from triangles, otherwise a wire ellipse.
		/// It's important that this Ellipse item is added to mesh with the right topology, either lines or triangles,
		/// based on this setting.
		/// </summary>
		public bool filled;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		public static Ellipse Get(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, int segments = 32, float duration = 0)
		{
			/* mesh: triangle */
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
			item.filled = true;

			return item;
		}

		/// <summary>
		/// Draws a wire ellipse.
		/// </summary>
		/// <param name="position">The centre of the ellipse.</param>
		/// <param name="size">The size/radius of the ellipse.</param>
		/// <param name="facing">The normal or direction the front of the ellipse is facing.</param>
		/// <param name="color">The colour of the ellipse.</param>
		/// <param name="segments">The resolution of the ellipse.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Ellipse GetWire(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, int segments = 32, float duration = 0)
		{
			/* mesh: line*/
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
			item.filled = false;

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
		public static Ellipse GetArc(
			ref Vector3 position, ref Vector2 size, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			float duration = 0)
		{
			/* mesh: triangle */
			Ellipse item = ItemPool<Ellipse>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.filled = true;
			item.rotation = 0;
			item.startAngle = startAngle;
			item.endAngle = endAngle;
			item.drawAxes = DrawEllipseAxes.Never;
			item.drawArcSegments = DrawArcSegments.Never;
			item.filled = true;

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
		public static Ellipse GetWireArc(
			ref Vector3 position, ref Vector2 size, ref Vector3 facing,
			float startAngle, float endAngle, ref Color color, int segments = 32,
			DrawArcSegments drawArcSegments = DrawArcSegments.OpenOnly, DrawEllipseAxes drawAxes = DrawEllipseAxes.Never,
			float duration = 0)
		{
			/* mesh: line */
			Ellipse item = ItemPool<Ellipse>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.segments = segments;
			item.filled = true;
			item.rotation = 0;
			item.startAngle = startAngle;
			item.endAngle = endAngle;
			item.drawArcSegments = drawArcSegments;
			item.drawAxes = drawAxes;
			item.filled = true;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

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
		/// Only relevant when <see cref="filled"/> is false.</param>
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
		/// Only relevant when <see cref="filled"/> is false.</param>
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
		/// Only relevant when <see cref="filled"/> is false.</param>
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
		/// If true the ellipse resolution (segments) will be adjusted based on the distance to the camera
		/// so that it will always appear smooth.
		/// </summary>
		/// <param name="autoResolution">.</param>
		/// <returns></returns>
		public Ellipse SetAutoResolution(bool autoResolution = true)
		{
			this.autoResolution = autoResolution;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 position = this.position;
			DebugDraw.FindAxisVectors(ref facing, ref DebugDraw.up, out Vector3 up, out Vector3 right);

			if (hasStateTransform)
			{
				position = stateTransform.MultiplyPoint3x4(position);
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}

			float angle1 = Mathf.Min(startAngle, endAngle);
			float angle2 = Mathf.Clamp(Mathf.Max(startAngle, endAngle) - angle1, 0, 360);
			bool isOpen = angle2 < 360;
			angle1 = Mathf.Repeat(angle1, 360);
			angle2 = angle1 + angle2;
			angle2 = (rotation + angle2) * Mathf.Deg2Rad;
			angle1 = (rotation + angle1) * Mathf.Deg2Rad;

			int segments = autoResolution
				? autoResolution
					? DebugDraw.AutoResolution(
						Mathf.Max(DebugDraw.DistanceFromCamera(ref position), 0),
						Mathf.Max(size.x, size.y), 4, 64, 128)
					: this.segments
				: this.segments;

			Color clr = GetColor(ref color);
			
			int centreVertexIndex = -1;

			if (filled)
			{
				mesh.AddVertex(ref position);
				mesh.AddColor(ref clr);
				centreVertexIndex = mesh.vertexIndex++;
			}
			
			int arcStartVertexIndex = mesh.vertexIndex;

			if (angle1 < angle2)
			{
				float deltaAngle = (Mathf.PI * 2) / Mathf.Max(segments, 3);
				float angle = angle1;

				Vector2 p = new Vector2(
					Mathf.Cos(angle) * size.x,
					Mathf.Sin(angle) * size.y);
				mesh.AddVertex(
					position.x + right.x * p.x + up.x * p.y,
					position.y + right.y * p.x + up.y * p.y,
					position.z + right.z * p.x + up.z * p.y);
				mesh.AddColor(ref clr);
				mesh.vertexIndex++;
				
				while (angle < angle2)
				{
					angle += deltaAngle;

					if (angle > angle2)
					{
						angle = angle2;
					}

					p.x = Mathf.Cos(angle) * size.x;
					p.y = Mathf.Sin(angle) * size.y;
					mesh.AddVertex(
						position.x + right.x * p.x + up.x * p.y,
						position.y + right.y * p.x + up.y * p.y,
						position.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref clr);
					
					if (filled)
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
			
			if (filled)
				return;

			int loopVertexCount = mesh.vertexIndex - arcStartVertexIndex;
			
			// Arc segments
			if (drawArcSegments == DrawArcSegments.Always || isOpen && drawArcSegments == DrawArcSegments.OpenOnly)
			{
				centreVertexIndex = mesh.vertexIndex;
				mesh.AddVertex(ref position);
				mesh.AddColor(ref clr);
				mesh.vertexIndex++;
				
				if (loopVertexCount > 0)
				{
					mesh.AddIndices(
						centreVertexIndex,
						arcStartVertexIndex,
						centreVertexIndex,
						mesh.vertexIndex - 2);
				}
				// The start and end angles are the same so no vertices were added.
				else
				{
					Vector2 p = new Vector2(
						Mathf.Cos(angle1) * size.x,
						Mathf.Sin(angle1) * size.y);
					mesh.AddVertex(
						position.x + right.x * p.x + up.x * p.y,
						position.y + right.y * p.x + up.y * p.y,
						position.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref clr);
					mesh.AddIndices(
						centreVertexIndex,
						mesh.vertexIndex++);
				}
			}
			
			// Axes
			if (loopVertexCount > 0 && drawAxes != DrawEllipseAxes.Never)
			{
				for (int i = 0; i < 4; i++)
				{
					float angle = rotation * Mathf.Deg2Rad + Mathf.PI * 0.5f * i;
             		
					if (!CheckAngle(angle, angle1, angle2, drawAxes))
						continue;
             		
					if (centreVertexIndex == -1)
					{
						centreVertexIndex = mesh.vertexIndex;
						mesh.AddVertex(ref position);
						mesh.AddColor(ref clr);
						mesh.vertexIndex++;
					}
             		
					Vector2 p = new Vector2(
						Mathf.Cos(angle) * size.x,
						Mathf.Sin(angle) * size.y);
					mesh.AddVertex(
						position.x + right.x * p.x + up.x * p.y,
						position.y + right.y * p.x + up.y * p.y,
						position.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref clr);
					mesh.AddIndices(
						centreVertexIndex,
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