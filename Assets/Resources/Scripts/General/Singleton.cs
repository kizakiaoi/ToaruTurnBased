﻿using UnityEngine;
/// <summary>
/// code source. really helpful!
/// http://wiki.unity3d.com/index.php/Toolbox
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance'" + typeof(T) +
                    "' already destroyed on application quit." +
                    "Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it");
                        return _instance;
                    }

                    if(_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so'" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }

                    else
                    {
                        Debug.Log("[Singleton] Using Instance already created: " +
                            _instance.gameObject.name);
                    }
                }
                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In Pricinple, a Singleton is only destroyed when application quits;
    /// If any script calls Instacen after It have been destroyed,
    /// it will create a buggy ghost object that will stay on the Editor Scene
    /// even after stopping to play the application. really bad.
    /// so this was made to sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestory()
    {
        applicationIsQuitting = true;
    }

}
