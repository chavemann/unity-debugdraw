using UnityEngine;

namespace DebugDrawAttachments
{

	/// <summary>
	/// An item with a start and end point.
	/// </summary>
	public interface IAttachableLine
	{

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