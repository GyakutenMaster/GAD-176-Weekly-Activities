using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwelve
{
    public class GameData : Singleton<GameData>
    {
        [Header("Camera")]
        [SerializeField] protected GameObject cameraPrefab;
        public GameObject CameraPrefab { get { return cameraPrefab; } }

        // UI Related
        [Header("User Interface")]
        [SerializeField] protected GameObject startScreen;
        public GameObject StartScreen { get { return startScreen; } }

        [SerializeField] protected Transform inGameUIParent;
        public Transform InGameUIParent { get {  return inGameUIParent; } }

        [SerializeField] protected GameObject roundOverText;
        public GameObject RoundOverText { get {  return roundOverText; } }

        [SerializeField] protected InGamePlayerUI inGameUi;
        public InGamePlayerUI InGameUi { get {  return inGameUi; } }


        [Header("Player")]
        [SerializeField] protected GameObject tankPrefab;
        public GameObject PlayerPrefab { get { return tankPrefab; } }


        public void Awake()
        {
            if(TankGameManager.Instance == null)
            {
                Debug.Log("Creating Game Manager Instance");
            }
        }

    }
}
