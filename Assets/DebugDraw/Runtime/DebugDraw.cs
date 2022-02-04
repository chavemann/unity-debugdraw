#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Attachments;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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

	// private static readonly List<BaseAttachment> Attachments = new List<BaseAttachment>();
	internal static DebugDrawMesh pointMeshInstance;
	internal static DebugDrawMesh lineMeshInstance;
	internal static DebugDrawMesh triangleMeshInstance;
	private static DebugDrawTimer timerInstance;
	private static GameObject timerInstanceObj;

	private static bool _useFixedUpdate;
	#if DEBUG_DRAW_EDITOR
	private static bool _enableInEditMode = true;
	#else
	private static bool _enableInEditMode = false;
	#endif

	private static bool doFixedUpdate;

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
					DestroyObj(timerInstanceObj);
					timerInstance = null;
					timerInstanceObj = null;
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

	#if UNITY_EDITOR
	[InitializeOnLoadMethod]
	private static void InitializeOnLoad()
	{
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		EditorSceneManager.activeSceneChangedInEditMode += OnactiveSceneChangedInEditMode;
		
		if (!EditorApplication.isPlayingOrWillChangePlaymode)
		{
			Initialize();
		}
	}

	private static void OnactiveSceneChangedInEditMode(Scene current, Scene next)
	{
		Log.Print("OnactiveSceneChangedInEditMode", timerInstance != null);
		if (timerInstance)
		{
			DestroyObj(timerInstanceObj);
			timerInstance = null;
			timerInstanceObj = null;
		}

		Initialize();
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state)
	{
		if (state == PlayModeStateChange.ExitingPlayMode)
		{
			Log.Print("-- OnPlayModeStateChanged ------------------");
			Clear();
		}
	}
	#endif
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void RuntimeInitialize()
	{
		Log.Print("-- DebugDraw.RuntimeInitialize -----------------------------   ");

		Clear();
		Initialize();
	}
	
	private static void Initialize()
	{
		Log.Print("== Initialising ============================");
		
		if (_enableInEditMode || Application.isPlaying)
		{
			timerInstanceObj = new GameObject { hideFlags = HideFlags.DontSave };
			timerInstanceObj.transform.SetSiblingIndex(int.MaxValue);
			SceneVisibilityManager.instance.DisablePicking(timerInstanceObj, true);
			timerInstanceObj.name = $"__DebugDraw[{Mathf.Abs(timerInstanceObj.GetInstanceID())}]__";
			timerInstance = timerInstanceObj.AddComponent<DebugDrawTimer>();
		}
		else
		{
			timerInstanceObj = null;
			timerInstance = null;
		}

		if (Application.isPlaying)
		{
			Object.DontDestroyOnLoad(timerInstanceObj);
		}
		
		if (pointMeshInstance == null)
		{
			Log.Print("  >> Creating Meshes");
			pointMeshInstance = new DebugDrawMesh(MeshTopology.Points);
			lineMeshInstance = new DebugDrawMesh(MeshTopology.Lines);
			triangleMeshInstance = new DebugDrawMesh(MeshTopology.Triangles);
		}
		
		if (_enableInEditMode || Application.isPlaying)
		{
			Log.Print("  >> Init Meshes");
			pointMeshInstance.CreateAll();
			lineMeshInstance.CreateAll();
			triangleMeshInstance.CreateAll();
		}
		
		UpdateFixedUpdateFlag();
		
		Camera.onPreCull -= OnCameraPreCullDelegate;
		Camera.onPreCull += OnCameraPreCullDelegate;
	}

	[DefaultExecutionOrder(-10000)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SelectionBase]
	public class DebugDrawTimer : MonoBehaviour
	{

		#if UNITY_EDITOR
		private EditorApplication.CallbackFunction OnDeferredDestroyDelegate;
		#endif
		
		private bool updateInFixedUpdate;
		
		private void OnEnable()
		{
			if (timerInstanceObj != null && gameObject != timerInstanceObj || !_enableInEditMode && !Application.isPlaying)
			{
				// Having the DebugDraw game object selected when recompiling can throw errors in the console.
				// Defer Destroy using the EditorApplication.update callback avoids those errors.
				if (!Application.isPlaying)
				{
					#if UNITY_EDITOR
					OnDeferredDestroyDelegate = OnDeferredDestroy;
					EditorApplication.update += OnDeferredDestroyDelegate;
					#endif
				}
				else
				{
					DestroyObj(gameObject);
				}
			}
		}

		private void OnDeferredDestroy()
		{
			DestroyObj(gameObject);
			EditorApplication.update -= OnDeferredDestroyDelegate;
		}

		private void FixedUpdate()
		{
			if (!doFixedUpdate)
				return;
			
			pointMeshInstance.Update();
			lineMeshInstance.Update();
			triangleMeshInstance.Update();
		}

		private void Update()
		{
			if (doFixedUpdate)
				return;
			
			pointMeshInstance.Update();
			lineMeshInstance.Update();
			triangleMeshInstance.Update();
		}
	}
	
	private static void OnCameraPreCull(Camera cam)
	{
		if (Application.isPlaying)
		{
			Camera mainCam = Camera.main;
	
			if (mainCam == null)
			{
				#if UNITY_EDITOR
				mainCam = SceneView.lastActiveSceneView.camera;
				#else
				return;
				#endif
			}
			
			if (cam != mainCam)
			{
				return;
			}
		}
		#if UNITY_EDITOR
		else if (cam != SceneView.lastActiveSceneView.camera)
		{
			return;
		}
		#endif
	
		pointMeshInstance.Build();
		lineMeshInstance.Build();
		triangleMeshInstance.Build();
		
		DrawMesh(pointMeshInstance);
		DrawMesh(lineMeshInstance);
		DrawMesh(triangleMeshInstance);
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
	
	#if DEBUG_DRAW_DEV
	#endif

	#endif

	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */

	public static void Clear()
	{
		if (pointMeshInstance != null)
		{
			pointMeshInstance.ClearAll();
			lineMeshInstance.ClearAll();
			triangleMeshInstance.ClearAll();
		}
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
	
	// public static T Add<T>(T attachment) where T : BaseAttachment
	// {
	// 	Log.Print("DebugDraw.Add", attachment.index);
	// 	if (attachment.index != -1)
	// 		return attachment;
	//
	// 	attachment.index = Attachments.Count;
	// 	Attachments.Add(attachment);
	// 	return attachment;
	// }
	//
	// public static T Remove<T>(T attachment) where T : BaseAttachment
	// {
	// 	Log.Print("DebugDraw.Remove", attachment.index);
	// 	if (attachment.index == -1)
	// 		return attachment;
	//
	// 	BaseAttachment swapped = Attachments[Attachments.Count - 1];
	// 	swapped.index = attachment.index;
	// 	Attachments[attachment.index] = swapped;
	// 	attachment.index = -1;
	// 	Attachments.RemoveAt(Attachments.Count - 1);
	// 	return attachment;
	// }

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

	private static void UpdateFixedUpdateFlag()
	{
		doFixedUpdate = _useFixedUpdate && Application.isPlaying;
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