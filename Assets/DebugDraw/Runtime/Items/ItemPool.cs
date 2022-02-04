using System.Collections.Generic;
using UnityEngine;

namespace Items
{

	public static class ItemPool<T> where T : BaseItem, new()
	{
		
		private static int poolSize = 32;
		private static int poolIndex = poolSize;
		private static readonly List<T> Pool = new List<T>(poolSize);
		
		static ItemPool()
		{
			for (int i = 0; i < poolSize; i++)
			{
				Pool.Add(new T());
			}
		}

		public static T Get(float duration)
		{
			T item = poolIndex > 0 ? Pool[--poolIndex] : new T();
			item.expires = duration >= 0 ? Time.time + duration : Mathf.Infinity;
			item.hasStateColor = DebugDraw.hasColor;
			item.hasStateTransform = DebugDraw.hasTransform;
			
			if (DebugDraw.hasColor)
			{
				item.stateColor = DebugDraw._color;
			}
			
			if (DebugDraw.hasTransform)
			{
				item.stateTransform = DebugDraw._transform;
			}
			
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
		
			Pool[poolIndex++] = item;
		}

	}

}