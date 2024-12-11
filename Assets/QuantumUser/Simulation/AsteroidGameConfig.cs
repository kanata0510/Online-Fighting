using System;
using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Asteroids
{
    public class AsteroidsGameConfig: AssetObject
    {
        [Header("Asteroids configuration")]
        [Tooltip("Prototype reference to spawn asteroids")]
        public AssetRef<EntityPrototype> AsteroidPrototype;
        [Tooltip("Speed applied to the asteroid when spawned")]
        public FP PunchRecoveryTime = FP._1;
        public FP PunchAnimationRecoveryTime = FP._1;
        public FP PunchPower = FP._1;
        public Int32 MaxHP = 100;
        public Int32 PunchDamage = 40;
    }
}