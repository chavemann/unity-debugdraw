using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// An item with a position.
	/// </summary>
	public interface IPointItem
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