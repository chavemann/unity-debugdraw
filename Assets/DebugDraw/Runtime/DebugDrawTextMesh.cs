using Items;
using UnityEngine;

public class DebugDrawTextMesh : DebugDrawMesh
{

	private const float SizeOnScreen = 1;
	
	private static readonly GUIContent TextGUIContent = new GUIContent();
	
	public DebugDrawTextMesh() : base(MeshTopology.Points)
	{
	}

	public new void Build()
	{
		if (itemCount == 0)
			return;
		
		Color guiColor = GUI.color;
		Matrix4x4 guiMatrix = GUI.matrix;
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		Rect rect = new Rect(0, 0, 0, 0);

		for(int i = itemCount - 1; i >= 0; i--)
		{
			Text item = (Text) items[i];

			Vector3 p = DebugDraw.cam.WorldToScreenPoint(item.position);
			
			// This text is behind the camera 
			if (p.z < 0.25f)
				continue;
			
			p.y = screenSize.y - p.y;
			p.z = 0;
			Matrix4x4 m = Matrix4x4.Translate(new Vector3(p.x, p.y, 0));
			
			if (item.scale != 1 || item.autoSize)
			{
				float s = item.scale;

				if (item.autoSize)
				{
					if (DebugDraw.cam.WorldToScreenPoint(item.position).z <= 0.25f)
						continue;
					
					s *= 10.0f / new Vector3(
						item.position.x - DebugDraw.camPosition.x,
						item.position.y - DebugDraw.camPosition.y,
						item.position.z - DebugDraw.camPosition.z).magnitude;
				}
				
				m *= Matrix4x4.Scale(new Vector3(s, s, s));
			}
			
			TextGUIContent.text = item.text;
			DebugDraw.TextStyle.alignment = item.align;
			GUI.matrix = m;
			GUI.color = item.GetColor(ref item.color);
			GUI.Label(rect, TextGUIContent, DebugDraw.TextStyle);
		}

		GUI.color = guiColor;
		GUI.matrix = guiMatrix;
	}

}