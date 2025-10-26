using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawShowcase.Sections
{

public class DebugCameraSection : BaseSection
{
	
	private void OnTriggerEnter(Collider other)
	{
		if (DebugDrawCamera.IsActive)
			return;
		
		Transform tr = other.transform;
		Camera cam = tr.GetComponentInChildren<Camera>();
		
		if (!cam)
			return;
		
		Showcase.ToggleDebugCamera();
	}
	
}

}
