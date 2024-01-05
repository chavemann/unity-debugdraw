using System.Runtime.CompilerServices;
using DebugDrawUtils.DebugDrawAttachments;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// A basic line.
	/// </summary>
	public class Line : BaseLineItem, IAttachablePoint
	{
		/* mesh: line */

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line Get(ref Vector3 p1, ref Vector3 p2, ref Color color1, ref Color color2, EndTime? duration = null)
		{
			Line item = ItemPool<Line>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.color = color1;
			item.color2 = color2;

			return item;
		}

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color">The line's colour.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Line Get(ref Vector3 p1, ref Vector3 p2, ref Color color, EndTime? duration = null)
		{
			return Get(ref p1, ref p2, ref color, ref color, duration);
		}

		public PointAttachment AttachTo(GameObjectOrTransform obj)
		{
			PointAttachment attachment = AttachmentPool<PointAttachment>.Get(this);
			attachment.item = this;
			attachment.pointItem = this;
			attachment.obj.Set(obj);
			return attachment;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override void Build(DebugDrawMesh mesh)
		{
			Color clr1 = GetColor(ref color);
			Color clr2 = GetColor(ref color2);

			mesh.AddLine(this, ref p1, ref p2, ref clr1, ref clr2);
		}

		internal override void Release()
		{
			ItemPool<Line>.Release(this);
		}

		public void SetPosition(Vector3 position)
		{
			Vector3 delta = new Vector3(
				position.x - (p1.x + p2.x) * 0.5f,
				position.y - (p1.y + p2.y) * 0.5f,
				position.z - (p1.z + p2.z) * 0.5f);

			p1.x += delta.x;
			p1.y += delta.y;
			p1.z += delta.z;
			p2.x += delta.x;
			p2.y += delta.y;
			p2.z += delta.z;
		}

		public Vector3 GetPosition()
		{
			return new Vector3(
				(p1.x + p2.x) * 0.5f,
				(p1.y + p2.y) * 0.5f,
				(p1.z + p2.z) * 0.5f);
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Util -- */

		/// <summary>
		/// Clamps the line from p1 to p2 to the specified lengths.
		/// </summary>
		/// <param name="p1">The line start point.</param>
		/// <param name="p2">The line end point.</param>
		/// <param name="minLength">The min length. Set to a negative value for no lower limit.</param>
		/// <param name="maxLength">The min length. Set to a negative value for no upper limit.</param>
		public static void Clamp(ref Vector3 p1, ref Vector3 p2, float minLength, float maxLength)
		{
			Vector3 delta = new Vector3(
				p2.x - p1.x,
				p2.y - p1.y,
				p2.z - p1.z);
			float length = delta.sqrMagnitude;

			if(minLength > 0 && length < minLength * minLength)
			{
				length = 1 / Mathf.Sqrt(length) * minLength;
			}
			else if(float.IsPositiveInfinity(maxLength) && length > maxLength * maxLength)
			{
				length = 1 / Mathf.Sqrt(length) * maxLength;
			}
			else
			{
				return;
			}

			p2.x = p1.x + delta.x * length;
			p2.y = p1.y + delta.y * length;
			p2.z = p1.z + delta.z * length;
		}

	}

}
