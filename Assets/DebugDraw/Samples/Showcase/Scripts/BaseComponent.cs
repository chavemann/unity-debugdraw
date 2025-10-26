using UnityEngine;

namespace DebugDrawSamples.Showcase
{

public class BaseComponent : MonoBehaviour
{
	
	public Transform tr { get; protected set; }
	
	protected virtual void OnEnable()
	{
		tr = transform;
	}
	
}

}