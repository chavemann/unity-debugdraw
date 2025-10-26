using System;
using System.Runtime.CompilerServices;

namespace DebugDrawUtils
{

public struct EndTime
{
	
	public readonly Duration type;
	public float time;
	
	public EndTime(float time)
	{
		type = time >= 0 ? Duration.Time : Duration.Infinite;
		this.time = time >= 0 ? DebugDraw.frameTime + time : 0;
	}
	
	public EndTime(Duration type)
	{
		this.type = type;
		time = 0;
		
		if (type == Duration.Time)
		{
			throw new ArgumentException("A duration type of Time requires a time value.");
		}
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator EndTime(float time)
	{
		return new EndTime(time);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator EndTime(Duration type)
	{
		return new EndTime(type);
	}
	
	public override bool Equals(object other)
	{
		if (other is not EndTime otherTime)
			return false;
		
		EndTime a = type == Duration.Default ? DebugDraw.defaultDuration : this;
		EndTime b = otherTime.type == Duration.Default ? DebugDraw.defaultDuration : otherTime;
		
		if (a.type == Duration.Time && b.type == Duration.Time)
			return a.time == b.time;
		
		return a.type == otherTime.type;
	}
	
	public override int GetHashCode()
	{
		return type == Duration.Time
			? time.GetHashCode()
			: type.GetHashCode();
	}
	
	public override string ToString()
	{
		switch (type)
		{
			case Duration.Default:
			case Duration.Infinite:
			case Duration.Once:
				return type.ToString();
			case Duration.Time:
				return $"{type} {time}";
		}
		
		return "";
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Expired(float currentTime)
	{
		switch (type)
		{
			case Duration.Infinite:
				return false;
			case Duration.Once:
				if (time < 0)
					return true;
				time = -1;
				return false;
			case Duration.Time:
				return time < currentTime;
		}
		
		return true;
	}
	
}

}
