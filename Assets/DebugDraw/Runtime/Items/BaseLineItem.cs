using DebugDrawAttachments;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawItems
{

	public abstract class BaseLineItem : BaseItem, IAttachableLine
	{
		
		/// <summary>
		/// Start point.
		/// </summary>
		public Vector3 p1;
		/// <summary>
		/// End point.
		/// </summary>
		public Vector3 p2;
		/// <summary>
		/// The end point color.
		/// </summary>
		public Color color2;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Attach this item to one or more GameObjects. This item and it's attachment will automatically expire
		/// if any of the attached objects are destroyed.
		/// </summary>
		/// <param name="startObj">The object the start of the lines is attached to.</param>
		/// <param name="endObj">The object the end of the lines is attached to.</param>
		/// <returns></returns>
		public LineAttachment AttachTo(GameObjectOrTransform startObj, GameObjectOrTransform endObj)
		{
			LineAttachment attachment = AttachmentPool<LineAttachment>.Get(this);
			attachment.item = this;
			attachment.lineItem = this;
			attachment.start.Set(startObj);
			attachment.end.Set(endObj);
			return attachment;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		public void SetPositions(Vector3 start, Vector3 end)
		{
			p1 = start;
			p2 = end;
		}

		public void SetStartPosition(Vector3 position)
		{
			p1 = position;
		}

		public void SetEndPosition(Vector3 position)
		{
			p2 = position;
		}

		public Vector3 GetStartPosition()
		{
			return p1;
		}

		public Vector3 GetEndPosition()
		{
			return p2;
		}

	}

}