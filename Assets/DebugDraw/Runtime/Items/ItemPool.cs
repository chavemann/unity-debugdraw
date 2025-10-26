using System.Collections.Generic;

namespace DebugDrawUtils.Items
{

public static class ItemPool<T> where T : BaseItem, new()
{
	
	private static int poolSize = 1;
	private static int poolIndex = poolSize;
	private static readonly List<T> Pool = new(poolSize);
	
	static ItemPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			Pool.Add(new T());
		}
	}
	
	public static T Get(EndTime? duration)
	{
		T item = poolIndex > 0 ? Pool[--poolIndex] : new T();
		item.expires = DebugDraw.GetTime(duration);
		item.hasStateColor = DebugDraw.hasColor;
		item.hasStateTransform = DebugDraw.hasTransform;
		
		if (DebugDraw.hasColor)
		{
			item.stateColor = DebugDraw.color;
		}
		
		if (DebugDraw.hasTransform)
		{
			item.stateTransform = DebugDraw.transform;
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
		
		item.index = -1;
		item.mesh = null;
		Pool[poolIndex++] = item;
	}
	
}

}
