using DebugDrawUtils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawAttachments
{

/// <summary>
/// An item with a position.
/// </summary>
public interface IAttachablePoint
{
	
	/// <summary>
	/// Attach this item to a GameObjects. This item and it's attachment will automatically expire
	/// if the attached object is destroyed.
	/// This is only applicable to certain items and will return null otherwise.
	/// </summary>
	/// <param name="obj">The object the start of the lines is attached to.</param>
	/// <returns></returns>
	PointAttachment AttachTo(GameObjectOrTransform obj);
	
	/// <summary>
	/// Sets this item's position.
	/// </summary>
	/// <param name="position">The new position</param>
	/// <returns></returns>
	void SetPosition(Vector3 position);
	
	/// <summary>
	/// Gets this item's position.
	/// </summary>
	/// <returns></returns>
	Vector3 GetPosition();
	
}

}
