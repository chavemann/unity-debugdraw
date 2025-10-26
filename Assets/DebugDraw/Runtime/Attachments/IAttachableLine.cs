using DebugDrawUtils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawAttachments
{
	
/// <summary>
/// An item with a start and end point.
/// </summary>
public interface IAttachableLine
	{
		
		/// <summary>
		/// Attach this item to one or more GameObjects. This item and it's attachment will automatically expire
		/// if any of the attached objects are destroyed.
		/// </summary>
		/// <param name="startObj">The object the start of the lines is attached to.</param>
		/// <param name="endObj">The object the end of the lines is attached to.</param>
		/// <returns></returns>
		LineAttachment AttachTo(GameObjectOrTransform startObj, GameObjectOrTransform endObj);
		
		/// <summary>
		/// Sets this item's position.
		/// </summary>
		/// <param name="start">The new start position</param>
		/// <param name="end">The new end position</param>
		/// <returns></returns>
		void SetPositions(Vector3 start, Vector3 end);
		
		/// <summary>
		/// Sets this item's start position.
		/// </summary>
		/// <param name="position">The new start position</param>
		/// <returns></returns>
		void SetStartPosition(Vector3 position);
		
		/// <summary>
		/// Sets this item's end position.
		/// </summary>
		/// <param name="position">The new end position</param>
		/// <returns></returns>
		void SetEndPosition(Vector3 position);
		
		/// <summary>
		/// Gets this item's start position.
		/// </summary>
		/// <returns></returns>
		Vector3 GetStartPosition();
		
		/// <summary>
		/// Gets this item's end position.
		/// </summary>
		/// <returns></returns>
		Vector3 GetEndPosition();
		
	}
	
}
