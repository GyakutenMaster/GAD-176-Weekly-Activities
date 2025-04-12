using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekEleven
{
    /// <summary>
    /// Manages the game logic, including starting, resetting, and handling the flow of rounds.
    /// </summary>
    
    // lets turn the tank game manager into a singleton.
    public class TankGameManager : Singleton<TankGameManager>
    {
        [SerializeField] protected int numberOfPlayers = 2;
        [SerializeField] protected float preGameWaitTime = 3f;
        [SerializeField] protected float timeBeforeRoundStart = 2f;

        protected Coroutine gameLogicRoutine;
        protected Coroutine restartLogicRoutine;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            StartGame();
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
            if (restartLogicRoutine == null)
            {
                restartLogicRoutine = StartCoroutine(RestartRound());
            }
        }

        private IEnumerator RestartRound()
        {
            // Wait for a short duration, reset the round, and then proceed to the next round
            yield return new WaitForSeconds(1);

            // lets access the tanke spawn manager instance and call reset round.
            TankSpawnManager.Instance.ResetRound();

            //  invoke the event to disable player movement
            GameEvents.EnablePlayerMovementEvent?.Invoke(false);

            yield return new WaitForSeconds(timeBeforeRoundStart);
            BeginRound();
            restartLogicRoutine = null;
        }

        private void BeginRound()
        {
            // Enable tank movement at the beginning of a round by invoking our enable player movement event and passing in true.
            GameEvents.EnablePlayerMovementEvent?.Invoke(true);

            // lets access the tank ui instance and call the in game function 
            TankUIManager.Instance.InGame();
        }


        public void PlayerHasWon(GameObject winner)
        {
            // Handle the scenario when a player has won the round
            if (winner.GetComponent<Tank>() != null)
            {
                winner.GetComponent<Tank>().PlayerScore++;
            }

            // lets access the tank ui manaager instance and call the post round function
            TankUIManager.Instance.PostRound(winner);

            ResetRound();
        }

        /// <summary>
        /// Custom update function to control when and where updates occur in the game logic.
        /// </summary>
        /// <returns></returns>
        private IEnumerator GameLogic()
        {
            // lets call the tank ui manager instance and call the set up function.
            TankUIManager.Instance.Setup(numberOfPlayers);

            // lets call the pre game function from our tank ui manager instance.
            TankUIManager.Instance.PreGame();


            // lets access thet ank spawn manager instance and call the spawn tanks function.
            TankSpawnManager.Instance.SpawnTanks(numberOfPlayers);

            // lastly lets access the tank camera controller instance and call OnGameStart function
            TankCameraController.Instance.OnGameStart();

            yield return new WaitForSeconds(preGameWaitTime);

        
            BeginRound();

            yield return null; // Signal the coroutine when the next "frame/update" should occur
        }
    }
}
