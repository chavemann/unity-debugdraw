using DebugDrawItems;
using UnityEngine;

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
		/// Moves the attachment point along the line away from the start point.
		/// </summary>
		public float startDistance;
		/// <summary>
		/// Moves the attachment point along the line away from the end point.
		/// </summary>
		public float endDistance;
		/// <summary>
		/// The item associated with this attachment.
		/// </summary>
		public BaseItem item { get; internal set; }
		/// <summary>
		/// The line item associated with this attachment.
		/// </summary>
		public IAttachableLine lineItem { get; internal set; }

		/* ------------------------------------------------------------------------------------- */
		/* -- Init -- */

		public LineAttachment()
		{
			start = new AttachmentObject<LineAttachment>(this);
			end = new AttachmentObject<LineAttachment>(this);
		}

		public LineAttachment SetDistances(float start = 0, float end = 0)
		{
			startDistance = start;
			endDistance = end;

			return this;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		internal override bool Update()
		{
			if (item.index == -1)
				return false;
			if (!start || !end)
				return false;

			if (startDistance != 0 || endDistance != 0)
			{
				Vector3 p1 = start.CalculatePosition();
				Vector3 p2 = end.CalculatePosition();
				Vector3 n = new Vector3(
					p2.x - p1.x,
					p2.y - p1.y,
					p2.z - p1.z);
				n.Normalize();

				if (startDistance != 0)
				{
					p1.x += n.x * startDistance;
					p1.y += n.y * startDistance;
					p1.z += n.z * startDistance;
				}

				if (endDistance != 0)
				{
					p2.x -= n.x * endDistance;
					p2.y -= n.y * endDistance;
					p2.z -= n.z * endDistance;
				}
				
				lineItem.SetPositions(p1, p2);
			}
			else
			{
				lineItem.SetPositions(
					start.CalculatePosition(),
					end.CalculatePosition());
			}

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