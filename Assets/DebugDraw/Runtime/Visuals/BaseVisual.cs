using System.Runtime.CompilerServices;
using UnityEngine;

namespace Visuals
{

	/// <summary>
	/// TODO: 
	/// </summary>
	public abstract class BaseVisual
	{

		private static int nextId;
		internal readonly int id = nextId++;

		protected static Vector3 vector3Ref;

		/// <summary>
		/// Is this visual alive. Will be true when created, and false when the visual expires or is manually removed.
		/// A visual should not be used when it is not alive.
		/// </summary>
		public bool isAlive => index != -1;

		internal int index = -1;
		internal float expires = -1;

		internal abstract bool Update();
		
		internal abstract void Release();

		/// <summary>
		/// TODO:
		/// </summary>
		public void Destroy()
		{
			expires = -1;
		}
		
		/// <summary>
		/// Updates the duration of this attachment.
		/// </summary>
		/// <param name="duration">The duration starting from the current time.</param>
		public T SetDuration<T>(float duration) where T : BaseVisual
		{
			expires = Time.time + duration;
			return (T) this;
		}

		// [MethodImpl(MethodImplOptions.AggressiveInlining)]
		// protected bool ReleaseInternal()
		// {
		// 	if (released)
		// 		return false;
		//
		// 	return true;
		// }

	}

}