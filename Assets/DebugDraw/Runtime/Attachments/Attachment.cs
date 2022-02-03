using System;
using UnityEngine;

namespace Attachments
{

	/// <summary>
	/// Simple component to keep track of objects that have visuals attached.
	/// </summary>
	// [AddComponentMenu("")]
	[ExecuteAlways]
	public class Attachment : MonoBehaviour
	{

		internal bool destroyed;
		internal new Transform transform;

		internal int attachCount;

		internal void OnEnable()
		{
			transform = base.transform;
		}
		
		private void OnDestroy()
		{
			destroyed = true;
		}

		public void AddAttachment()
		{
			attachCount++;
		}

		public void RemoveAttachment()
		{
			if (--attachCount < 0)
			{
				DebugDraw.DestroyObj(this);
			}
		}

	}

}