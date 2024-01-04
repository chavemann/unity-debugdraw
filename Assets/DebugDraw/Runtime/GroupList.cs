using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DebugDrawUtils
{

internal class GroupList
{

	private static readonly List<Group> ClearList = new();

	public Group currentGroup;
	public Group nextGroup;

	private Group[] groupPool = new Group[4];
	private int groupPoolSize;
	private readonly Dictionary<string, Group> groups = new();

	private Group[] groupStack = new Group[4];
	private int groupStackIndex;

	public void Reset()
	{
		currentGroup = null;
		nextGroup = null;
		groupStackIndex = 0;

		ClearList.Clear();

		foreach (Group group in groups.Values)
		{
			ClearList.Add(group);
		}

		foreach (Group group in ClearList)
		{
			Release(group);
		}

		groups.Clear();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryGet(string name, out Group group)
	{
		return groups.TryGetValue(name, out group);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Group GetOrCreate(string name, EndTime? defaultDuration = null)
	{
		if (!TryGet(name, out Group group))
		{
			group = groupPoolSize != 0
				? groupPool[--groupPoolSize]
				: new Group();

			group.name = name;
			group.isActive = true;
			group.itemCount = 0;
			groups.Add(name, group);

			if (!defaultDuration.HasValue)
			{
				group.defaultDuration = default;
			}
		}

		if (defaultDuration.HasValue)
		{
			group.defaultDuration = defaultDuration.Value;
		}

		return group;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Release(Group group)
	{
		if (groupPoolSize == groupPool.Length)
		{
			Array.Resize(ref groupPool, groupPool.Length * 2);
		}

		groupPool[groupPoolSize++] = group;
		groups.Remove(group.name);

		group.Release();
	}

	public Group Push(Group group)
	{
		if (group == null)
			return null;
		if (group.isCurrent)
			return group;
		if (!group.isActive)
			return group;

		group.isCurrent = true;
		currentGroup = group;

		if (groupStackIndex == groupStack.Length)
		{
			Array.Resize(ref groupStack, groupStack.Length * 2);
		}

		groupStack[groupStackIndex++] = group;

		return group;
	}

	public Group Push(string name, EndTime? defaultDuration = null)
	{
		if (name == null)
			return null;

		Group group = GetOrCreate(name, defaultDuration);
		return Push(group);
	}

	public void Pop()
	{
		if (currentGroup == null)
			return;

		currentGroup.isCurrent = false;
		currentGroup = groupStack[--groupStackIndex];
	}

	public void SetNext(string name, EndTime? defaultDuration = null)
	{
		nextGroup = GetOrCreate(name, defaultDuration);
	}

	public void SetNext(Group group)
	{
		if (group == null || !group.isActive)
			return;

		nextGroup = group;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ResetStack()
	{
		if (currentGroup != null)
		{
			currentGroup = null;
			groupStackIndex = 0;
		}

		nextGroup = null;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Group GetCurrent()
	{
		if (nextGroup != null)
		{
			Group group = nextGroup;
			nextGroup = null;
			return group;
		}

		return currentGroup;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public EndTime? GetDefaultDuration()
	{
		return nextGroup?.defaultDuration ?? currentGroup?.defaultDuration;
	}

}

}
