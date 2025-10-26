#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DebugDrawUtils
{

#if DEBUG_DRAW
public partial class LogMessage : Groupable
{
	
	private const float ScreenPadding = 10;
	
	private static readonly GUIContent MessageGUIContent = new();
	private static readonly Regex ColorTagRichTextRegex = new(@"<color\s*=\s*.+?\s*>|<\/color>", RegexOptions.Compiled | RegexOptions.Singleline);
	
	private static LogMessage messages;
	private static readonly Dictionary<string, LogMessage> MessageIds = new();
	private static LogMessage[] messagePool = new LogMessage[4];
	private static int messagePoolSize;
	internal static bool hasMessages;
	
	internal static readonly GroupList Groups = new();
	
	private string id;
	private bool active;
	private LogMessage prev;
	private LogMessage next;
	private float height;
	private string text;
	private string shadowText;
	private EndTime expires;
	private bool invalidateHeight;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private LogMessage InsertBefore(LogMessage message)
	{
		message.next = this;
		
		if (prev != null)
		{
			prev.next = message;
		}
		
		message.prev = prev;
		prev = message;
		return message;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void Slice()
	{
		if (prev != null)
		{
			prev.next = next;
		}
		
		if (next != null)
		{
			next.prev = prev;
		}
		
		prev = null;
		next = null;
	}
	
	internal static void Reset()
	{
		Groups.Reset();
		Clear();
	}
	
	internal static void Clear()
	{
		LogMessage message = messages;
		
		while (message != null)
		{
			LogMessage next = message.next;
			message.Release();
			message = next;
		}
		
		messages = null;
		hasMessages = false;
		
		MessageIds.Clear();
	}
	
	internal static void Clear(string groupName)
	{
		if (!Groups.TryGet(groupName, out Group group))
			return;
		
		Clear(group);
	}
	
	internal static void Clear(Group group)
	{
		if (group == null)
			return;
		if (!group.isActive)
			return;
		
		for (int i = 0; i < group.itemCount; i++)
		{
			LogMessage message = (LogMessage) group.items[i];
			Remove(message);
		}
		
		group.Clear();
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Group CreateGroup(string name, EndTime? defaultDuration = null)
	{
		return Groups.GetOrCreate(name, defaultDuration);
	}
	
	internal static void ReleaseGroup(Group group)
	{
		if (group == null)
			return;
		if (!group.isActive)
			return;
		
		for (int i = 0; i < group.itemCount; i++)
		{
			group.items[i].group = null;
		}
		
		Groups.Release(group);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Group BeginGroup(string name, EndTime? defaultDuration = null)
	{
		return Groups.Push(name, defaultDuration);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Group BeginGroup(Group group)
	{
		return Groups.Push(group);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void EndGroup()
	{
		Groups.Pop();
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void NextGroup(string name, EndTime? defaultDuration = null)
	{
		Groups.SetNext(name, defaultDuration);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void NextGroup(Group group)
	{
		Groups.SetNext(group);
	}
	
	internal static LogMessage Add(string id, EndTime? duration = null, string text = null)
	{
		LogMessage message;
		Group currentGroup = Groups.GetCurrent();
		
		if (id != "")
		{
			if (!MessageIds.TryGetValue(id, out message))
			{
				message = messagePoolSize > 0 ? messagePool[--messagePoolSize] : new LogMessage();
				message.id = id;
				MessageIds.Add(id, message);
				currentGroup?.Add(message);
			}
		}
		else
		{
			message = messagePoolSize > 0 ? messagePool[--messagePoolSize] : new LogMessage();
			currentGroup?.Add(message);
		}
		
		message
			.Duration(duration)
			.SetText(text);
		
		if (message.active)
		{
			if (message != messages)
			{
				message.Slice();
				messages = messages.InsertBefore(message);
			}
		}
		else if (messages != null)
		{
			messages = messages.InsertBefore(message);
		}
		else
		{
			messages = message;
		}
		
		message.active = true;
		hasMessages = true;
		
		return message;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Remove(LogMessage message)
	{
		if (message == messages)
		{
			messages = message.next;
			hasMessages = messages != null;
		}
		
		message.Slice();
		message.Release();
	}
	
	internal static void Update()
	{
		float time = DebugDraw.frameTime;
		LogMessage nextMessage = messages;
		LogMessage previousMessage = messages;
		int i = 1;
		
		Rect rect = GetScreenRect();
		
		while (nextMessage != null)
		{
			i++;
			LogMessage message = nextMessage;
			previousMessage = message;
			
			nextMessage = nextMessage.next;
			
			if (message.invalidateHeight)
			{
				MessageGUIContent.text = message.text;
				message.height = Log.MessageStyle.CalcHeight(MessageGUIContent, rect.width);
				message.invalidateHeight = false;
			}
			
			if (message.expires.Expired(time))
			{
				Remove(message);
			}
		}
		
		if (--i > Log.maxMessages)
		{
			LogMessage message = previousMessage;
			
			while (message != null && i-- > Log.maxMessages)
			{
				LogMessage prev = message.prev;
				Remove(message);
				message = prev;
			}
		}
	}
	
	internal static void Draw()
	{
		Color guiColor = GUI.color;
		
		for (int i = 0; i < 2; i++)
		{
			Rect rect = GetScreenRect();
			rect.y = Screen.height - ScreenPadding;
			LogMessage message = messages;
			
			if (Log.messageShadowColor.HasValue)
			{
				if (i == 0)
				{
					GUI.color = Log.messageShadowColor.GetValueOrDefault();
					rect.x += 1;
					rect.y += 1;
				}
				else
				{
					GUI.color = guiColor;
				}
			}
			
			while (message != null)
			{
				MessageGUIContent.text = Log.messageShadowColor.HasValue && i == 0
					? message.shadowText
					: message.text;
				
				rect.y -= message.height;
				rect.height -= message.height;
				
				if (rect.height <= 0)
					break;
				
				GUI.Label(rect, MessageGUIContent, Log.MessageStyle);
				message = message.next;
			}
			
			if (!Log.messageShadowColor.HasValue)
				break;
		}
	}
	
	private static Rect GetScreenRect()
	{
		return new Rect(
			ScreenPadding, ScreenPadding,
			Screen.width - ScreenPadding * 2, Screen.height - ScreenPadding * 2);
	}
	
	/// <summary>
	/// Set the text for this message.
	/// </summary>
	private LogMessage SetText(string newText)
	{
		string previousText = text;
		
		if (string.IsNullOrEmpty(newText))
		{
			shadowText = "";
			text = "";
			invalidateHeight = text != previousText;
			return this;
		}
		
		shadowText = Log.messageShadowColor.HasValue
			? ColorTagRichTextRegex.Replace(newText, "")
			: newText;
		
		if (Log.nextMessageColor.HasValue)
		{
			text = $"<color=#{ColorUtility.ToHtmlStringRGBA(Log.nextMessageColor.GetValueOrDefault())}>{newText}</color>";
			Log.nextMessageColor = null;
		}
		else
		{
			text = newText;
		}
		
		invalidateHeight = text != previousText;
		return this;
	}
	
	/// <summary>
	/// Set the duration for this message.
	/// </summary>
	/// <param name="duration">If left to default/null will use <see cref="DebugDraw.defaultDuration"/></param>
	public LogMessage Duration(EndTime? duration = null)
	{
		expires = DebugDraw.GetTime(duration, Groups);
		return this;
	}
	
	/// <summary>
	/// Sets the group this item belongs to.
	/// </summary>
	/// <param name="newGroup"></param>
	/// <returns></returns>
	public LogMessage Group(Group newGroup)
	{
		if (newGroup == group)
			return this;
		
		group?.Remove(this);
		group = newGroup;
		group?.Add(this);
		
		return this;
	}
	
	private void Release()
	{
		if (messagePoolSize == messagePool.Length)
		{
			Array.Resize(ref messagePool, messagePool.Length * 2);
		}
		
		active = false;
		prev = next = null;
		if (id != null)
		{
			MessageIds.Remove(id);
			id = null;
		}
		
		messagePool[messagePoolSize++] = this;
		
		ReleaseFromGroup();
	}
	
}
#endif

}
