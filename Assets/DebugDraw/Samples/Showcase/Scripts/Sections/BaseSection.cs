using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	[ExecuteAlways]
	public class BaseSection : MonoBehaviour
	{

		protected Transform tr;
		
		private void OnEnable()
		{
			tr = transform;
			
			Showcase.InitRandom(tr);
			Init();
		}

		protected virtual void Init()
		{
			
		}

		private void OnValidate()
		{
			if (tr)
			{
				Showcase.InitRandom(tr);
				Init();
			}
		}

	}

}