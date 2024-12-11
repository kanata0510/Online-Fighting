using System;
using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Asteroids
{
    public class FightGameConfig: AssetObject
    {
        public AssetRef<EntityPrototype> PunchColliderPrototype;
        public FP PunchRecoveryTime = FP._1;
        public FP PunchAnimationRecoveryTime = FP._1;
        public FP PunchPower = FP._1;
        public Int32 MaxHP = 100;
        public Int32 PunchDamage = 40;
    }
}