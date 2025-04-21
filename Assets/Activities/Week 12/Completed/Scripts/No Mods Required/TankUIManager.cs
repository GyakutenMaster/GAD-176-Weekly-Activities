using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekTwelve.Completed
{
    public class TankUIManager : Singleton<TankUIManager>
    {
        protected GameObject startScreen;
        protected Transform inGameUIParent;
        protected GameObject roundOverText;
        protected InGamePlayerUI inGameUi;
        protected List<InGamePlayerUI> allPlayerUIInstances = new List<InGamePlayerUI>(); // a list of all the player ui


        /// <summary>
        /// Set up UI for the specified number of players.
        /// </summary>
        /// <param name="numberOfPlayers">Number of players.</param>
        public void Setup(int numberOfPlayers)
        {

            if(startScreen == null)
            {
                startScreen = GameData.Instance.StartScreen;
            }
            if(inGameUi == null)
            {
                inGameUi = GameData.Instance.InGameUi;
            }

            if(roundOverText == null)
            {
                roundOverText = GameData.Instance.RoundOverText;
            }

            if(inGameUIParent == null)
            {
                inGameUIParent = GameData.Instance.InGameUIParent;
            }

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
        public void PostRound(GameObject winningPlayer)
        {
            startScreen.SetActive(false);
            inGameUIParent.gameObject.SetActive(false);
            roundOverText.SetActive(true);
            if (winningPlayer.GetComponent<Tank>())
            {
                roundOverText.GetComponentInChildren<Text>().text = "Player " + winningPlayer.GetComponent<Tank>().CurrentPlayerNumber.ToString() + " Wins!";
            }
        }
    }
}
