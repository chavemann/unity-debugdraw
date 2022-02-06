using UnityEngine;

namespace DebugDrawUtils
{

	/// <summary>
	/// A convenience wrapper for allowing a single method to accept a Transform or GameObject.
	/// </summary>
	public readonly struct GameObjectOrTransform
	{

		public readonly Transform transform;

		public GameObjectOrTransform(Transform transform)
		{
			this.transform = transform;
		}

		public static implicit operator GameObjectOrTransform(GameObject gameObject)
		{
			return new GameObjectOrTransform(gameObject ? gameObject.transform : null);
		}

		public static implicit operator GameObjectOrTransform(Transform transform)
		{
			return new GameObjectOrTransform(transform);
		}

		public static implicit operator Transform(GameObjectOrTransform obj)
		{
			return obj.transform;
		}

	}

}