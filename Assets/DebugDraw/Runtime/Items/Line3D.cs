using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

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
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Color color1, ref Color color2, float duration = 0)
		{
			Line3D item = ItemPool<Line3D>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.size = size;
			item.color = color1;
			item.color2 = color2;
			item.faceCamera = true;

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
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Color color, float duration = 0)
		{
			return Get(ref p1, ref p2, size, ref color, ref color, duration);
		}
		
		/// <summary>
		/// Draws a 3D line that orients itself towards the camera.
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
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Vector3 facing, ref Color color1, ref Color color2, float duration = 0)
		{
			Line3D item = ItemPool<Line3D>.Get(duration);
			
			item.p1 = p1;
			item.p2 = p2;
			item.size = size;
			item.facing = facing;
			item.color = color1;
			item.color2 = color2;
			item.faceCamera = false;

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
		public static Line3D Get(ref Vector3 p1, ref Vector3 p2, float size, ref Vector3 facing, ref Color color, float duration = 0)
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
			// Vector3 forward, right, up;
			//
			// if (faceCamera)
			// {
			// 	forward = DebugDraw.camForward;
			// 	right = DebugDraw.camRight;
			// 	up = DebugDraw.camUp;
			// }
			// else
			// {
			// 	forward = facing;
			// 	DebugDraw.FindAxisVectors(ref forward, ref DebugDraw.up, out up, out right);
			// }

			float size1 = size;
			float size2 = size;
			Color clr1 = GetColor(ref color);
			Color clr2 = GetColor(ref color2);
			
			Vector3 dir = new Vector3(
				p2.x - p1.x,
				p2.y - p1.y,
				p2.z - p1.z);
			float length = dir.magnitude;
			dir.x /= length;
			dir.y /= length;
			dir.z /= length;
			
			if(autoSize)
			{
				size1 *= Mathf.Max(Vector3.Dot(new Vector3(
					p1.x - DebugDraw.camPosition.x,
					p1.y - DebugDraw.camPosition.y,
					p1.z - DebugDraw.camPosition.z), DebugDraw.camForward), 0) * BaseAutoSizeDistanceFactor;
				size2 *= Mathf.Max(Vector3.Dot(new Vector3(
					p2.x - DebugDraw.camPosition.x,
					p2.y - DebugDraw.camPosition.y,
					p2.z - DebugDraw.camPosition.z), DebugDraw.camForward), 0) * BaseAutoSizeDistanceFactor;
			}

			Vector3 n;
			
			if(faceCamera)
			{
				n = Vector3.Cross(DebugDraw.camForward, dir);
				n.Normalize();
			}
			else
			{
				DebugDraw.FindAxisVectors(ref dir, ref facing, out _, out n);
			}

			mesh.AddVertex(
				p1.x + n.x * size1,
				p1.y + n.y * size1,
				p1.z + n.z * size1);
			mesh.AddVertex(
				p1.x - n.x * size1,
				p1.y - n.y * size1,
				p1.z - n.z * size1);
			mesh.AddVertex(
				p2.x - n.x * size2,
				p2.y - n.y * size2,
				p2.z - n.z * size2);
			mesh.AddVertex(
				p2.x + n.x * size2,
				p2.y + n.y * size2,
				p2.z + n.z * size2);
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