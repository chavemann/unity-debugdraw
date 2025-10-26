using DebugDrawUtils.Items;

namespace DebugDrawUtils.Attachments
{

/// <summary>
/// Attaches a debug item to an object.
/// </summary>
public class PointAttachment : BaseAttachment
	{
		
		/// <summary>
		/// The attached object.
		/// </summary>
		public readonly AttachmentObject<PointAttachment> obj;
		
		/// <summary>
		/// The Debug item associated with this attachment.
		/// </summary>
		public BaseItem Item { get; internal set; }
		
		/// <summary>
		/// The point item associated with this attachment.
		/// </summary>
		public IAttachablePoint PointItem { get; internal set; }
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Init -- */
		
		public PointAttachment()
		{
			obj = new AttachmentObject<PointAttachment>(this);
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		internal override bool Update()
		{
			if (Item.index == -1)
				return false;
			if (!obj)
				return false;
			
			PointItem.SetPosition(obj.CalculatePosition());
			
			return true;
		}
		
		internal override void Release()
		{
			if (Item != null)
			{
				Item.Remove();
				Item = null;
				PointItem = null;
			}
			
			AttachmentPool<PointAttachment>.Release(this);
		}
		
	}

}
