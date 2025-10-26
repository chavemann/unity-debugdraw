using UnityEngine;

namespace DebugDrawShowcase
{

public class Spinner : BaseComponent
{
	
	public Vector3 spin;
	
	private Vector3 angles;
	
	protected override void OnEnable()
	{
		base.OnEnable();
		
		angles = Transform.rotation.eulerAngles;
	}
	
	private void Update()
	{
		angles += spin * Time.deltaTime;
		Transform.rotation = Quaternion.Euler(angles);
	}
	
}

}
