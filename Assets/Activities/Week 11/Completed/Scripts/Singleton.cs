using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEleven.Completed
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // here lets search the scene using find object of type, but this time we'll pass in T as the component to search for.
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        // here let's create a new GameObject() we can also pass in a string for a name.
                        // for the name let's use typeof(T) and access the name property and let's add singleton afterwards.
                        GameObject singletonObject = new GameObject(typeof(T).Name + " Singleton");
                        // finally let's access the singleton gameobject and use Add Component, the component we'll add is T
                        instance = singletonObject.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        private void OnDestroy()
        {
            instance = null;
        }

    }
}