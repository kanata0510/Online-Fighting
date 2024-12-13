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
                if (f.Unsafe.TryGetPointer<Punch>(info.Other, out var punch))
                {
                    // Chara Hit Punch
                    f.Signals.OnCollisionCharacterHitPunch(info, character, punch);
                }
            }
        }
    }
}