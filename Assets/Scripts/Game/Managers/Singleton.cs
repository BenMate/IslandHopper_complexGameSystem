using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => instance;
    static T instance;

    void Awake()
    {
        if(instance == null)
            instance = GetComponent<T>();
        else
            Destroy(this);
    }
}