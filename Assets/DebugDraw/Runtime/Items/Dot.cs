using System.Runtime.CompilerServices;
using DebugDrawUtils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// A 3D "point". Can be made to always face the camera and adjust its size so it's always independent on the
	/// distance from the camera.
	/// </summary>
	public class Dot : BasePointItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The size of the dot.
		/// </summary>
		public float radius;
		/// <summary>
		/// If true adjusts the size of the dot so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		/// <summary>
		/// The forward direction of the dot. Automatically updated if faceCamera is true.
		/// </summary>
		public Vector3 facing;
		/// <summary>
		/// If true the dot will automatically rotate to face the camera.
		/// </summary>
		public bool faceCamera;
		/// <summary>
		/// The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.
		/// If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getter -- */

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
		public static Dot Get(ref Vector3 position, float radius, ref Color color, int segments = 0, EndTime? duration = null)
		{
			Dot item = ItemPool<Dot>.Get(duration);

			item.position = position;
			item.radius = radius;
			item.faceCamera = true;
			item.color = color;
			item.segments = segments;
			item.autoSize = false;

			return item;
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
		public static Dot Get(ref Vector3 position, float radius, ref Color color, ref Vector3 facing, int segments = 0, EndTime? duration = null)
		{
			Dot item = ItemPool<Dot>.Get(duration);

			item.position = position;
			item.radius = radius;
			item.facing = facing;
			item.faceCamera = false;
			item.color = color;
			item.segments = segments;
			item.autoSize = false;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// If true adjusts the size of the dot so it approximately remains the same size on screen.
		/// </summary>
		/// <param name="autoSize">.</param>
		/// <returns></returns>
		public Dot SetAutoSize(bool autoSize = true)
		{
			this.autoSize = autoSize;

			return this;
		}

		/// <summary>
		/// Sets <see cref="segments"/> to zero so that it will be calculated dynamically based
		/// on the distance to the camera.
		/// </summary>
		/// <returns></returns>
		public Dot SetAutoResolution()
		{
			segments = 0;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 position = this.position;
			Vector3 right, up;

			if (faceCamera)
			{
				right = DebugDraw.camRight;
				up = DebugDraw.camUp;
			}
			else
			{
				DebugDraw.FindAxisVectors(ref facing, ref DebugDraw.forward, out up, out right);
			}

			if (hasStateTransform)
			{
				if (faceCamera || autoSize)
				{
					Matrix4x4 m = Matrix4x4.TRS(
						DebugDraw.positionIdentity,
						faceCamera ? DebugDraw.rotationIdentity : stateTransform.rotation,
						autoSize ? DebugDraw.scaleIdentity : stateTransform.lossyScale);

					right = m.MultiplyVector(right);
					up = m.MultiplyVector(up);
				}
				else
				{
					right = stateTransform.MultiplyVector(right);
					up = stateTransform.MultiplyVector(up);
				}

				position = stateTransform.MultiplyPoint3x4(position);
			}

			float size = radius;

			float dist = autoSize || this.segments <= 0
				? Mathf.Max(DebugDraw.DistanceFromCamera(ref position), 0)
				: 0;

			if (autoSize && !DebugDraw.camOrthographic)
			{
				size *= dist * BaseAutoSizeDistanceFactor;
			}

			int segments = this.segments <= 0
				? Ellipse.DefaultAutoResolution(dist, size)
				: this.segments;

			Color clr = GetColor(ref color);

			if (segments < 3)
			{
				mesh.AddVertex(
					position.x + right.x * -size + up.x * -size,
					position.y + right.y * -size + up.y * -size,
					position.z + right.z * -size + up.z * -size);
				mesh.AddVertex(
					position.x + right.x * +size + up.x * -size,
					position.y + right.y * +size + up.y * -size,
					position.z + right.z * +size + up.z * -size);
				mesh.AddVertex(
					position.x + right.x * +size + up.x * +size,
					position.y + right.y * +size + up.y * +size,
					position.z + right.z * +size + up.z * +size);
				mesh.AddVertex(
					position.x + right.x * -size + up.x * +size,
					position.y + right.y * -size + up.y * +size,
					position.z + right.z * -size + up.z * +size);
				mesh.AddColorX4(ref clr);
				// Tri 1
				mesh.AddIndexX3();
				// Tri 2
				mesh.AddIndices(
					mesh.vertexIndex++,
					mesh.vertexIndex - 4,
					mesh.vertexIndex - 2);
			}
			else
			{
				mesh.AddVertex(ref position);
				mesh.AddColor(ref clr);
				int firstVertexIndex = mesh.vertexIndex;
				mesh.vertexIndex++;

				float angle = -Mathf.PI * 0.25f;
				float angleDelta = (Mathf.PI * 2) / segments;

				for (int i = 0, j = segments - 1; i < segments; j = i++)
				{
					Vector2 p = new Vector2(
						Mathf.Cos(angle) * size,
						Mathf.Sin(angle) * size);
					mesh.AddVertex(
						position.x + right.x * p.x + up.x * p.y,
						position.y + right.y * p.x + up.y * p.y,
						position.z + right.z * p.x + up.z * p.y);
					mesh.AddColor(ref clr);

					mesh.AddIndices(
						firstVertexIndex,
						firstVertexIndex + j + 1,
						mesh.vertexIndex++);

					angle += angleDelta;
				}
			}
		}

		internal override void Release()
		{
			ItemPool<Dot>.Release(this);
		}

	}

}
