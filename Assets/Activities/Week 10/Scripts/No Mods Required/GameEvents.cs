using GAD176.WeeklyActivities.WeekNine.Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTen
{
    public static class GameEvents
    {
        public delegate void DelegateNoParam();
        public delegate void DelegateOneParam<t1>(t1 param1);
        public delegate void DelegateTwoParam<t1, t2>(t1 param1, t2 param2);
        public delegate void DelegateThreeParam<t1, t2, t3>(t1 param1, t2 param2, t3 param3);


        public static DelegateNoParam OnRoundStartEvent;
        public static DelegateNoParam ResetRoundEvent;
        public static DelegateNoParam PreGameStartEvent;

        public static DelegateOneParam<int> SpawnPlayerEvent;
        public static DelegateOneParam<bool> EnablePlayerMovementEvent;
        public static DelegateOneParam<GameObject> PlayerHasWonEvent;
        public static DelegateOneParam<GameObject> PlayerHasDiedEvent;

        public static DelegateTwoParam<Transform, float> OnTakeDamageEvent;
        public static DelegateTwoParam<PlayerNumber, int> OnPlayerScoreEvent;

    }
}
