using UnityEngine;

namespace DebugDrawShowcase
{

public class BaseComponent : MonoBehaviour
{
	
	public Transform Transform { get; protected set; }
	
	protected virtual void OnEnable()
	{
		Transform = transform;
	}
	
}

}
