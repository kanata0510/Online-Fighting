using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Fighting
{
    [Preserve]
    public unsafe class CharacterCollisionSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D
    {
        public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            if (f.Unsafe.TryGetPointer<PlayerCharacter>(info.Entity, out var character))
            {
                Debug.Log("character : "+character->PlayerNumber);
                if (f.Unsafe.TryGetPointer<PunchRef>(info.Other, out var punch) && character->PlayerNumber != punch->PlayerNumber)
                {
                    Debug.Log("punch : "+punch->PlayerNumber);
                    // Chara Hit Punch
                    f.Signals.OnCollisionCharacterHitPunch(info, character, punch);
                }
            }
        }
    }
}