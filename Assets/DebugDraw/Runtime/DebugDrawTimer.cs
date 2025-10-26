#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DebugDrawUtils
{

public static partial class DebugDraw
{
	
	#if DEBUG_DRAW
	
	/// <summary>
	/// For some reason <c>HideAndDontSave</c> seemed to break some of the functionality so every new instance will be
	/// visible in the inspector until a scene change happens.
	/// </summary>
	private const HideFlags TimerHideFlags = HideFlags.DontSave | HideFlags.NotEditable;
	// private const HideFlags TimerHideFlags = HideFlags.HideAndDontSave;
	
	private static readonly WaitForFixedUpdate WaitForFixedUpdate = new();
	
	/// <summary>
	/// A component automatically added to the scene with HideFlags.DontSave set that
	/// handles updating all debug items at the end of every frame.
	/// </summary>
	[DefaultExecutionOrder(0xffffff)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	private class DebugDrawTimer : MonoBehaviour
	{
		
		private UnityAction<Scene, Scene> onActiveSceneChangedDelegate;
		private bool updateInFixedUpdate;
		
		private bool pendingDestroy;
		private DebugDrawFrameStartTimer startTimer;
		
		private void Start()
		{
			StartCoroutine(OnPostFixedUpdate());
		}
		
		private void OnEnable()
		{
			if (pendingDestroy || !Application.isPlaying && !enableInEditMode)
			{
				DestroyTimer();
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
			}
			
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			#pragma warning disable 162
			if (UpdateInstanceScene && timerInstance == this)
			{
				if (onActiveSceneChangedDelegate == null)
				{
					onActiveSceneChangedDelegate = OnActiveSceneChanged;
				}
				
				SceneManager.activeSceneChanged -= onActiveSceneChangedDelegate;
				SceneManager.activeSceneChanged += onActiveSceneChangedDelegate;
			}
			#pragma warning restore 162
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
		
		private IEnumerator OnPostFixedUpdate()
		{
			while (true)
			{
				yield return WaitForFixedUpdate;
				
				if (doFixedUpdate)
				{
					DoUpdate();
				}
			}
			// ReSharper disable once IteratorNeverReturns
		}
		
		private void LateUpdate()
		{
			if (doFixedUpdate)
				return;
			
			DoUpdate();
		}
		
		private void DoUpdate()
		{
			// Log.Print("--- DebugDrawTimer.DoUpdate", gameObject.GetInstanceID());
			
			camUpdated = false;
			
			pointMeshInstance.Update();
			lineMeshInstance.Update();
			triangleMeshInstance.Update();
			textMeshInstance.Update();
			UpdateAttachments();
			requiresBuild = true;
			
			Groups.ResetStack();
			LogMessage.Groups.ResetStack();
			
			if (LogMessage.hasMessages)
			{
				LogMessage.Update();
			}
		}
		
		private void OnGUI()
		{
			if (Event.current.type != EventType.Repaint)
				return;
			
			textMeshInstance.globalOrigin = globalOrigin;
			textMeshInstance.globalRotation = globalRotation;
			textMeshInstance.Build();
			
			if (LogMessage.hasMessages)
			{
				LogMessage.Draw();
			}
		}
		
		internal void DestroyTimer()
		{
			if (gameObject)
			{
				// Not sure why but even with the null checks it would still throw an exception when exiting play mode.
				try
				{
					if (this)
						DestroyObj(this);
				}
				catch (MissingReferenceException) { }
				
				try
				{
					if (startTimer)
						DestroyObj(startTimer);
				}
				catch (MissingReferenceException) { }
				
				try
				{
					if (gameObject)
						DestroyObj(gameObject);
				}
				catch (MissingReferenceException) { }
				
				startTimer = null;
			}
			
			pendingDestroy = false;
		}
		
	}
	
	/// <summary>
	/// Complementary to DebugDrawTimer, this has the execution order set to execute before anything
	/// to initialise certain DebugDraw values needed every frame.
	/// </summary>
	[DefaultExecutionOrder(-0xffffff)]
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
			// Log.Print("-------------------- DebugDrawFrameStartTimer.DoUpdate");
			
			UpdateCamera();
			frameTime = beforeInitialise ? 0 : useUnscaledTime ? Time.unscaledTime : Time.time;
		}
		
	}
	
	#endif
	
}

}
