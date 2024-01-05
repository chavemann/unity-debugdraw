namespace DebugDrawUtils
{

public class Groupable
{

	internal Group group;
	/// <summary>
	/// The index this items is stored at if it belongs to a group.
	/// </summary>
	internal int groupIndex = -1;

	/// <summary>
	/// Releases this item when it gets removed from a mesh, returning it to a pool and
	/// resetting any values if necessary.
	/// </summary>
	internal void ReleaseFromGroup()
	{
		if (groupIndex != -1)
		{
			group.Remove(this);
			groupIndex = -1;
		}
	}

}

}
