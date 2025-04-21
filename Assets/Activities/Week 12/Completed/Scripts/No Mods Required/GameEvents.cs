using GAD176.WeeklyActivities.WeekNine.Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD176.WeeklyActivities.WeekTwelve.Completed
{
    public static class GameEvents
    {
        public delegate void DelegateNoParam();
        public delegate void DelegateOneParam<t1>(t1 param1);
        public delegate void DelegateTwoParam<t1, t2>(t1 param1, t2 param2);
        public delegate void DelegateThreeParam<t1, t2, t3>(t1 param1, t2 param2, t3 param3);


        public static DelegateOneParam<bool> EnablePlayerMovementEvent;
        public static DelegateTwoParam<Transform, float> OnTakeDamageEvent;
    }
}
