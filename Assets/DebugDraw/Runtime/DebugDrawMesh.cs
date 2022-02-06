using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugDrawItems;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Direct access to a debug mesh.
/// Normally this class won't be used directly - instead it will automatically be created and managed by <see cref="DebugDraw"/>.
/// </summary>
public partial class DebugDrawMesh
{
	
	/* ------------------------------------------------------------------------------------- */
	/* -- Shader Params -- */
		
	public static readonly int SrcBlend	= Shader.PropertyToID("_SrcBlend");
	public static readonly int DstBlend	= Shader.PropertyToID("_DstBlend");
	public static readonly int Cull		= Shader.PropertyToID("_Cull");
	public static readonly int ZWrite	= Shader.PropertyToID("_ZWrite");
	public static readonly int ZTest	= Shader.PropertyToID("_ZTest");

	/* ------------------------------------------------------------------------------------- */
	/* -- Public -- */
	
	/// <summary>
	/// The mesh vertices populated during <see cref="Build"/> - do not modify directly. 
	/// </summary>
	public readonly List<Vector3> vertices = new List<Vector3>();
	/// <summary>
	/// The mesh vertex colours populated during <see cref="Build"/> - do not modify directly. 
	/// </summary>
	public readonly List<Color> colours = new List<Color>();
	/// <summary>
	/// The mesh indices populated during <see cref="Build"/> - do not modify directly. 
	/// </summary>
	public readonly List<int> indices = new List<int>();

	/* ------------------------------------------------------------------------------------- */
	/* -- Private -- */

	private bool hasMesh;
	internal Mesh mesh;
	private MeshRenderer meshRenderer;
	private readonly MeshTopology type;
	private bool hasMaterial;
	internal Material material;
	
	protected readonly List<BaseItem> items = new List<BaseItem>();
	private int itemsSize = 1;
	internal int itemCount;

	/// <summary>
	/// Tracks the current vertex index during <see cref="Build"/>.
	/// </summary>
	internal int vertexIndex;

	/* ------------------------------------------------------------------------------------- */
	/* -- Init -- */

	/// <summary>
	/// Creates a mesh with the specified topology
	/// </summary>
	/// <param name="type">The kind of mesh topology</param>
	public DebugDrawMesh(MeshTopology type)
	{
		this.type = type;
		
		for (int i = 0; i < itemsSize; i++)
		{
			items.Add(null);
		}
	}

	/// <summary>
	/// Creates the <see cref="Mesh"/> used by this instance if it does not exist.
	/// </summary>
	public void CreateMesh()
	{
		if (!hasMesh || !mesh)
		{
			mesh = new Mesh { hideFlags = HideFlags.HideAndDontSave };
			mesh.MarkDynamic();
			hasMesh = true;
		}
	}

	internal void CreateMaterial()
	{
		if (!hasMaterial || !material)
		{
			hasMaterial = true;
			material = new Material(Shader.Find("Hidden/Internal-Colored")) { hideFlags = HideFlags.HideAndDontSave };
			SetInvertColours(false);
			SetCulling(CullMode.Front);
			SetDepthTesting();
		}
	}

	internal void CreateAll()
	{
		CreateMesh();
		CreateMaterial();
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */

	/// <summary>
	/// Sets this mesh's material (if it has one) blend mode to invert destination colors.
	/// </summary>
	/// <param name="invert">True to invert colours</param>
	public DebugDrawMesh SetInvertColours(bool invert = true)
	{
		if (!hasMaterial)
			return this;
		
		if (invert)
		{
			material.SetInt(SrcBlend, (int) BlendMode.OneMinusDstColor);
			material.SetInt(DstBlend, (int) BlendMode.Zero);
		}
		else
		{
			material.SetInt(SrcBlend, (int) BlendMode.SrcAlpha);
			material.SetInt(DstBlend, (int) BlendMode.OneMinusSrcAlpha);
		}

		return this;
	}

	/// <summary>
	/// Sets this mesh's material (if it has one) culling mode.
	/// </summary>
	/// <param name="mode">The cull mode</param>
	public DebugDrawMesh SetCulling(CullMode mode)
	{
		if (!hasMaterial)
			return this;
		
		material.SetInt(Cull, (int) mode);
		
		return this;
	}
	
	/// <summary>
	/// Sets this mesh's material (if it has one) depth testing.
	/// </summary>
	/// <param name="enabled">Is depth testing enabled</param>
	public DebugDrawMesh SetDepthTesting(bool enabled = true)
	{
		if (!hasMaterial)
			return this;

		SetDepthTesting(enabled, enabled);

		return this;
	}

	/// <summary>
	/// Sets this mesh's material (if it has one) depth testing.
	/// </summary>
	/// <param name="write">Enable depth writes</param>
	/// <param name="test">Enable depth tests</param>
	public DebugDrawMesh SetDepthTesting(bool write, bool test)
	{
		if (!hasMaterial)
			return this;
		
		material.SetInt(ZWrite, write ? 1 : 0);
		material.SetInt(ZTest, (int) (test
			? CompareFunction.LessEqual
			: CompareFunction.Always));

		return this;
	}
	
	/// <summary>
	/// Add an item to this mesh. Items are only "rendered" when <see cref="Build"/> is called.
	/// Normally this method won't be used directly - instead use the specific debug methods (e.g. <see cref="Items.Line"/>)
	/// or the similar static methods in <see cref="DebugDraw"/>
	/// </summary>
	/// <param name="item"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T Add<T>(T item) where T : BaseItem
	{
		if (item.mesh != null)
			return item;
		
		if (itemCount == itemsSize)
		{
			itemsSize *= 2;
		
			for (int i = itemCount; i < itemsSize; i++)
			{
				items.Add(null);
			}
		}
			
		items[item.index = itemCount++] = item;
		item.mesh = this;
		return item;
	}
	
	/// <summary>
	/// Immediately removes the item from this mesh.
	/// </summary>
	/// <param name="item">The item to remove</param>
	public void Remove(BaseItem item)
	{
		if (item.mesh != this)
			return;
		
		BaseItem swapped = items[--itemCount];
		swapped.index = item.index;
		items[item.index] = swapped;

		item.index = -1;
		item.mesh = null;
		item.Release();
	}

	/// <summary>
	/// Removes all debug items added to this mesh.
	/// </summary>
	public void Clear()
	{
		for (int i = itemCount - 1; i >= 0; i--)
		{
			BaseItem item = items[i];
			item.index = -1;
			item.mesh = null;
			item.Release();
		}

		itemCount = 0;
	}

	/// <summary>
	/// Clears the this DebugDrawMesh's <see cref="Mesh"/> if it has been created.
	/// </summary>
	public void ClearMesh()
	{
		if (!hasMesh)
			return;
		
		vertices.Clear();
		colours.Clear();
		indices.Clear();
		mesh.Clear();
	}

	/// <summary>
	/// Clears all the items as well as this DebugDrawMesh's <see cref="Mesh"/>
	/// </summary>
	public void ClearAll()
	{
		Clear();
		ClearMesh();
	}

	internal void OffsetTime(float time)
	{
		for (int i = itemCount - 1; i >= 0; i--)
		{
			BaseItem item = items[i];

			if (item.expires != float.PositiveInfinity)
			{
				item.expires += time;
			}
		}
	}

	/// <summary>
	/// Updates all items, clearing ones that have expired.
	/// </summary>
	public void Update()
	{
		float time = DebugDraw.GetTime();

		for(int i = itemCount - 1; i >= 0; i--)
		{
			BaseItem item = items[i];
			
			if(item.expires < time)
			{
				item.index = -1;
				item.mesh = null;
				item.Release();
				
				item = items[--itemCount];
				item.index = i;
				items[i] = item;
			}
		}
	}

	/// <summary>
	/// Builds all the debug items, filling the <see cref="vertices"/>, <see cref="colours"/>, and <see cref="indices"/> arrays.
	/// Will also call <see cref="UpdateMesh"/> if this instance has an associated <see cref="Mesh"/>
	/// </summary>
	public void Build()
	{
		if (itemCount == 0)
		{
			if (vertexIndex > 0)
			{
				vertices.Clear();
				colours.Clear();
				indices.Clear();
				mesh.Clear(false);
				vertexIndex = 0;
			}
			
			return;
		}
		
		vertices.Clear();
		colours.Clear();
		indices.Clear();

		vertexIndex = 0;
		
		for(int i = itemCount - 1; i >= 0; i--)
		{
			items[i].Build(this);
		}

		if (hasMesh)
		{
			UpdateMesh(mesh);
		}
	}

	/// <summary>
	/// Push the new vertex data to the mesh. If <see cref="CreateMesh"/> was called, this will automatically
	/// be called during <see cref="Build"/>
	/// </summary>
	/// <param name="mesh">The mesh to update</param>
	public void UpdateMesh(Mesh mesh)
	{
		mesh.Clear(false);
		
		if (vertexIndex > 0)
		{
			mesh.SetVertices(vertices);
			mesh.SetColors(colours);
			mesh.SetIndices(indices, type, 0);
		}
	}
	
	/* ------------------------------------------------------------------------------------- */
	#region >> Utility Methods <<
	/* ------------------------------------------------------------------------------------- */

	/* ------------------------------------------------------------------------------------- */
	/* -- Index -- */
	
	
	/// <summary>
	/// Adds an index, incrementing <see cref="vertexIndex"/>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddIndex()
	{
		indices.Add(vertexIndex++);
	}

	/// <summary>
	/// Adds an index.
	/// </summary>
	/// <param name="index">The index to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddIndex(int index)
	{
		indices.Add(index);
	}

	/// <summary>
	/// Adds a previous index (<c>vertexIndex - fromEnd</c>)
	/// </summary>
	/// <param name="fromEnd"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddPreviousVertexIndex(int fromEnd = 1)
	{
		indices.Add(vertexIndex - fromEnd);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Color -- */
	
	/// <summary>
	/// Adds a color
	/// </summary>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColor(ref Color color)
	{
		colours.Add(color);
	}

	/// <summary>
	/// Adds the same color twice
	/// </summary>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX2(ref Color color)
	{
		colours.Add(color);
		colours.Add(color);
	}
	
	/// <summary>
	/// Adds the same color three times
	/// </summary>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX3(ref Color color)
	{
		colours.Add(color);
		colours.Add(color);
		colours.Add(color);
	}
	
	/// <summary>
	/// Adds the same for times twice
	/// </summary>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX4(ref Color color)
	{
		colours.Add(color);
		colours.Add(color);
		colours.Add(color);
		colours.Add(color);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Color * BaseItem.Color -- */

	/// <summary>
	/// Adds a color
	/// </summary>
	/// <param name="item">The item with which the color will be multiplied</param>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColor(BaseItem item, ref Color color)
	{
		colours.Add(item.hasStateColor ? item.stateColor * color : color);
	}

	/// <summary>
	/// Adds the same color twice
	/// </summary>
	/// <param name="item">The item with which the color will be multiplied</param>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX2(BaseItem item, ref Color color)
	{
		if (item.hasStateColor)
		{
			Color clr = item.stateColor * color;
			colours.Add(clr);
			colours.Add(clr);
		}
		else
		{
			colours.Add(color);
			colours.Add(color);
		}
	}
	
	/// <summary>
	/// Adds the same color three times
	/// </summary>
	/// <param name="item">The item with which the color will be multiplied</param>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX3(BaseItem item, ref Color color)
	{
		if (item.hasStateColor)
		{
			Color clr = item.stateColor * color;
			colours.Add(clr);
			colours.Add(clr);
			colours.Add(clr);
		}
		else
		{
			colours.Add(color);
			colours.Add(color);
			colours.Add(color);
		}
	}
	
	/// <summary>
	/// Adds the same for times twice
	/// </summary>
	/// <param name="item">The item with which the color will be multiplied</param>
	/// <param name="color">The color to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddColorX4(BaseItem item, ref Color color)
	{
		if (item.hasStateColor)
		{
			Color clr = item.stateColor * color;
			colours.Add(clr);
			colours.Add(clr);
			colours.Add(clr);
			colours.Add(clr);
		}
		else
		{
			colours.Add(color);
			colours.Add(color);
			colours.Add(color);
			colours.Add(color);
		}
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Vertex -- */

	/// <summary>
	/// Adds a vertex
	/// </summary>
	/// <param name="vertex">The vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(ref Vector3 vertex)
	{
		vertices.Add(vertex);
	}

	/// <summary>
	/// Adds a vertex
	/// </summary>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	/// <param name="z">The z value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(float x, float y, float z)
	{
		vertices.Add(new Vector3(x, y, z));
	}
	
	/// <summary>
	/// Adds a vertex with z set to 0
	/// </summary>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(float x, float y)
	{
		vertices.Add(new Vector3(x, y));
	}

	/// <summary>
	/// Adds two vertices and indices forming a line
	/// </summary>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Vector3 v1, ref Vector3 v2)
	{
		vertices.Add(v1);
		vertices.Add(v2);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}

	/// <summary>
	/// Adds two vertices, colors and indices forming a line
	/// </summary>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr">The line color</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Vector3 v1, ref Vector3 v2, ref Color clr)
	{
		vertices.Add(v1);
		vertices.Add(v2);
		colours.Add(clr);
		colours.Add(clr);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}

	/// <summary>
	/// Adds two vertices, colors, and indices forming a line
	/// </summary>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr1">The line color at the start point</param>
	/// <param name="clr2">The line color at the end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Vector3 v1, ref Vector3 v2, ref Color clr1, ref Color clr2)
	{
		vertices.Add(v1);
		vertices.Add(v2);
		colours.Add(clr1);
		colours.Add(clr2);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Vertex * Matrix -- */
	
	/// <summary>
	/// Transforms and adds a vertex.
	/// </summary>
	/// <param name="m">The matrix to transform the vertex by</param>
	/// <param name="vertex">The vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(ref Matrix4x4 m, ref Vector3 vertex)
	{
		vertices.Add(m.MultiplyPoint3x4(vertex));
	}
	
	/// <summary>
	/// Transforms and adds a vertex.
	/// </summary>
	/// <param name="m">The matrix to transform the vertex by</param>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	/// <param name="z">The z value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(ref Matrix4x4 m, float x, float y, float z)
	{
		vertices.Add(m.MultiplyPoint3x4(new Vector3(x, y, z)));
	}
	
	/// <summary>
	/// Transforms and adds a vertex with z set to 0.
	/// </summary>
	/// <param name="m">The matrix to transform the vertex by</param>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(ref Matrix4x4 m, float x, float y)
	{
		vertices.Add(m.MultiplyPoint3x4(new Vector3(x, y)));
	}
	
	/// <summary>
	/// Adds two vertices and indices forming a line
	/// </summary>
	/// <param name="m">The matrix to transform the vertices by</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Matrix4x4 m, ref Vector3 v1, ref Vector3 v2)
	{
		vertices.Add(m.MultiplyPoint3x4(v1));
		vertices.Add(m.MultiplyPoint3x4(v2));
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}
	
	/// <summary>
	/// Adds and transforms two vertices, color and indices forming a line
	/// </summary>
	/// <param name="m">The matrix to transform the vertices by</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr">The line color</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Matrix4x4 m, ref Vector3 v1, ref Vector3 v2, Color clr)
	{
		vertices.Add(m.MultiplyPoint3x4(v1));
		vertices.Add(m.MultiplyPoint3x4(v2));
		colours.Add(clr);
		colours.Add(clr);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}
	
	/// <summary>
	/// Adds and transforms two vertices, colors, and indices forming a line
	/// </summary>
	/// <param name="m">The matrix to transform the vertices by</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr1">The line color at the start point</param>
	/// <param name="clr2">The line color at the end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(ref Matrix4x4 m, ref Vector3 v1, ref Vector3 v2, ref Color clr1, ref Color clr2)
	{
		vertices.Add(m.MultiplyPoint3x4(v1));
		vertices.Add(m.MultiplyPoint3x4(v2));
		colours.Add(clr1);
		colours.Add(clr2);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Vertex * BaseItem.Matrix -- */
	
	/// <summary>
	/// Transforms and adds a vertex.
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="vertex">The vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(BaseItem item, ref Vector3 vertex)
	{
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(vertex) : vertex);
	}
	
	/// <summary>
	/// Transforms and adds a vertex.
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	/// <param name="z">The z value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(BaseItem item, float x, float y, float z)
	{
		vertices.Add(item.hasStateTransform
			? item.stateTransform.MultiplyPoint3x4(new Vector3(x, y, z))
			: new Vector3(x, y, z));
	}
	
	/// <summary>
	/// Transforms and adds a vertex with z set to 0.
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="x">The x value of the vertex to add</param>
	/// <param name="y">The y value of the vertex to add</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddVertex(BaseItem item, float x, float y)
	{
		vertices.Add(item.hasStateTransform
			? item.stateTransform.MultiplyPoint3x4(new Vector3(x, y))
			: new Vector3(x, y));
	}
	
	/// <summary>
	/// Adds two vertices and indices forming a line
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(BaseItem item, ref Vector3 v1, ref Vector3 v2)
	{
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v1) : v1);
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v2) : v2);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}
	
	/// <summary>
	/// Adds and transforms two vertices, color and indices forming a line
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr">The line color</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(BaseItem item, ref Vector3 v1, ref Vector3 v2, Color clr)
	{
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v1) : v1);
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v2) : v2);
		colours.Add(clr);
		colours.Add(clr);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}
	
	/// <summary>
	/// Adds and transforms two vertices, colors, and indices forming a line
	/// </summary>
	/// <param name="item">The item whose whose state will be used to transform the vertex</param>
	/// <param name="v1">The line start point</param>
	/// <param name="v2">The line end point</param>
	/// <param name="clr1">The line color at the start point</param>
	/// <param name="clr2">The line color at the end point</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddLine(BaseItem item, ref Vector3 v1, ref Vector3 v2, ref Color clr1, ref Color clr2)
	{
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v1) : v1);
		vertices.Add(item.hasStateTransform ? item.stateTransform.MultiplyPoint3x4(v2) : v2);
		colours.Add(clr1);
		colours.Add(clr2);
		indices.Add(vertexIndex++);
		indices.Add(vertexIndex++);
	}
	
	/* ------------------------------------------------------------------------------------- */
	#endregion
	/* ------------------------------------------------------------------------------------- */

}