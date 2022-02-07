#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static partial class DebugDraw
{

	#if DEBUG_DRAW

	/// <summary>
	/// For some reason <c>HideAndDontSave</c> seemed to break some of the functionality so every new instance will be
	/// visible in the inspector until a scene change happens.
	/// </summary>
	private const HideFlags TimerHideFlags = HideFlags.DontSave | HideFlags.NotEditable;
	// private const HideFlags TimerHideFlags = HideFlags.HideAndDontSave;

	/// <summary>
	/// TODO: 
	/// </summary>
	[DefaultExecutionOrder(10000)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	private class DebugDrawTimer : MonoBehaviour
	{

		private UnityAction<Scene, Scene> onActiveSceneChangedDelegate;
		private bool updateInFixedUpdate;

		private bool pendingDestroy;
		private DebugDrawFrameStartTimer startTimer;
		
		private void OnEnable()
		{
			if (pendingDestroy)
			{
				GameObject go = gameObject;
				DestroyObj(this);
				DestroyObj(startTimer);
				DestroyObj(go);
				pendingDestroy = false;
				return;
			}

			if (!startTimer)
			{
				startTimer = gameObject.AddComponent<DebugDrawFrameStartTimer>();
			}
			
			gameObject.hideFlags = hideFlags = TimerHideFlags;
			
			if (!timerInstance)
			{
				UpdateInstance(this);
				Clear();
			}
			else if (timerInstance == this)
			{
				Clear();
			}
			
			if (UpdateInstanceScene && timerInstance == this)
			{
				if (onActiveSceneChangedDelegate == null)
				{
					onActiveSceneChangedDelegate = OnActiveSceneChanged;
				}

				SceneManager.activeSceneChanged -= onActiveSceneChangedDelegate;
				SceneManager.activeSceneChanged += onActiveSceneChangedDelegate;
			}
		}

		private void OnActiveSceneChanged(Scene prev, Scene current)
		{
			UpdateTimerInstanceScene();

			ClearCamera();
		}

		private void OnDisable()
		{
			if (timerInstance == this || timerInstance == null)
			{
				// Hide this instance because even though it appears to be destroyed properly (Update etc. are not called)
				// it still shows in the inspector until a new scene is loaded.
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				pendingDestroy = true;
				UpdateInstance(null);
			}

			if (onActiveSceneChangedDelegate != null)
			{
				SceneManager.activeSceneChanged -= onActiveSceneChangedDelegate;
			}
		}

		private void FixedUpdate()
		{
			if (!doFixedUpdate)
				return;

			DoUpdate();
		}

		private void LateUpdate()
		{
			if (doFixedUpdate)
				return;

			DoUpdate();
		}

		private static void DoUpdate()
		{
			// Log.Print("DebugDrawTimer.DoUpdate", gameObject.GetInstanceID());
			
			camUpdated = false;

			pointMeshInstance.Update();
			lineMeshInstance.Update();
			triangleMeshInstance.Update();
			textMeshInstance.Update();
			UpdateAttachments();
			requiresBuild = true;
			requiresDraw = true;

			if (LogMessage.hasMessages)
			{
				LogMessage.Update();
			}
		}

		private void OnGUI()
		{
			if (Event.current.type != EventType.Repaint)
				return;
			
			textMeshInstance.Build();
			
			if (LogMessage.hasMessages)
			{
				LogMessage.Draw();
			}
		}

	}

	/// <summary>
	/// TODO:
	/// </summary>
	[DefaultExecutionOrder(-10000)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	private class DebugDrawFrameStartTimer : MonoBehaviour
	{

		private void OnEnable()
		{
			hideFlags = TimerHideFlags;
		}

		private void FixedUpdate()
		{
			if (!doFixedUpdate)
				return;

			DoUpdate();
		}

		private void Update()
		{
			if (doFixedUpdate)
				return;

			DoUpdate();
		}

		private void DoUpdate()
		{
			UpdateCamera();
			frameTime = beforeInitialise ? 0 : Time.time;
		}

	}
	
	#endif
	

}