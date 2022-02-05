#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if DEBUG_DRAW
internal class LogMessage
{

	private const float ScreenPadding = 10;
	
	private static LogMessage messages;
	private static readonly Dictionary<int, LogMessage> MessageIds = new Dictionary<int, LogMessage>();
	private static readonly List<LogMessage> MessagePool = new List<LogMessage>();
	private static int messagePoolIndex;
	internal static bool hasMessages;

	private static readonly GUIContent MessageGUIContent = new GUIContent();

	private bool active;
	private LogMessage prev;
	private LogMessage next;
	internal int id;
	internal string text;
	internal float expires;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private LogMessage InsertBefore(LogMessage message)
	{
		message.next = this;
		prev = message;
		return message;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Slice()
	{
		if (prev != null)
		{
			prev.next = next;
		}

		if (next != null)
		{
			next.prev = prev;
		}
	}
	
	internal static void Clear()
	{
		LogMessage message = messages;

		while (message!= null)
		{
			message.prev = message.next = null;
			Release(message);
			message = message.prev;
		}
		
		messages = null;
		hasMessages = false;
		
		MessageIds.Clear();
	}

	internal static void Add(string text, int id, float duration)
	{
		LogMessage message;

		if (id >= 0)
		{
			if (!MessageIds.TryGetValue(id, out message))
			{
				message = messagePoolIndex > 0 ? MessagePool[--messagePoolIndex] : new LogMessage();
				MessageIds.Add(id, message);
			}
		}
		else
		{
			message = messagePoolIndex > 0 ? MessagePool[--messagePoolIndex] : new LogMessage();
		}
		
		message.expires = DebugDraw.GetTime(duration);
		message.text = text;
		message.id = id;

		if (message.active)
		{
			if (message != messages)
			{
				message.Slice();
				messages = messages.InsertBefore(message);
			}
		}
		else if(messages != null)
		{
			messages = messages.InsertBefore(message);
		}
		else
		{
			messages = message;
		}

		message.active = true;
		hasMessages = true;
	}

	private static void Release(LogMessage message)
	{
		if (messagePoolIndex == MessagePool.Count)
		{
			int newSize = Mathf.Max(MessagePool.Count * 2, 2);

			for (int i = messagePoolIndex; i < newSize; i++)
			{
				MessagePool.Add(null);
			}
		}

		message.active = false;
		MessagePool[messagePoolIndex++] = message;
	}

	internal static void Update()
	{
		float time = DebugDraw.GetTime();
		LogMessage message = messages;
		int i = 1;

		while (message!= null)
		{
			if (i++ > Log.maxMessages)
			{
				LogMessage lastMessage = message;
				
				if (message.prev != null)
				{
					message.prev.next = null;
				}
				
				while (message!= null)
				{
					message.prev = message.next = null;
					Release(message);
					message = message.prev;
				}

				if (lastMessage == messages)
				{
					messages = null;
					hasMessages = false;
				}
				
				break;
			}
			
			if (message.expires < time)
			{
				LogMessage next = message.next;
				
				if (message == messages)
				{
					messages = message.next;
				}
				
				message.Slice();
				Release(message);
				
				message = next;
			}
			else
			{
				message = message.next;
			}
		}

		hasMessages = messages != null;
	}

	internal static void Draw()
	{
		LogMessage message = messages;

		Rect rect = new Rect(
			ScreenPadding, ScreenPadding,
			Screen.width - ScreenPadding * 2, Screen.height - ScreenPadding * 2);

		while (message!= null)
		{
			MessageGUIContent.text = message.text;
			float height = Log.MessageStyle.CalcHeight(MessageGUIContent, rect.width);
			GUI.Label(rect, MessageGUIContent, Log.MessageStyle);

			rect.y += height;
			rect.height -= height;

			if (rect.height <= 0)
				break;
			
			message = message.next;
		}
	}

}
#endif