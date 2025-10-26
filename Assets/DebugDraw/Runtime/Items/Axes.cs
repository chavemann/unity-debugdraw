using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

/// <summary>
/// Draws a set of axes.
/// </summary>
public class Axes : BasePointItem
	{
		
		/* mesh: line */
		
		private static readonly Color XAxisColor = Color.red;
		private static readonly Color YAxisColor = Color.green;
		private static readonly Color ZAxisColor = Color.blue;
		
		/// <summary>
		/// The orientation of the axes.
		/// </summary>
		public Quaternion rotation;
		
		/// <summary>
		/// The size of each axis. Set to zero to not draw an axis.
		/// </summary>
		public Vector3 size;
		
		/// <summary>
		/// If true the axis line extends in both directions, other only in the positive.
		/// </summary>
		public bool doubleSided;
		
		/// <summary>
		/// The color of the x axis. Defaults to red.
		/// </summary>
		public Color xColor
		{
			get => color;
			set => color = value;
		}
		
		/// <summary>
		/// The color of the y axis. Defaults to green.
		/// </summary>
		public Color yColor;
		
		/// <summary>
		/// The color of the z axis. Defaults to blue.
		/// </summary>
		public Color zColor;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws lines along the x, y, and z axes.
		/// </summary>
		/// <param name="position">The axes origin.</param>
		/// <param name="rotation">The orientation of the axes.</param>
		/// <param name="size">The size of each axis. Set to zero to not draw an axis.</param>
		/// <param name="doubleSided">If true the axis line extends in both directions, other only in the positive.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Axes object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Axes Get(ref Vector3 position, ref Quaternion rotation, ref Vector3 size, bool doubleSided = false, EndTime? duration = null)
		{
			Axes item = ItemPool<Axes>.Get(duration);
			
			item.position = position;
			item.rotation = rotation;
			item.doubleSided = doubleSided;
			item.size = size;
			item.color = XAxisColor;
			item.yColor = YAxisColor;
			item.zColor = ZAxisColor;
			
			return item;
		}
		
		/// <summary>
		/// Draws lines along the x, y, and z axes.
		/// </summary>
		/// <param name="position">The axes origin.</param>
		/// <param name="rotation">The orientation of the axes.</param>
		/// <param name="size">The size of each the axes.</param>
		/// <param name="doubleSided">If true the axis line extends in both directions, other only in the positive.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Axes object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Axes Get(ref Vector3 position, ref Quaternion rotation, float size, bool doubleSided = false, EndTime? duration = null)
		{
			Axes item = ItemPool<Axes>.Get(duration);
			
			item.position = position;
			item.rotation = rotation;
			item.doubleSided = doubleSided;
			item.size = new Vector3(size, size, size);
			item.color = XAxisColor;
			item.yColor = YAxisColor;
			item.zColor = ZAxisColor;
			
			return item;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		/// <summary>
		/// Sets a custom colors for all axis.
		/// </summary>
		/// <param name="color">The custom color.</param>
		/// <returns></returns>
		public Axes SetColours(Color color)
		{
			xColor = color;
			yColor = color;
			zColor = color;
			
			return this;
		}
		
		/// <summary>
		/// Sets a custom colors for each axis.
		/// </summary>
		/// <param name="xColor">The custom x axis color.</param>
		/// <param name="yColor">The custom y axis color.</param>
		/// <param name="zColor">The custom z axis color.</param>
		/// <returns></returns>
		public Axes SetColours(Color xColor, Color yColor, Color zColor)
		{
			this.xColor = xColor;
			this.yColor = yColor;
			this.zColor = zColor;
			
			return this;
		}
		
		internal override void Build(DebugDrawMesh mesh)
		{
			Matrix4x4 m = Matrix4x4.TRS(position, rotation, Vector3.one);
			
			if (size.x > 0)
			{
				Color clr = GetColor(ref color);
				Vector3 p1 = new Vector3(doubleSided ? -size.x : 0, 0, 0);
				Vector3 p2 = new Vector3(size.x, 0, 0);
				mesh.AddLine(ref m, ref p1, ref p2, ref clr, ref clr);
			}
			
			if (size.y > 0)
			{
				Color clr = GetColor(ref yColor);
				Vector3 p1 = new Vector3(0, doubleSided ? -size.y : 0, 0);
				Vector3 p2 = new Vector3(0, size.y, 0);
				mesh.AddLine(ref m, ref p1, ref p2, ref clr, ref clr);
			}
			
			if (size.z > 0)
			{
				Color clr = GetColor(ref zColor);
				Vector3 p1 = new Vector3(0, 0, doubleSided ? -size.z : 0);
				Vector3 p2 = new Vector3(0, 0, size.z);
				mesh.AddLine(ref m, ref p1, ref p2, ref clr, ref clr);
			}
		}
		
		internal override void Release()
		{
			ItemPool<Axes>.Release(this);
		}
		
	}

}
