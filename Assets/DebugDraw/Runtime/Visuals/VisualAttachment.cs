using System.Runtime.CompilerServices;
using UnityEngine;

namespace Visuals
{

	public struct VisualAttachment
	{

		private Transform transform;
		private Vector3 localOffset;
		private Vector3 worldOffset;
		private bool hasLocalOffset;

		internal void Clear()
		{
			transform = null;
		}

		internal void Set(GameObject obj, ref Vector3 localOffset, ref Vector3 worldOffset)
		{
			transform = !ReferenceEquals(obj, null) && obj ? obj.transform : null;
			this.localOffset = localOffset;
			this.worldOffset = worldOffset;
			hasLocalOffset = localOffset != Vector3.zero;
		}

		internal void SetWithWorld(GameObject obj, ref Vector3 worldOffset)
		{
			transform = !ReferenceEquals(obj, null) && obj ? obj.transform : null;
			this.worldOffset = worldOffset;
			hasLocalOffset = false;
		}

		internal void SetWithLocal(GameObject obj, ref Vector3 localOffset)
		{
			transform = !ReferenceEquals(obj, null) && obj ? obj.transform : null;
			this.localOffset = localOffset;
			hasLocalOffset = localOffset != Vector3.zero;
		}

		internal void Set(GameObject obj)
		{
			transform = !ReferenceEquals(obj, null) && obj ? obj.transform : null;
			worldOffset = default;
			hasLocalOffset = false;
		}

		internal void SetOffset(ref Vector3 localOffset, ref Vector3 worldOffset)
		{
			this.localOffset = localOffset;
			this.worldOffset = worldOffset;
			hasLocalOffset = localOffset != Vector3.zero;
		}

		internal void SetWorldOffset(ref Vector3 worldOffset)
		{
			this.worldOffset = worldOffset;
		}

		internal void SetLocalOffset(ref Vector3 localOffset)
		{
			this.localOffset = localOffset;
			hasLocalOffset = false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 CalculatePosition()
		{
			return this
				? hasLocalOffset
					? transform.TransformPoint(localOffset) + worldOffset
					: transform.position + worldOffset
				: hasLocalOffset
					? localOffset + worldOffset
					: worldOffset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool(VisualAttachment obj)
		{
			return !ReferenceEquals(obj.transform, null) && obj.transform;
		}

	}

}