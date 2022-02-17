using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	/// <summary>
	/// Displays some text at the specified position. Note that this uses GUI.Label so
	/// it isn't true 3D text.
	/// </summary>
	public class Text : BasePointItem
	{
		/* mesh: text */

		/// <summary>
		/// The text to display.
		/// </summary>
		public string text;
		/// <summary>
		/// Where to anchor the text.
		/// </summary>
		public TextAnchor align;
		/// <summary>
		/// The text scale. Set to 1 for default.
		/// </summary>
		public float scale;
		/// <summary>
		/// If true, the text will scale based on the distance to the camera.
		/// </summary>
		public bool useWorldSize;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="position">The world space position of the text.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="color">The text color.</param>
		/// <param name="align">Where to anchor the text.</param>
		/// <param name="scale">The text scale. Set to 1 for default.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Text object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Text Get(ref Vector3 position, string text, ref Color color, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
		{
			Text item = ItemPool<Text>.Get(duration);
			
			item.position = position;
			item.text = text;
			item.color = color;
			item.align = align;
			item.scale = scale;
			item.useWorldSize = false;

			return item;
		}

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="position">The world space position of the text.</param>
		/// <param name="text">The text to display.</param>
		/// <param name="align">Where to anchor the text.</param>
		/// <param name="scale">The text scale. Set to 1 for default.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
		/// <returns>The Text object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Text Get(ref Vector3 position, string text, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
		{
			return Get(ref position, text, ref DebugDraw.colorIdentity, align, scale, duration);
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		public Text SetUseWorldSize(bool useWorldSize = true)
		{
			this.useWorldSize = useWorldSize;

			return this;
		}

		internal override void Release()
		{
			ItemPool<Text>.Release(this);
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			throw new System.NotImplementedException();
		}

	}

}