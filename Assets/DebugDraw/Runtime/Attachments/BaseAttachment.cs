namespace DebugDrawUtils.Attachments
{
	
/// <summary>
/// The abstract base for all attachment types.
/// Attachments will also be automatically destroyed when they expire or any
/// game objects they are attached to are destroyed.
/// </summary>
public abstract class BaseAttachment
	{
		
		/// <summary>
		/// Is this visual alive. Will be true when created, and false when the visual expires or is manually removed.
		/// A visual should not be used when it is not alive.
		/// </summary>
		public bool IsAlive => !destroyed && index != -1;
		
		internal int index = -1;
		internal bool destroyed;
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Methods -- */
		
		/// <summary>
		/// Destroys this attachment. Attachments will also be automatically destroyed when
		/// they expire or any game objects they are attached to are destroyed.
		/// </summary>
		public void Destroy()
		{
			destroyed = true;
		}
		
		public static implicit operator bool(BaseAttachment baseAttachment)
		{
			return baseAttachment != null;
		}
		
		/* ------------------------------------------------------------------------------------- */
		/* -- Private -- */
		
		internal abstract bool Update();
		
		internal abstract void Release();
		
	}
	
}
