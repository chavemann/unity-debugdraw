#if !DEBUG_DRAW_OFF
#define DEBUG_DRAW
#endif

using UnityEngine;

public static partial class DebugDraw
{

	#if DEBUG_DRAW

	private const HideFlags TimerHideFlags = HideFlags.DontSave;
	// private const HideFlags TimerHideFlags = HideFlags.HideAndDontSave;

	/// <summary>
	/// TODO: 
	/// </summary>
	[DefaultExecutionOrder(10000)]
	[ExecuteAlways]
	[AddComponentMenu("")]
	private class DebugDrawTimer : MonoBehaviour
	{

		#if UNITY_EDITOR
		#endif
		
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
				timerInstance = this;
				Clear();
			}
			else if (timerInstance == this)
			{
				Clear();
			}
		}

		private void OnDisable()
		{
			if (timerInstance == this || timerInstance == null)
			{
				pendingDestroy = true;
				timerInstance = null;
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

		private void DoUpdate()
		{
			UpdateVisuals();
			pointMeshInstance.Update();
			lineMeshInstance.Update();
			triangleMeshInstance.Update();
			requiresBuild = true;
			requiresDraw = true;
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

			frameTime = beforeInitialise ? 0 : Time.time;
		}

		private void Update()
		{
			if (doFixedUpdate)
				return;

			frameTime = beforeInitialise ? 0 : Time.time;
		}

	}
	
	#endif
	

}