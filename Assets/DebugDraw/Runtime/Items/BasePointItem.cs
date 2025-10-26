using DebugDrawUtils.Attachments;
using DebugDrawUtils.Utils;
using UnityEngine;

namespace DebugDrawUtils.Items
{

/// <summary>
/// A base item that has a position.
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
		attachment.Item = this;
		attachment.PointItem = this;
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
