using DebugDrawAttachments;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// An base item that has a position.
	/// </summary>
	public abstract class BasePointItem : BaseItem, IAttachablePoint
	{
		/// <summary>
		/// This item's position.
		/// </summary>
		public Vector3 position;

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		/// <summary>
		/// Attach this item to a GameObjects. This item and it's attachment will automatically expire
		/// if the attached object is destroyed.
		/// This is only applicable to certain items and will return null otherwise.
		/// </summary>
		/// <param name="obj">The object the start of the lines is attached to.</param>
		/// <returns></returns>
		public PointAttachment AttachTo(GameObjectOrTransform obj)
		{
			PointAttachment attachment = AttachmentPool<PointAttachment>.Get(this);
			attachment.item = this;
			attachment.pointItem = this;
			attachment.obj.Set(obj);
			return attachment;
		}

		public virtual void SetPosition(Vector3 position)
		{
			this.position = position;
		}

		public virtual Vector3 GetPosition()
		{
			return position;
		}

	}

}