using UnityEngine;

public class Singleton<T> : MonoBehaviour
	where T : Singleton<T>
{
	public static T Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = (T) this;
		}
		else
		{
			Debug.LogWarning("Aborted an attempt to create another instance of `" + GetType().Name + "`; A singleton can only have one instance.");
			Destroy(gameObject);
		}
	}
}