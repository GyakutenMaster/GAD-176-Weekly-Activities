using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwelve.Completed
{
    public abstract class ObjectPoolItem : MonoBehaviour
    {
        [SerializeField] protected GameObject objectPrefabIdentifier;
        public GameObject ObjectPoolIdententifier
        {
            get
            {
                return objectPrefabIdentifier;
            }
            set
            {
                objectPrefabIdentifier = value;
            }
        }

        private Coroutine despawnAfterTimeRoutine;


        public abstract void OnSpawn();

        public virtual void OnDespawn()
        {
            // here lets call stop despawn over time.
            StopDespawnOverTime();
        }

        public void DespawnAfterTime(float time)
        {
            if(despawnAfterTimeRoutine == null) 
            {
                // here lets set the despawn after time routine to be 
                // start coroutine an call the return to pool after seconds routine and pass in the time parameter.
                despawnAfterTimeRoutine = StartCoroutine(ReturnToPoolAfterSeconds(time));
            }
        }

        private IEnumerator ReturnToPoolAfterSeconds(float time)
        {
            // here lets wait for seconds and past in the time.
            yield return new WaitForSeconds(time);
            // here lets access the object pooler instance and pass in this gameobject.
            ObjectPooler.Instance.ReturnToPool(gameObject);
        }

        public void StopDespawnOverTime()
        {
            if (despawnAfterTimeRoutine != null)
            {
                // here lets stop the coroutine and pass in the despawn after time routine.
                StopCoroutine(despawnAfterTimeRoutine);
                // then lets set our despawn after time routine to be null.
                despawnAfterTimeRoutine = null;
            }
        }
    }
}