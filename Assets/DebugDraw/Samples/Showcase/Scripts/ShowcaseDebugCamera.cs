using DebugDrawUtils;
using UnityEngine;

namespace DebugDrawShowcase
{

public class ShowcaseDebugCamera : DebugDrawCamera
{
	
	private static readonly GUIStyle MessageStyle = new();
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void RuntimeInit()
	{
		factory = o => o.AddComponent<ShowcaseDebugCamera>();
		
		MessageStyle.alignment = TextAnchor.UpperCenter;
		MessageStyle.fontSize = 30;
		MessageStyle.normal.textColor = Color.white;
	}
	
	private void OnGUI()
	{
		GUI.Label(
			new Rect(0, 10, Screen.width, Screen.height),
			"- Debug Camera -\n<size=20>Enter to toggle</size>",
			MessageStyle);
	}
	
}

}
