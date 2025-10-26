using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawUtils.Items
{

/// <summary>
/// Batch draw multiple 3d lines.
/// Both the positions, and colors list must be non-null and the same size.
/// Each line must have two entries, one for the start point and one for the end.
/// The sizes list must have item for each two in the positions list.
/// </summary>
public class Lines3D : BaseItem
	{
		
		/* mesh: triangle */
		
		/// <summary>
		/// The positions of the start and end points of each line.
		/// </summary>
		public List<Vector3> positions;
		
		/// <summary>
		/// The line thickness, one item for each pair of positions and colors.
		/// </summary>
		public List<float> sizes;
		
		/// <summary>
		/// The colors of the start and end points of each line.
		/// </summary>
		public List<Color> colors;
		
		/// <summary>
		/// If true adjusts the size of the line so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		
		/// <summary>
		/// The forward direction of the line. Automatically updated if faceCamera is true.
		/// If null the lines will automatically rotate to face the camera.
		/// </summary>
		public Vector3? facing;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a batch of 3D lines that orients itself towards the camera.
		/// </summary>
		/// <param name="positions">The positions of the start and end points of each line.</param>
		/// <param name="sizes">The line thickness, one item for each pair of positions and colors.</param>
		/// <param name="colors">The colors of the start and end points of each line.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Lines3D Get(List<Vector3> positions, List<float> sizes, List<Color> colors, EndTime? duration = null)
		{
			Lines3D item = ItemPool<Lines3D>.Get(duration);
			
			item.positions = positions;
			item.sizes = sizes;
			item.colors = colors;
			item.autoSize = false;
			item.facing = null;
			
			return item;
		}
		
		/// <summary>
		/// Draws a 3D line facing the given direction.
		/// </summary>
		/// <param name="positions">The positions of the start and end points of each line.</param>
		/// <param name="sizes">The line thickness, one item for each pair of positions and colors.</param>
		/// <param name="colors">The colors of the start and end points of each line.</param>
		/// <param name="facing">The forward direction of the line. Automatically updated if faceCamera is true.
		/// If null the lines will automatically rotate to face the camera.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Lines3D Get(List<Vector3> positions, List<float> sizes, List<Color> colors, Vector3? facing, EndTime? duration = null)
		{
			Lines3D item = ItemPool<Lines3D>.Get(duration);
			
			item.positions = positions;
			item.sizes = sizes;
			item.colors = colors;
			item.autoSize = false;
			item.facing = facing;
			
			return item;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		/// <summary>
		/// If true adjusts the size of the lines so it approximately remains the same size on screen.
		/// </summary>
		/// <param name="autoSize"></param>
		/// <returns></returns>
		public Lines3D SetAutoSize(bool autoSize = true)
		{
			this.autoSize = autoSize;
			
			return this;
		}
		
		/// <summary>
		/// If null the line will automatically rotate to face the camera.
		/// </summary>
		/// <param name="facing"></param>
		/// <returns></returns>
		public Lines3D SetFacing(Vector3? facing)
		{
			this.facing = facing;
			
			return this;
		}
		
		internal override void Build(DebugDrawMesh mesh)
		{
			ref Vector3 camP = ref DebugDraw.camPosition;
			bool hasStateTransform = this.hasStateTransform;
			ref Matrix4x4 stateTransform = ref this.stateTransform;
			bool hasStateColor = this.hasStateColor;
			ref Color stateColor = ref this.stateColor;
			bool autoSize = this.autoSize && !DebugDraw.camOrthographic;
			bool faceCamera = !this.facing.HasValue;
			Vector3 facing = this.facing.GetValueOrDefault();
			
			List<Vector3> positions = this.positions;
			List<float> sizes = this.sizes;
			List<Color> colors = this.colors;
			
			int vertexIndex = mesh.vertexIndex;
			List<Vector3> meshVertices = mesh.vertices;
			List<Color> meshColors = mesh.colours;
			List<int> meshIndices = mesh.indices;
			
			int sizeIndex = 0;
			
			for (int i = positions.Count - 2; i >= 0; i -= 2)
			{
				Vector3 p1 = hasStateTransform ? stateTransform.MultiplyPoint3x4(positions[i]) : positions[i];
				Vector3 p2 = hasStateTransform ? stateTransform.MultiplyPoint3x4(positions[i + 1]) : positions[i + 1];
				float size1 = sizes[sizeIndex++];
				float size2 = size1;
				Color clr1 = hasStateColor ? colors[i] * stateColor : colors[i];
				Color clr2 = hasStateColor ? colors[i + 1] * stateColor : colors[i + 1];
				
				Vector3 dir = new(
					p2.x - p1.x,
					p2.y - p1.y,
					p2.z - p1.z);
				float length = dir.magnitude;
				dir.x /= length;
				dir.y /= length;
				dir.z /= length;
				
				if (autoSize)
				{
					float dist1 = Vector3.Dot(new Vector3(
						p1.x - camP.x,
						p1.y - camP.y,
						p1.z - camP.z), DebugDraw.camForward);
					float dist2 = Vector3.Dot(new Vector3(
						p2.x - camP.x,
						p2.y - camP.y,
						p2.z - camP.z), DebugDraw.camForward);
					
					if (dist1 <= 0 && dist2 <= 0)
						return;
					
					if (dist1 <= 0 || dist2 <= 0)
					{
						if (dist1 <= 0)
						{
							float t = -dist1 / (dist2 - dist1);
							p1.x += dir.x * length * t;
							p1.y += dir.y * length * t;
							p1.z += dir.z * length * t;
							clr1 = Color.Lerp(clr1, clr2, t);
							size1 = 0;
							size2 *= Mathf.Max(dist2, 0) * BaseAutoSizeDistanceFactor;
						}
						else
						{
							float t = 1 - dist2 / (dist2 - dist1);
							p2.x = p1.x + dir.x * length * t;
							p2.y = p1.y + dir.y * length * t;
							p2.z = p1.z + dir.z * length * t;
							clr2 = Color.Lerp(clr1, clr2, t);
							size1 *= Mathf.Max(dist1, 0) * BaseAutoSizeDistanceFactor;
							size2 = 0;
						}
					}
					else
					{
						size1 *= Mathf.Max(dist1, 0) * BaseAutoSizeDistanceFactor;
						size2 *= Mathf.Max(dist2, 0) * BaseAutoSizeDistanceFactor;
					}
				}
				
				Vector3 n1, n2;
				
				if (faceCamera)
				{
					Vector3 d = new(
						p1.x - camP.x,
						p1.y - camP.y,
						p1.z - camP.z);
					d.Normalize();
					n1 = Vector3.Cross(d, dir);
					n1.Normalize();
					d.x = p1.x - camP.x;
					d.y = p1.y - camP.y;
					d.z = p1.z - camP.z;
					d.Normalize();
					n2 = Vector3.Cross(d, dir);
					n2.Normalize();
				}
				else
				{
					DebugDraw.FindAxisVectors(ref dir, ref facing, out _, out n1);
					n2 = n1;
				}
				
				meshVertices.Add(new Vector3(
					p1.x + n1.x * size1,
					p1.y + n1.y * size1,
					p1.z + n1.z * size1));
				meshVertices.Add(new Vector3(
					p1.x - n1.x * size1,
					p1.y - n1.y * size1,
					p1.z - n1.z * size1));
				meshVertices.Add(new Vector3(
					p2.x - n2.x * size2,
					p2.y - n2.y * size2,
					p2.z - n2.z * size2));
				meshVertices.Add(new Vector3(
					p2.x + n2.x * size2,
					p2.y + n2.y * size2,
					p2.z + n2.z * size2));
				meshColors.Add(clr1);
				meshColors.Add(clr1);
				meshColors.Add(clr2);
				meshColors.Add(clr2);
				// Tri 1
				meshIndices.Add(vertexIndex++);
				meshIndices.Add(vertexIndex++);
				meshIndices.Add(vertexIndex++);
				// Tri 2
				meshIndices.Add(vertexIndex++);
				meshIndices.Add(vertexIndex - 4);
				meshIndices.Add(vertexIndex - 2);
			}
			
			mesh.vertexIndex = vertexIndex;
		}
		
		internal override void Release()
		{
			ItemPool<Lines3D>.Release(this);
		}
		
	}

}
