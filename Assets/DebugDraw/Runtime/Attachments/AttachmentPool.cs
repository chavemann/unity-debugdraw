using System.Collections.Generic;
using DebugDrawUtils.Items;

namespace DebugDrawUtils.Attachments
{
	
public static class AttachmentPool<T> where T : BaseAttachment, new()
{
	
	private static int poolSize = 1;
	private static int poolIndex = 0;
	private static readonly List<T> Pool = new(poolSize);
	
	static AttachmentPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			Pool.Add(null);
		}
	}
	
	public static T Get(BaseItem debugItem)
	{
		debugItem.hasStateTransform = false;
		
		T item = poolIndex > 0 ? Pool[--poolIndex] : new T();
		item.destroyed = false;
		DebugDraw.AddAttachment(item);
		
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
		Pool[poolIndex++] = item;
	}
	
}
	
}
