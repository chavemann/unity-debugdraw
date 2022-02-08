using System.Runtime.CompilerServices;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// A 3D "point". Can be made to always face the camera and adjust its size so it's always independent on the
	/// distance from the camera.
	/// </summary>
	public class Dot : BaseItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The position of the dot.
		/// </summary>
		public Vector3 position;
		/// <summary>
		/// The size of the dot.
		/// </summary>
		public float radius;
		/// <summary>
		/// If true adjusts the size of the dot so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		/// <summary>
		/// The forward direction of the dot. Automatically update if faceCamera is true.
		/// </summary>
		public Vector3 facing;
		/// <summary>
		/// If true the dot will automatically rotate to face the camera.
		/// </summary>
		public bool faceCamera;
		/// <summary>
		/// The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.
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
		/// <param name="segments">The shape/resolution of the dot. 0 or 4 = square, >= 3 = circle.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Dot object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dot Get(ref Vector3 position, float radius, ref Color color, int segments = 0, float duration = 0)
		{
			Dot item = ItemPool<Dot>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.faceCamera = true;
			item.color = color;
			item.segments = segments;

			return item;
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
		public static Dot Get(ref Vector3 position, float radius, ref Color color, ref Vector3 facing, int segments = 0, float duration = 0)
		{
			Dot item = ItemPool<Dot>.Get(duration);
			
			item.position = position;
			item.radius = radius;
			item.facing = facing;
			item.faceCamera = false;
			item.color = color;
			item.segments = segments; 

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

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 forward, right, up;
			
			if (faceCamera)
			{
				forward = DebugDraw.camForward;
				right = DebugDraw.camRight;
				up = DebugDraw.camUp;
			}
			else
			{
				forward = facing;
				MathUtils.FindBestAxisVectors(ref forward, out up, out right);
			}

			Matrix4x4 m = hasStateTransform
				? stateTransform * new Matrix4x4(right, up, forward, new Vector4(position.x, position.y, position.z, 1))
				: new Matrix4x4(right, up, forward, new Vector4(position.x, position.y, position.z, 1));

			float size = radius;

			if (autoSize)
			{
				Vector3 worldPos = hasStateTransform
					? m.MultiplyPoint3x4(position)
					: position;
				
				size *= new Vector3(
					worldPos.x - DebugDraw.camPosition.x,
					worldPos.y - DebugDraw.camPosition.y,
					worldPos.z - DebugDraw.camPosition.z).magnitude * BaseAutoSizeDistanceFactor;
			}
			
			Color clr = GetColor(ref color);
			
			if (segments < 3)
			{
				mesh.AddVertex(ref m, -size, -size);
				mesh.AddVertex(ref m, +size, -size);
				mesh.AddVertex(ref m, +size, +size);
				mesh.AddVertex(ref m, -size, +size);
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
				mesh.AddVertex(ref m, 0, 0, 0);
				mesh.AddColor(ref clr);
				int firstVertexIndex = mesh.vertexIndex;
				mesh.vertexIndex++;
				
				float angle = -Mathf.PI * 0.25f;
				float angleDelta = (Mathf.PI * 2) / segments;

				for (int i = 0, j = segments - 1; i < segments; j = i++)
				{
					mesh.AddVertex(ref m,
						Mathf.Cos(angle) * size,
						Mathf.Sin(angle) * size);
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