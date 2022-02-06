#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

#if DEBUG_DRAW
internal class LogMessage
{

	private const float ScreenPadding = 10;

	private static readonly GUIContent MessageGUIContent = new GUIContent();
	private static readonly Regex ColorTagRichTextRegex = new Regex(@"<color\s*=\s*.+?\s*>|<\/color>", RegexOptions.Compiled | RegexOptions.Singleline);
	
	private static LogMessage messages;
	private static readonly Dictionary<int, LogMessage> MessageIds = new Dictionary<int, LogMessage>();
	private static readonly List<LogMessage> MessagePool = new List<LogMessage>();
	private static int messagePoolIndex;
	internal static bool hasMessages;

	private static float totalMessageHeight;

	private bool active;
	private LogMessage prev;
	private LogMessage next;
	private float height;
	private int id;
	private string text;
	private string shadowText;
	private float expires;

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
			Release(message);
			message = message.prev;
		}
		
		messages = null;
		hasMessages = false;
		
		MessageIds.Clear();
	}

	internal static void Add(int id, float duration, string text)
	{
		LogMessage message;

		if (id != 0)
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
		
		message.id = id;
		message.expires = DebugDraw.GetTime(duration);
		message.shadowText = Log.messageShadowColor.HasValue
			? ColorTagRichTextRegex.Replace(text, "") : text;

		if (Log.nextMessageColor.HasValue)
		{
			message.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(Log.nextMessageColor.GetValueOrDefault())}>{text}</color>";
			Log.nextMessageColor = null;
		}
		else
		{
			message.text = text;
		}

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
		message.prev = message.next = null;
		MessagePool[messagePoolIndex++] = message;
	}

	internal static void Update()
	{
		float time = DebugDraw.GetTime();
		LogMessage message = messages;
		int i = 1;
		
		Rect rect = GetScreenRect();
		totalMessageHeight = 0;

		while (message!= null)
		{
			if (i++ > Log.maxMessages)
			{
				LogMessage lastMessage = message;
				
				if (message.prev != null)
				{
					message.prev.next = null;
				}
				
				while (message != null)
				{
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
				message.height = Log.MessageStyle.CalcHeight(MessageGUIContent, rect.width);
				totalMessageHeight += message.height;
				message = message.next;
			}
		}

		hasMessages = messages != null;
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

			while (message!= null)
			{
				MessageGUIContent.text = Log.messageShadowColor.HasValue && i == 0
					? message.shadowText : message.text;

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

}
#endif