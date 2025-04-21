using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekTwelve
{
    /// <summary>
    /// ScriptableObject for managing in-game UI elements specific to each player.
    /// </summary>
    [CreateAssetMenu(fileName = "Week 12", menuName = "Week 12/Player Ingame UI", order = 0)]
    public class InGamePlayerUI : ScriptableObject
    {
        [SerializeField] protected GameObject inGameUIPrefab;

        protected PlayerNumber playerReferenceNumber;
        protected Text playerText;
        protected GameObject uiInstance;

        /// <summary>
        /// Set up the in-game UI for a specific player.
        /// </summary>
        /// <param name="playerReferenceNumber">The player number to associate with this UI.</param>
        /// <param name="uiParent">The parent transform for the UI.</param>
        public void SetUp(PlayerNumber playerReferenceNumber, Transform uiParent)
        {
            this.playerReferenceNumber = playerReferenceNumber;
            uiInstance = Object.Instantiate(inGameUIPrefab, uiParent);
            playerText = uiInstance.GetComponentInChildren<Text>();
        }

        /// <summary>
        /// Disables the player text.
        /// </summary>
        public void DisableText()
        {
            playerText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Enables the player text for a specific player number.
        /// </summary>
        /// <param name="playerNumberToCheck">The player number to enable the text for.</param>
        public void EnableText(PlayerNumber playerNumberToCheck)
        {
            if (playerReferenceNumber == playerNumberToCheck)
            {
                playerText.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Sets the player text with the player number and an amount.
        /// </summary>
        /// <param name="playerNumberCheck">The player number to check against.</param>
        /// <param name="amount">The amount to display.</param>
        public void SetPlayerText(PlayerNumber playerNumberCheck, int amount)
        {
            if (playerNumberCheck == playerReferenceNumber)
            {
                playerText.text = "Player " + playerReferenceNumber.ToString() + ": " + amount;
            }
        }
    }
}
