#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using UnityEngine;

public static partial class Log
{
	/*
	 * These methods are generated automatically from the Log.Print** methods.
	 */
	/* <ShowGenMethods> */
	
	/// <summary>
	///   <para>Prints a message on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">String for display.</param>
	public static void Show(int id, float duration, string message)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, message);
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public static void Show(int id, float duration, object message)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) message);
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a formatted message on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public static void ShowFormat(int id, float duration, string format, params object[] args)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, string.Format(format, args));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints an Object on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Object for display.</param>
	public static void Show(int id, float duration, Object val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Transform on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Transform for display.</param>
	public static void Show(int id, float duration, Transform val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a bool on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">bool for display.</param>
	public static void Show(int id, float duration, bool val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a char on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">char for display.</param>
	public static void Show(int id, float duration, char val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a sbyte on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">sbyte for display.</param>
	public static void Show(int id, float duration, sbyte val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a short on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">short for display.</param>
	public static void Show(int id, float duration, short val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints an int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">int for display.</param>
	public static void Show(int id, float duration, int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a long on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">long for display.</param>
	public static void Show(int id, float duration, long val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a byte on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">byte for display.</param>
	public static void Show(int id, float duration, byte val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a ushort on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">ushort for display.</param>
	public static void Show(int id, float duration, ushort val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a uint on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">uint for display.</param>
	public static void Show(int id, float duration, uint val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a ulong on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">ulong for display.</param>
	public static void Show(int id, float duration, ulong val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a float on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">float for display.</param>
	public static void Show(int id, float duration, float val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a double on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">double for display.</param>
	public static void Show(int id, float duration, double val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a decimal on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">decimal for display.</param>
	public static void Show(int id, float duration, decimal val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(val));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Vector2 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Vector2 for display.</param>
	public static void Show(int id, float duration, Vector2 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Vector3 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Vector3 for display.</param>
	public static void Show(int id, float duration, Vector3 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Vector4 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Vector4 for display.</param>
	public static void Show(int id, float duration, Vector4 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Vector2Int for display.</param>
	public static void Show(int id, float duration, Vector2Int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Vector3Int for display.</param>
	public static void Show(int id, float duration, Vector3Int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Matrix4x4 for display.</param>
	public static void Show(int id, float duration, Matrix4x4 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Quaternion on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Quaternion for display.</param>
	public static void Show(int id, float duration, Quaternion val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a Rect on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">Rect for display.</param>
	public static void Show(int id, float duration, Rect val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a RectInt on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="val">RectInt for display.</param>
	public static void Show(int id, float duration, RectInt val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, val.ToString());
		}
		#endif

	}
	
	/// <summary>
	///   <para>Logs a list the Unity Console.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="list">List for display.</param>
	public static void Show<T>(int id, float duration, IEnumerable<T> list)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(list));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a list of items on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="args">Items for display.</param>
	public static void Show(int id, float duration, params object[] args)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetArgString(args));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a list of Objects on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="args">Items for display.</param>
	public static void Show(int id, float duration, Object[] args)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(args));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a list of GameObject on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="args">Items for display.</param>
	public static void Show(int id, float duration, GameObject[] args)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, (string) GetString(args));
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and bool on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Show(int id, float duration, string message, Object val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and bool on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Show(int id, float duration, string message, bool val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and string on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Show(int id, float duration, string message, string val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and char on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">char for display.</param>
	public static void Show(int id, float duration, string message, char val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and sbyte on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">sbyte for display.</param>
	public static void Show(int id, float duration, string message, sbyte val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and short on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">short for display.</param>
	public static void Show(int id, float duration, string message, short val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">int for display.</param>
	public static void Show(int id, float duration, string message, int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and long on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">long for display.</param>
	public static void Show(int id, float duration, string message, long val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and byte on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">byte for display.</param>
	public static void Show(int id, float duration, string message, byte val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and ushort on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ushort for display.</param>
	public static void Show(int id, float duration, string message, ushort val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and uint on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">uint for display.</param>
	public static void Show(int id, float duration, string message, uint val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and ulong on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ulong for display.</param>
	public static void Show(int id, float duration, string message, ulong val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and float on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">float for display.</param>
	public static void Show(int id, float duration, string message, float val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and double on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">double for display.</param>
	public static void Show(int id, float duration, string message, double val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and decimal on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">decimal for display.</param>
	public static void Show(int id, float duration, string message, decimal val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {GetString(val)}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Vector2 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2 for display.</param>
	public static void Show(int id, float duration, string message, Vector2 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Vector3 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3 for display.</param>
	public static void Show(int id, float duration, string message, Vector3 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Vector4 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector4 for display.</param>
	public static void Show(int id, float duration, string message, Vector4 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2Int for display.</param>
	public static void Show(int id, float duration, string message, Vector2Int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3Int for display.</param>
	public static void Show(int id, float duration, string message, Vector3Int val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Matrix4x4 for display.</param>
	public static void Show(int id, float duration, string message, Matrix4x4 val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Quaternion on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Quaternion for display.</param>
	public static void Show(int id, float duration, string message, Quaternion val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and Rect on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Rect for display.</param>
	public static void Show(int id, float duration, string message, Rect val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/// <summary>
	///   <para>Prints a message and RectInt on the screen.</para>
	/// </summary>
	/// <param name="id">If non-zero, a unique key to prevent the same message from being added multiple times.</param>
	/// <param name="duration">How long to display the message, in seconds. Pass 0 to only display for the next frame.</param>
	/// <param name="message">Message for display.</param>
	/// <param name="val">RectInt for display.</param>
	public static void Show(int id, float duration, string message, RectInt val)
	{
		#if DEBUG_DRAW
		if (DebugDraw.hasInstance)
		{
			LogMessage.Add(id, duration, $"{message} {val.ToString()}");
		}
		#endif

	}
	
	/* </ShowGenMethods> */

}