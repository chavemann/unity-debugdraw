using System.Runtime.CompilerServices;
using Items;
using UnityEngine;

namespace Visuals
{

	// TODO: Scrap this system and instead use Item.attach(*) methods.
	//       This way the Item.Get init code doesn't have to be duplicated
	
	/// <summary>
	/// TODO:
	/// </summary>
	public class LineVisual : BaseVisual
	{

		/// <summary>
		/// TOD:
		/// </summary>
		public VisualAttachment start;
		/// <summary>
		/// TOD:
		/// </summary>
		public VisualAttachment end;
		/// <summary>
		/// TOD:
		/// </summary>
		public Line line { get; private set; }
		// TODO: Arrow support
		
		/// <summary>
		/// TODO: 
		/// </summary>
		/// <param name="startObj"></param>
		/// <param name="endObj"></param>
		/// <param name="startLocalOffset"></param>
		/// <param name="endLocalOffset"></param>
		/// <param name="startWorldOffset"></param>
		/// <param name="endWorldOffset"></param>
		/// <param name="color1"></param>
		/// <param name="color2"></param>
		/// <param name="duration"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LineVisual Line(
			GameObject startObj, GameObject endObj,
			Vector3 startLocalOffset, Vector3 endLocalOffset,
			Vector3 startWorldOffset, Vector3 endWorldOffset,
			Color color1, Color color2, float duration = -1)
		{
			LineVisual item = VisualPool<LineVisual>.Get(duration);

			item.start.Set(startObj, ref startLocalOffset, ref startWorldOffset);
			item.end.Set(endObj, ref endLocalOffset, ref endWorldOffset);
			item.line = Items.Line.Get(ref vector3Ref, ref vector3Ref, ref color1, ref color2, -1);
			DebugDraw.lineMeshInstance?.Add(item.line);

			return item;
		}

		// TODO: Wrapper methods Get/Line methods

		/// <summary>
		/// TODO:
		/// </summary>
		/// <param name="localOffset"></param>
		/// <param name="worldOffset"></param>
		/// <returns></returns>
		public LineVisual SetStartOffsets(Vector3 localOffset, Vector3 worldOffset)
		{
			start.SetOffset(ref localOffset, ref worldOffset);
			return this;
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <param name="localOffset"></param>
		/// <param name="worldOffset"></param>
		/// <returns></returns>
		public LineVisual SetEndOffsets(Vector3 localOffset, Vector3 worldOffset)
		{
			end.SetOffset(ref localOffset, ref worldOffset);
			return this;
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <param name="startLocalOffset"></param>
		/// <param name="endLocalOffset"></param>
		/// <param name="startWorldOffset"></param>
		/// <param name="endWorldOffset"></param>
		/// <returns></returns>
		public LineVisual SetOffsets(
			Vector3 startLocalOffset, Vector3 endLocalOffset,
			Vector3 startWorldOffset, Vector3 endWorldOffset)
		{
			start.SetOffset(ref startLocalOffset, ref startWorldOffset);
			end.SetOffset(ref endLocalOffset, ref endWorldOffset);
			return this;
		}

		internal override bool Update()
		{
			if (!start && !end)
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
			
			start.Clear();
			end.Clear();
			
			VisualPool<LineVisual>.Release(this);
		}

	}

}