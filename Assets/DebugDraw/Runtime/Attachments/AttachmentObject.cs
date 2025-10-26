using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawAttachments
{
	
public class AttachmentObject<T> where T : BaseAttachment
{
	
	internal readonly T attachment;
	internal bool hasTransform;
	internal Transform transform;
	internal Vector3 localOffset;
	internal Vector3 worldOffset;
	internal bool hasLocalOffset;
	
	public AttachmentObject(T attachment)
	{
		this.attachment = attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Set(Transform tr)
	{
		SetTransform(tr);
		worldOffset = default;
		hasLocalOffset = false;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Set(Transform tr, Vector3 localOffset, Vector3 worldOffset)
	{
		SetTransform(tr);
		this.localOffset = localOffset;
		this.worldOffset = worldOffset;
		hasLocalOffset = localOffset != Vector3.zero;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T SetWithWorld(Transform tr, Vector3 worldOffset)
	{
		SetTransform(tr);
		this.worldOffset = worldOffset;
		hasLocalOffset = false;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T SetWithLocal(Transform tr, Vector3 localOffset)
	{
		SetTransform(tr);
		this.localOffset = localOffset;
		hasLocalOffset = localOffset != default;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T SetOffset(Vector3 localOffset, Vector3 worldOffset)
	{
		this.localOffset = localOffset;
		this.worldOffset = worldOffset;
		hasLocalOffset = localOffset != Vector3.zero;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T SetWorldOffset(Vector3 worldOffset)
	{
		this.worldOffset = worldOffset;
		return attachment;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T SetLocalOffset(Vector3 localOffset)
	{
		this.localOffset = localOffset;
		hasLocalOffset = localOffset != default;
		return attachment;
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
	private void SetTransform(Transform tr)
	{
		if (!ReferenceEquals(tr, null) && tr)
		{
			hasTransform = true;
			transform = tr;
		}
		else
		{
			hasTransform = false;
			transform = null;
		}
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator bool(AttachmentObject<T> obj)
	{
		return obj.hasTransform && obj.transform;
	}
	
}
	
}
