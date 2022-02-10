using UnityEngine;

namespace DebugDrawAttachments
{

	/// <summary>
	/// An item with a position.
	/// </summary>
	public interface IAttachablePoint
	{

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