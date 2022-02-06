using UnityEngine;

namespace DebugDrawAttachments
{

	/// <summary>
	/// The abstract base for all attachment types.
	/// Attachments will also be automatically destroyed when they expire or any
	/// game objects they are attached to are destroyed.
	/// </summary>
	public abstract class BaseAttachment
	{

		protected static Vector3 vector3Ref;

		/// <summary>
		/// Is this visual alive. Will be true when created, and false when the visual expires or is manually removed.
		/// A visual should not be used when it is not alive.
		/// </summary>
		public bool isAlive => !destroyed && index != -1;

		internal int index = -1;
		internal bool destroyed;

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Destroys this attachment. Attachments will also be automatically destroyed when
		/// they expire or any game objects they are attached to are destroyed.
		/// </summary>
		public void Destroy()
		{
			destroyed = true;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Private -- */

		internal abstract bool Update();
		
		internal abstract void Release();

	}

}