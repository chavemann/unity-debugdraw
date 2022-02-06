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
		/// The Debug Line item associated with this attachment.
		/// </summary>
		public Line line { get; internal set; }

		public LineAttachment()
		{
			start = new AttachmentObject<LineAttachment>(this);
			end = new AttachmentObject<LineAttachment>(this);
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override bool Update()
		{
			if (line.index == -1)
				return false;
			if (!start || !end)
				return false;

			line.p1 = start.CalculatePosition();
			line.p2 = end.CalculatePosition();

			return true;
		}

		internal override void Release()
		{
			if (line != null)
			{
				line.Remove();
				line = null;
			}
			
			AttachmentPool<LineAttachment>.Release(this);
		}

	}

}