#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace
namespace DebugDrawUtils
{

public partial class LogMessage
{

	/*
	 * These methods are generated automatically from the Log.Print** methods.
	 */
	/* <TextGenMethods> */
	
	/// <summary>
	///   <para>Displays a message on the screen.</para>
	/// </summary>
	/// <param name="message">String for display.</param>
	public LogMessage Text(string message)
	{
		return SetText(message);
	}
	
	/// <summary>
	///   <para>Displays a message on the screen.</para>
	/// </summary>
	/// <param name="message">String or object to be converted to string representation for display.</param>
	public LogMessage Text(object message)
	{
		return SetText((string) message);
	}
	
	/// <summary>
	///   <para>Displays a formatted message on the screen.</para>
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Format arguments.</param>
	public LogMessage TextFormat(string format, params object[] args)
	{
		return SetText(string.Format(format, args));
	}
	
	/// <summary>
	///   <para>Displays an Object on the screen.</para>
	/// </summary>
	/// <param name="val">Object for display.</param>
	public LogMessage Text(Object val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a Transform on the screen.</para>
	/// </summary>
	/// <param name="val">Transform for display.</param>
	public LogMessage Text(Transform val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a bool on the screen.</para>
	/// </summary>
	/// <param name="val">bool for display.</param>
	public LogMessage Text(bool val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a char on the screen.</para>
	/// </summary>
	/// <param name="val">char for display.</param>
	public LogMessage Text(char val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a sbyte on the screen.</para>
	/// </summary>
	/// <param name="val">sbyte for display.</param>
	public LogMessage Text(sbyte val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a short on the screen.</para>
	/// </summary>
	/// <param name="val">short for display.</param>
	public LogMessage Text(short val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays an int on the screen.</para>
	/// </summary>
	/// <param name="val">int for display.</param>
	public LogMessage Text(int val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a long on the screen.</para>
	/// </summary>
	/// <param name="val">long for display.</param>
	public LogMessage Text(long val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a byte on the screen.</para>
	/// </summary>
	/// <param name="val">byte for display.</param>
	public LogMessage Text(byte val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a ushort on the screen.</para>
	/// </summary>
	/// <param name="val">ushort for display.</param>
	public LogMessage Text(ushort val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a uint on the screen.</para>
	/// </summary>
	/// <param name="val">uint for display.</param>
	public LogMessage Text(uint val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a ulong on the screen.</para>
	/// </summary>
	/// <param name="val">ulong for display.</param>
	public LogMessage Text(ulong val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a float on the screen.</para>
	/// </summary>
	/// <param name="val">float for display.</param>
	public LogMessage Text(float val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a double on the screen.</para>
	/// </summary>
	/// <param name="val">double for display.</param>
	public LogMessage Text(double val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a decimal on the screen.</para>
	/// </summary>
	/// <param name="val">decimal for display.</param>
	public LogMessage Text(decimal val)
	{
		return SetText((string) Log.GetString(val));
	}
	
	/// <summary>
	///   <para>Displays a Vector2 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector2 for display.</param>
	public LogMessage Text(Vector2 val)
	{
		return SetText((string) Log.GetString(ref val));
	}
	
	/// <summary>
	///   <para>Displays a Vector3 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector3 for display.</param>
	public LogMessage Text(Vector3 val)
	{
		return SetText((string) Log.GetString(ref val));
	}
	
	/// <summary>
	///   <para>Displays a Vector4 on the screen.</para>
	/// </summary>
	/// <param name="val">Vector4 for display.</param>
	public LogMessage Text(Vector4 val)
	{
		return SetText((string) Log.GetString(ref val));
	}
	
	/// <summary>
	///   <para>Displays a Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="val">Vector2Int for display.</param>
	public LogMessage Text(Vector2Int val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Displays a Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="val">Vector3Int for display.</param>
	public LogMessage Text(Vector3Int val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Displays a Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="val">Matrix4x4 for display.</param>
	public LogMessage Text(Matrix4x4 val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Displays a Quaternion on the screen.</para>
	/// </summary>
	/// <param name="val">Quaternion for display.</param>
	public LogMessage Text(Quaternion val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Displays a Rect on the screen.</para>
	/// </summary>
	/// <param name="val">Rect for display.</param>
	public LogMessage Text(Rect val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Displays a RectInt on the screen.</para>
	/// </summary>
	/// <param name="val">RectInt for display.</param>
	public LogMessage Text(RectInt val)
	{
		return SetText(val.ToString());
	}
	
	/// <summary>
	///   <para>Logs a list the Unity Console.</para>
	/// </summary>
	/// <param name="list">List for display.</param>
	public LogMessage Text<T>(IEnumerable<T> list)
	{
		return SetText((string) Log.GetString(list));
	}
	
	/// <summary>
	///   <para>Displays a dictionary on the screen.</para>
	/// </summary>
	/// <param name="dict">Dictionary for display.</param>
	public LogMessage Text<TK, TV>(IDictionary<TK, TV> dict)
	{
		return SetText((string) Log.GetDictString(dict));
	}
	
	/// <summary>
	///   <para>Displays pairs of KEY:VALUE on the screen.</para>
	/// </summary>
	/// <param name="args">Pairs of Key, Values to log.</param>
	public LogMessage TextKeyValue(params object[] args)
	{
		return SetText((string) Log.GetKeyValuePairsString(args));
	}
	
	/// <summary>
	///   <para>Displays a list of items on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public LogMessage Text(params object[] args)
	{
		return SetText((string) Log.GetArgString(args));
	}
	
	/// <summary>
	///   <para>Displays a list of Objects on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public LogMessage Text(Object[] args)
	{
		return SetText((string) Log.GetString(args));
	}
	
	/// <summary>
	///   <para>Displays a list of GameObject on the screen.</para>
	/// </summary>
	/// <param name="args">Items for display.</param>
	public LogMessage Text(GameObject[] args)
	{
		return SetText((string) Log.GetString(args));
	}
	
	/// <summary>
	///   <para>Displays a message and bool on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public LogMessage Text(string message, Object val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and bool on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public LogMessage Text(string message, bool val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and string on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">bool for display.</param>
	public LogMessage Text(string message, string val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and char on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">char for display.</param>
	public LogMessage Text(string message, char val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and sbyte on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">sbyte for display.</param>
	public LogMessage Text(string message, sbyte val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and short on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">short for display.</param>
	public LogMessage Text(string message, short val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">int for display.</param>
	public LogMessage Text(string message, int val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and long on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">long for display.</param>
	public LogMessage Text(string message, long val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and byte on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">byte for display.</param>
	public LogMessage Text(string message, byte val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and ushort on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ushort for display.</param>
	public LogMessage Text(string message, ushort val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and uint on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">uint for display.</param>
	public LogMessage Text(string message, uint val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and ulong on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">ulong for display.</param>
	public LogMessage Text(string message, ulong val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and float on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">float for display.</param>
	public LogMessage Text(string message, float val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and double on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">double for display.</param>
	public LogMessage Text(string message, double val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and decimal on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">decimal for display.</param>
	public LogMessage Text(string message, decimal val)
	{
		return SetText($"{message} {Log.GetString(val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and Vector2 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2 for display.</param>
	public LogMessage Text(string message, Vector2 val)
	{
		return SetText($"{message} {Log.GetString(ref val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and Vector3 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3 for display.</param>
	public LogMessage Text(string message, Vector3 val)
	{
		return SetText($"{message} {Log.GetString(ref val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and Vector4 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector4 for display.</param>
	public LogMessage Text(string message, Vector4 val)
	{
		return SetText($"{message} {Log.GetString(ref val)}");
	}
	
	/// <summary>
	///   <para>Displays a message and Vector2Int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector2Int for display.</param>
	public LogMessage Text(string message, Vector2Int val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/// <summary>
	///   <para>Displays a message and Vector3Int on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Vector3Int for display.</param>
	public LogMessage Text(string message, Vector3Int val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/// <summary>
	///   <para>Displays a message and Matrix4x4 on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Matrix4x4 for display.</param>
	public LogMessage Text(string message, Matrix4x4 val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/// <summary>
	///   <para>Displays a message and Quaternion on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Quaternion for display.</param>
	public LogMessage Text(string message, Quaternion val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/// <summary>
	///   <para>Displays a message and Rect on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">Rect for display.</param>
	public LogMessage Text(string message, Rect val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/// <summary>
	///   <para>Displays a message and RectInt on the screen.</para>
	/// </summary>
	/// <param name="message">Message for display.</param>
	/// <param name="val">RectInt for display.</param>
	public LogMessage Text(string message, RectInt val)
	{
		return SetText($"{message} {val.ToString()}");
	}
	
	/* </TextGenMethods> */

}

}
