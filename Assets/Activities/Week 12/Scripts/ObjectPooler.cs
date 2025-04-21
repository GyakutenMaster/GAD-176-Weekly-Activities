using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAD176.WeeklyActivities.WeekTwelve
{
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        // this is my dictionary.
        private Dictionary<GameObject, List<GameObject>> objectPools = new Dictionary<GameObject, List<GameObject>>();

        // spawn from the pool
        public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (objectPools.ContainsKey(prefab) != true)
            {
                // here we have no current entry, so lets create one.
                // call the create new pool entry and pass in the preab.
                CreateNewPoolEntry(prefab);
            }

            // here we have a pool entry.
            // so we want to set the current pool to be that.
            // so lets set the current pool to be the object pools dictionary and pass in the prefab as the key.
            // this return back the list of gameobjects associated with the prefab.
            List<GameObject> currentPool = objectPools[prefab];

            Debug.Log(currentPool.Count);

            if (currentPool.Count <= 0)
            {
                // here lets call instantiate and pass in the prefab to spawn.
                GameObject newInstance = Instantiate(prefab);
                // then lets call the Add to pool function, pass in the new instance, the prefab, and the current pool.
                AddToPool(newInstance, prefab, currentPool);
            }

            /// here set the current object to spawn to be the first element of the current pool
            GameObject currentObjectToSpawn = currentPool[0];

            // here use remove at on the current pool and remove the first elmeent.
            currentPool.RemoveAt(0);


            if (currentObjectToSpawn)
            {

                // access the position of the current objectToSpawn and set it to the position.
                currentObjectToSpawn.transform.position = position;
                // access the rotation of the current objectToSpawn and set it to the rotation.
                currentObjectToSpawn.transform.rotation = rotation;

                // here for the current object to spawn search for the object pool item script and call the OnSpawnFunction.
                currentObjectToSpawn.GetComponent<ObjectPoolItem>().OnSpawn();
            }

            return currentObjectToSpawn;
        }

        // return to the pool
        public void ReturnToPool(GameObject obj)
        {
            if (!obj)
            {
                // need to check the object hasn't already been returned/destroyed.
                return;
            }

            // here lets search the obj for an objectpool item script.
            ObjectPoolItem poolIdentifier = obj.GetComponent<ObjectPoolItem>();

            if (poolIdentifier != null)
            {
                // here we are checking if it's all ready in the ppol, and the object it isn't already being destroyed this is useful
                // for when objects like tank shells die instantly, but also after time.
                if (objectPools.ContainsKey(poolIdentifier.ObjectPoolIdententifier) && !objectPools[poolIdentifier.ObjectPoolIdententifier].Contains(obj))
                {
                    // here access the object pools dictionary [] and pass in the objectpooolidentifier of the pool identifer as the key
                    // you'll then want to use the add function of the list returned and pass in the obj.
                    objectPools[poolIdentifier.ObjectPoolIdententifier].Add(obj);

                    //Debug.Log(objectPools[poolIdentifier.ObjectPoolIdententifier].Count);
                }
            }
           
            if (poolIdentifier)
            {
                // lets call Ondespawn on the pool identifier.
                poolIdentifier.OnDespawn();
            }
        }

        public void ReturnToPool(GameObject obj, float time)
        {
            if(time > 0)
            {
                // here search the obj for the object pool item script.
                ObjectPoolItem item = obj.GetComponent<ObjectPoolItem>();

                if (item != null)
                {
                    // here lets access the item and call Despawn after time and pass in the time variable.
                    item.DespawnAfterTime(time);
                }
            }
        }

        private IEnumerator ReturnToPoolAfterSeconds(GameObject obj, float time)
        {
            // here lets wait for new seconds and pass in the time.
            yield return new WaitForSeconds(time);
            // here lets call the return to pool function and pass in the object.
            ReturnToPool(obj);
        }

        /// <summary>
        /// this adds a new entry to the dictionary.
        /// </summary>
        /// <param name="prefab"></param>
        private void CreateNewPoolEntry(GameObject prefab)
        {
            Debug.Log("Not currently in the Object Pool, creating entry!");
            List<GameObject> newPool = new List<GameObject>();

            // here lets access the object pools dictionary and add a new entry with the prefab as the key and the new pool as the list.
            objectPools.Add(prefab, newPool);
        }

        /// <summary>
        /// This one adds a new pool object to an existing dictionary.
        /// </summary>
        /// <param name="newInstance"></param>
        /// <param name="prefabOriginal"></param>
        /// <param name="pool"></param>
        private void AddToPool(GameObject newInstance, GameObject prefabOriginal, List<GameObject> pool)
        {
            if (!newInstance.GetComponent<ObjectPoolItem>())
            {
                // here we don't have the object pool item script, so lets add it to the new instance using AddComponent.
                newInstance.AddComponent<ObjectPoolItem>();
            }
            if (newInstance.GetComponent<ObjectPoolItem>())
            {
                // here we have the component, so let's access the component and access the ObjectPoolIndentifier, lets set it to be the original prefab.
                newInstance.GetComponent<ObjectPoolItem>().ObjectPoolIdententifier = prefabOriginal;
            }
            // here lets set the new instance to be inactive in the scene.
            newInstance.SetActive(false);
            // here lets add the new instance to our pool.
            pool.Add(newInstance);
        }
    }
}