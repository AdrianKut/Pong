using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if( Instance != null && Instance != this )
        {
            Debug.LogWarning( $"There is another {typeof( T ).Name} on scene!" );
            Destroy( gameObject );
        }
        else
        {
            Instance = this as T;
        }
    }
}
