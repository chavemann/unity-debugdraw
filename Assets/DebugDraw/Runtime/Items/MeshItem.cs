using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws a wireframe mesh from a list of vertices, colors, and indices.
	/// It will be up to the user to ensure these are all valid.
	/// </summary>
	public class MeshItem : BaseItem
	{
		
		/* mesh: line */
		
		/// <summary>
		/// The list of vertices.
		/// </summary>
		public List<Vector3> vertices;
		
		/// <summary>
		/// The list of colors.
		/// </summary>
		public List<Color> colors;
		
		/// <summary>
		/// The list of triangle indices.
		/// </summary>
		public List<int> indices;
		
		/// <summary>
		/// If non-null, will be used instead of the color list.
		/// </summary>
		public new Color? color;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */
		
		/// <summary>
		/// Draws a wireframe mesh.
		/// </summary>
		/// <param name="vertices">The list of vertices.</param>
		/// <param name="colors">The list of colors.</param>
		/// <param name="indices">The list of triangle indices.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MeshItem Get(List<Vector3> vertices, List<Color> colors, List<int> indices, EndTime? duration = null)
		{
			MeshItem item = ItemPool<MeshItem>.Get(duration);
			
			item.vertices = vertices;
			item.colors = colors;
			item.indices = indices;
			item.color = null;
			
			return item;
		}
		
		/// <summary>
		/// Draws a wireframe mesh with a single color.
		/// </summary>
		/// <param name="vertices">The list of vertices.</param>
		/// <param name="indices">The list of triangle indices.</param>
		/// <param name="color">The color of the mesh.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MeshItem Get(List<Vector3> vertices, List<int> indices, ref Color color, EndTime? duration = null)
		{
			MeshItem item = ItemPool<MeshItem>.Get(duration);
			
			item.vertices = vertices;
			item.colors = null;
			item.indices = indices;
			item.color = color;
			
			return item;
		}
		
		/// <summary>
		/// Draws a wireframe mesh. This will allocate new lists and fetch the mesh data so it's advisable to
		/// not call this every frame and instead create it once keep a reference to it.
		/// </summary>
		/// <param name="mesh">The mesh.</param>
		/// <param name="color">The color of the mesh. If null the mesh must have color data associated with it</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Line object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MeshItem Get(Mesh mesh, Color? color, EndTime? duration = null)
		{
			MeshItem item = ItemPool<MeshItem>.Get(duration);
			
			item.vertices = new List<Vector3>();
			item.indices = new List<int>();
			
			mesh.GetVertices(item.vertices);
			mesh.GetIndices(item.indices, 0);
			
			if (!color.HasValue)
			{
				item.colors = new List<Color>();
				mesh.GetColors(item.colors);
				
				if (item.colors.Count != item.vertices.Count)
				{
					item.colors.Clear();
					
					for (int i = item.vertices.Count - 1; i >= 0; i--)
					{
						item.colors.Add(DebugDraw.colorIdentity);
					}
				}
			}
			else
			{
				item.colors = null;
			}
			
			item.color = color;
			
			return item;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		internal override void Build(DebugDrawMesh mesh)
		{
			int vertexIndex = mesh.vertexIndex;
			
			if (color.HasValue)
			{
				Color clr = color.GetValueOrDefault();
				clr = GetColor(ref clr);
				
				List<Color> meshColors = mesh.colours;
				
				for (int i = vertices.Count - 1; i >= 0; i--)
				{
					meshColors.Add(clr);
				}
			}
			else
			{
				mesh.colours.AddRange(colors);
			}
			
			if (hasStateTransform)
			{
				List<Vector3> meshVertices = mesh.vertices;
				List<Vector3> vertices = this.vertices;
				
				ref Matrix4x4 m = ref stateTransform;
				
				foreach (Vector3 vertex in vertices)
				{
					meshVertices.Add(m.MultiplyPoint3x4(vertex));
				}
			}
			else
			{
				mesh.vertices.AddRange(vertices);
			}
			
			List<int> meshIndices = mesh.indices;
			List<int> indices = this.indices;
			
			for (int i = indices.Count - 3; i >= 0; i -= 3)
			{
				int i1 = vertexIndex + indices[i];
				int i2 = vertexIndex + indices[i + 1];
				int i3 = vertexIndex + indices[i + 2];
				
				meshIndices.Add(i1);
				meshIndices.Add(i2);
				meshIndices.Add(i2);
				meshIndices.Add(i3);
				meshIndices.Add(i3);
				meshIndices.Add(i1);
			}
			
			mesh.vertexIndex += vertices.Count;
		}
		
		internal override void Release()
		{
			ItemPool<MeshItem>.Release(this);
		}
		
	}

}
