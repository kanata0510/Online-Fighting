
using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class CharacterCollisionSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D
    {
        public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            // Projectile is colliding with something
            if (f.Unsafe.TryGetPointer<Punch>(info.Entity, out var punch1))
            {
                if (f.Unsafe.TryGetPointer<PlayerCharacter>(info.Other, out var character1))
                {
                    // Punch Hit Chara
                    //f.Signals.OnCollisionPunchHitCharacter(info, punch1, character1);
                }
            }

            // Ship is colliding with something
            if (f.Unsafe.TryGetPointer<PlayerCharacter>(info.Entity, out var character2))
            {
                if (f.Unsafe.TryGetPointer<Punch>(info.Other, out var punch2))
                {
                    // Chara Hit Punch
                    f.Signals.OnCollisionCharacterHitPunch(info, character2, punch2);
                }
            }
        }
    }
}