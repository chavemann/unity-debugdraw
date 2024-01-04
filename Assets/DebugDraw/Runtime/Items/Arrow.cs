using System.Runtime.CompilerServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DebugDrawUtils.DebugDrawItems
{

	/// <summary>
	/// Draws an arrow.
	/// </summary>
	public class Arrow : Line
	{
		/* mesh: line */

		/// <summary>
		/// The properties of the head at the start of this arrow.
		/// </summary>
		public readonly ArrowHead startHead;
		/// <summary>
		/// The properties of the head at the start of this arrow.
		/// </summary>
		public readonly ArrowHead endHead;
		/// <summary>
		/// If true the arrow heads will automatically orient themselves to be perpendicular to the camera.
		/// </summary>
		public bool faceCamera;
		/// <summary>
		/// If true adjusts the size of the arrow heads so it approximately remains the same size on screen.
		/// </summary>
		public bool autoSize;
		/// <summary>
		/// The line will always remain at least this long.
		/// </summary>
		public float minLength;
		/// <summary>
		/// The line will not be able to get longer than this.
		/// </summary>
		public float maxLength;

		/* ------------------------------------------------------------------------------------- */
		/* -- Getters -- */

		/// <summary>
		/// Draws an arrow.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="startSize">The size of the arrow head at the start of the line.</param>
		/// <param name="endSize">The size of the arrow head at the end of the line.</param>
		/// <param name="startShape">The shape of the head at the start of the line.</param>
		/// <param name="endShape">The shape of the head at the end of the line.</param>
		/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
		/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Arrow object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Arrow Get(
			ref Vector3 p1, ref Vector3 p2, ref Color color1, ref Color color2,
			float startSize, float endSize,
			ArrowShape startShape = ArrowShape.Arrow, ArrowShape endShape = ArrowShape.Arrow,
			bool faceCamera = false, bool autoSize = false, EndTime duration = default)
		{
			Arrow item = ItemPool<Arrow>.Get(duration);

			item.p1 = p1;
			item.p2 = p2;
			item.color = color1;
			item.color2 = color2;
			item.startHead.shape = startShape;
			item.startHead.SetSize(startSize);
			item.startHead.offset = 0;
			item.endHead.shape = endShape;
			item.endHead.SetSize(endSize);
			item.endHead.offset = 0;
			item.faceCamera = faceCamera;
			item.autoSize = autoSize;
			item.minLength = 0;
			item.maxLength = float.PositiveInfinity;

			return item;
		}

		/// <summary>
		/// Draws an arrow.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="startSize">The size of the arrow head at the start of the line.</param>
		/// <param name="endSize">The size of the arrow head at the end of the line.</param>
		/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
		/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Arrow object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Arrow Get(
			ref Vector3 p1, ref Vector3 p2, ref Color color1, ref Color color2,
			float startSize, float endSize, bool faceCamera = false, bool autoSize = false, EndTime duration = default)
		{
			return Get(
				ref p1, ref p2, ref color1, ref color2, startSize, endSize,
				startSize > 0 ? ArrowShape.Arrow : ArrowShape.None,
				endSize > 0 ? ArrowShape.Arrow : ArrowShape.None,
				faceCamera, autoSize, duration);
		}

		/// <summary>
		/// Draws an arrow.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color1">The line's colour at the start.</param>
		/// <param name="color2">The line's colour at the end.</param>
		/// <param name="size">The size of the arrow head.</param>
		/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
		/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Arrow object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Arrow Get(ref Vector3 p1, ref Vector3 p2, ref Color color1, ref Color color2, float size, bool faceCamera = false,bool autoSize = false, EndTime duration = default)
		{
			return Get(
				ref p1, ref p2, ref color1, ref color2, size, size,
				ArrowShape.None, ArrowShape.Arrow, faceCamera, autoSize, duration);
		}

		/// <summary>
		/// Draws an arrow.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color">The arrow's colour.</param>
		/// <param name="startSize">The size of the arrow head at the start of the line.</param>
		/// <param name="endSize">The size of the arrow head at the end of the line.</param>
		/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
		/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Arrow object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Arrow Get(
			ref Vector3 p1, ref Vector3 p2, ref Color color,
			float startSize, float endSize, bool faceCamera = false, bool autoSize = false, EndTime duration = default)
		{
			return Get(
				ref p1, ref p2, ref color, ref color, startSize, endSize,
				startSize > 0 ? ArrowShape.Arrow : ArrowShape.None,
				endSize > 0 ? ArrowShape.Arrow : ArrowShape.None,
				faceCamera, autoSize, duration);
		}

		/// <summary>
		/// Draws an arrow.
		/// </summary>
		/// <param name="p1">The start of the line.</param>
		/// <param name="p2">The end of the line.</param>
		/// <param name="color">The arrow's colour.</param>
		/// <param name="size">The size of the arrow head.</param>
		/// <param name="faceCamera">If true the arrow heads will automatically orient themselves to be perpendicular to the camera.</param>
		/// <param name="autoSize">If true adjusts the size of the arrow heads so it approximately remains the same size on screen.</param>
		/// <param name="duration">How long the item will last in seconds. Set to 0 for only the next frame, and negative to persist forever.</param>
		/// <returns>The Arrow object.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Arrow Get(ref Vector3 p1, ref Vector3 p2, ref Color color, float size, bool faceCamera = false,bool autoSize = false, EndTime duration = default)
		{
			return Get(
				ref p1, ref p2, ref color, ref color, size, size,
				ArrowShape.None, ArrowShape.Arrow, faceCamera, autoSize, duration);
		}

		public Arrow()
		{
			startHead = new ArrowHead(this);
			endHead = new ArrowHead(this);
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		/// <summary>
		/// Set the min and max length for this line.
		/// </summary>
		/// <param name="minLength">Set to negative or zero to have no lower limit.</param>
		/// <param name="maxLength">Set to PositiveInfinity to have no upper limit.</param>
		/// <returns></returns>
		public Arrow SetLimits(float minLength, float maxLength)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;

			return this;
		}

		internal override void Build(DebugDrawMesh mesh)
		{
			Vector3 p1 = this.p1;
			Vector3 p2 = this.p2;

			Vector3 dir = new Vector3(
				p2.x - p1.x,
				p2.y - p1.y,
				p2.z - p1.z);
			float length = dir.magnitude;

			if (length <= 0)
				return;

			dir.x /= length;
			dir.y /= length;
			dir.z /= length;

			if (startHead.offset != 0)
			{
				p1.x += dir.x * startHead.offset;
				p1.y += dir.y * startHead.offset;
				p1.z += dir.z * startHead.offset;
				length -= startHead.offset;
			}

			if (endHead.offset != 0)
			{
				p2.x -= dir.x * endHead.offset;
				p2.y -= dir.y * endHead.offset;
				p2.z -= dir.z * endHead.offset;
				length -= endHead.offset;
			}

			Color clr1 = GetColor(ref color);
			Color clr2 = GetColor(ref color2);

			if (minLength > 0 || !float.IsPositiveInfinity(maxLength))
			{
				if(minLength > 0 && length < minLength)
				{
					length = minLength;
				}
				else if(!float.IsPositiveInfinity(maxLength) && length > maxLength)
				{
					length = maxLength;
				}

				p2.x = p1.x + dir.x * length;
				p2.y = p1.y + dir.y * length;
				p2.z = p1.z + dir.z * length;

				mesh.AddLine(this, ref p1, ref p2, ref clr1, ref clr2);
			}
			else
			{
				mesh.AddLine(this, ref p1, ref p2, ref clr1, ref clr2);
			}

			ArrowHead head = endHead;
			int p1Index = mesh.vertexIndex - 2;
			int index = mesh.vertexIndex - 1;
			ref Color clr = ref color2;
			float flip = -1;

			for (int i = 0; i < 2; i++)
			{
				if (head.shape != ArrowShape.None)
				{
					Vector3 n;

					if (faceCamera)
					{
						n = Vector3.Cross(DebugDraw.camForward, dir);
						n.Normalize();
					}
					else
					{
						DebugDraw.FindAxisVectors(ref dir, ref DebugDraw.forward, out Vector3 _, out n);
					}

					if (head.shape == ArrowShape.Arrow || head.shape == ArrowShape.Line)
					{
						float headLength = head.shape == ArrowShape.Arrow ? head.length : 0;

						float size = autoSize && !DebugDraw.camOrthographic
							? Mathf.Max(Vector3.Dot(new Vector3(
								p1.x - DebugDraw.camPosition.x,
								p1.y - DebugDraw.camPosition.y,
								p1.z - DebugDraw.camPosition.z), DebugDraw.camForward), 0) * BaseAutoSizeDistanceFactor
							: 1;
						mesh.AddColorX2(ref clr);
						mesh.AddVertex(this,
							p2.x + dir.x * flip * headLength * size + n.x * head.width * size,
							p2.y + dir.y * flip * headLength * size + n.y * head.width * size,
							p2.z + dir.z * flip * headLength * size + n.z * head.width * size);
						mesh.AddVertex(this,
							p2.x + dir.x * flip * headLength * size - n.x * head.width * size,
							p2.y + dir.y * flip * headLength * size - n.y * head.width * size,
							p2.z + dir.z * flip * headLength * size - n.z * head.width * size);

						if (head.shape == ArrowShape.Arrow)
						{
							mesh.AddIndices(
								index, mesh.vertexIndex++,
								index, mesh.vertexIndex++);
						}
						else
						{
							mesh.AddIndexX2();
						}
					}
				}

				head = startHead;
				p2 = p1;
				index = p1Index;
				clr = ref color;
				flip = 1;
			}
		}

		internal override void Release()
		{
			ItemPool<Arrow>.Release(this);
		}

	}

	public class ArrowHead
	{

		/// <summary>
		/// The shape of the arrow head.
		/// </summary>
		public ArrowShape shape;
		/// <summary>
		/// The width (perpendicular size) of the arrow head.
		/// </summary>
		public float width;
		/// <summary>
		/// The length (along the arrow direction) of the arrow head.
		/// </summary>
		public float length;
		/// <summary>
		/// Pushes the head away from the target point by this distance.
		/// </summary>
		public float offset;

		private readonly Arrow arrow;

		public ArrowHead(Arrow arrow)
		{
			this.arrow = arrow;
		}

		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */

		public Arrow Clear()
		{
			shape = ArrowShape.None;
			width = length = 0;

			return arrow;
		}

		/// <summary>
		/// Sets the width and length of this arrow head.
		/// </summary>
		public Arrow SetSize(float size)
		{
			width = size;
			length = size;

			return arrow;
		}

		/// <summary>
		/// Individually sets the width and length of this arrow head.
		/// </summary>
		public Arrow SetSize(float width, float length)
		{
			this.width = width;
			this.length = length;

			return arrow;
		}

		/// <summary>
		/// Sets the target offset distance.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public Arrow SetOffset(float offset)
		{
			this.offset = offset;

			return arrow;
		}

	}

	/// <summary>
	/// The shape of the arrow head.
	/// </summary>
	public enum ArrowShape
	{

		/// <summary>
		/// Don't draw any arrow head.
		/// </summary>
		None,
		/// <summary>
		/// A normal arrow head made up of two lines.
		/// </summary>
		Arrow,
		/// <summary>
		/// A single line perpendicular to the arrow's line.
		/// </summary>
		Line,

	}

}
