using System.Collections.Generic;
using Visuals;
using UnityEngine;

namespace Items
{

	public static class VisualPool<T> where T : BaseVisual, new()
	{
		
		private static int poolSize = 1;
		private static int poolIndex = 0;
		private static readonly List<T> Pool = new List<T>(poolSize);
		
		static VisualPool()
		{
			for (int i = 0; i < poolSize; i++)
			{
				Pool.Add(null);
			}
		}

		public static T Get(float duration)
		{
			T item = poolIndex > 0 ? Pool[--poolIndex] : new T();
			item.expires = DebugDraw.GetTime(duration);
			DebugDraw.AddVisual(item);
			
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