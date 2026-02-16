using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindAnyObjectByType(typeof(T));

                if (instance == null)
                    Debug.LogError("No object of " + typeof(T).Name + " is no found");
            }
            return instance;
        }
    }
    public static bool IsValid => instance != null;

    static T instance = null;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(Instance.gameObject);
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

    protected static T InstantiateInstance(string prefabPathInResources)
    {
        Object resourceOb = Resources.Load(prefabPathInResources);

        Debug.Assert(resourceOb != null, prefabPathInResources);

        if (instance != null)
        {
            Destroy(Instance.gameObject);
        }
        GameObject ob = UnityEngine.Object.Instantiate(resourceOb) as GameObject;
        UnityEngine.Object.DontDestroyOnLoad(ob);
        instance = ob.GetComponent<T>();
        if (instance == null)
            Debug.LogWarningFormat("Resource from {0} doesn't have component {1}.", prefabPathInResources, typeof(T).Name);

        return instance;
    }
}