using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Sphere : BasePointItem
	{
		/* mesh: line */

		/// <summary>
		/// The radius of the sphere in each axis.
		/// </summary>
		public Vector3 radius;
		/// <summary>
		/// The orientation of the sphere.
		/// </summary>
		public Quaternion orientation;
		/// <summary>
		/// The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a sphere made up of four three along each axis.
		/// </summary>
		/// <param name="position">The centre of the sphere.</param>
		/// <param name="radius">The radius of the sphere in each axis.</param>
		/// <param name="color">The color of the sphere.</param>
		/// <param name="segments">The resolution of the sphere. If set to zero will be adjusted based on the distance to the camera.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Sphere object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Sphere Get(ref Vector3 position, ref Vector3 radius, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = ItemPool<Sphere>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.orientation = DebugDraw.rotationIdentity;

			return item;
		}
		
		/// <summary>
		/// Draws a sphere made up of four three along each axis with the given orientation.
		/// </summary>
		/// <param name="position">The centre of the sphere.</param>
		/// <param name="radius">The radius of the sphere in each axis.</param>
		/// <param name="orientation">The orientation of the sphere.</param>
		/// <param name="color">The color of the sphere.</param>
		/// <param name="segments">The resolution of the sphere.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Sphere object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Sphere Get(ref Vector3 position, ref Vector3 radius, ref Quaternion orientation, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = ItemPool<Sphere>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.orientation = orientation;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 position = this.position;
			Vector3 forward = orientation * DebugDraw.forward;
			Vector3 right = orientation * DebugDraw.right;
			Vector3 up = orientation * DebugDraw.up;
			
			if (hasStateTransform)
			{
				position = stateTransform.MultiplyPoint3x4(position);
				forward = stateTransform.MultiplyVector(forward);
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}
			
			Color clr = GetColor(ref color);
			
			// XY
			Vector2 size = new Vector2(radius.x, radius.y);
			Ellipse.BuildArc(
				mesh, ref position, ref right, ref up, ref size, 0,
				0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, false);
			// XZ
			size = new Vector2(radius.x, radius.z);
			Ellipse.BuildArc(
				mesh, ref position, ref right, ref forward, ref size, 0,
				0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, false);
			// ZY
			size = new Vector2(radius.z, radius.y);
			Ellipse.BuildArc(
				mesh, ref position, ref forward, ref up, ref size, 0,
				0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, false);
		}

		internal override void Release()
		{
			ItemPool<Sphere>.Release(this);
		}

	}

}