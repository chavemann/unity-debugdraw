#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

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
[DefaultExecutionOrder(10000)]
[ExecuteAlways]
[AddComponentMenu("")]
[SelectionBase]
public partial class DebugDraw : MonoBehaviour
{

	/* ------------------------------------------------------------------------------------- */
	/* -- Private -- */
	
	private static readonly List<BaseAttachment> Attachments = new List<BaseAttachment>();
	internal static DebugDrawMesh pointMeshInstance;
	internal static DebugDrawMesh lineMeshInstance;
	internal static DebugDrawMesh triangleMeshInstance;
	private static DebugDraw instance;
	
	private static bool _useFixedUpdate;
	#if DEBUG_DRAW_EDITOR
	private static bool _enableInEditMode = true;
	#else
	private static bool _enableInEditMode = false;
	#endif

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

			if (instance)
			{
				instance.InitUpdateMethods();
			}
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
					Init();
				}
				else if (instance != null)
				{
					DestroyImmediate(instance.gameObject);
					instance.doNotInit = true;
					instance = null;
					instance = null;
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
	public new static Matrix4x4 transform
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
	private static readonly EditorApplication.CallbackFunction OnInitializeDeferredDelegate = OnInitializeDeferred;
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void RuntimeInitialize()
	{
		Log.Print("-- DebugDraw.RuntimeInitialize --------------------------- --");
		ResetAll();
		Init();
	}

	#if UNITY_EDITOR
	[InitializeOnLoadMethod]
	private static void InitializeOnLoad()
	{
		Log.Print("-- DebugDraw.InitializeOnLoad -----------------------------");
		EditorSceneManager.sceneOpened += OnEditorSceneOpened;
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		
		if (!EditorApplication.isPlayingOrWillChangePlaymode)
		{
			// When the editor is launched, InitializeOnLoad seems to be called before the scene is loaded
			// so delay it until update before initialising.
			EditorApplication.update += OnInitializeDeferredDelegate;
		}
		
		ResetAll();
		Init();
	}

	private static void OnInitializeDeferred()
	{
		EditorApplication.update -= OnInitializeDeferredDelegate;
		Init();
	}

	private static void OnEditorSceneOpened(Scene scene, OpenSceneMode mode)
	{
		Init();
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state)
	{
		if (state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.ExitingPlayMode)
		{
			if (instance)
			{
				instance.OnDisable();
			}
		}

		if (state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.EnteredPlayMode)
		{
			Init();
		}
	}
	
	#if DEBUG_DRAW_DEV
	#endif
	
	#endif

	private static void Init()
	{
		if (!_enableInEditMode && !Application.isPlaying)
			return;
		
		if (instance != null && !instance.gameObject.scene.IsValid())
		{
			DestroyObj(instance);
			instance = null;
		}

		if (instance == null)
		{
			Log.Print(">> Creating DebugDraw");
			GameObject obj = new GameObject { hideFlags = HideFlags.DontSave };
			SceneVisibilityManager.instance.DisablePicking(obj, true);
			obj.name = $"__DebugDraw[{Mathf.Abs(obj.GetInstanceID())}]__";
			instance = obj.AddComponent<DebugDraw>(); 
			instance.OnEnable();
	
			if (Application.isPlaying)
			{
				DontDestroyOnLoad(obj);
			}
		}
		
		if (pointMeshInstance == null)
		{
			Log.Print(">> Creating Meshes");
			pointMeshInstance = new DebugDrawMesh(MeshTopology.Points);
			lineMeshInstance = new DebugDrawMesh(MeshTopology.Lines);
			triangleMeshInstance = new DebugDrawMesh(MeshTopology.Triangles);
		}
	}

	private static void ResetAll()
	{
		Log.Print("DebugDraw.ResetAll", instance !=  null ? instance.gameObject.GetInstanceID() : 0);
		foreach (BaseAttachment attachment in Attachments)
		{
			attachment.index = -1;
			attachment.Release();
		}
		
		Attachments.Clear();

		if (pointMeshInstance != null)
		{
			pointMeshInstance.Destroy();
			lineMeshInstance.Destroy();
			triangleMeshInstance.Destroy();
		}
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- MonoBehaviour Init -- */

	private static readonly Camera.CameraCallback OnCameraPreCullDelegate = OnCameraPreCull;
	private bool isEnabled;
	private bool updateInFixedUpdate;
	internal bool doNotInit;

	private void Awake()
	{
		OnEnable();
	}

	private void OnEnable()
	{
		if (isEnabled)
			return;
		
		if (doNotInit || !_enableInEditMode && !Application.isPlaying || instance != null && this != instance)
		{
			DestroyImmediate(gameObject);
			return;
		}
		
		isEnabled = true;
		Log.Print("????????? DebugDraw.OnEnable", gameObject.GetInstanceID());

		if (instance == null || !instance.gameObject.scene.IsValid())
		{
			instance = this;
			Init();
		}
		
		if (instance == this)
		{
			Log.Print(">> Init meshes");
			pointMeshInstance.Init(true, gameObject);
			GameObject lineObj = lineMeshInstance.Init(true, gameObject, "Lines");
			GameObject triObj = triangleMeshInstance.Init(true, gameObject, "Triangles");

			Camera.onPreCull += OnCameraPreCullDelegate;
		}
		
		InitUpdateMethods();
	}

	private void OnDisable()
	{
		if (!isEnabled || instance != null && instance != this)
			return;
		
		Log.Print("DebugDraw.OnDisable", gameObject.GetInstanceID());
		Camera.onPreCull -= OnCameraPreCullDelegate;

		isEnabled = false;
		ResetAll();
	}
	
	private void InitUpdateMethods()
	{
		if (instance != this)
			return;
		
		updateInFixedUpdate = Application.isPlaying && _useFixedUpdate;
	}

	private void FixedUpdate()
	{
		if (!updateInFixedUpdate)
			return;

		UpdateAttachments();
		pointMeshInstance.Update();
		lineMeshInstance.Update();
		triangleMeshInstance.Update();
	}

	private void LateUpdate()
	{
		if (updateInFixedUpdate)
			return;

		UpdateAttachments();
		pointMeshInstance.Update();
		lineMeshInstance.Update();
		triangleMeshInstance.Update();
	}

	private static void UpdateAttachments()
	{
		float time = Time.time;
		
		int attachmentCount = Attachments.Count;
		Log.Print("UpdateAttachments", attachmentCount);
		
		for(int i = attachmentCount - 1; i >= 0; i--)
		{
			BaseAttachment attachment = Attachments[i];
				
			if(attachment.expires < time || !attachment.Update())
			{
				Log.Print("  Expiring attachment", attachment.expires, time);
				attachment.index = -1;
				attachment.Release();
				
				attachment = Attachments[--attachmentCount];
				attachment.index = i;
				Attachments[i] = attachment;
			}
		}
		
		if (attachmentCount < Attachments.Count)
		{
			Attachments.RemoveRange(attachmentCount, Attachments.Count - attachmentCount);
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

		Log.Print("DebugDraw.OnCameraPreCull");
		pointMeshInstance.Build();
		lineMeshInstance.Build();
		triangleMeshInstance.Build();
	}
	#endif

	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */
	
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

	public static T Add<T>(T attachment) where T : BaseAttachment
	{
		Log.Print("DebugDraw.Add", attachment.index);
		if (attachment.index != -1)
			return attachment;

		attachment.index = Attachments.Count;
		Attachments.Add(attachment);
		return attachment;
	}

	public static T Remove<T>(T attachment) where T : BaseAttachment
	{
		Log.Print("DebugDraw.Remove", attachment.index);
		if (attachment.index == -1)
			return attachment;

		BaseAttachment swapped = Attachments[Attachments.Count - 1];
		swapped.index = attachment.index;
		Attachments[attachment.index] = swapped;
		attachment.index = -1;
		Attachments.RemoveAt(Attachments.Count - 1);
		return attachment;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void DestroyObj(Object obj)
	{
		if (Application.isPlaying)
		{
			Destroy(obj);
		}
		else
		{
			DestroyImmediate(obj);
		}
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