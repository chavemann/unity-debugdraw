using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

public class DebugCameraSection : BaseSection
{
	
	private void OnTriggerEnter(Collider other)
	{
		if (DebugDrawCamera.isActive)
			return;
		
		Transform tr = other.transform;
		Camera cam = tr.GetComponentInChildren<Camera>();
		
		if (!cam)
			return;
		
		Showcase.ToggleDebugCamera();
	}
	
}

}