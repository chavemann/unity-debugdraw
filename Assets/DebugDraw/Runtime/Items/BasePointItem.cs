using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// An base item that has a position.
	/// </summary>
	public abstract class BasePointItem : BaseItem, IPointItem
	{
		/// <summary>
		/// This item's position.
		/// </summary>
		public Vector3 position;

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		public virtual void SetPosition(Vector3 position)
		{
			this.position = position;
		}

		public virtual Vector3 GetPosition()
		{
			return position;
		}

	}

}