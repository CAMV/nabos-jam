using UnityEngine;

/// <summary>
/// Singleton base class to be used ad pattern
/// </summary>
/// <typeparam name="T">Class to be used as a singleton</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component {

    protected static T _instance;

    public static T Instance
    {
        get { return _instance; }
    }

    protected virtual void OnAwake() { }

    // Use this for initialization
    void Awake ()
    {
        if (_instance != null && _instance != this)
        {
            Destroy (gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad (gameObject);
            OnAwake();
        }
    }
}