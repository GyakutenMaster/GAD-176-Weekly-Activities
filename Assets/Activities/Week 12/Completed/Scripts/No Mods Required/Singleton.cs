using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwelve.Completed
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
                    // search the scene to if there is one.
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject(typeof(T).Name + " Singleton");
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