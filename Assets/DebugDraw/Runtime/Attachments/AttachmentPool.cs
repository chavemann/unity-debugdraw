using System.Collections.Generic;
using Attachments;
using UnityEngine;

namespace Items
{

	public static class AttachmentPool<T> where T : BaseAttachment, new()
	{
		
		private static int poolSize = 32;
		private static int poolIndex = poolSize;
		private static readonly List<T> Pool = new List<T>(poolSize);
		
		static AttachmentPool()
		{
			for (int i = 0; i < poolSize; i++)
			{
				Pool.Add(new T());
			}
		}

		public static T Get(float duration)
		{
			if (poolIndex == 0)
			{
				return new T();
			}
		
			T item = Pool[--poolIndex];
			item.expires = duration >= 0 ? Time.time + duration : Mathf.Infinity;
			item.released = false;
			
			return item;
		}

		public static void Release(T item)
		{
			if (poolIndex == poolSize)
			{
				poolSize *= 2;
				
				for (int i = poolIndex; i < poolSize; i++)
				{
					Pool.Add(null);
				}
			}
		
			item.released = true;
			Pool[poolIndex++] = item;
		}

	}

}