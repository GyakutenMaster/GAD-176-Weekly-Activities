using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAD176.WeeklyActivities.WeekTwo.Completed
{
    public class LunarLanderUIManager : MonoBehaviour
    {
        [SerializeField] private InGameUI inGameUI = new InGameUI();

        public InGameUI InGameUI
        {
            get
            {
                return inGameUI;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            InGameUI.SetUp(this);
        }
    }
}