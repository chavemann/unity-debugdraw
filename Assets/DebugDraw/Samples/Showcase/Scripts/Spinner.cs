using System;
using UnityEngine;

namespace DebugDrawSamples.Showcase.Scripts
{

	public class Spinner : BaseComponent
	{

		public Vector3 spin;

		private Vector3 angles;

		protected override void OnEnable()
		{
			base.OnEnable();

			angles = tr.rotation.eulerAngles;
		}

		private void Update()
		{
			angles += spin * Time.deltaTime;
			Quaternion rotation = tr.rotation;
			rotation.eulerAngles = angles;
			tr.rotation = rotation;
		}

	}

}