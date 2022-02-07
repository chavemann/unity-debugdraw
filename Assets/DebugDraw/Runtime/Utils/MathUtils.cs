using UnityEngine;

namespace DebugDrawUtils
{

	internal static class MathUtils
	{

		/// <summary>
		/// Find good arbitrary axis vectors to represent U and V axes of a plane, using this vector as the normal of the plane.
		/// </summary>
		public static void FindBestAxisVectors(ref Vector3 v, out Vector3 up, out Vector3 right)
		{
			Vector3 n = new Vector3(
				Mathf.Abs(v.x),
				Mathf.Abs(v.y),
				Mathf.Abs(v.z));

			// Find best basis vectors.
			if(n.z > n.x && n.z > n.y)
			{
				up = new Vector3(1, 0, 0);
			}
			else
			{
				up = new Vector3(0, 0, 1);
			}

			float dot = Vector3.Dot(up, v);
			
			up = new Vector3(
				up.x - v.x * dot,
				up.y - v.y * dot,
				up.z - v.z * dot).normalized;
			
			right = Vector3.Cross(up, v);
		}

	}

}