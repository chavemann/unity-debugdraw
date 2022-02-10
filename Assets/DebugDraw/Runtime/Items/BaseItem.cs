using System.Runtime.CompilerServices;
using DebugDrawAttachments;
using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// The base for all debug visual items.
	/// </summary>
	public abstract class BaseItem
	{

		protected const float BaseAutoSizeDistanceFactor = 1 / 25f;

		/// <summary>
		/// This item's color.
		/// </summary>
		public Color color;

		/// <summary>
		/// The mesh this item was added to.
		/// </summary>
		internal DebugDrawMesh mesh;
		/// <summary>
		/// The index of this mesh once added to a <see cref="DebugDrawMesh"/>.
		/// </summary>
		internal int index = -1;
		/// <summary>
		/// When this item expires. Calculated when created based on a duration.
		/// </summary>
		internal float expires;

		/// <summary>
		/// Stores the global <see cref="DebugDraw"/> transform state when this item was created.
		/// </summary>
		internal Matrix4x4 stateTransform;
		/// <summary>
		/// True if is stateTransform not default.
		/// </summary>
		internal bool hasStateTransform;
		/// <summary>
		/// Stores the global <see cref="DebugDraw"/> color state when this item was created.
		/// </summary>
		internal Color stateColor;
		/// <summary>
		/// True if is stateColor not default.
		/// </summary>
		internal bool hasStateColor;

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Updates the duration of this item.
		/// </summary>
		/// <param name="duration">The duration starting from the current time.</param>
		public T SetDuration<T>(float duration) where T : BaseItem
		{
			expires = DebugDraw.GetTime(duration);
			return (T) this;
		}

		/// <inheritdoc cref="SetDuration{T}(float)"/>
		public BaseItem SetDuration(float duration)
		{
			expires = DebugDraw.GetTime(duration);
			return this;
		}

		/// <summary>
		/// Sets this item's transform. Normally this will be set by using <see cref="DebugDraw.transform"/>.
		/// </summary>
		/// <param name="transform"></param>
		public void SetGlobalTransform(Matrix4x4 transform)
		{
			hasStateTransform = transform != DebugDraw.matrixIdentity;

			if (hasStateTransform)
			{
				stateTransform = transform;
			}
		}

		/// <summary>
		/// Clears this item's transform. Normally this will be set by using <see cref="DebugDraw.transform"/>.
		/// </summary>
		public void ClearGlobalTransform()
		{
			hasStateTransform = false;
		}

		/// <summary>
		/// Sets this item's color. Normally this will be set by using <see cref="DebugDraw.color"/>.
		/// </summary>
		/// <param name="color"></param>
		public void SetGlobalColor(Color color)
		{
			hasStateColor = color != DebugDraw.colorIdentity;

			if (hasStateColor)
			{
				stateColor = color;
			}
		}

		/// <summary>
		/// Clears this item's color. Normally this will be set by using <see cref="DebugDraw.color"/>.
		/// </summary>
		public void ClearGlobalColor()
		{
			hasStateColor = false;
		}

		/// <summary>
		/// Multiplies the given color with this item state's color if it has one
		/// </summary>
		/// <param name="color">.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Color GetColor(ref Color color) => hasStateColor ? color * stateColor : color;

		/// <summary>
		/// Removes this item.
		/// </summary>
		public void Remove()
		{
			if (index != -1)
			{
				mesh?.Remove(this);
			}
		}

		/// <summary>
		/// Calculates and adds this item's vertices to the given mesh.
		/// </summary>
		/// <param name="mesh">The mesh to build into.</param>
		internal abstract void Build(DebugDrawMesh mesh);
		
		/// <summary>
		/// Releases this item when it gets removed from a mesh, returning it to a pool and
		/// resetting any values if necessary.
		/// </summary>
		internal abstract void Release();

		public static implicit operator bool(BaseItem baseItem)
		{
			return baseItem != null;
		}

	}

}