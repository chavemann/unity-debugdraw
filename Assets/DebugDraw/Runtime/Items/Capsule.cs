using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws a wireframe capsule.
	/// </summary>
	public class Capsule : BaseLineItem
	{
		/* mesh: line */

		/// <summary>
		/// The capsule radius.
		/// </summary>
		public float radius;
		/// <summary>
		/// The resolution of the capsule. If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;
		/// <summary>
		/// The forward vector used to orient the capsule if p1 -> p2 is considered the up vector.
		/// If null, an arbitrary axis will be chosen.
		/// </summary>
		public Vector3? forward;
		/// <summary>
		/// If true draw a full wireframe capsule, otherwise draw an approximation made
		/// of circles along each axis.
		/// </summary>
		public bool wireframe;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		public static Capsule Get(ref Vector3 p1, ref Vector3 p2, float radius, ref Color color, int segments = 32, float duration = 0)
		{
			Capsule item = ItemPool<Capsule>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.forward = null;
			item.wireframe = false;

			return item;
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
		public static Capsule GetWire(ref Vector3 p1, ref Vector3 p2, float radius, ref Color color, int segments = 32, float duration = 0)
		{
			Capsule item = ItemPool<Capsule>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.forward = null;
			item.wireframe = true;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Sets The forward vector used to orient the capsule.
		/// </summary>
		/// <param name="forward"></param>
		/// <returns></returns>
		public Capsule SetForward(Vector3? forward)
		{
			this.forward = forward;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 p1 = this.p1;
			Vector3 p2 = this.p2;
			Vector3 delta = p1 - p2;
			float length = delta.magnitude;
			Vector3 up = delta / length;
			Vector3 forward, right;

			if (this.forward.HasValue)
			{
				forward = this.forward.GetValueOrDefault();
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

			int segments;

			if (this.segments <= 0)
			{
				float d1 = DebugDraw.DistanceFromCamera(ref p1) + radius;
				float d2 = DebugDraw.DistanceFromCamera(ref p2) + radius;
				segments = Mathf.Max(this.segments <= 0
					? Ellipse.DefaultAutoResolution(
						Mathf.Max(d1 > 0 && d2 > 0 ? Mathf.Min(d1, d2) : Mathf.Max(d1, d2), 0),
						radius)
					: this.segments, 4);
			}
			else
			{
				segments = Mathf.Max(this.segments, 4);
			}

			Vector2 size = new Vector2(radius, radius);
			Color clr = GetColor(ref color);

			Ellipse.BuildArc(
				mesh, ref p1, ref right, ref forward, ref size, 0, 0,
				0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
			Ellipse.BuildArc(
				mesh, ref p2, ref right, ref forward, ref size, 0, 0,
				0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);

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

				int startIndex = mesh.vertexIndex;

				Ellipse.BuildArc(
					mesh, ref p1, ref r, ref up, ref size, 0, 0,
					0, 180, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);

				mesh.AddIndices(
					mesh.vertexIndex - 1,
					mesh.vertexIndex);

				Ellipse.BuildArc(
					mesh, ref p2, ref r, ref up, ref size, 0, 0,
					180, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);

				mesh.AddIndices(
					mesh.vertexIndex - 1,
					startIndex);
			}
		}

		internal override void Release()
		{
			ItemPool<Capsule>.Release(this);
		}

	}

}
