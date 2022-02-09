using System.Runtime.CompilerServices;
using UnityEngine;

namespace DebugDrawItems
{

	public class Quad : BasePointItem
	{
		/* mesh: triangle */

		/// <summary>
		/// The second point of the quad.
		/// </summary>
		public Vector3 p2;
		/// <summary>
		/// The third point of the quad.
		/// </summary>
		public Vector3 p3;
		/// <summary>
		/// The fourth point of the quad.
		/// </summary>
		public Vector3 p4;
		/// <summary>
		/// The color of the quad's second point.
		/// </summary>
		public Color color2;
		/// <summary>
		/// The color of the quad's third point.
		/// </summary>
		public Color color3;
		/// <summary>
		/// The color of the quad's fourth point.
		/// </summary>
		public Color color4;
		/// <summary>
		/// True for a filled quad, otherwise a wire quad.
		/// It's important that this Quad item is added to a mesh with the right topology, either lines or quads,
		/// based on this setting.
		/// </summary>
		public bool filled;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a filled quad.
		/// </summary>
		/// <param name="p1">The first point of the quad.</param>
		/// <param name="p2">The second point of the quad.</param>
		/// <param name="p3">The third point of the quad.</param>
		/// <param name="p4">The fourth point of the quad.</param>
		/// <param name="color">The colour of the quad.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quad Get(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector3 p4, ref Color color, float duration = 0)
		{
			Quad item = ItemPool<Quad>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.p4 = p4;
			item.color = color;
			item.color2 = color;
			item.color3 = color;
			item.color4 = color;
			item.filled = true;

			return item;
		}
		
		/// <summary>
		/// Draws a filled quad.
		/// </summary>
		/// <param name="p1">The first point of the quad.</param>
		/// <param name="p2">The second point of the quad.</param>
		/// <param name="p3">The third point of the quad.</param>
		/// <param name="p4">The fourth point of the quad.</param>
		/// <param name="color1">The color of the quad's first point..</param>
		/// <param name="color2">The color of the quad's second point.</param>
		/// <param name="color3">The color of the quad's third point.</param>
		/// <param name="color4">The color of the quad's fourth point.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quad Get(
			ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector3 p4,
			ref Color color1, ref Color color2, ref Color color3, ref Color color4, float duration = 0)
		{
			Quad item = ItemPool<Quad>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.p4 = p4;
			item.color = color1;
			item.color2 = color2;
			item.color3 = color3;
			item.color4 = color4;
			item.filled = true;

			return item;
		}
		
		/// <summary>
		/// Draws a wire quad.
		/// </summary>
		/// <param name="p1">The first point of the quad.</param>
		/// <param name="p2">The second point of the quad.</param>
		/// <param name="p3">The third point of the quad.</param>
		/// <param name="p4">The third point of the quad.</param>
		/// <param name="color">The colour of the quad.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quad GetWire(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector3 p4, ref Color color, float duration = 0)
		{
			Quad item = ItemPool<Quad>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.p4 = p4;
			item.color = color;
			item.color2 = color;
			item.color3 = color;
			item.color4 = color;
			item.filled = false;

			return item;
		}
		
		/// <summary>
		/// Draws a wire quad.
		/// </summary>
		/// <param name="p1">The first point of the quad.</param>
		/// <param name="p2">The second point of the quad.</param>
		/// <param name="p3">The third point of the quad.</param>
		/// <param name="p4">The fourth point of the quad.</param>
		/// <param name="color1">The color of the quad's first point..</param>
		/// <param name="color2">The color of the quad's second point.</param>
		/// <param name="color3">The color of the quad's third point.</param>
		/// <param name="color4">The color of the quad's fourth point.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The ellipse object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quad GetWire(
			ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector3 p4,
			ref Color color1, ref Color color2, ref Color color3, ref Color color4, float duration = 0)
		{
			Quad item = ItemPool<Quad>.Get(duration);
			
			item.position = p1;
			item.p2 = p2;
			item.p3 = p3;
			item.p4 = p4;
			item.color = color1;
			item.color2 = color2;
			item.color3 = color3;
			item.color4 = color4;
			item.filled = false;

			return item;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		public override void SetPosition(Vector3 position)
		{
			Vector3 delta = new Vector3(
				position.x - (this.position.x + p2.x + p3.x + p4.x) * 0.25f,
				position.y - (this.position.y + p2.y + p3.y + p4.y) * 0.25f,
				position.z - (this.position.z + p2.z + p3.z + p4.z) * 0.25f);

			this.position.x += delta.x;
			this.position.y += delta.y;
			this.position.z += delta.z;
			p2.x += delta.x;
			p2.y += delta.y;
			p2.z += delta.z;
			p3.x += delta.x;
			p3.y += delta.y;
			p3.z += delta.z;
			p4.x += delta.x;
			p4.y += delta.y;
			p4.z += delta.z;
		}

		public override Vector3 GetPosition()
		{
			return new Vector3(
				(position.x + p2.x + p3.x + p4.x) * 0.5f,
				(position.y + p2.y + p3.y + p4.y) * 0.5f,
				(position.z + p2.z + p3.z + p4.z) * 0.5f);
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			mesh.AddVertices(this, ref position, ref p2, ref p3, ref p4);
			mesh.AddColors(this, ref color, ref color2, ref color3, ref color4);

			if (filled)
			{
				mesh.AddIndexX3();
				mesh.AddIndices(
					mesh.vertexIndex++,
					mesh.vertexIndex - 4,
					mesh.vertexIndex - 2);
			}
			else
			{
				mesh.AddIndices(
					// Line 1
					mesh.vertexIndex++,
					mesh.vertexIndex,
					// Line 2
					mesh.vertexIndex++,
					mesh.vertexIndex);
				mesh.AddIndices(
					// Line 3
					mesh.vertexIndex++,
					mesh.vertexIndex,
					// Line 4
					mesh.vertexIndex++,
					mesh.vertexIndex - 4);
			}
		}

		internal override void Release()
		{
			ItemPool<Quad>.Release(this);
		}

	}

}