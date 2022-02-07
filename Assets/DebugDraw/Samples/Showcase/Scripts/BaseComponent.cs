using UnityEngine;

namespace DebugDrawSamples.Showcase.Scripts
{

	public class BaseComponent : MonoBehaviour
	{

		protected Transform tr;

		protected virtual void OnEnable()
		{
			tr = transform;
		}

	}

}