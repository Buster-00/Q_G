using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    //variable of the instance
    private static T instance;

    //Property of the instance 
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject newGameObject = new GameObject();
                    instance = newGameObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    //In case any Singleton class has awake method, we need to prepare here as well
    protected virtual void awake()
    {
        instance = this as T;
    }
}
