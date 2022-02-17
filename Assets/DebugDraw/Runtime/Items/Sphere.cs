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
		/// <summary>
		/// If true draw a sphere made up of multiple rings, otherwise draw an approximation made
		/// of a circle along each axis.
		/// </summary>
		public bool wireframe;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
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
		public static Sphere Get(ref Vector3 position, float radius, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = ItemPool<Sphere>.Get(duration);
			
			item.position = position;
			item.radius = new Vector3(radius, radius, radius);
			item.color = color;
			item.segments = segments;
			item.orientation = DebugDraw.rotationIdentity;
			item.wireframe = false;

			return item;
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
		public static Sphere Get(ref Vector3 position, ref Vector3 radius, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = ItemPool<Sphere>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.orientation = DebugDraw.rotationIdentity;
			item.wireframe = false;

			return item;
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
		public static Sphere Get(ref Vector3 position, ref Vector3 radius, ref Quaternion orientation, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = ItemPool<Sphere>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.color = color;
			item.segments = segments;
			item.orientation = orientation;
			item.wireframe = false;

			return item;
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
		public static Sphere GetWire(ref Vector3 position, float radius, ref Color color, int segments = 32, float duration = 0)
		{
			Vector3 r = new Vector3(radius, radius, radius);
			Sphere item = Get(ref position, ref r, ref color, segments, duration);
			item.wireframe = true;

			return item;
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
		public static Sphere GetWire(ref Vector3 position, ref Vector3 radius, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = Get(ref position, ref radius, ref color, segments, duration);
			item.wireframe = true;

			return item;
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
		public static Sphere GetWire(ref Vector3 position, ref Vector3 radius, ref Quaternion orientation, ref Color color, int segments = 32, float duration = 0)
		{
			Sphere item = Get(ref position, ref radius, ref orientation, ref color, segments, duration);
			item.wireframe = true;

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

			if (wireframe)
			{
				int segments = Mathf.Max(this.segments <= 0
					? Ellipse.DefaultAutoResolution(
						Mathf.Max(DebugDraw.DistanceFromCamera(ref position), 0),
						Mathf.Max(Mathf.Max(radius.x, radius.y), radius.z))
					: this.segments, 4);
				int rings = (segments - 1) / 2 + 2;

				Vector2 size;

				// XZ rings
				for (int i = rings - 2; i > 0; i--)
				{
					float a = (i / (float) (rings - 1) - 0.5f) * Mathf.PI;
					float c = Mathf.Cos(a);
					float s = Mathf.Sin(a);
					size = new Vector2(radius.x * c, radius.z * c);
					Vector3 p = position + up * (radius.y * s);
					Ellipse.BuildArc(
						mesh, ref p, ref right, ref forward, ref size, 0, 0,
						0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
				}
				
				// Y rings
				float aspect = radius.z / radius.x;
				size = new Vector2(radius.x, radius.y);

				for (int i = rings - 1; i > 0; i--)
				{
					float a = i / (float) (rings - 1) * Mathf.PI;
					float c = Mathf.Cos(a);
					float s = Mathf.Sin(a);
					Vector3 r = new Vector3(
						c * right.x + s * forward.x * aspect,
						c * right.y + s * forward.y * aspect,
						c * right.z + s * forward.z * aspect);

					Ellipse.BuildArc(
						mesh, ref position, ref r, ref up, ref size, 0, 0,
						0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
				}
			}
			else
			{
				int segments = this.segments > 0
					? Mathf.Max(this.segments, 4)
					: 0;

				// XY
				Vector2 size = new Vector2(radius.x, radius.y);
				Ellipse.BuildArc(
					mesh, ref position, ref right, ref up, ref size, 0, 0,
					0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
				// XZ
				size = new Vector2(radius.x, radius.z);
				Ellipse.BuildArc(
					mesh, ref position, ref right, ref forward, ref size, 0, 0,
					0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
				// ZY
				size = new Vector2(radius.z, radius.y);
				Ellipse.BuildArc(
					mesh, ref position, ref forward, ref up, ref size, 0, 0,
					0, 360, segments, DrawArcSegments.Never, DrawEllipseAxes.Never, ref clr, true);
			}
		}

		internal override void Release()
		{
			ItemPool<Sphere>.Release(this);
		}

	}

}