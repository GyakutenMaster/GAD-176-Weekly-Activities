using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekNine.Completed
{
    public class TankUIManager : MonoBehaviour
    {
        [SerializeField] protected GameObject startScreen;
        [SerializeField] protected Transform inGameUIParent;
        [SerializeField] protected GameObject roundOverText;
        [SerializeField] protected InGamePlayerUI inGameUi;

        protected List<InGamePlayerUI> allPlayerUIInstances = new List<InGamePlayerUI>(); // a list of all the player ui

        private void OnEnable()
        {
            // Initialization code can be added here if needed.
        }

        private void OnDisable()
        {
            // Cleanup code can be added here if needed.
        }

        /// <summary>
        /// Set up UI for the specified number of players.
        /// </summary>
        /// <param name="numberOfPlayers">Number of players.</param>
        public void Setup(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                InGamePlayerUI newInstance = ScriptableObject.Instantiate(inGameUi);
                allPlayerUIInstances.Add(newInstance);
                newInstance.SetUp((PlayerNumber)(i + 1), inGameUIParent);
            }
        }

        /// <summary>
        /// Display pregame UI.
        /// </summary>
        public void PreGame()
        {
            startScreen.SetActive(true);
            inGameUIParent.gameObject.SetActive(false);
            roundOverText.SetActive(false);
        }

        /// <summary>
        /// Display in-game UI.
        /// </summary>
        public void InGame()
        {
            startScreen.SetActive(false);
            inGameUIParent.gameObject.SetActive(true);
            roundOverText.SetActive(false);
        }

        /// <summary>
        /// Update the score for a player.
        /// </summary>
        /// <param name="playerNumber">Player number.</param>
        /// <param name="score">Player's score.</param>
        public void UpdateScore(PlayerNumber playerNumber, int score)
        {
            for (int i = 0; i < allPlayerUIInstances.Count; i++)
            {
                allPlayerUIInstances[i].SetPlayerText(playerNumber, score);
            }
        }

        /// <summary>
        /// Called when the round is over.
        /// </summary>
        /// <param name="playerNumber">Player number who wins the round.</param>
        public void PostRound(PlayerNumber playerNumber)
        {
            startScreen.SetActive(false);
            inGameUIParent.gameObject.SetActive(false);
            roundOverText.SetActive(true);
            roundOverText.GetComponentInChildren<Text>().text = "Player " + playerNumber.ToString() + " Wins!";
        }
    }
}
