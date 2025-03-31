using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekNine.Completed
{
    /// <summary>
    /// Manages the game logic, including starting, resetting, and handling the flow of rounds.
    /// </summary>
    public class TankGameManager : MonoBehaviour
    {
        [SerializeField] protected int numberOfPlayers = 2;
        [SerializeField] protected float preGameWaitTime = 3f;
        [SerializeField] protected float timeBeforeRoundStart = 2f;

        protected Coroutine gameLogicRoutine;
        protected Coroutine restartLogicRoutine;

        protected TankSpawnManager spawnManager;
        protected TankUIManager uiManager;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        // Start is called before the first frame update
        void Awake()
        {
            FindReferences();
            StartGame();
        }

        void FindReferences()
        {
            // Find references to required components
            spawnManager = FindObjectOfType<TankSpawnManager>();
            uiManager = FindObjectOfType<TankUIManager>();
        }

        private void StartGame()
        {
            // Start the initial game logic routine
            if (gameLogicRoutine == null)
            {
                gameLogicRoutine = StartCoroutine(GameLogic());
            }
        }

        /// <summary>
        /// Called when the round needs to be reset, introducing a delay before the round starts.
        /// </summary>
        private void ResetRound()
        {
            // Disable tank movement during the reset
            EnableTankMovement(false);

            if (restartLogicRoutine == null)
            {
                restartLogicRoutine = StartCoroutine(RestartRound());
            }
        }

        private IEnumerator RestartRound()
        {
            // Wait for a short duration, reset the round, and then proceed to the next round
            yield return new WaitForSeconds(1);
            if (spawnManager)
            {
                spawnManager.ResetRound();
            }
            yield return new WaitForSeconds(timeBeforeRoundStart);
            BeginRound();
            restartLogicRoutine = null;
        }

        private void BeginRound()
        {
            // Enable tank movement at the beginning of a round
            EnableTankMovement(true);
            if (uiManager)
            {
                uiManager.InGame();
            }
        }

        private void EnableTankMovement(bool enabled)
        {
            // Enable or disable tank movement based on the provided flag
            if (spawnManager)
            {
                List<GameObject> allTanksSpawned = spawnManager.AllTanksSpawnedIn;
                for (int i = 0; i < allTanksSpawned.Count; i++)
                {
                    allTanksSpawned[i].GetComponent<Tank>().EnableInput(enabled);
                }
            }
        }

        public void PlayerHasWon(GameObject winner)
        {
            // Handle the scenario when a player has won the round
            if (winner.GetComponent<Tank>() != null)
            {
                winner.GetComponent<Tank>().PlayerScore++;
            }
            if (uiManager)
            {
                uiManager.PostRound(winner.GetComponent<Tank>().CurrentPlayerNumber);
            }
            ResetRound();
        }

        /// <summary>
        /// Custom update function to control when and where updates occur in the game logic.
        /// </summary>
        /// <returns></returns>
        private IEnumerator GameLogic()
        {
            // Reset the game
            // Start pre-game setup
            if (uiManager)
            {
                uiManager.PreGame();
            }
            // Spawn the tanks
            if (spawnManager)
            {
                spawnManager.ClearAndResetAllSpawns();
                spawnManager.SpawnTanks(numberOfPlayers);
            }
            if (uiManager)
            {
                uiManager.Setup(numberOfPlayers);
            }
            yield return new WaitForSeconds(preGameWaitTime);
            // Start the game
            if (uiManager)
            {
                uiManager.InGame();
            }
            BeginRound();
            yield return null; // Signal the coroutine when the next "frame/update" should occur
        }
    }
}
