using DebugDrawItems;

namespace DebugDrawAttachments
{

	/// <summary>
	/// Attaches a debug <see cref="DebugDrawItems.Line"/> between two objects.
	/// </summary>
	public class LineAttachment : BaseAttachment
	{
		
		/// <summary>
		/// The object the start of the lines is attached to.
		/// </summary>
		public readonly AttachmentObject<LineAttachment> start;
		/// <summary>
		/// The object the end of the lines is attached to.
		/// </summary>
		public readonly AttachmentObject<LineAttachment> end;
		/// <summary>
		/// The item associated with this attachment.
		/// </summary>
		public BaseItem item { get; internal set; }
		/// <summary>
		/// The line item associated with this attachment.
		/// </summary>
		public IAttachableLine lineItem { get; internal set; }

		public LineAttachment()
		{
			start = new AttachmentObject<LineAttachment>(this);
			end = new AttachmentObject<LineAttachment>(this);
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override bool Update()
		{
			if (item.index == -1)
				return false;
			if (!start || !end)
				return false;

			lineItem.SetPositions(
				start.CalculatePosition(),
				end.CalculatePosition());

			return true;
		}

		internal override void Release()
		{
			if (item != null)
			{
				item.Remove();
				item = null;
				lineItem = null;
			}
			
			AttachmentPool<LineAttachment>.Release(this);
		}

	}

}