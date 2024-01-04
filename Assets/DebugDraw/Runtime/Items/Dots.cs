using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugDrawUtils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Batch draw multiple lines.
	/// Each of the positions, sizes,s and colors list must be non null and the same size.
	/// </summary>
	public class Dots : BaseItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The positions of each dot.
		/// </summary>
		public List<Vector3> positions;
		/// <summary>
		/// The sizes each dot.
		/// </summary>
		public List<float> sizes;
		/// <summary>
		/// The colors each dot.
		/// </summary>
		public List<Color> colors;

		/// <summary>
		/// If true adjusts the size of all dot so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		/// <summary>
		/// The forward direction of all dots. Automatically updated if faceCamera is true.
		/// </summary>
		public Vector3 facing;
		/// <summary>
		/// If true all dots will automatically rotate to face the camera.
		/// </summary>
		public bool faceCamera;
		/// <summary>
		/// The shape/resolution of all dots. 0 or 4 = square, >= 3 = circle.
		/// If set to zero will be adjusted based on the distance to the camera.
		/// </summary>
		public int segments;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

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
		public static Dots Get(List<Vector3> positions, List<float> sizes, List<Color> colors, int segments = 0, EndTime duration = default)
		{
			Dots item = ItemPool<Dots>.Get(duration);

			item.positions = positions;
			item.sizes = sizes;
			item.colors = colors;
			item.faceCamera = true;
			item.segments = segments;

			return item;
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
		public static Dots Get(List<Vector3> positions, List<float> sizes, List<Color> colors, ref Vector3 facing, int segments = 0, EndTime duration = default)
		{
			Dots item = ItemPool<Dots>.Get(duration);

			item.positions = positions;
			item.sizes = sizes;
			item.colors = colors;
			item.facing = facing;
			item.faceCamera = false;
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
		public Dots SetAutoSize(bool autoSize = true)
		{
			this.autoSize = autoSize;

			return this;
		}

		/// <summary>
		/// Sets <see cref="segments"/> to zero so that it will be calculated dynamically based
		/// on the distance to the camera.
		/// </summary>
		/// <returns></returns>
		public Dots SetAutoResolution()
		{
			segments = 0;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			bool hasStateTransform = this.hasStateTransform;
			ref Matrix4x4 stateTransform = ref this.stateTransform;
			bool hasStateColor = this.hasStateColor;
			ref Color stateColor = ref this.stateColor;
			bool autoSize = this.autoSize && !DebugDraw.camOrthographic;
			bool autoResolution = segments <= 0;

			List<Vector3> positions = this.positions;
			List<float> sizes = this.sizes;
			List<Color> colors = this.colors;

			int vertexIndex = mesh.vertexIndex;
			List<Vector3> meshVertices = mesh.vertices;
			List<Color> meshColors = mesh.colours;
			List<int> meshIndices = mesh.indices;

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

			if (faceCamera || autoSize)
			{
				Matrix4x4 m = Matrix4x4.TRS(
					DebugDraw.positionIdentity,
					faceCamera || !hasStateTransform ? DebugDraw.rotationIdentity : stateTransform.rotation,
					autoSize || !hasStateTransform ? DebugDraw.scaleIdentity : stateTransform.lossyScale);

				right = m.MultiplyVector(right);
				up = m.MultiplyVector(up);
			}
			else
			{
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}

			for (int i = positions.Count - 1; i >= 0; i--)
			{
				Vector3 position = positions[i];
				float size = sizes[i];
				Color clr = hasStateColor ? colors[i] * stateColor : colors[i];

				if (hasStateTransform)
				{
					position = stateTransform.MultiplyPoint3x4(position);
				}

				float dist = autoSize || autoResolution
					? Mathf.Max(DebugDraw.DistanceFromCamera(ref position), 0)
					: 0;

				if (autoSize)
				{
					size *= dist * BaseAutoSizeDistanceFactor;
				}

				int segments = autoResolution
					? Ellipse.DefaultAutoResolution(dist, size)
					: this.segments;

				if (segments < 3)
				{
					meshVertices.Add(new Vector3(
						position.x + right.x * -size + up.x * -size,
						position.y + right.y * -size + up.y * -size,
						position.z + right.z * -size + up.z * -size));
					meshVertices.Add(new Vector3(
						position.x + right.x * +size + up.x * -size,
						position.y + right.y * +size + up.y * -size,
						position.z + right.z * +size + up.z * -size));
					meshVertices.Add(new Vector3(
						position.x + right.x * +size + up.x * +size,
						position.y + right.y * +size + up.y * +size,
						position.z + right.z * +size + up.z * +size));
					meshVertices.Add(new Vector3(
						position.x + right.x * -size + up.x * +size,
						position.y + right.y * -size + up.y * +size,
						position.z + right.z * -size + up.z * +size));
					meshColors.Add(clr);
					meshColors.Add(clr);
					meshColors.Add(clr);
					meshColors.Add(clr);
					// Tri 1
					meshIndices.Add(vertexIndex++);
					meshIndices.Add(vertexIndex++);
					meshIndices.Add(vertexIndex++);
					// Tri 2
					meshIndices.Add(vertexIndex++);
					meshIndices.Add(vertexIndex - 4);
					meshIndices.Add(vertexIndex - 2);
				}
				else
				{
					meshVertices.Add(position);
					meshColors.Add(clr);
					int firstVertexIndex = vertexIndex;
					vertexIndex++;

					float angle = -Mathf.PI * 0.25f;
					float angleDelta = (Mathf.PI * 2) / segments;

					for (int j = 0, k = segments - 1; j < segments; k = j++)
					{
						Vector2 p = new Vector2(
							Mathf.Cos(angle) * size,
							Mathf.Sin(angle) * size);
						meshVertices.Add(new Vector3(
							position.x + right.x * p.x + up.x * p.y,
							position.y + right.y * p.x + up.y * p.y,
							position.z + right.z * p.x + up.z * p.y));
						meshColors.Add(clr);

						meshIndices.Add(firstVertexIndex);
						meshIndices.Add(firstVertexIndex + k + 1);
						meshIndices.Add(vertexIndex++);

						angle += angleDelta;
					}
				}
			}

			mesh.vertexIndex = vertexIndex;
		}

		internal override void Release()
		{
			ItemPool<Dots>.Release(this);
		}

	}

}
