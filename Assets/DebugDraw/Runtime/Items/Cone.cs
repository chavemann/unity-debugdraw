using System.Runtime.CompilerServices;
using DebugDrawUtils.DebugDrawAttachments;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws a wireframe cone.
	/// </summary>
	public class Cone : BasePointItem, IAttachableLine
	{
		/* mesh: line */

		/// <summary>
		/// The direction the cone.
		/// </summary>
		public Vector3 direction;
		/// <summary>
		/// The length of the cone.
		/// </summary>
		public float length;
		/// <summary>
		/// The angle of the cone.
		/// </summary>
		public float angle;
		/// <summary>
		/// If true, the length is treated as the radius of the cone.
		/// If false the length is the distance from the origin to the centre of the cones cap.
		/// </summary>
		public bool round;
		/// <summary>
		/// Should a cap be drawn on the cone.
		/// When round is true the cap will be made up of two arcs, otherwise two perpendicular lines.
		/// </summary>
		public bool drawCap;
		/// <summary>
		/// The resolution of the cone. If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;
		/// <summary>
		/// The up vector used to orient the cone if <see cref="direction"/> is considered the forward vector.
		/// If null, an arbitrary axis will be chosen.
		/// </summary>
		public Vector3? up;
		/// <summary>
		/// If true draw a full wireframe cone, otherwise draw an approximation shell.
		/// </summary>
		public bool wireframe;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		/// When round is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Cylinder object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cone Get(ref Vector3 origin, ref Vector3 direction, float length, float angle, ref Color color, int segments = 32, bool round = false, bool drawCap = false, float? duration = null)
		{
			Cone item = ItemPool<Cone>.Get(duration);

			item.position = origin;
			item.direction = direction;
			item.length = length;
			item.angle = angle;
			item.color = color;
			item.segments = segments;
			item.round = round;
			item.drawCap = drawCap;
			item.up = null;
			item.wireframe = false;

			return item;
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
		/// When round is true the cap will be made up of two arcs, otherwise two perpendicular lines.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Cylinder object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cone GetWire(ref Vector3 origin, ref Vector3 direction, float length, float angle, ref Color color, int segments = 32, bool round = false, bool drawCap = false, float? duration = null)
		{
			Cone item = Get(ref origin, ref direction, length, angle, ref color, segments, round, drawCap, duration);
			item.wireframe = true;

			return item;
		}

		public LineAttachment AttachTo(GameObjectOrTransform startObj, GameObjectOrTransform endObj)
		{
			LineAttachment attachment = AttachmentPool<LineAttachment>.Get(this);
			attachment.item = this;
			attachment.lineItem = this;
			attachment.start.Set(startObj);
			attachment.end.Set(endObj);
			return attachment;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Sets the cone's origin point.
		/// </summary>
		/// <param name="origin"></param>
		/// <returns></returns>
		public Cone SetOrigin(Vector3 origin)
		{
			position = origin;

			return this;
		}

		/// <summary>
		/// Sets the cone's direction.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public Cone SetDirection(Vector3 direction)
		{
			this.direction = direction;

			return this;
		}

		/// <summary>
		/// Sets the cone's angle.
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public Cone SetAngle(float angle)
		{
			this.angle = angle;

			return this;
		}

		/// <summary>
		/// Sets the cone's length.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public Cone SetLength(float length)
		{
			this.length = length;

			return this;
		}

		public void SetPositions(Vector3 start, Vector3 end)
		{
			position = start;
			direction = new Vector3(
				end.x - start.x,
				end.y - start.y,
				end.z - start.z);
			length = direction.magnitude;
			direction.Normalize();
		}

		public void SetStartPosition(Vector3 position)
		{
			this.position = position;
		}

		public void SetEndPosition(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public Vector3 GetStartPosition()
		{
			return position;
		}

		public Vector3 GetEndPosition()
		{
			return new Vector3(
				position.x + direction.x * length,
				position.y + direction.y * length,
				position.z + direction.z * length);
		}

		/// <summary>
		/// Sets the up vector used to orient the cone.
		/// </summary>
		/// <param name="up"></param>
		/// <returns></returns>
		public Cone SetUp(Vector3? up)
		{
			this.up = up;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 up = direction.normalized;
			Vector3 p1 = position;
			Vector3 p2 = new Vector3(
				p1.x + up.x * length,
				p1.y + up.y * length,
				p1.z + up.z * length);
			Vector3 forward, right;

			if (this.up.HasValue)
			{
				forward = this.up.GetValueOrDefault();
				DebugDraw.FindAxisVectors(ref up, ref forward, out forward, out right);
			}
			else
			{
				DebugDraw.FindAxisVectors(ref up, ref DebugDraw.forward, out forward, out right);
			}

			if (hasStateTransform)
			{
				p1 = stateTransform.MultiplyPoint3x4(p1);
				p2 = stateTransform.MultiplyPoint3x4(p2);
				forward = stateTransform.MultiplyVector(forward);
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}

			Color clr = GetColor(ref color);

			int segments;

			float angle = Mathf.Clamp(this.angle * 0.5f, 0, 90) * Mathf.Deg2Rad;
			float sx = Mathf.Sin(angle);

			if (this.segments <= 0)
			{
				float d1 = DebugDraw.DistanceFromCamera(ref p1) + length;
				float d2 = DebugDraw.DistanceFromCamera(ref p2) + length;
				segments = Mathf.Max(this.segments <= 0
					? Ellipse.DefaultAutoResolution(
						Mathf.Max(d1 > 0 && d2 > 0 ? Mathf.Min(d1, d2) : Mathf.Max(d1, d2), 0),
						sx * length)
					: this.segments, 4);
			}
			else
			{
				segments = Mathf.Max(this.segments, 4);
			}

			mesh.AddVertex(ref p1);
			mesh.AddColor(ref clr);
			mesh.vertexIndex++;

			float radius = round
				? sx * length
				: Mathf.Tan(angle) * length;

			if (round)
			{
				float sy = Mathf.Cos(angle) * length;
				p2.x = p1.x + up.x * sy;
				p2.y = p1.y + up.y * sy;
				p2.z = p1.z + up.z * sy;
			}

			mesh.AddVertex(ref p1);
			mesh.AddColor(ref clr);
			mesh.vertexIndex++;

			int startIndex = mesh.vertexIndex;

			// End
			Vector2 size = new Vector2(radius, radius);
			Ellipse.BuildArc(
				mesh, ref p2, ref right, ref forward, ref size, 0, 0,
				0, 360, segments, DrawArcSegments.Never,
				drawCap && !round ? DrawEllipseAxes.Always : DrawEllipseAxes.Never,
				ref clr, true);

			int endIndex = drawCap && !round
				? mesh.vertexIndex - 8
				: mesh.vertexIndex - 1;

			// Ribs
			if (wireframe)
			{
				for (int i = startIndex; i < endIndex; i++)
				{
					mesh.AddIndices(startIndex - 1, i);
				}
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					float a = Mathf.PI * (i / 2.0f);
					float x = Mathf.Cos(a);
					float y = Mathf.Sin(a);

					mesh.AddVertex(
						p2.x + right.x * x * radius + forward.x * y * radius,
						p2.y + right.y * x * radius + forward.y * y * radius,
						p2.z + right.z * x * radius + forward.z * y * radius);
					mesh.AddColor(ref clr);
					mesh.AddIndices(
						startIndex - 1,
						mesh.vertexIndex++);
				}
			}

			if (round && drawCap)
			{
				size.x = length;
				size.y = length;
				angle = Mathf.Clamp(this.angle * 0.5f, 0, 90);

				int rings = wireframe ? (segments - 1) / 2 + 2 : 3;

				for (int i = rings - 1; i > 0; i--)
				{
					float a = i / (float) (rings - 1) * Mathf.PI;
					float c = Mathf.Cos(a);
					float s = Mathf.Sin(a);
					Vector3 r = new Vector3(
						c * right.x + s * forward.x,
						c * right.y + s * forward.y,
						c * right.z + s * forward.z);

					Ellipse.BuildArc(
						mesh, ref p1, ref up, ref r, ref size, 0, 0,
						-angle, angle, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
				}

				// Ellipse.BuildArc(
				// 	mesh, ref p1, ref up, ref right, ref size, 0,
				// 	-angle, angle, segments,
				// 	DrawArcSegments.Never, DrawEllipseAxes.Never,
				// 	ref clr, true);
				// Ellipse.BuildArc(
				// 	mesh, ref p1, ref up, ref forward, ref size, 0,
				// 	-angle, angle, segments,
				// 	DrawArcSegments.Never, DrawEllipseAxes.Never,
				// 	ref clr, true);
			}
		}

		internal override void Release()
		{
			ItemPool<Cone>.Release(this);
		}

	}

}
