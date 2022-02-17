using UnityEngine;

namespace DebugDrawSamples.Showcase.Sections
{

	[ExecuteAlways]
	public class BaseSection : MonoBehaviour
	{

		public int seed;
		
		protected Transform tr;
		
		private void OnEnable()
		{
			tr = transform;
			
			Showcase.InitRandom(tr, seed);
			Init();
		}

		protected virtual void Init()
		{
			
		}

		protected bool HasChanged(Transform tr)
		{
			if (!tr || !tr.hasChanged)
				return false;

			tr.hasChanged = false;
			return true;
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