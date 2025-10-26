using System;
using System.Runtime.CompilerServices;

namespace DebugDrawUtils
{

public class Group : IDisposable
{
	
	public string Name { get; internal set; }
	public EndTime? defaultDuration;
	
	internal Groupable[] items = new Groupable[4];
	public int itemCount;
	
	internal GroupList groupList;
	
	/// <summary>
	/// False when this group has been released.
	/// </summary>
	internal bool isActive;
	
	/// <summary>
	/// True when this group is the current group new items are added to.
	/// </summary>
	internal bool isCurrent;
	
	internal Group() { }
	
	/// <summary>
	/// Set to null to not specify a duration override.
	/// </summary>
	/// <param name="defaultDuration"></param>
	public void Duration(EndTime? defaultDuration)
	{
		this.defaultDuration = defaultDuration;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void Release()
	{
		itemCount = 0;
		isActive = false;
		isCurrent = false;
		defaultDuration = null;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void Clear()
	{
		itemCount = 0;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void Add(Groupable item)
	{
		if (itemCount == items.Length)
		{
			Array.Resize(ref items, items.Length * 2);
		}
		
		item.group = this;
		item.groupIndex = itemCount;
		items[itemCount++] = item;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void Remove(Groupable item)
	{
		if (!isActive)
			return;
		
		if (itemCount > 1)
		{
			items[item.groupIndex] = items[--itemCount];
			items[item.groupIndex].groupIndex = item.groupIndex;
		}
		
		item.group = null;
		item.groupIndex = -1;
	}
	
	public void Dispose()
	{
		if (!isCurrent)
			return;
		
		Log.Print("Group.Dispose");
		groupList.Pop();
	}
	
}

}
