#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using DebugDrawUtils.DebugDrawItems;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils
{

/// <summary>
/// Provides some extended logging overloads, e.g. <see cref="Print(GameObject[])"/>.
/// Also supports all the same Debug.Log** methods so it can be a drop-in replacement.
/// </summary>
public static partial class Log
{

	private static readonly StringBuilder ArgsBuffer = new();
	private static readonly StringBuilder GetStringBuffer = new();

	private static readonly List<TimerSet> timers = new();
	private static int timerIndex;

	/// <summary>
	/// Can be set for all log methods instead of passing individual contexts to each Log call.
	/// </summary>
	public static Object defaultLogContext;

	/// <summary>
	/// The log type used when using the extended log override methods.
	/// </summary>
	public static LogType defaultLogType = LogType.Log;

	/// <summary>
	/// When logging arrays, this controls the maximum number of items that will be displayed
	/// </summary>
	public static int maxArrayItems = 100;

	/// <summary>
	/// The max number of on screen messages in the queue - once the queue fills up old messages will be removed.
	/// </summary>
	public static int maxMessages = 100;

	/// <summary>
	/// Adds a subtle drop shadow to on screen messages to make them easier to read.
	/// Set to null to disable.
	/// </summary>
	public static Color? messageShadowColor = new Color(0, 0, 0, 0.5f);

	/// <summary>
	/// Sets the color of only the next on screen message. Will be reset during the next call to Show**.
	/// Set to null to reset.
	/// </summary>
	public static Color? nextMessageColor;

	/// <summary>
	/// Use to adjust the default style of on screen messages.
	/// </summary>
	public static readonly GUIStyle MessageStyle = new();

	static Log()
	{
		MessageStyle.normal.textColor = Color.white;
		MessageStyle.fontSize = 14;
	}

	/// <summary>
	/// Chain with <see cref="Text(string)"/> to display a string on screen with the specific id and optionally duration.
	/// Displaying text with the same id will update the existing text instead of creating a entry.
	/// </summary>
	/// <param name="id">The text id. An empty string always creates a new message, in which case <see cref="Text(string)"/> can instead be called directly.</param>
	/// <param name="duration">How long the text will stay on screen before disappearing.
	///   Will default to <see cref="DebugDraw.defaultDuration"/> if not specified.</param>
	/// <returns>The displayed message. Chain with the <see cref="LogMessage.Text(string)"/> method to set the message's text.</returns>
	public static LogMessage Display(string id, EndTime duration = default)
	{
		return LogMessage.Add(id, duration);
	}

	/// <summary>
	/// See <see cref="Display(string,DebugDrawUtils.EndTime)"/>.
	/// Convenience to provide an integer id by automatically converting it to a string.
	/// </summary>
	public static LogMessage Display(int id, EndTime duration = default)
	{
		return LogMessage.Add(id.ToString(), duration);
	}

	/* ------------------------------------------------------------------------------------- */
	#region -- Basic Unity log methods --

	/* ------------------------------------------------------------------------------------- */
	/* -- Log -- */

	/// <summary>
	///   <para>Logs a message to the Unity Console.</para>
	/// </summary>
	/// <param name="message">String for display.</param>
	public static void Print(string message)
	{
		Debug.unityLogger.Log(LogType.Log, (object) message, defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message to the Unity Console.</para>
	/// </summary>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public static void Print(object message)
	{
		Debug.unityLogger.Log(LogType.Log, message, defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a formatted message to the Unity Console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public static void PrintFormat(string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Log, defaultLogContext, format, args);
	}

	/// <summary>
	///   <para>Logs a formatted message to the Unity Console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	/// <param name="context">Object to which the message applies.</param>
	public static void PrintFormat(Object context, string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
	}

	/// <summary>
	///   <para>Logs a formatted message to the Unity Console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	/// <param name="context">Object to which the message applies.</param>
	/// <param name="logType">Type of message e.g. warn or error etc.</param>
	/// <param name="logOptions">Option flags to treat the log message special.</param>
	public static void PrintFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
	{
		Debug.LogFormat(logType, logOptions, context, format, args);
	}

	/// <summary>
	///   <para>A variant of Debug.unityLogger.Log that logs an error message to the console.</para>
	/// </summary>
	/// <param name="exception">Runtime Exception.</param>
	public static void PrintException(Exception exception)
	{
		Debug.unityLogger.LogException(exception, defaultLogContext);
	}

	/// <summary>
	///   <para>A variant of Debug.unityLogger.Log that logs an error message to the console.</para>
	/// </summary>
	/// <param name="context">Object to which the message applies.</param>
	/// <param name="exception">Runtime Exception.</param>
	public static void PrintException(Exception exception, Object context)
	{
		Debug.unityLogger.LogException(exception, context);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Warn -- */

	/// <summary>
	///   <para>A variant of Debug.Log that logs a warning message to the console.</para>
	/// </summary>
	/// <param name="message">String for display.</param>
	public static void Warn(string message)
	{
		Debug.unityLogger.Log(LogType.Warning, (object) message, defaultLogContext);
	}

	/// <summary>
	///   <para>A variant of Debug.Log that logs a warning message to the console.</para>
	/// </summary>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public static void Warn(object message)
	{
		Debug.unityLogger.Log(LogType.Warning, GetString(message), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a formatted warning message to the Unity Console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public static void WarnFormat(string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Warning, defaultLogContext, format, args);
	}

	/// <summary>
	///   <para>Logs a formatted warning message to the Unity Console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	/// <param name="context">Object to which the message applies.</param>
	public static void WarnFormat(Object context, string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Error -- */

	/// <summary>
	///   <para>A variant of Debug.Log that logs an error message to the console.</para>
	/// </summary>
	/// <param name="message">String for display.</param>
	public static void Error(string message)
	{
		Debug.unityLogger.Log(LogType.Error, (object) message, defaultLogContext);
	}

	/// <summary>
	///   <para>A variant of Debug.Log that logs an error message to the console.</para>
	/// </summary>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public static void Error(object message)
	{
		Debug.unityLogger.Log(LogType.Error, GetString(message), defaultLogContext);
	}

	/// <summary>
	///   <para>>Logs a formatted error message to the Unity console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public static void ErrorFormat(string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Error, defaultLogContext, format, args);
	}

	/// <summary>
	///   <para>>Logs a formatted error message to the Unity console.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	/// <param name="context">Object to which the message applies.</param>
	public static void ErrorFormat(Object context, string format, params object[] args)
	{
		Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
	}

	#endregion
	/* ------------------------------------------------------------------------------------- */

	/* ------------------------------------------------------------------------------------- */
	#region -- Extended Log Overrides --

	public static object GetString(object message)
	{
		switch (message)
		{
			case null:
				return "Null";
			case string str:
				return GetString(str);
			case object[] objs:
				return GetString(objs);
			case Object obj:
				return obj.ToString();
			case Vector2 v:
				return GetString(ref v);
			case Vector3 v:
				return GetString(ref v);
			case Vector4 v:
				return GetString(ref v);
			case IEnumerable enumerable:
				return GetString(enumerable);
			case IFormattable formattable:
				return formattable.ToString(null, CultureInfo.InvariantCulture);
			default:
				return message.ToString();
		}
	}

	public static object GetString(IEnumerable list)
	{
		StringBuilder buffer = GetStringBuffer;
		buffer.Clear().Append("[");
		bool addComma = false;

		int i = 0;
		bool hasMore = false;
		foreach (object item in list)
		{
			if (i == maxArrayItems)
			{
				hasMore = true;
				break;
			}

			if (addComma)
			{
				buffer.Append(", ");
			}
			else
			{
				addComma = true;
			}

			buffer.Append(GetString(item));

			i++;
		}

		if (hasMore)
		{
			buffer.Append("...");
		}

		return buffer.Append("]").ToString();
	}

	public static object GetDictString<TK, TV>(IDictionary<TK, TV> dict)
	{
		StringBuilder buffer = GetStringBuffer;
		buffer.Clear().Append("{");
		bool addComma = false;

		int i = 0;
		bool hasMore = false;
		foreach (KeyValuePair<TK, TV> entry in dict)
		{
			if (i == maxArrayItems)
			{
				hasMore = true;
				break;
			}

			if (addComma)
			{
				buffer.Append(", ");
			}
			else
			{
				addComma = true;
			}

			buffer.Append(GetString(entry.Key));
			buffer.Append(":");
			buffer.Append(GetString(entry.Value));

			i++;
		}

		if (hasMore)
		{
			buffer.Append("...");
		}

		return buffer.Append("}").ToString();
	}

	public static object GetKeyValuePairsString(params object[] args)
	{
		StringBuilder buffer = GetStringBuffer;
		buffer.Clear().Append("{");
		bool addComma = false;

		int itemCount = 0;
		bool hasMore = false;
		int end = args.Length / 2 * 2;
		for (int i = 0; i < end; i += 2)
		{
			if (itemCount == maxArrayItems)
			{
				hasMore = true;
				break;
			}

			if (addComma)
			{
				buffer.Append(", ");
			}
			else
			{
				addComma = true;
			}

			buffer.Append(GetString(args[i]));
			buffer.Append(":");
			buffer.Append(GetString(args[i + 1]));

			itemCount++;
		}

		if (hasMore)
		{
			buffer.Append("...");
		}

		return buffer.Append("}").ToString();
	}

	public static object GetString(Object message) => message ? message.ToString() : "null";
	public static object GetString(Transform message) => message.ToString();
	public static object GetString(IFormattable message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(string message) => message;
	public static object GetString(bool message) => message.ToString(CultureInfo.InvariantCulture);
	public static object GetString(char message) => message.ToString(CultureInfo.InvariantCulture);
	public static object GetString(sbyte message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(short message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(int message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(long message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(byte message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(ushort message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(uint message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(ulong message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(float message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(double message) => message.ToString(null, CultureInfo.InvariantCulture);
	public static object GetString(decimal message) => message.ToString(null, CultureInfo.InvariantCulture);

	public static object GetString(ref Vector2 v)
	{
		return $"<{v.x.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.y.ToString("0.0###", CultureInfo.InvariantCulture)}>";
	}

	public static object GetString(ref Vector3 v)
	{
		return
			$"<{v.x.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.y.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.z.ToString("0.0###", CultureInfo.InvariantCulture)}>";
	}

	public static object GetString(ref Vector4 v)
	{
		return
			$"<{v.x.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.y.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.z.ToString("0.0###", CultureInfo.InvariantCulture)}, {v.w.ToString("0.0###", CultureInfo.InvariantCulture)}>";
	}

	public static object GetArgString(object[] args)
	{
		StringBuilder buffer = ArgsBuffer;
		buffer.Clear();

		bool first = true;
		foreach (object arg in args)
		{
			if (first)
			{
				first = false;
			}
			else
			{
				buffer.Append(" ");
			}

			buffer.Append(GetString(arg));
		}

		return buffer.ToString();
	}

	/// <summary>
	///   <para>Logs an Object to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Object for display.</param>
	public static void Print(Object val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Transform to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Transform for display.</param>
	public static void Print(Transform val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a bool to the Unity Console.</para>
	/// </summary>
	/// <param name="val">bool for display.</param>
	public static void Print(bool val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a char to the Unity Console.</para>
	/// </summary>
	/// <param name="val">char for display.</param>
	public static void Print(char val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a sbyte to the Unity Console.</para>
	/// </summary>
	/// <param name="val">sbyte for display.</param>
	public static void Print(sbyte val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a short to the Unity Console.</para>
	/// </summary>
	/// <param name="val">short for display.</param>
	public static void Print(short val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs an int to the Unity Console.</para>
	/// </summary>
	/// <param name="val">int for display.</param>
	public static void Print(int val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a long to the Unity Console.</para>
	/// </summary>
	/// <param name="val">long for display.</param>
	public static void Print(long val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a byte to the Unity Console.</para>
	/// </summary>
	/// <param name="val">byte for display.</param>
	public static void Print(byte val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a ushort to the Unity Console.</para>
	/// </summary>
	/// <param name="val">ushort for display.</param>
	public static void Print(ushort val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a uint to the Unity Console.</para>
	/// </summary>
	/// <param name="val">uint for display.</param>
	public static void Print(uint val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a ulong to the Unity Console.</para>
	/// </summary>
	/// <param name="val">ulong for display.</param>
	public static void Print(ulong val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a float to the Unity Console.</para>
	/// </summary>
	/// <param name="val">float for display.</param>
	public static void Print(float val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a double to the Unity Console.</para>
	/// </summary>
	/// <param name="val">double for display.</param>
	public static void Print(double val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a decimal to the Unity Console.</para>
	/// </summary>
	/// <param name="val">decimal for display.</param>
	public static void Print(decimal val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Vector2 to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Vector2 for display.</param>
	public static void Print(Vector2 val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(ref val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Vector3 to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Vector3 for display.</param>
	public static void Print(Vector3 val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(ref val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Vector4 to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Vector4 for display.</param>
	public static void Print(Vector4 val)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(ref val), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Vector2Int to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Vector2Int for display.</param>
	public static void Print(Vector2Int val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Vector3Int to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Vector3Int for display.</param>
	public static void Print(Vector3Int val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Matrix4x4 to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Matrix4x4 for display.</param>
	public static void Print(Matrix4x4 val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Quaternion to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Quaternion for display.</param>
	public static void Print(Quaternion val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a Rect to the Unity Console.</para>
	/// </summary>
	/// <param name="val">Rect for display.</param>
	public static void Print(Rect val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a RectInt to the Unity Console.</para>
	/// </summary>
	/// <param name="val">RectInt for display.</param>
	public static void Print(RectInt val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) val.ToString(), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a list the Unity Console.</para>
	/// </summary>
	/// <param name="list">List for display.</param>
	public static void Print<T>(IEnumerable<T> list)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(list), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a dictionary to the Unity Console.</para>
	/// </summary>
	/// <param name="dict">Dictionary for display.</param>
	public static void Print<TK, TV>(IDictionary<TK, TV> dict)
	{
		Debug.unityLogger.Log(defaultLogType, GetDictString(dict), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs pairs of KEY:VALUE to the Unity Console.</para>
	/// </summary>
	/// <param name="args">Pairs of Key, Values to log.</param>
	public static void PrintKeyValue(params object[] args)
	{
		Debug.unityLogger.Log(defaultLogType, GetKeyValuePairsString(args), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a list of items to the Unity Console.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static void Print(params object[] args)
	{
		Debug.unityLogger.Log(defaultLogType, GetArgString(args), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a list of Objects to the Unity Console.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static void Print(Object[] args)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(args), defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a list of GameObject to the Unity Console.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static void Print(GameObject[] args)
	{
		Debug.unityLogger.Log(defaultLogType, GetString(args), defaultLogContext);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- String, Value variants -- */

	/// <summary>
	///   <para>Logs a message and bool to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Print(string message, Object val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and bool to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Print(string message, bool val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and string to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static void Print(string message, string val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and char to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">char for display.</param>
	public static void Print(string message, char val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and sbyte to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">sbyte for display.</param>
	public static void Print(string message, sbyte val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and short to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">short for display.</param>
	public static void Print(string message, short val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and int to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">int for display.</param>
	public static void Print(string message, int val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and long to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">long for display.</param>
	public static void Print(string message, long val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and byte to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">byte for display.</param>
	public static void Print(string message, byte val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and ushort to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ushort for display.</param>
	public static void Print(string message, ushort val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and uint to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">uint for display.</param>
	public static void Print(string message, uint val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and ulong to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ulong for display.</param>
	public static void Print(string message, ulong val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and float to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">float for display.</param>
	public static void Print(string message, float val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and double to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">double for display.</param>
	public static void Print(string message, double val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and decimal to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">decimal for display.</param>
	public static void Print(string message, decimal val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Vector2 to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2 for display.</param>
	public static void Print(string message, Vector2 val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(ref val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Vector3 to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3 for display.</param>
	public static void Print(string message, Vector3 val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(ref val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Vector4 to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector4 for display.</param>
	public static void Print(string message, Vector4 val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {GetString(ref val)}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Vector2Int to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2Int for display.</param>
	public static void Print(string message, Vector2Int val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Vector3Int to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3Int for display.</param>
	public static void Print(string message, Vector3Int val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Matrix4x4 to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Matrix4x4 for display.</param>
	public static void Print(string message, Matrix4x4 val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Quaternion to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Quaternion for display.</param>
	public static void Print(string message, Quaternion val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and Rect to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Rect for display.</param>
	public static void Print(string message, Rect val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	/// <summary>
	///   <para>Logs a message and RectInt to the Unity Console.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">RectInt for display.</param>
	public static void Print(string message, RectInt val)
	{
		Debug.unityLogger.Log(defaultLogType, (object) $"{message} {val.ToString()}", defaultLogContext);
	}

	#endregion
	/* ------------------------------------------------------------------------------------- */

	/* ------------------------------------------------------------------------------------- */
	#region -- Util Methods --

	/// <summary>
	/// <para>
	///		Basic method to measure the execution time of code.
	///		Make sure to call <see cref="TimeEnd"/> or <see cref="TimeStop"/> for each call to <see cref="Time"/>
	/// </para>
	/// </summary>
	/// <param name="message">An optional message. to display before the measured time.</param>
	public static void Time(string message = "")
	{
		if (timerIndex >= timers.Count)
		{
			timers.Add(new TimerSet());
		}

		TimerSet set = timers[timerIndex++];
		set.timer.Restart();
		set.message = message;
	}

	/// <summary>
	/// <para>Stops the timer and prints the optional message set with <see cref="Time"/> and elapsed milliseconds.</para>
	/// </summary>
	public static void TimeEnd()
	{
		if (timerIndex <= 0)
			return;

		TimerSet set = timers[--timerIndex];
		set.timer.Stop();

		Print(set.message != ""
			? $"{set.message}: {set.timer.Elapsed.TotalMilliseconds}ms"
			: $"{set.timer.Elapsed.TotalMilliseconds}ms");
	}

	/// <summary>
	/// <para>Stops a timer without printing the message and time.</para>
	/// </summary>
	/// <returns>The elapsed milliseconds.</returns>
	public static double TimeStop()
	{
		if (timerIndex <= 0)
			return 0.0;

		TimerSet set = timers[--timerIndex];
		set.timer.Stop();
		return set.timer.Elapsed.TotalMilliseconds;
	}

	#endregion
	/* ------------------------------------------------------------------------------------- */

	public static void Reset()
	{
		#if DEBUG_DRAW
		LogMessage.Reset();
		#endif
	}

	public static void Clear()
	{
		#if DEBUG_DRAW
		LogMessage.Clear();
		#endif
	}

	public static void Clear(string groupName)
	{
		#if DEBUG_DRAW
		LogMessage.Clear(groupName);
		#endif
	}

	public static void Clear(Group group)
	{
		#if DEBUG_DRAW
		LogMessage.Clear(group);
		#endif
	}

	/// <summary>
	/// Creates a group with the specified name, and default duration.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="defaultDuration"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group CreateGroup(string name, EndTime? defaultDuration = null)
	{
		return LogMessage.CreateGroup(name, defaultDuration);
	}

	/// <summary>
	/// Use to release a created group when it is no longer needed.
	/// This will not clear items added to the group.
	/// </summary>
	/// <param name="group"></param>
	public static void ReleaseGroup(Group group)
	{
		LogMessage.ReleaseGroup(group);
	}

	///  <summary>
	///  Sets the current group all new items after this call are added to. This can be used to group and clear specific groups of items.
	///  Be sure to call a matching <see cref="EndGroup"/> for each call to <see cref="BeginGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>.<br/>
	///  An item will only be added to the group specified by the last call to this method, but nested calls to Begin/End are supported.<br/>
	///  If <see cref="NextGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/> is set, that will take precedence.<br/>
	///  Groups are cleared at the start of each frame so failing to call <see cref="EndGroup"/> will not cause a memory leak.
	///  </summary>
	///  <param name="name">The group name. Empty strings are ignored,
	/// 		and calling this multiple times in a row with the same name will have no effect.</param>
	///  <param name="defaultDuration"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group BeginGroup(string name, EndTime? defaultDuration = null)
	{
		return LogMessage.BeginGroup(name, defaultDuration);
	}

	/// <inheritdoc cref="BeginGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group BeginGroup(Group group)
	{
		return LogMessage.BeginGroup(group);
	}

	/// <summary>
	/// Ends the previous group.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void EndGroup()
	{
		LogMessage.EndGroup();
	}

	/// <summary>
	/// If not empty, only the next item add/drawn will be added to this group.<br/>
	/// Alternatively <see cref="BaseItem.Group"/> can be called to change individual items.<br/>
	///   e.g. `DebugDraw.Line(...).Group("MyGroupName");`
	/// </summary>
	/// <param name="name"></param>
	/// <param name="defaultDuration"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NextGroup(string name, EndTime? defaultDuration = null)
	{
		LogMessage.NextGroup(name, defaultDuration);
	}

	/// <inheritdoc cref="NextGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NextGroup(Group group)
	{
		LogMessage.NextGroup(group);
	}

	//

	private class TimerSet
	{
		public string message;
		public readonly Stopwatch timer = new();
	}

}

}
