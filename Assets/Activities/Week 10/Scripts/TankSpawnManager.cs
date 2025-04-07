using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTen
{
    public class TankSpawnManager : MonoBehaviour
    {
        [SerializeField] protected List<Transform> allPossibleSpawnPoints = new List<Transform>(); // this a list of all the possible tank spawn locations
        protected List<Transform> startingAllPossibleSpawnPoints = new List<Transform>();// storing the starting value of all possible spawn points
        [SerializeField] protected GameObject tankPrefab; // a list of all the possible tank prefabs
        protected List<GameObject> allTanksSpawnedIn = new List<GameObject>(); // a list of all the tanks spawned in
        protected List<GameObject> allTanksAlive = new List<GameObject>(); // a list of all the tanks spawned in

        private void OnEnable()
        {
            // subscribe to our reset round event.
            GameEvents.ResetRoundEvent += ResetRound;
            // subscribe our spawn tanks function to our spawn player event
            GameEvents.SpawnPlayerEvent += SpawnTanks;
            // unsubscribe our tank died function to our player has died event.
            GameEvents.PlayerHasDiedEvent += TankDied;
        }

        private void OnDisable()
        {
            // unsubscribe from our reset round event.
            GameEvents.ResetRoundEvent -= ResetRound;
            // unsubscribe our spawn tanks function from our spawn player event
            GameEvents.SpawnPlayerEvent -= SpawnTanks;
            // unsubscribe our tank died function from our player has died event.
            GameEvents.PlayerHasDiedEvent -= TankDied;
        }

        private void Start()
        {
        }


        public List<GameObject> AllTanksSpawnedIn
        {
            get
            {
                return allTanksSpawnedIn;
            }
        }

        public List<GameObject> AllTanksAlive
        {
            get
            {
                return allTanksAlive;
            }
        }

        /// <summary>
        /// This is called in our scene to help us with debugging.
        /// </summary>
        private void OnDrawGizmos()
        {
            // loops through all the possible spawn points
            for (int i = 0; i < allPossibleSpawnPoints.Count; i++)
            {
                Gizmos.color = Color.red; // set the color of our gizmo to red
                Gizmos.DrawSphere(allPossibleSpawnPoints[i].position, 0.25f); // draw a gizmo for our spawn point location
            }
        }

        public void TankDied(GameObject tankDead)
        {
            AllTanksAlive.Remove(tankDead);

            if (AllTanksAlive.Count <= 1)
            {
                // invoke our player has won event and pass in the only living tank which should the first element of the list.
                GameEvents.PlayerHasWonEvent?.Invoke(AllTanksAlive[0]);
            }
        }

        private void ResetRound()
        {
            AllTanksAlive.Clear();
            ClearAndResetAllSpawns();
            ResetTankPositions();
            for (int i = 0; i < AllTanksSpawnedIn.Count; i++)
            {
                AllTanksSpawnedIn[i].GetComponent<Tank>().Respawn();
                AllTanksAlive.Add(allTanksSpawnedIn[i]);
            }
        }

        /// <summary>
        /// Resets our game and destroys the current tanks
        /// </summary>
        private void ClearAndResetAllSpawns()
        {
            startingAllPossibleSpawnPoints.Clear(); // clear our starting spawn points
                                                    // get a new copy of all the possible spawn points
            for (int i = 0; i < allPossibleSpawnPoints.Count; i++)
            {
                startingAllPossibleSpawnPoints.Add(allPossibleSpawnPoints[i]); // do a hard copy, and copy across all the possible spawn points to our private list
            }
        }

        private void ResetTankPositions()
        {

            // we are good to go
            for (int i = 0; i < AllTanksSpawnedIn.Count; i++)
            {
                // checking if I have enough unique prefabs so I can spawn different tanks
                // spawn in a tank prefab, at a random spawn point
                Transform tempSpawnPoint = startingAllPossibleSpawnPoints[Random.Range(0, startingAllPossibleSpawnPoints.Count)]; // getting a random spawn point
                AllTanksSpawnedIn[i].transform.position = tempSpawnPoint.position;

                startingAllPossibleSpawnPoints.Remove(tempSpawnPoint); // remove the temp spawn point from our possible spawn point list
            }
        }

        public void SpawnTanks(int NumberToSpawn)
        {
            ClearAndResetAllSpawns();
            if (allPossibleSpawnPoints.Count >= NumberToSpawn)
            {
                // we are good to go
                for (int i = 0; i < NumberToSpawn; i++)
                {
                    // checking if I have enough unique prefabs so I can spawn different tanks
                    // spawn in a tank prefab, at a random spawn point
                    Transform tempSpawnPoint = startingAllPossibleSpawnPoints[Random.Range(0, startingAllPossibleSpawnPoints.Count)]; // getting a random spawn point
                    GameObject clone = Instantiate(tankPrefab, tempSpawnPoint.position, tankPrefab.transform.rotation);
                    startingAllPossibleSpawnPoints.Remove(tempSpawnPoint); // remove the temp spawn point from our possible spawn point list
                    allTanksSpawnedIn.Add(clone); // keep track of the tank we just spawned in
                    allTanksAlive.Add(clone);
                    // set the player number
                    clone.GetComponent<Tank>().CurrentPlayerNumber = (PlayerNumber)(i + 1);
                }
            }
        }
    }
}
