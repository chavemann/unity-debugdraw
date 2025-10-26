using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugDrawSamples.Showcase
{

public class Bobber : BaseComponent
{
	
	public Vector3 speed = new Vector3(0, 1, 0);
	public Vector3 range = new Vector3(0, 2, 0);
	public Vector3 phase;
	
	[SerializeField, HideInInspector]
	private Vector3 startPos;
	
	private void Reset()
	{
		phase.x = Random.value * Mathf.PI;
		phase.y = Random.value * Mathf.PI;
		phase.z = Random.value * Mathf.PI;
	}
	
	private void Awake()
	{
		startPos = transform.localPosition;
	}
	
	private void Update()
	{
		tr.localPosition = new Vector3(
			startPos.x + Mathf.Cos((Time.time + phase.x) * speed.x) * range.x,
			startPos.y + Mathf.Cos((Time.time + phase.y) * speed.y) * range.y,
			startPos.z + Mathf.Cos((Time.time + phase.z) * speed.z) * range.z);
	}
	
}

}