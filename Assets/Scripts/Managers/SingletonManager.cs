using UnityEngine;

internal class SingletonManager<T> : MonoBehaviour
    where T : MonoBehaviour
{
    protected static T s_Instance = null;
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                var managerContainer = GetManagersContainerObject();
                s_Instance = managerContainer.AddComponent<T>();
            }

            return s_Instance;
        }
        set
        {
            s_Instance = value;
        }
    }

    private const string ManagerContainer = "==== MANAGERS ====";

    private static GameObject GetManagersContainerObject()
    {
        var obj = GameObject.Find(ManagerContainer);
        if (obj == null)
        {
            obj = new GameObject(ManagerContainer);
        }

        return obj;
    }
}

