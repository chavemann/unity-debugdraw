using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Square : BasePointItem
	{
		/* mesh: line */

		/// <summary>
		/// The half size of the square.
		/// </summary>
		public Vector2 size;
		/// <summary>
		/// The normal or direction the front of the square is facing.
		/// </summary>
		public Vector3 facing;
		/// <summary>
		/// True for a filled square made up from triangles, otherwise a wire ellipse.
		/// It's important that this Square item is added to mesh with the right topology, either lines or triangles,
		/// based on this setting.
		/// </summary>
		public bool filled;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a filled square.
		/// </summary>
		/// <param name="position">The centre of the square.</param>
		/// <param name="size">The half size of the square.</param>
		/// <param name="facing">The normal or direction the front of the square is facing.</param>
		/// <param name="color">The colour of the square.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Square Get(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, float duration = 0)
		{
			/* mesh: triangle */
			Square item = ItemPool<Square>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.filled = true;

			return item;
		}
		
		/// <summary>
		/// Draws a wire square.
		/// </summary>
		/// <param name="position">The centre of the square.</param>
		/// <param name="size">The half size of the square.</param>
		/// <param name="facing">The normal or direction the front of the square is facing.</param>
		/// <param name="color">The colour of the square.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Square GetWire(ref Vector3 position, ref Vector2 size, ref Vector3 facing, ref Color color, float duration = 0)
		{
			/* mesh: line */
			Square item = ItemPool<Square>.Get(duration);
			
			item.position = position;
			item.size = size;
			item.facing = facing;
			item.color = color;
			item.filled = false;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 position = this.position;
			DebugDraw.FindAxisVectors(ref facing, ref DebugDraw.up, out Vector3 up, out Vector3 right);
			
			if (hasStateTransform)
			{
				position = stateTransform.MultiplyPoint3x4(position);
				right = stateTransform.MultiplyVector(right);
				up = stateTransform.MultiplyVector(up);
			}
			
			Color clr = GetColor(ref color);
			
			mesh.AddVertex(
				position.x + right.x * -size.x + up.x * -size.y,
				position.y + right.y * -size.x + up.y * -size.y,
				position.z + right.z * -size.x + up.z * -size.y);
			mesh.AddVertex(
				position.x + right.x * +size.x + up.x * -size.y,
				position.y + right.y * +size.x + up.y * -size.y,
				position.z + right.z * +size.x + up.z * -size.y);
			mesh.AddVertex(
				position.x + right.x * +size.x + up.x * +size.y,
				position.y + right.y * +size.x + up.y * +size.y,
				position.z + right.z * +size.x + up.z * +size.y);
			mesh.AddVertex(
				position.x + right.x * -size.x + up.x * +size.y,
				position.y + right.y * -size.x + up.y * +size.y,
				position.z + right.z * -size.x + up.z * +size.y);
			mesh.AddColorX4(ref clr);

			if (filled)
			{
				mesh.AddIndexX3();
				mesh.AddIndices(
					mesh.vertexIndex++,
					mesh.vertexIndex - 4,
					mesh.vertexIndex - 2);
			}
			else
			{
				mesh.AddIndexX2();
				mesh.AddIndices(
					mesh.vertexIndex - 1,
					mesh.vertexIndex++);
				mesh.AddIndices(
					mesh.vertexIndex - 1,
					mesh.vertexIndex++);
				mesh.AddIndices(
					mesh.vertexIndex - 1,
					mesh.vertexIndex - 4);
			}
		}

		internal override void Release()
		{
			ItemPool<Square>.Release(this);
		}

	}

}