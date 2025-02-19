using System;
using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Fighting
{
    public class FightGameConfig: AssetObject
    {
        public FPAnimationCurve Curve;
        public FP PunchRecoveryTime = FP._1;
        public FP PunchAnimationRecoveryTime = FP._1;
        public FP PunchPower = FP._1;
        public Int32 MaxHP = 100;
        public Int32 PunchDamage = 30;
    }
}