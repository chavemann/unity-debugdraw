using DebugDrawUtils.DebugDrawAttachments;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
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
