using System.Runtime.CompilerServices;
using UnityEngine;

namespace Items
{

	/// <summary>
	/// Displays some text at the specified position. Note that this uses GUI.Label so
	/// it isn't true 3D text.
	/// </summary>
	public class Text : BaseItem
	{
		/* mesh: text */

		/// <summary>
		/// The world space position of the text.
		/// </summary>
		public Vector3 position;
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
		public bool autoSize;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="position">The world space position of the text</param>
		/// <param name="text">The text to display</param>
		/// <param name="color">The text color</param>
		/// <param name="align">Where to anchor the text</param>
		/// <param name="scale">The text scale. Set to 1 for default</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
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

			return item;
		}

		/// <summary>
		/// Draws a line.
		/// </summary>
		/// <param name="position">The world space position of the text</param>
		/// <param name="text">The text to display</param>
		/// <param name="align">Where to anchor the text</param>
		/// <param name="scale">The text scale. Set to 1 for default</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist.</param>
		/// <returns>The Text object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Text Get(ref Vector3 position, string text, TextAnchor align = TextAnchor.UpperLeft, float scale = 1, float duration = 0)
		{
			Color color = Color.white;
			return Get(ref position, text, ref color, align, scale, duration);
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		public Text SetAutoSize(bool autoSize = true)
		{
			this.autoSize = autoSize;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			throw new System.NotImplementedException();
		}

		internal override void Release()
		{
			ItemPool<Text>.Release(this);
		}

	}

}