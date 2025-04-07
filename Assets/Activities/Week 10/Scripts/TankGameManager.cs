using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTen
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

        private void OnEnable()
        {
            // subcribe our player has won event to our player has won event
            GameEvents.PlayerHasWonEvent += PlayerHasWon;
        }

        private void OnDisable()
        {
            // unsubcribe our player has won event to our player has won event
            GameEvents.PlayerHasWonEvent -= PlayerHasWon;
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

            // invoke our reset round event.
            GameEvents.ResetRoundEvent?.Invoke();

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
            // invoke the event to start the round.
            GameEvents.OnRoundStartEvent?.Invoke();
        }


        private void PlayerHasWon(GameObject winner)
        {
            // Handle the scenario when a player has won the round
            if (winner.GetComponent<Tank>() != null)
            {
                winner.GetComponent<Tank>().PlayerScore++;
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
            // invoke our pre game start event.
            GameEvents.PreGameStartEvent?.Invoke();

            //invoke our spawn player event
            GameEvents.SpawnPlayerEvent?.Invoke(numberOfPlayers);

            yield return new WaitForSeconds(preGameWaitTime);

            // invoke our begin round event
            BeginRound();

            yield return null; // Signal the coroutine when the next "frame/update" should occur
        }
    }
}
