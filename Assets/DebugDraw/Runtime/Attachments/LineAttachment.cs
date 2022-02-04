using System.Runtime.CompilerServices;
using Items;
using UnityEngine;

namespace Attachments
{

	public class LineAttachment : BaseAttachment
	{

		private AttachedObj start;
		private AttachedObj end;
		private Line line;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LineAttachment Line(
			GameObject startObj, GameObject endObj,
			Vector3 startLocalOffset, Vector3 endLocalOffset,
			Vector3 startWorldOffset, Vector3 endWorldOffset,
			Color color1, Color color2, float duration = -1)
		{
			LineAttachment item = AttachmentPool<LineAttachment>.Get(duration);
			
			item.start.Set(startObj, ref startLocalOffset, ref startWorldOffset);
			item.end.Set(endObj, ref endLocalOffset, ref endWorldOffset);
			item.line = Items.Line.Get(ref vector3Ref, ref vector3Ref, ref color1, ref color2, -1);
			// DebugDraw.lineMeshInstance.Add(item.line);

			return item;
		}

		public LineAttachment SetStartOffsets(Vector3 localOffset, Vector3 worldOffset)
		{
			start.localOffset = localOffset;
			start.worldOffset = worldOffset;
			return this;
		}

		public LineAttachment SetEndOffsets(Vector3 localOffset, Vector3 worldOffset)
		{
			end.localOffset = localOffset;
			end.worldOffset = worldOffset;
			return this;
		}

		public LineAttachment SetOffsets(
			Vector3 startLocalOffset, Vector3 endLocalOffset,
			Vector3 startWorldOffset, Vector3 endWorldOffset)
		{
			start.localOffset = startLocalOffset;
			start.worldOffset = startWorldOffset;
			end.localOffset = endLocalOffset;
			end.worldOffset = endWorldOffset;
			return this;
		}

		internal override bool Update()
		{
			if (!start && !end)
			{
				return false;
			}

			line.p1 = start.CalculatePosition();
			line.p2 = end.CalculatePosition();

			return true;
		}

		internal override void Release()
		{
			if (!ReleaseInternal())
				return;

			if (line != null)
			{
				line.Remove();
				line = null;
			}
			
			start.Clear();
			end.Clear();
			
			AttachmentPool<LineAttachment>.Release(this);
		}

	}

}