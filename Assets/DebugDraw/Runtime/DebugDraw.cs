#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Visuals;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

// TODO: Test with DEBUG_DRAW_OFF
/// <summary>
/// TODO: Write
/// TODO: Mention static  vs instance methods
/// TODO: Mention defines DEBUG_DRAW_OFF, DEBUG_DRAW_EDITOR
/// </summary>
public static partial class DebugDraw
{

	/* ------------------------------------------------------------------------------------- */
	/* -- Private -- */

	private static readonly int DefaultLayer = LayerMask.NameToLayer("Default");

	private static readonly List<BaseVisual> Visuals = new List<BaseVisual>();
	private static int visualCount;
	private static int visualListSize = 1;
	internal static DebugDrawMesh pointMeshInstance;
	internal static DebugDrawMesh lineMeshInstance;
	internal static DebugDrawMesh triangleMeshInstance;

	private static bool _useFixedUpdate;
	#if DEBUG_DRAW_EDITOR
	private static bool _enableInEditMode = true;
	#else
	private static bool _enableInEditMode = false;
	#endif

	internal static bool hasInstance;
	
	internal static Color _color = Color.white;
	internal static Matrix4x4 _transform = Matrix4x4.identity;
	internal static bool hasColor;
	internal static bool hasTransform;
	private static readonly List<DebugDrawState> States = new List<DebugDrawState>();
	private static int stateIndex;

	/// <summary>
	/// Should DebugDraw update itself during FixedUpdate instead of Update.
	/// Should be set to true if you are running your game logic and using DebugDraw from FixedUpdate.
	/// </summary>
	public static bool useFixedUpdate
	{
		get => _useFixedUpdate;
		set
		{
			if (_useFixedUpdate == value)
				return;
			
			_useFixedUpdate = value;
			UpdateFixedUpdateFlag();
		}
	}

	// TODO: Test enableInEditMode again
	/// <summary>
	/// Set to true to also allow using DebugDraw in edit mode. Make sure to enable before using any of
	/// the debug draw visual methods in the editor.
	/// </summary>
	public static bool enableInEditMode
	{
		get => _enableInEditMode;
		set
		{
			if (_enableInEditMode == value)
				return;
	
			_enableInEditMode = value;
			
			if (!Application.isPlaying)
			{
				if (_enableInEditMode)
				{
					Initialize();
				}
				else if (timerInstance != null)
				{
					DestroyObj(timerInstance.gameObject);
					UpdateInstance(null);
				}
			}
		}
	}
	
	/// <summary>
	/// All item colors will be multiplied by this.
	/// </summary>
	public static Color color
	{
		get => _color;
		set
		{
			_color = value;
			hasColor = value != Color.white;
		}
	}
	
	/// <summary>
	/// All item will be transformed by this.
	/// </summary>
	public static Matrix4x4 transform
	{
		get => _transform;
		set
		{
			_transform = value;
			hasTransform = value != Matrix4x4.identity;
		}
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Initialisation -- */

	#if DEBUG_DRAW
	
	private static readonly Camera.CameraCallback OnCameraPreCullDelegate = OnCameraPreCull;
	
	private static DebugDrawTimer timerInstance;
	private static float frameTime;
	private static bool beforeInitialise; 
	private static Camera cam;

	private static bool doFixedUpdate;
	private static bool requiresBuild = true;
	private static bool requiresDraw = true;

	#if UNITY_EDITOR
	
	[InitializeOnLoadMethod]
	private static void InitializeOnLoad()
	{
		// Log.Print("---- InitializeOnLoad ---------------------------------- ");
		
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		EditorSceneManager.activeSceneChangedInEditMode += OnactiveSceneChangedInEditMode;
		EditorSceneManager.sceneOpening += OnEditorSceneOpening;
		
		Clear();
		Initialize(true);
	}

	private static void OnEditorSceneOpening(string path, OpenSceneMode mode)
	{
		if (mode == OpenSceneMode.Single)
		{
			frameTime = 0;
			beforeInitialise = true;
			Clear();
		}
	}

	private static void OnactiveSceneChangedInEditMode(Scene current, Scene next)
	{
		// Log.Print("---- OnactiveSceneChangedInEditMode ----------------------------------", frameTime);
		
		Initialize();
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state)
	{
		if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.ExitingEditMode)
		{
			// Log.Print("---- OnPlayModeStateChanged ----------------------------------");
			
			Clear();
			frameTime = 0;
			beforeInitialise = true;
		}
	}
	#endif
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void RuntimeInitialize()
	{
		// Log.Print("---- RuntimeInitialize ----------------------------------");

		Initialize(true);
	}
	
	private static void Initialize(bool createTimer = false)
	{
		// Log.Print("---- [Initialising] ---------------------------------- ");
		
		beforeInitialise = false;

		if (createTimer && !timerInstance)
		{
			GameObject timerInstanceObj = new GameObject();
			timerInstanceObj.name = $"__DebugDraw[{Mathf.Abs(timerInstanceObj.GetInstanceID())}]__";
			timerInstance = timerInstanceObj.AddComponent<DebugDrawTimer>();
			UpdateInstance(timerInstance);
		}
		
		if (pointMeshInstance == null)
		{
			pointMeshInstance = new DebugDrawMesh(MeshTopology.Points);
			lineMeshInstance = new DebugDrawMesh(MeshTopology.Lines);
			triangleMeshInstance = new DebugDrawMesh(MeshTopology.Triangles);
		}

		if (Visuals.Count == 0)
		{
			for (int i = 0; i < visualListSize; i++)
			{
				Visuals.Add(null);
			}
		}
		
		if (_enableInEditMode || Application.isPlaying)
		{
			pointMeshInstance.CreateAll();
			lineMeshInstance.CreateAll();
			triangleMeshInstance.CreateAll();
		}
		
		UpdateFixedUpdateFlag();
		
		Camera.onPreCull -= OnCameraPreCullDelegate;
		Camera.onPreCull += OnCameraPreCullDelegate;
	}

	private static void UpdateInstance(DebugDrawTimer instance)
	{
		timerInstance = instance;
		hasInstance = instance;
	}

	private static void OnCameraPreCull(Camera _)
	{
		if (!requiresDraw)
			return;
		
		cam = Camera.main;
	
		if (!cam)
		{
			#if UNITY_EDITOR
			cam = SceneView.lastActiveSceneView.camera;
			#else
			return;
			#endif
		}

		if (requiresBuild)
		{
			pointMeshInstance.Build();
			lineMeshInstance.Build();
			triangleMeshInstance.Build();
			requiresBuild = false;
		}
		
		DrawMesh(pointMeshInstance);
		DrawMesh(lineMeshInstance);
		DrawMesh(triangleMeshInstance);
		requiresDraw = false;
	}
	
	private static void DrawMesh(DebugDrawMesh mesh)
	{
		if (mesh.vertexIndex > 0)
		{
			Graphics.DrawMesh(
				mesh.mesh, Vector3.zero, Quaternion.identity, mesh.material,
				DefaultLayer);
		}
	}
	
	#endif

	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */

	public static void Clear()
	{
		for (int i = visualCount - 1; i >= 0; i--)
		{
			BaseVisual visual = Visuals[i];
			visual.index = -1;
			visual.Release();
		}
		
		if (pointMeshInstance != null)
		{
			pointMeshInstance.ClearAll();
			lineMeshInstance.ClearAll();
			triangleMeshInstance.ClearAll();
		}

		visualCount = 0;
		
		Log.Clear();
	}

	/// <summary>
	/// Sets the blend mode to invert destination colors for all debug visuals
	/// </summary>
	/// <param name="invert">True to invert colours</param>
	public static void SetInvertColours(bool invert = true)
	{
		if (pointMeshInstance == null)
			return;
		
		pointMeshInstance.SetInvertColours(invert);
		lineMeshInstance.SetInvertColours(invert);
		triangleMeshInstance.SetInvertColours(invert);
	}
	
	/// <summary>
	/// Set the culling mode for all debug visuals
	/// </summary>
	/// <param name="mode">The cull mode</param>
	public static void SetCulling(CullMode mode)
	{
		if (pointMeshInstance == null)
			return;
		
		pointMeshInstance.SetCulling(mode);
		lineMeshInstance.SetCulling(mode);
		triangleMeshInstance.SetCulling(mode);
	}
	
	/// <summary>
	/// Set the depth testing for all debug visuals
	/// </summary>
	/// <param name="enabled">Is depth testing enabled</param>
	public static void SetDepthTesting(bool enabled = true)
	{
		SetDepthTesting(enabled, enabled);
	}
	
	/// <summary>
	/// Sets the depth testing for all debug visual
	/// </summary>
	/// <param name="write">Enable depth writes</param>
	/// <param name="test">Enable depth tests</param>
	public static void  SetDepthTesting(bool write, bool test)
	{
		pointMeshInstance.SetDepthTesting(write, test);
		lineMeshInstance.SetDepthTesting(write, test);
		triangleMeshInstance.SetDepthTesting(write, test);
	}
	
	/// <summary>
	/// Stores the current state (color and transform) on the stack.
	/// </summary>
	public static void PushState()
	{
		DebugDrawState state;
		
		if (stateIndex == States.Count)
		{
			States.Add(state = new DebugDrawState());
		}
		else
		{
			state = States[stateIndex++];
		}
	
		state.color = _color;
		state.transform = _transform;
		state.hasColor = hasColor;
		state.hasTransform = hasTransform;
	}
	
	/// <summary>
	/// Restores the current state from the stack.
	/// </summary>
	public static void PopState()
	{
		if (stateIndex == 0)
			return;
		
		DebugDrawState state = States[--stateIndex];
		_color = state.color;
		_transform = state.transform;
		hasColor = state.hasColor;
		hasTransform = state.hasTransform;
	}
	
	private static void UpdateFixedUpdateFlag()
	{
		doFixedUpdate = _useFixedUpdate && Application.isPlaying;
	}

	internal static T AddVisual<T>(T visual) where T : BaseVisual
	{
		if (visual.index != -1)
			return visual;
	
		if (visualCount == visualListSize)
		{
			visualListSize *= 2;
	
			for (int i = visualCount; i < visualListSize; i++)
			{
				Visuals.Add(null);
			}
		}

		Visuals[visual.index = visualCount++] = visual;
		return visual;
	}

	private static void UpdateVisuals()
	{
		float time = GetTime();
		
		for (int i = visualCount - 1; i >= 0; i--)
		{
			BaseVisual visual = Visuals[i];
			
			if (visual.expires < time || !visual.Update())
			{
				BaseVisual swap = Visuals[--visualCount];
				swap.index = i;
				Visuals[i] = visual;
				
				visual.index = -1;
				visual.Release();
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void DestroyObj(Object obj)
	{
		if (Application.isPlaying)
		{
			Object.Destroy(obj);
		}
		else
		{
			Object.DestroyImmediate(obj);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetTime()
	{
		return frameTime;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetTime(float duration)
	{
		return duration < 0
			? float.PositiveInfinity
			: frameTime + duration;
	}

	/* ------------------------------------------------------------------------------------- */

	private class DebugDrawState
	{
	
		// ReSharper disable MemberHidesStaticFromOuterClass
		public Color color;
		public Matrix4x4 transform;
		public bool hasColor;
		public bool hasTransform;
		// ReSharper restore MemberHidesStaticFromOuterClass
	
	}

}