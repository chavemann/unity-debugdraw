#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace
namespace DebugDrawUtils
{

public static partial class Log
{
	
	/*
	 * These methods are generated automatically from the Log.Print** methods.
	 */
	/* <TextGenMethods> */
	
	/// <summary>
	///   <para>Displays a message on the screen.</para>
	/// </summary>
	/// <param name="message">String for display.</param>
	public static LogMessage Text(string message)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, message) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message on the screen.</para>
	/// </summary>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public static LogMessage Text(object message)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) message) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a formatted message on the screen.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public static LogMessage TextFormat(string format, params object[] args)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, string.Format(format, args)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays an Object on the screen.</para>
	/// </summary>
	/// <param name="val">Object for display.</param>
	public static LogMessage Text(Object val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Transform on the screen.</para>
	/// </summary>
	/// <param name="val">Transform for display.</param>
	public static LogMessage Text(Transform val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a bool on the screen.</para>
	/// </summary>
	/// <param name="val">bool for display.</param>
	public static LogMessage Text(bool val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a char on the screen.</para>
	/// </summary>
	/// <param name="val">char for display.</param>
	public static LogMessage Text(char val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a sbyte on the screen.</para>
	/// </summary>
	/// <param name="val">sbyte for display.</param>
	public static LogMessage Text(sbyte val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a short on the screen.</para>
	/// </summary>
	/// <param name="val">short for display.</param>
	public static LogMessage Text(short val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays an int on the screen.</para>
	/// </summary>
	/// <param name="val">int for display.</param>
	public static LogMessage Text(int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a long on the screen.</para>
	/// </summary>
	/// <param name="val">long for display.</param>
	public static LogMessage Text(long val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a byte on the screen.</para>
	/// </summary>
	/// <param name="val">byte for display.</param>
	public static LogMessage Text(byte val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a ushort on the screen.</para>
	/// </summary>
	/// <param name="val">ushort for display.</param>
	public static LogMessage Text(ushort val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a uint on the screen.</para>
	/// </summary>
	/// <param name="val">uint for display.</param>
	public static LogMessage Text(uint val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a ulong on the screen.</para>
	/// </summary>
	/// <param name="val">ulong for display.</param>
	public static LogMessage Text(ulong val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a float on the screen.</para>
	/// </summary>
	/// <param name="val">float for display.</param>
	public static LogMessage Text(float val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a double on the screen.</para>
	/// </summary>
	/// <param name="val">double for display.</param>
	public static LogMessage Text(double val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a decimal on the screen.</para>
	/// </summary>
	/// <param name="val">decimal for display.</param>
	public static LogMessage Text(decimal val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Vector2 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector2 for display.</param>
	public static LogMessage Text(Vector2 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(ref val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Vector3 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector3 for display.</param>
	public static LogMessage Text(Vector3 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(ref val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Vector4 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector4 for display.</param>
	public static LogMessage Text(Vector4 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(ref val)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="val">Vector2Int for display.</param>
	public static LogMessage Text(Vector2Int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="val">Vector3Int for display.</param>
	public static LogMessage Text(Vector3Int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="val">Matrix4x4 for display.</param>
	public static LogMessage Text(Matrix4x4 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Quaternion on the screen.</para>
	/// </summary>
	/// <param name="val">Quaternion for display.</param>
	public static LogMessage Text(Quaternion val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a Rect on the screen.</para>
	/// </summary>
	/// <param name="val">Rect for display.</param>
	public static LogMessage Text(Rect val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a RectInt on the screen.</para>
	/// </summary>
	/// <param name="val">RectInt for display.</param>
	public static LogMessage Text(RectInt val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, val.ToString()) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Logs a list the Unity Console.</para>
	/// </summary>
	/// <param name="list">List for display.</param>
	public static LogMessage Text<T>(IEnumerable<T> list)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(list)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a dictionary on the screen.</para>
	/// </summary>
	/// <param name="dict">Dictionary for display.</param>
	public static LogMessage Text<TK, TV>(IDictionary<TK, TV> dict)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetDictString(dict)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays pairs of KEY:VALUE on the screen.</para>
	/// </summary>
	/// <param name="args">Pairs of Key, Values to log.</param>
	public static LogMessage TextKeyValue(params object[] args)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetKeyValuePairsString(args)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a list of items on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static LogMessage Text(params object[] args)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetArgString(args)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a list of Objects on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static LogMessage Text(Object[] args)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(args)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a list of GameObject on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public static LogMessage Text(GameObject[] args)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, (string) GetString(args)) : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and bool on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static LogMessage Text(string message, Object val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and bool on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static LogMessage Text(string message, bool val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and string on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public static LogMessage Text(string message, string val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and char on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">char for display.</param>
	public static LogMessage Text(string message, char val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and sbyte on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">sbyte for display.</param>
	public static LogMessage Text(string message, sbyte val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and short on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">short for display.</param>
	public static LogMessage Text(string message, short val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">int for display.</param>
	public static LogMessage Text(string message, int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and long on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">long for display.</param>
	public static LogMessage Text(string message, long val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and byte on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">byte for display.</param>
	public static LogMessage Text(string message, byte val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and ushort on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ushort for display.</param>
	public static LogMessage Text(string message, ushort val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and uint on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">uint for display.</param>
	public static LogMessage Text(string message, uint val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and ulong on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ulong for display.</param>
	public static LogMessage Text(string message, ulong val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and float on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">float for display.</param>
	public static LogMessage Text(string message, float val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and double on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">double for display.</param>
	public static LogMessage Text(string message, double val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and decimal on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">decimal for display.</param>
	public static LogMessage Text(string message, decimal val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Vector2 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2 for display.</param>
	public static LogMessage Text(string message, Vector2 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(ref val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Vector3 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3 for display.</param>
	public static LogMessage Text(string message, Vector3 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(ref val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Vector4 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector4 for display.</param>
	public static LogMessage Text(string message, Vector4 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {GetString(ref val)}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2Int for display.</param>
	public static LogMessage Text(string message, Vector2Int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3Int for display.</param>
	public static LogMessage Text(string message, Vector3Int val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Matrix4x4 for display.</param>
	public static LogMessage Text(string message, Matrix4x4 val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Quaternion on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Quaternion for display.</param>
	public static LogMessage Text(string message, Quaternion val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and Rect on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Rect for display.</param>
	public static LogMessage Text(string message, Rect val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/// <summary>
	///   <para>Displays a message and RectInt on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">RectInt for display.</param>
	public static LogMessage Text(string message, RectInt val)
	{
		#if DEBUG_DRAW
		return DebugDraw.hasInstance ? LogMessage.Add("", null, $"{message} {val.ToString()}") : null;
		#else
		return null;
		#endif
	}
	
	/* </TextGenMethods> */
	
}

}
