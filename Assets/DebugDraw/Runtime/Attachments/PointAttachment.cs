// ReSharper disable once CheckNamespace

using DebugDrawUtils.DebugDrawItems;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawAttachments
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
		public BaseItem item { get; internal set; }
		
		/// <summary>
		/// The point item associated with this attachment.
		/// </summary>
		public IAttachablePoint pointItem { get; internal set; }
		
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
			if (item.index == -1)
				return false;
			if (!obj)
				return false;
			
			pointItem.SetPosition(obj.CalculatePosition());
			
			return true;
		}
		
		internal override void Release()
		{
			if (item != null)
			{
				item.Remove();
				item = null;
				pointItem = null;
			}
			
			AttachmentPool<PointAttachment>.Release(this);
		}
		
	}

}
