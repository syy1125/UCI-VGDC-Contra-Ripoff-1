using System.Collections;
using UnityEngine;

public abstract class AbstractMovement : MonoBehaviour
{
	public float MovementSpeed;

	protected Rigidbody2D Rb;
	protected Collider2D Collider;

	public LayerMask GroundMask;
	public LayerMask PlatformDetectorMask;
	public int DefaultLayer;
	public int IgnorePlatformLayer;

	/*protected virtual void Start()
	{
		Rb = GetComponent<Rigidbody2D>();
		Collider = GetComponent<Collider2D>();
	}*/

	protected virtual void Reset()
	{
		Rb = GetComponent<Rigidbody2D>();
		Collider = GetComponent<Collider2D>();
	}

	protected virtual void Start()
	{
		StartCoroutine(MonitorPlatformPassThrough());
	}

	protected bool IsGrounded()
	{
		return Collider.IsTouchingLayers(GroundMask.value);
	}

	private IEnumerator MonitorPlatformPassThrough()
	{
		while (true)
		{
			// Wait until script declares that it should fall through platforms, then do so.
			yield return new WaitUntil(ShouldPassThroughPlatform);
			gameObject.layer = IgnorePlatformLayer;

			// Wait until script declares that it should not fall through platforms
			// then wait until it is no longer inside a platform (to avoid glitching).
			yield return new WaitWhile(ShouldPassThroughPlatform);
			yield return new WaitWhile(() => Collider.IsTouchingLayers(PlatformDetectorMask.value));
			gameObject.layer = DefaultLayer;
		}
	}

	/// <summary>
	/// Whether the entity that this script controls should drop through the platforms.
	/// Override this method to implement custom platform drop through logic.
	/// </summary>
	protected virtual bool ShouldPassThroughPlatform()
	{
		return false;
	}
}