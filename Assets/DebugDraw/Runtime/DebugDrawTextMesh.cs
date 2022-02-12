using DebugDrawItems;
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

		float lineHeight = DebugDraw.TextStyle.lineHeight;

		for(int i = itemCount - 1; i >= 0; i--)
		{
			Text item = (Text) items[i];

			Vector3 p = DebugDraw.cam.WorldToViewportPoint(item.position);
			
			// This text is behind the camera 
			if (p.z < 0.25f)
				continue;
			// Or too far outside the screen.
			if (p.x < -1f || p.x > 2f || p.y < -1f || p.y > 2f)
				continue;
			
			p.y = 1 - p.y;
			Matrix4x4 m = Matrix4x4.Translate(new Vector3(p.x * screenSize.x, p.y * screenSize.y, 0));

			float scale = item.scale;

			if (scale != 1 || item.useWorldSize)
			{
				if (item.useWorldSize)
				{
					scale *= DebugDraw.textBaseWorldDistance /
							 // Prevent the text from getting too big or risk the font not being able to
							 // fit into a single texture.
							 Mathf.Max(p.z, 0.5f);

					if (scale * lineHeight < DebugDraw.minTextSize)
						continue;
				}

				m *= Matrix4x4.Scale(new Vector3(scale, scale, scale));
			}
			
			TextGUIContent.text = item.text;
			DebugDraw.TextStyle.alignment = item.align;
			GUI.matrix = m;

			if (DebugDraw.textShadowColor.HasValue)
			{
				rect.x = rect.y = 1 / scale;
				GUI.color = DebugDraw.textShadowColor.GetValueOrDefault();
				GUI.Label(rect, TextGUIContent, DebugDraw.TextStyle);
				rect.x = 0;
				rect.y = 0;
			}
			
			GUI.color = item.GetColor(ref item.color);
			GUI.Label(rect, TextGUIContent, DebugDraw.TextStyle);
		}

		GUI.color = guiColor;
		GUI.matrix = guiMatrix;
	}

}