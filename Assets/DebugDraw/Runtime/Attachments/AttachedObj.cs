using System.Runtime.CompilerServices;
using UnityEngine;

namespace Attachments
{

	public struct AttachedObj
	{

		public Attachment attachment { get; internal set; }
		internal bool isSet;
		internal Vector3 localOffset;
		internal Vector3 worldOffset;

		internal void Clear()
		{
			if (!isSet)
				return;

			Log.Print("AttachedObj.Clear");
			DebugDraw.DestroyObj(attachment);
			attachment.RemoveAttachment();
			isSet = false;
			attachment = null;
		}

		internal void Set(GameObject obj, ref Vector3 localOffset, ref Vector3 worldOffset)
		{
			Log.Print("AttachedObj.Set", obj, obj.GetInstanceID()); 
			if (obj == null)
			{
				Clear();
				return;
			}
			
			attachment = obj.GetComponent<Attachment>();

			if (!attachment)
			{
				Log.Print("   Creating component");
				attachment = obj.AddComponent<Attachment>();
				attachment.hideFlags = HideFlags.DontSave;
			}

			isSet = true;
			this.localOffset = localOffset;
			this.worldOffset = worldOffset;
			// attachment.Init();
			attachment.AddAttachment();
			Log.Print(isSet, (bool)attachment, attachment.GetInstanceID());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 CalculatePosition()
		{
			// return isSet && !attachment.destroyed
			return isSet && !attachment.destroyed
				? attachment.transform.TransformPoint(localOffset) + worldOffset
				: localOffset + worldOffset;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool(AttachedObj obj)
		{
			return !obj.isSet || !obj.attachment.destroyed;
		}

	}

}