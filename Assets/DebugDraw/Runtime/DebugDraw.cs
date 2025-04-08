#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DebugDrawUtils.DebugDrawAttachments;
using DebugDrawUtils.DebugDrawItems;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils
{

/// <summary>
/// <para>
/// A static class that provides various methods for drawing debug visuals.
/// This class should be usable with no setup - simply include the package and call the methods.
/// </para>
/// <para>
/// The following  compiler directives are available:
/// </para>
/// <list type="bullet">
/// 	<item><description><b>DEBUG_DRAW_OFF:</b> Set this flag to disable the global static DebugDraw instance.
/// 	When set, the static DebugDraw methods should not be used, but won't throw errors.
/// 	</description></item>
/// </list>
/// </summary>
public static partial class DebugDraw
{

	/* ------------------------------------------------------------------------------------- */
	/* -- Private -- */

	/// <summary>
	/// Just for testing, set this to true so that the instance is visible in the hierarchy
	/// after a scene change.
	/// </summary>
	private const bool UpdateInstanceScene = false;

	public static readonly int DefaultLayer = LayerMask.NameToLayer("Default");
	public static Color colorIdentity = Color.white;
	public static Matrix4x4 matrixIdentity = Matrix4x4.identity;
	public static Quaternion rotationIdentity = Quaternion.identity;
	public static Vector3 positionIdentity = Vector3.zero;
	public static Vector3 scaleIdentity = Vector3.one;
	public static Vector3 up = Vector3.up;
	public static Vector3 down = Vector3.down;
	public static Vector3 right = Vector3.right;
	public static Vector3 left = Vector3.left;
	public static Vector3 forward = Vector3.forward;
	public static Vector3 back = Vector3.back;

	private static readonly List<BaseAttachment> Attachments = new();
	private static int attachmentCount;
	private static int attachmentListSize;
	internal static DebugDrawMesh pointMeshInstance;
	internal static DebugDrawMesh lineMeshInstance;
	internal static DebugDrawMesh triangleMeshInstance;
	internal static DebugDrawTextMesh textMeshInstance;

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
	private static readonly List<DebugDrawState> States = new();
	private static int stateIndex;

	internal static readonly GroupList Groups = new();

	/* ------------------------------------------------------------------------------------- */
	/* -- Public -- */

	/// <summary>
	/// Modify the default properties of all Text.
	/// </summary>
	public static readonly GUIStyle TextStyle = new();

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
	/// If true, will rebuild meshes for each camera that is rendered.
	/// </summary>
	public static bool updatePerCamera = true;

	#if UNITY_EDITOR
	public static bool isActive => pointMeshInstance != null && (EditorApplication.isPlayingOrWillChangePlaymode || _enableInEditMode);
	public static bool isPlaying => EditorApplication.isPlayingOrWillChangePlaymode;
	#else
	public static bool isActive => pointMeshInstance != null && (Application.isPlaying || _enableInEditMode);
	public static bool isPlaying => Application.isPlaying;
	#endif

	/// <summary>
	/// All item colors will be multiplied by this.
	/// </summary>
	public static Color color
	{
		get => _color;
		set
		{
			_color = value;
			hasColor = value != colorIdentity;
		}
	}

	/// <summary>
	/// The origin of all debug graphics.
	/// </summary>
	public static Vector3 globalOrigin = Vector3.zero;

	/// <summary>
	/// The global rotation of all debug graphics.
	/// </summary>
	public static Quaternion globalRotation = Quaternion.identity;

	/// <summary>
	/// All item will be transformed by this.
	/// </summary>
	public static Matrix4x4 transform
	{
		get => _transform;
		set
		{
			_transform = value;
			hasTransform = value != matrixIdentity;
		}
	}

	/// <summary>
	/// Set to null for no shadow
	/// </summary>
	public static Color? textShadowColor = new Color(0, 0, 0, 0.5f);

	/// <summary>
	/// Text smaller than this size on screen won't be rendered.
	/// Only applicable if <see cref="DebugDrawItems.Text.SetUseWorldSize"/> is set.
	/// </summary>
	public static float minTextSize = 5;

	/// <summary>
	/// At what distance from the camera will text on screen approximately be it's original size.
	/// Only applicable if <see cref="DebugDrawItems.Text.SetUseWorldSize"/> is set.
	/// </summary>
	public static float textBaseWorldDistance = 10;

	/// <summary>
	/// If true uses `Time.unscaledTime` instead of `Time.time` for durations.
	/// </summary>
	public static bool useUnscaledTime = false;

	/// <summary>
	/// The default duration for all items when one is not provided.
	/// </summary>
	public static EndTime defaultDuration = Duration.Once;

	public static DebugDrawMesh pointMesh => pointMeshInstance;
	public static DebugDrawMesh lineMesh => lineMeshInstance;
	public static DebugDrawMesh triangleMesh => triangleMeshInstance;

	#if DEBUG_DRAW
	private static DebugDrawTimer timerInstance;

	private static bool doFixedUpdate;
	private static bool requiresBuild = true;
	#endif

	internal static float frameTime;
	private static bool beforeInitialise;
	private static CameraInitState hasCamera = CameraInitState.Pending;
	private static bool camUpdated;
	public static Camera cam { get; internal set; }
	public static Transform camTransform { get; internal set; }
	public static Vector3 camPosition = Vector3.zero;
	public static Vector3 camForward = Vector3.forward;
	public static Vector3 camRight = Vector3.right;
	public static Vector3 camUp = Vector3.up;
	public static float camFOV;
	public static bool camOrthographic;
	public static float camOrthoSize;
	public static float camFOVAngle;

	/// <summary>
	/// The number of active debug items.
	/// </summary>
	public static int itemCount => pointMeshInstance != null
		? pointMeshInstance.itemCount + lineMeshInstance.itemCount + triangleMeshInstance.itemCount + textMeshInstance.itemCount
		: 0;

	/// <summary>
	/// How many vertices where drawn on the last frame.
	/// </summary>
	public static int vertexCount => pointMeshInstance != null
		? pointMeshInstance.vertexIndex + lineMeshInstance.vertexIndex + triangleMeshInstance.vertexIndex
		: 0;

	/* ------------------------------------------------------------------------------------- */
	/* -- Initialisation -- */

	static DebugDraw()
	{
		TextStyle.normal.textColor = Color.white;
		TextStyle.fontSize = 14;
	}

	#if DEBUG_DRAW

	#if UNITY_EDITOR
	private static readonly EditorApplication.CallbackFunction OnEditorFlushFrameDelegate = OnEditorFlushFrame;
	private static bool pendingEditorFrameFlush;
	#endif

	private static readonly Camera.CameraCallback OnCameraPreCullDelegate = OnCameraPreCull;
	#if SRP_AVAILABLE
	private static readonly Action<ScriptableRenderContext, Camera> OnBeginCameraRenderingDelegate = OnBeginCameraRendering;
	#endif

	#if UNITY_EDITOR
	[InitializeOnLoadMethod]
	#else
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
	private static void InitializeOnLoad()
	{
		// Log.Print("---- InitializeOnLoad ---------------------------------- ");
		// Log.Print("---- ---------------- ---------------------------------- ");

		#if UNITY_EDITOR
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChangedInEditMode;
		EditorSceneManager.sceneOpening += OnEditorSceneOpening;
		#endif

		Clear();
		Initialize(true);
	}

	#if UNITY_EDITOR
	private static void OnEditorSceneOpening(string path, OpenSceneMode mode)
	{
		if (mode == OpenSceneMode.Single)
		{
			frameTime = 0;
			beforeInitialise = true;
			Clear();
		}
	}

	private static void OnActiveSceneChangedInEditMode(Scene current, Scene next)
	{
		// Log.Print("---- OnActiveSceneChangedInEditMode ----------------------------------", frameTime);

		Initialize();
		UpdateTimerInstanceScene();
		ClearCamera();
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state)
	{
		// Log.Print($"-- DebugDraw.OnPlayModeStateChanged ({state}) ----------------------------------");

		if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.ExitingEditMode)
		{
			if (state == PlayModeStateChange.ExitingEditMode)
			{
				Initialize();
			}

			Clear();
			frameTime = 0;
			beforeInitialise = true;
		}
		else if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.EnteredEditMode)
		{
			UpdateTimerInstanceScene();
		}

		if (state == PlayModeStateChange.EnteredEditMode)
		{
			if (_enableInEditMode)
			{
				cam = null;
				camTransform = null;
				hasCamera = CameraInitState.Pending;
			}
			else
			{
				Clear();

				if (timerInstance)
				{
					UpdateInstance(null);
				}
			}
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
		// Log.Print("---- [Initialising] ----------------------------------", isPlaying);

		Camera.onPreCull -= OnCameraPreCullDelegate;
		#if SRP_AVAILABLE
		RenderPipelineManager.beginCameraRendering -= OnBeginCameraRenderingDelegate;
		#endif

		if (!_enableInEditMode && !isPlaying)
		{
			Destroy();
			return;
		}

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
			textMeshInstance = new DebugDrawTextMesh();
		}

		if (_enableInEditMode || isPlaying)
		{
			pointMeshInstance.CreateAll();
			lineMeshInstance.CreateAll();
			triangleMeshInstance.CreateAll();
		}

		UpdateFixedUpdateFlag();

		Camera.onPreCull += OnCameraPreCullDelegate;
		#if SRP_AVAILABLE
		RenderPipelineManager.beginCameraRendering += OnBeginCameraRenderingDelegate;
		#endif

		ClearCamera();
	}

	private static void UpdateInstance(DebugDrawTimer instance)
	{
		timerInstance = instance;
		hasInstance = instance;
	}

	private static void OnCameraPreCull(Camera camera)
	{
		DoBeforeRender(camera);
	}

	#if SRP_AVAILABLE
	private static void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
	{
		DoBeforeRender(camera);
	}
	#endif

	private static void DoBeforeRender(Camera camera)
	{
		if (cam != camera)
		{
			if (!updatePerCamera)
				return;

			InitCamera(camera);
			UpdateCamera();
			requiresBuild = true;
		}

		#if UNITY_EDITOR
		if (EditorApplication.isPaused && !pendingEditorFrameFlush)
		{
			EditorApplication.update += OnEditorFlushFrameDelegate;
			pendingEditorFrameFlush = true;
		}
		#endif

		if (!Application.isPlaying)
		{
			UpdateCamera();
			requiresBuild = true;
		}

		if (requiresBuild && pointMeshInstance != null)
		{
			pointMeshInstance.Build();
			lineMeshInstance.Build();
			triangleMeshInstance.Build();
			requiresBuild = false;
		}

		Camera renderCam = updatePerCamera ? cam : null;
		DrawMesh(pointMeshInstance, renderCam);
		DrawMesh(lineMeshInstance, renderCam);
		DrawMesh(triangleMeshInstance, renderCam);
	}

	#if UNITY_EDITOR
	private static void OnEditorFlushFrame()
	{
		EditorApplication.update -= OnEditorFlushFrameDelegate;
		pendingEditorFrameFlush = false;
	}
	#endif

	private static void DrawMesh(DebugDrawMesh mesh, Camera camera)
	{
		if (mesh.vertexIndex == 0)
			return;

		Graphics.DrawMesh(
			mesh.mesh, globalOrigin, globalRotation, mesh.material,
			DefaultLayer, camera);
	}

	private static void UpdateTimerInstanceScene()
	{
		#pragma warning disable 162
		// ReSharper disable once ConditionIsAlwaysTrueOrFalse
		if (UpdateInstanceScene && hasInstance && timerInstance && timerInstance.gameObject)
		{
			SceneManager.MoveGameObjectToScene(timerInstance.gameObject, SceneManager.GetActiveScene());
			timerInstance.gameObject.transform.SetAsLastSibling();
		}
		#pragma warning restore 162
	}

	private static void Destroy()
	{
		if (hasInstance)
		{
			if (timerInstance)
			{
				timerInstance.DestroyTimer();
				timerInstance = null;
			}

			hasInstance = false;
		}

		if (pointMeshInstance != null)
		{
			pointMeshInstance.ClearAll();
			lineMeshInstance.ClearAll();
			triangleMeshInstance.ClearAll();
			textMeshInstance.ClearAll();
		}
	}

	#endif

	/* ------------------------------------------------------------------------------------- */
	/* -- Methods -- */

	internal static void ClearCamera()
	{
		hasCamera = CameraInitState.Pending;
		cam = null;
		camTransform = null;
		camPosition = default;
		camForward = Vector3.forward;
		camUp = Vector3.up;
		camFOV = 60;
		camOrthographic = false;
		camFOVAngle = Mathf.Tan(camFOV * 0.5f * Mathf.Deg2Rad);
	}

	internal static void UpdateCamera()
	{
		if (hasCamera == CameraInitState.Pending || hasCamera == CameraInitState.NotNull && !cam)
		{
			InitCamera();
		}

		camUpdated = true;

		if (hasCamera == CameraInitState.Null)
			return;

		if (camTransform)
		{
			camPosition = camTransform.position;
			camForward = camTransform.forward;
			camRight = camTransform.right;
			camUp = camTransform.up;
			camFOV = cam.fieldOfView;
			camOrthographic = cam.orthographic;
			camOrthoSize = cam.orthographicSize * 2;
			camFOVAngle = Mathf.Tan(camFOV * 0.5f * Mathf.Deg2Rad);
		}
	}

	/// <summary>
	/// DebugDraw caches a reference to Camera.main - call this to update that reference.
	/// Though this happens automatically when changing scenes etc.
	/// </summary>
	/// <param name="newCam">Leave as null to use Camera.main</param>
	public static void InitCamera(Camera newCam = null)
	{
		cam = newCam ? newCam : Camera.main;

		#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			if (!SceneView.lastActiveSceneView)
			{
				cam = null;
				camTransform = null;
				hasCamera = CameraInitState.Pending;
				return;
			}

			cam = SceneView.lastActiveSceneView.camera;
		}
		#endif

		hasCamera = cam != null ? CameraInitState.NotNull : CameraInitState.Null;

		if (hasCamera == CameraInitState.NotNull)
		{
			// ReSharper disable once PossibleNullReferenceException
			camTransform = cam.transform;

			if (camUpdated)
			{
				UpdateCamera();
			}
		}
		else
		{
			camTransform = null;
			camPosition = Vector3.zero;
			camForward = Vector3.forward;
			camRight = Vector3.right;
			camUp = Vector3.up;
		}
	}

	/// <summary>
	/// Clears all debug draw items.
	/// </summary>
	public static void Clear()
	{
		for (int i = attachmentCount - 1; i >= 0; i--)
		{
			BaseAttachment attachment = Attachments[i];
			attachment.Release();
		}

		if (pointMeshInstance != null)
		{
			pointMeshInstance.ClearAll();
			lineMeshInstance.ClearAll();
			triangleMeshInstance.ClearAll();
			textMeshInstance.Clear();
		}

		attachmentCount = 0;

		Groups.Reset();
		Log.Reset();
	}

	/// <summary>
	/// Removes all items that belong to the given group.
	/// </summary>
	/// <param name="groupName"></param>
	public static void Clear(string groupName)
	{
		if (pointMeshInstance == null)
			return;

		if (!Groups.TryGet(groupName, out Group group))
			return;

		Clear(group);
	}

	/// <inheritdoc cref="Clear(string)"/>
	public static void Clear(Group group)
	{
		if (group == null)
			return;
		if (!group.isActive)
			return;

		for (int i = group.itemCount - 1; i >= 0; i--)
		{
			BaseItem item = (BaseItem) group.items[i];
			item.mesh?.Remove(item);
		}

		group.Clear();
	}

	/// <summary>
	/// Sets the blend mode to invert destination colors for all debug visuals
	/// </summary>
	/// <param name="invert">True to invert colours.</param>
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
	/// <param name="mode">The cull mode.</param>
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
	/// <param name="enabled">Is depth testing enabled.</param>
	public static void SetDepthTesting(bool enabled = true)
	{
		SetDepthTesting(enabled, enabled);
	}

	/// <summary>
	/// Sets the depth testing for all debug visual
	/// </summary>
	/// <param name="write">Enable depth writes.</param>
	/// <param name="test">Enable depth tests.</param>
	public static void SetDepthTesting(bool write, bool test)
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
		if (stateIndex == States.Count)
		{
			States.Add(new DebugDrawState());
		}

		DebugDrawState state = States[stateIndex++];
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

	/// <summary>
	/// Creates a group with the specified name, and default duration.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="defaultDuration"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group CreateGroup(string name, EndTime? defaultDuration = null)
	{
		return Groups.GetOrCreate(name, defaultDuration);
	}

	/// <summary>
	/// Use to release a created group when it is no longer needed.
	/// This will not clear items added to the group.
	/// </summary>
	/// <param name="group"></param>
	public static void ReleaseGroup(Group group)
	{
		if (group == null)
			return;
		if (!group.isActive)
			return;

		for (int i = 0; i < group.itemCount; i++)
		{
			group.items[i].group = null;
		}

		Groups.Release(group);
	}

	///  <summary>
	///  Sets the current group all new items after this call are added to. This can be used to group and clear specific groups of items.
	///  Be sure to call a matching <see cref="EndGroup"/> for each call to <see cref="BeginGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>.<br/>
	///  An item will only be added to the group specified by the last call to this method, but nested calls to Begin/End are supported.<br/>
	///  If <see cref="NextGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/> is set, that will take precedence.<br/>
	///  Groups are cleared at the start of each frame so failing to call <see cref="EndGroup"/> will not cause a memory leak.
	///  </summary>
	///  <param name="name">The group name. Empty strings are ignored,
	/// 		and calling this multiple times in a row with the same name will have no effect.</param>
	///  <param name="defaultDuration"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group BeginGroup(string name, EndTime? defaultDuration = null)
	{
		return Groups.Push(name, defaultDuration);
	}

	/// <inheritdoc cref="BeginGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Group BeginGroup(Group group)
	{
		return Groups.Push(group);
	}

	/// <summary>
	/// Ends the previous group.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void EndGroup()
	{
		Groups.Pop();
	}

	/// <summary>
	/// If not empty, only the next item add/drawn will be added to this group.<br/>
	/// Alternatively <see cref="BaseItem.Group"/> can be called to change individual items.<br/>
	///   e.g. `DebugDraw.Line(...).Group("MyGroupName");`
	/// </summary>
	/// <param name="name"></param>
	/// <param name="defaultDuration"></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NextGroup(string name, EndTime? defaultDuration = null)
	{
		Groups.SetNext(name, defaultDuration);
	}

	/// <inheritdoc cref="NextGroup(string,System.Nullable{DebugDrawUtils.EndTime})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NextGroup(Group group)
	{
		Groups.SetNext(group);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetCurrentTime()
	{
		return frameTime;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static EndTime GetTime(EndTime? duration, GroupList groups = null)
	{
		EndTime time = (duration ?? (groups ?? Groups).GetDefaultDuration()) ?? defaultDuration;

		if (time.type != Duration.Default)
			return time;

		switch (time.type)
		{
			case Duration.Default:
				return Duration.Once;
			case Duration.Infinite:
			case Duration.Once:
				return time.type;
			case Duration.Time:
				return frameTime + time.time;
		}

		return Duration.Once;
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

	internal static T AddAttachment<T>(T attachment) where T : BaseAttachment
	{
		if (attachment.index != -1)
			return attachment;

		if (attachmentCount == attachmentListSize)
		{
			attachmentListSize = Mathf.Max(attachmentListSize * 2, 2);

			for (int i = attachmentCount; i < attachmentListSize; i++)
			{
				Attachments.Add(null);
			}
		}

		Attachments[attachment.index = attachmentCount++] = attachment;
		return attachment;
	}

	private static void UpdateFixedUpdateFlag()
	{
		#if DEBUG_DRAW
		doFixedUpdate = _useFixedUpdate && Application.isPlaying;
		#endif
	}

	private static void UpdateAttachments()
	{
		for (int i = attachmentCount - 1; i >= 0; i--)
		{
			BaseAttachment attachment = Attachments[i];

			if (attachment.destroyed || !attachment.Update())
			{
				BaseAttachment swap = Attachments[--attachmentCount];
				swap.index = i;
				Attachments[i] = swap;

				attachment.Release();
			}
		}
	}

	/* ------------------------------------------------------------------------------------- */
	/* -- Utils -- */

	/// <summary>
	/// Find good arbitrary axis vectors to represent U and V axes of a plane, using this vector as the normal of the plane.
	/// <param name="normal">The plane's normal vector.</param>
	/// <param name="up">The calculated up vector.</param>
	/// <param name="right">The calculated right vector.</param>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void FindBestAxisVectors(ref Vector3 normal, out Vector3 up, out Vector3 right)
	{
		Vector3 n = new Vector3(
			Mathf.Abs(normal.x),
			Mathf.Abs(normal.y),
			Mathf.Abs(normal.z));

		// Find best basis vectors.
		up = n.z > n.x && n.z > n.y
			? new Vector3(1, 0, 0)
			: new Vector3(0, 0, 1);

		float dot = Vector3.Dot(up, normal);

		up = new Vector3(
			up.x - normal.x * dot,
			up.y - normal.y * dot,
			up.z - normal.z * dot).normalized;

		right = Vector3.Cross(up, normal);
	}

	/// <summary>
	/// Find good arbitrary axis vectors to represent U and V axes of a plane, using this vector as the normal of the plane.
	/// <param name="normal">The plane's normal vector.</param>
	/// <param name="up">The calculated up vector.</param>
	/// <param name="right">The calculated right vector.</param>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void FindAxisVectors(ref Vector3 normal, ref Vector3 upAxis, out Vector3 up, out Vector3 right)
	{
		if (!Mathf.Approximately(Mathf.Abs(Vector3.Dot(normal, upAxis)), 1))
		{
			right = Vector3.Cross(normal, upAxis);
			right.Normalize();
		}
		else
		{
			up = Mathf.Abs(normal.z) > Mathf.Abs(normal.x) && Mathf.Abs(normal.z) > Mathf.Abs(normal.y)
				? new Vector3(1, 0, 0)
				: new Vector3(0, 0, 1);

			right = Vector3.Cross(normal, up);
			right.Normalize();
		}


		up = Vector3.Cross(right, normal);
		up.Normalize();
	}

	/// <summary>
	/// Returns a vector perpendicular to axis.
	/// </summary>
	/// <param name="axis"></param>
	/// <param name="up"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3 FindPerpendicular(ref Vector3 axis, ref Vector3 up)
	{
		Vector3 n = Vector3.Cross(axis, up);
		n.Normalize();
		return n;
	}

	/// <summary>
	/// Returns the distance from the given point to the camera plane.
	/// </summary>
	/// <param name="position"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistanceFromCamera(ref Vector3 position)
	{
		return Vector3.Dot(new Vector3(
			position.x - camPosition.x,
			position.y - camPosition.y,
			position.z - camPosition.z), camForward);
	}

	/// <summary>
	/// Returns the camera frustum height at the given distance.
	/// </summary>
	/// <param name="distance">The distance from the camera.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float CalculateFrustumHeight(float distance)
	{
		return !camOrthographic
			? 2.0f * distance * camFOVAngle
			: camOrthoSize;
	}

	/// <summary>
	/// Calculates a number/resolution based on a size and a distance from the camera.
	/// </summary>
	/// <param name="distance">The distance from the camera.</param>
	/// <param name="radius">The size</param>
	/// <param name="min">The min resolution when size is almost zero relative to the view height.</param>
	/// <param name="max">The resolution when size fills the screen height.</param>
	/// <param name="limit">The max possible resolution as size becomes larger than the view height.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int AutoResolution(float distance, float radius, int min, int max, int limit)
	{
		float frustumHeight = CalculateFrustumHeight(distance);
		float t = (radius * 2) / (frustumHeight);

		// Shift the lower bound up a bit
		const float S = 0.006f;
		t = Mathf.Max((t - S) / (1 - S), 0);

		// As the size gets smaller the resolution gets too low so at lower values
		// adjust t so it climbs faster the lower it is.
		const float Ss = 0.5f;
		if (t < Ss)
		{
			t /= Ss;
			t = (1 - Mathf.Pow(1 - t, 10f)) * Ss;
		}

		return Mathf.Clamp(Mathf.FloorToInt(Mathf.LerpUnclamped(min, max, t)), min, limit);
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

}
