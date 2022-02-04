using System.Runtime.CompilerServices;
using UnityEngine;

namespace Attachments
{

	public abstract class BaseAttachment
	{

		protected static Vector3 vector3Ref;

		internal int index = -1;
		internal float expires = -1;
		internal bool released;

		internal abstract bool Update();

		internal abstract void Release();

		public void Destroy()
		{
			// if (index != -1)
			// {
			// 	DebugDraw.Remove(this);
			// }

			Release();
		}
		
		/// <summary>
		/// Updates the duration of this attachment.
		/// </summary>
		/// <param name="duration">The duration starting from the current time.</param>
		public T SetDuration<T>(float duration) where T : BaseAttachment
		{
			expires = Time.time + duration;
			return (T) this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected bool ReleaseInternal()
		{
			if (released)
				return false;

			return true;
		}

	}

}