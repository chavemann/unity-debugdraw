using DebugDrawUtils.Items;
using UnityEngine;

namespace DebugDrawUtils.Attachments
{
	
public class BoxAttachment : BaseAttachment
{
	
	/// <summary>
	/// The attached object.
	/// </summary>
	public readonly AttachmentObject<BoxAttachment> obj;
	
	/// <summary>
	/// The Debug Line item associated with this attachment.
	/// </summary>
	public Box Box { get; internal set; }
	
	private bool updateColliderSize;
	private bool updateRendererSize;
	private Collider collider;
	private Renderer renderer;
	
	public BoxAttachment()
	{
		obj = new AttachmentObject<BoxAttachment>(this);
	}
	
	public BoxAttachment Init(Box box, Transform obj, BoxAttachmentSizeUpdate updateSize)
	{
		this.Box = box;
		this.obj.Set(obj);
		
		if (!this.obj.hasTransform)
			return this;
		
		if (updateSize == BoxAttachmentSizeUpdate.Collider || updateSize == BoxAttachmentSizeUpdate.Any)
		{
			collider = obj.transform.GetComponent<Collider>();
			updateColliderSize = collider;
			
			if (updateColliderSize)
			{
				updateSize = BoxAttachmentSizeUpdate.None;
			}
		}
		
		if (updateSize == BoxAttachmentSizeUpdate.Collider || updateSize == BoxAttachmentSizeUpdate.Any)
		{
			renderer = obj.transform.GetComponent<Renderer>();
			updateRendererSize = renderer;
		}
		
		return this;
	}
	
	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */
	
	internal override bool Update()
	{
		if (Box.index == -1)
			return false;
		if (!obj)
			return false;
		
		Bounds bounds = default;
		
		if (updateRendererSize)
		{
			if (renderer)
			{
				bounds = renderer.bounds;
			}
			else
			{
				updateRendererSize = false;
			}
		}
		else if (updateColliderSize)
		{
			if (collider)
			{
				bounds = collider.bounds;
			}
			else
			{
				updateColliderSize = false;
			}
		}
		
		if (updateRendererSize || updateColliderSize)
		{
			Box.position = bounds.center;
			Box.size = bounds.extents;
		}
		else
		{
			Box.position = obj.CalculatePosition();
		}
		
		return true;
	}
	
	internal override void Release()
	{
		if (Box != null)
		{
			Box.Remove();
			Box = null;
		}
		
		updateRendererSize = false;
		updateColliderSize = false;
		renderer = null;
		collider = null;
		
		AttachmentPool<BoxAttachment>.Release(this);
	}
	
}

/// <summary>
/// Defines how a BoxAttachment should update the box size.
/// </summary>
public enum BoxAttachmentSizeUpdate
	{
		
		/// <summary>
		/// Don't update the box size.
		/// </summary>
		None,
		
		/// <summary>
		/// Update the box size to match the attached collider.
		/// </summary>
		Collider,
		
		/// <summary>
		/// Update the box size to match the attached rendered.
		/// </summary>
		Renderer,
		
		/// <summary>
		/// Will first look for a collider component then rendered.
		/// </summary>
		Any,
		
	}
	
}
