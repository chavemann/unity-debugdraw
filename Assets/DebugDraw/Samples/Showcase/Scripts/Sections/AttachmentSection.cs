using DebugDrawAttachments;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	public class AttachmentSection : BaseSection
	{

		public Transform point;
		public float pointSize = 2;
		public Vector3 pointOffset;

		public Icon line1, line2, line3, line4;
		public Vector2 line2Dist;
		
		public Transform bounds;

		private PointAttachment pointAttachment;
		private LineAttachment line1Attachment;
		private LineAttachment line2Attachment;
		private BoxAttachment boundsAttachment;

		protected override void Init()
		{
			DestroyAttachment(pointAttachment);
			DestroyAttachment(line1Attachment);
			DestroyAttachment(line2Attachment);
			DestroyAttachment(boundsAttachment);
			pointAttachment = null;
			line1Attachment = null;
			line2Attachment = null;
			boundsAttachment = null;
			
			if (point)
			{
				pointAttachment = DebugDraw.Text(default, "Hello", Showcase.NiceColor(), TextAnchor.LowerCenter, pointSize, -1)
					.SetUseWorldSize()
					.AttachTo(point)
					.obj.SetLocalOffset(pointOffset);
			}

			if (line1 && line2)
			{
				line1Attachment = DebugDraw.Line(default, default, line1.iconColor, line2.iconColor, -1)
					.AttachTo(line1, line2);
			}

			if (line3 && line4)
			{
				line2Attachment = DebugDraw.Line(default, default, line3.iconColor, line4.iconColor, -1)
					.AttachTo(line3, line4)
					.SetDistances(line2Dist.x, line2Dist.y);
			}

			if (line3 && line4)
			{
				line2Attachment = DebugDraw.Line(default, default, line3.iconColor, line4.iconColor, -1)
					.AttachTo(line3, line4)
					.SetDistances(line2Dist.x, line2Dist.y);
			}

			if (bounds)
			{
				boundsAttachment = DebugDraw.Box(default, 0, Showcase.NiceColor(), -1)
					.AttachTo(bounds);
			}
		}

		private void DestroyAttachment(BaseAttachment attachment)
		{
			if (attachment && attachment.isAlive)
			{
				attachment.Destroy();
			}
		}
		
	}

}