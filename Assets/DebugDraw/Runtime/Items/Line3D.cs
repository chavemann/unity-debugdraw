using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawUtils.Items
{

/// <summary>
/// Draws a 3D line using two triangles.
/// </summary>
public class Line3D : Line
	{
		
		/* mesh: triangle */
		
		/// <summary>
		/// The line thickness.
		/// </summary>
		public float size;
		
		/// <summary>
		/// If true adjusts the size of the line so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		
		/// <summary>
		/// The forward direction of the line. Automatically updated if faceCamera is true.
		/// </summary>
		public Vector3 facing;
		
		/// <summary>
		/// If true the line will automatically rotate to face the camera.
		/// </summary>
		public bool faceCamera;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a 3D line that orients itself towards the camera.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="size">The line thickness.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Color color1, ref Color color2, EndTime? duration = null)
		{
			Line3D item = ItemPool<Line3D>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.size = size;
			item.color = color1;
			item.color2 = color2;
			item.faceCamera = true;
			item.autoSize = false;
			
			return item;
		}
		
		/// <summary>
		/// Draws a 3D line that orients itself towards the camera.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="size">The line thickness.</param>
		/// <param name="color">The line's colour.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
		/// <returns>The Line3D object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Color color, EndTime? duration = null)
		{
			return Get(ref p1, ref p2, size, ref color, ref color, duration);
		}
		
		/// <summary>
		/// Draws a 3D line facing the given direction.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="size">The line thickness.</param>
		/// <param name="facing">The forward direction of the line.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Vector3 facing, ref Color color1, ref Color color2, EndTime? duration = null)
		{
			Line3D item = ItemPool<Line3D>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.size = size;
			item.facing = facing;
			item.color = color1;
			item.color2 = color2;
			item.faceCamera = false;
			item.autoSize = false;
			
			return item;
		}
		
		/// <summary>
		/// Draws a 3D line that orients itself towards the camera.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="size">The line thickness.</param>
		/// <param name="facing">The forward direction of the line.</param>
		/// <param name="color">The line's colour.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
		/// <returns>The Line3D object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Vector3 facing, ref Color color, EndTime? duration = null)
		{
			return Get(ref p1, ref p2, size, ref facing, ref color, ref color, duration);
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		/// <summary>
		/// If true adjusts the size of the line so it approximately remains the same size on screen.
		/// </summary>
		/// <param name="autoSize"></param>
		/// <returns></returns>
		public Line3D SetAutoSize(bool autoSize = true)
		{
			this.autoSize = autoSize;
			
			return this;
		}
		
		/// <summary>
		/// If true the line will automatically rotate to face the camera.
		/// </summary>
		/// <param name="faceCamera"></param>
		/// <returns></returns>
		public Line3D SetFaceCamera(bool faceCamera = true)
		{
			this.faceCamera = faceCamera;
			
			return this;
		}
		
		internal override void Build(DebugDrawMesh mesh)
		{
			float size1 = size;
			float size2 = size;
			Color clr1 = GetColor(ref color);
			Color clr2 = GetColor(ref color2);
			
			ref Vector3 camP = ref DebugDraw.camPosition;
			Vector3 p1 = hasStateTransform ? stateTransform.MultiplyPoint3x4(this.p1) : this.p1;
			Vector3 p2 = hasStateTransform ? stateTransform.MultiplyPoint3x4(this.p2) : this.p2;
			
			Vector3 dir = new(
				p2.x - p1.x,
				p2.y - p1.y,
				p2.z - p1.z);
			float length = dir.magnitude;
			dir.x /= length;
			dir.y /= length;
			dir.z /= length;
			
			if (autoSize && !DebugDraw.camOrthographic)
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
			
			mesh.AddVertex(
				p1.x + n1.x * size1,
				p1.y + n1.y * size1,
				p1.z + n1.z * size1);
			mesh.AddVertex(
				p1.x - n1.x * size1,
				p1.y - n1.y * size1,
				p1.z - n1.z * size1);
			mesh.AddVertex(
				p2.x - n2.x * size2,
				p2.y - n2.y * size2,
				p2.z - n2.z * size2);
			mesh.AddVertex(
				p2.x + n2.x * size2,
				p2.y + n2.y * size2,
				p2.z + n2.z * size2);
			mesh.AddColorX2(ref clr1);
			mesh.AddColorX2(ref clr2);
			// Tri 1
			mesh.AddIndexX3();
			// Tri 2
			mesh.AddIndices(
				mesh.vertexIndex++,
				mesh.vertexIndex - 4,
				mesh.vertexIndex - 2);
		}
		
		internal override void Release()
		{
			ItemPool<Line3D>.Release(this);
		}
		
	}

}
