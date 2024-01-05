using System.Runtime.CompilerServices;
using DebugDrawUtils.DebugDrawAttachments;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws a wireframe axis aligned box.
	/// </summary>
	public class Box : BaseItem
	{
		/* mesh: line */

		/// <summary>
		/// This item's position.
		/// </summary>
		public Vector3 position;
		/// <summary>
		/// The half size of the box.
		/// </summary>
		public Vector3 size;
		/// <summary>
		/// The orientation of the box.
		/// </summary>
		public Quaternion orientation;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, float size, ref Color color, EndTime? duration = null)
		{
			Vector3 s = new Vector3(size, size, size);
			return Get(ref position, ref s, ref color, duration);
		}

		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="orientation">The orientation of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, float size, ref Quaternion orientation, ref Color color, EndTime? duration = null)
		{
			Vector3 s = new Vector3(size, size, size);
			return Get(ref position, ref s, ref orientation, ref color, duration);
		}

		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, ref Vector3 size, ref Color color, EndTime? duration = null)
		{
			Box item = ItemPool<Box>.Get(duration);

			item.position = position;
			item.size = size;
			item.color = color;
			item.orientation = DebugDraw.rotationIdentity;

			return item;
		}

		/// <summary>
		/// Draws an axis aligned box.
		/// </summary>
		/// <param name="position">The centre of the box.</param>
		/// <param name="size">The half size of the box.</param>
		/// <param name="orientation">The orientation of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Box object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Box Get(ref Vector3 position, ref Vector3 size, ref Quaternion orientation, ref Color color, EndTime? duration = null)
		{
			Box item = ItemPool<Box>.Get(duration);

			item.position = position;
			item.size = size;
			item.color = color;
			item.orientation = orientation;

			return item;
		}

		/// <summary>
		/// Attach this box to a GameObjects. This item and it's attachment will automatically expire
		/// if the attached objects is destroyed.
		/// </summary>
		/// <param name="obj">The object the box is attached to.</param>
		/// <param name="updateSize">How to update the box size based on the game object's bounds.</param>
		/// <returns></returns>
		public BoxAttachment AttachTo(GameObjectOrTransform obj, BoxAttachmentSizeUpdate updateSize = BoxAttachmentSizeUpdate.Any)
		{
			BoxAttachment attachment = AttachmentPool<BoxAttachment>.Get(this);

			return attachment.Init(this, obj, updateSize);
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			Matrix4x4 m = Matrix4x4.TRS(position, orientation, size);

			// Top vertices
			Vector3 v1 = m.MultiplyPoint3x4(new Vector3(-1, -1, -1));
			Vector3 v2 = m.MultiplyPoint3x4(new Vector3(+1, -1, -1));
			Vector3 v3 = m.MultiplyPoint3x4(new Vector3(+1, -1, +1));
			Vector3 v4 = m.MultiplyPoint3x4(new Vector3(-1, -1, +1));
			mesh.AddVertices(this, ref v1, ref v2, ref v3, ref v4);
			// Bottom vertices
			v1 = m.MultiplyPoint3x4(new Vector3(-1, +1, -1));
			v2 = m.MultiplyPoint3x4(new Vector3(+1, +1, -1));
			v3 = m.MultiplyPoint3x4(new Vector3(+1, +1, +1));
			v4 = m.MultiplyPoint3x4(new Vector3(-1, +1, +1));
			mesh.AddVertices(this, ref v1, ref v2, ref v3, ref v4);

			Color clr = GetColor(ref color);
			mesh.AddColorX4(ref clr);
			mesh.AddColorX4(ref clr);

			int i = mesh.vertexIndex;

			// Top edges
			mesh.AddQuadLineIndices();
			// Bottom edges
			mesh.AddQuadLineIndices();
			// Sides
			mesh.AddIndices(
				i + 0, i + 4,
				i + 1, i + 5,
				i + 2, i + 6,
				i + 3, i + 7);
		}

		internal override void Release()
		{
			ItemPool<Box>.Release(this);
		}

	}

}
