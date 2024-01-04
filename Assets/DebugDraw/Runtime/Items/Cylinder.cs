using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws a wireframe cylinder.
	/// </summary>
	public class Cylinder : BaseLineItem
	{
		/* mesh: line */

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// The radius at the start point of the cylinder.
		/// </summary>
		public Vector2 radius1;
		/// <summary>
		/// The radius at the end point of the cylinder.
		/// </summary>
		public Vector2 radius2;
		/// <summary>
		/// Draw axis at each end of the cylinder.
		/// </summary>
		public bool drawEndAxes;
		/// <summary>
		/// The resolution of the cylinder. If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;
		/// <summary>
		/// The forward vector used to orient the cylinder if p1 -> p2 is considered the up vector.
		/// If null, an arbitrary axis will be chosen.
		/// </summary>
		public Vector3? forward;
		/// <summary>
		/// If true draw a full wireframe cylinder, otherwise draw an approximation shell.
		/// </summary>
		public bool wireframe;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		public static Cylinder Get(ref Vector3 p1, ref Vector3 p2, float radius, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			return Get(ref p1, ref p2, radius, radius, ref color, segments, drawEndAxes, duration);
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
		public static Cylinder Get(ref Vector3 p1, ref Vector3 p2, float radius1, float radius2, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = ItemPool<Cylinder>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.radius1.x = radius1;
			item.radius1.y = radius1;
			item.radius2.x = radius2;
			item.radius2.y = radius2;
			item.color = color;
			item.segments = segments;
			item.drawEndAxes = drawEndAxes;
			item.forward = null;
			item.wireframe = false;

			return item;
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
		public static Cylinder Get(ref Vector3 p1, ref Vector3 p2, ref Vector2 radius, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			return Get(ref p1, ref p2, ref radius, ref radius, ref color, segments, drawEndAxes, duration);
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
		public static Cylinder Get(ref Vector3 p1, ref Vector3 p2, ref Vector2 radius1, ref Vector2 radius2, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = ItemPool<Cylinder>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.radius1 = radius1;
			item.radius2 = radius2;
			item.color = color;
			item.segments = segments;
			item.drawEndAxes = drawEndAxes;
			item.forward = null;
			item.wireframe = false;

			return item;
		}

		//

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
		public static Cylinder GetWire(ref Vector3 p1, ref Vector3 p2, float radius, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = Get(ref p1, ref p2, radius, radius, ref color, segments, drawEndAxes, duration);
			item.wireframe = true;

			return item;
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
		public static Cylinder GetWire(ref Vector3 p1, ref Vector3 p2, float radius1, float radius2, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = Get(ref p1, ref p2, radius1, radius2, ref color, segments, drawEndAxes, duration);
			item.wireframe = true;

			return item;
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
		public static Cylinder GetWire(ref Vector3 p1, ref Vector3 p2, Vector2 radius, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = Get(ref p1, ref p2, ref radius, ref radius, ref color, segments, drawEndAxes, duration);
			item.wireframe = true;

			return item;
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
		public static Cylinder GetWire(ref Vector3 p1, ref Vector3 p2, Vector2 radius1, Vector2 radius2, ref Color color, int segments = 32, bool drawEndAxes = false, EndTime duration = default)
		{
			Cylinder item = Get(ref p1, ref p2, ref radius1, ref radius2, ref color, segments, drawEndAxes, duration);
			item.wireframe = true;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Set the radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetRadius(float radius)
		{
			radius1.x = radius;
			radius1.y = radius;
			radius2.x = radius;
			radius2.y = radius;

			return this;
		}

		/// <summary>
		/// Set the radius.
		/// </summary>
		/// <param name="startRadius"></param>
		/// <param name="endRadius"></param>
		/// <returns></returns>
		public Cylinder SetRadius(float startRadius, float endRadius)
		{
			radius1.x = startRadius;
			radius1.y = startRadius;
			radius2.x = endRadius;
			radius2.y = endRadius;

			return this;
		}

		/// <summary>
		/// Set the radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetRadius(Vector2 radius)
		{
			radius1 = radius;
			radius2 = radius;

			return this;
		}

		/// <summary>
		/// Set the radius.
		/// </summary>
		/// <param name="startRadius"></param>
		/// <param name="endRadius"></param>
		/// <returns></returns>
		public Cylinder SetRadius(Vector2 startRadius, Vector2 endRadius)
		{
			radius1 = startRadius;
			radius2 = endRadius;

			return this;
		}

		/// <summary>
		/// Set start radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetStartRadius(float radius)
		{
			radius1.x = radius;
			radius1.y = radius;

			return this;
		}

		/// <summary>
		/// Set end radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetEndRadius(float radius)
		{
			radius2.x = radius;
			radius2.y = radius;

			return this;
		}

		/// <summary>
		/// Set start radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetStartRadius(Vector2 radius)
		{
			radius1 = radius;

			return this;
		}

		/// <summary>
		/// Set end radius.
		/// </summary>
		/// <param name="radius"></param>
		/// <returns></returns>
		public Cylinder SetEndRadius(Vector2 radius)
		{
			radius2 = radius;

			return this;
		}

		/// <summary>
		/// Sets the forward vector used to orient the cylinder.
		/// </summary>
		/// <param name="forward"></param>
		/// <returns></returns>
		public Cylinder SetForward(Vector3? forward)
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
			}

			int segments;

			if (this.segments <= 0)
			{
				float d1 = DebugDraw.DistanceFromCamera(ref p1) + Mathf.Max(radius1.x, radius1.y);
				float d2 = DebugDraw.DistanceFromCamera(ref p2) + Mathf.Max(radius2.x, radius2.y);
				segments = Mathf.Max(this.segments <= 0
					? Ellipse.DefaultAutoResolution(
						Mathf.Max(d1 > 0 && d2 > 0 ? Mathf.Min(d1, d2) : Mathf.Max(d1, d2), 0),
						Mathf.Max(Mathf.Max(radius1.x, radius1.y), Mathf.Max(radius2.x, radius2.y)))
					: this.segments, 4);
			}
			else
			{
				segments = Mathf.Max(this.segments, 4);
			}

			Color clr = GetColor(ref color);

			int p1StartIndex = mesh.vertexIndex;

			if (radius1.x != 0 || radius1.y != 0)
			{
				Ellipse.BuildArc(
					mesh, ref p1, ref right, ref forward, ref radius1, 0, 0,
					0, 360, segments, DrawArcSegments.Never,
					drawEndAxes ? DrawEllipseAxes.Always : DrawEllipseAxes.Never,
					ref clr, true);
			}
			else
			{
				mesh.AddVertex(ref p1);
				mesh.AddColor(ref clr);
				mesh.vertexIndex++;
			}

			int p1EndIndex = mesh.vertexIndex - 1;
			int p2StartIndex = mesh.vertexIndex;

			if (radius2.x != 0 || radius2.y != 0)
			{
				Ellipse.BuildArc(
					mesh, ref p2, ref right, ref forward, ref radius2, 0, 0,
					0, 360, segments, DrawArcSegments.Never,
					drawEndAxes ? DrawEllipseAxes.Always : DrawEllipseAxes.Never,
					ref clr, true);
			}
			else
			{
				mesh.AddVertex(ref p2);
				mesh.AddColor(ref clr);
				mesh.vertexIndex++;
			}

			int p2EndIndex = mesh.vertexIndex - 1;

			if (drawEndAxes)
			{
				p1EndIndex -= 8;
				p2EndIndex -= 8;
			}

			if (wireframe)
			{
				int ribs = Mathf.Max(
					p1EndIndex - p1StartIndex,
					p2EndIndex - p2StartIndex);

				for (int i = 0; i < ribs; i++)
				{
					mesh.AddIndices(
						Mathf.Min(p1StartIndex + i, p1EndIndex),
						Mathf.Min(p2StartIndex + i, p2EndIndex));
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
						p1.x + right.x * x * radius1.x + forward.x * y * radius1.y,
						p1.y + right.y * x * radius1.x + forward.y * y * radius1.y,
						p1.z + right.z * x * radius1.x + forward.z * y * radius1.y);
					mesh.AddVertex(
						p2.x + right.x * x * radius2.x + forward.x * y * radius2.y,
						p2.y + right.y * x * radius2.x + forward.y * y * radius2.y,
						p2.z + right.z * x * radius2.x + forward.z * y * radius2.y);
					mesh.AddColorX2(ref clr);
					mesh.AddIndexX2();
				}
			}
		}

		internal override void Release()
		{
			ItemPool<Cylinder>.Release(this);
		}

	}

}
