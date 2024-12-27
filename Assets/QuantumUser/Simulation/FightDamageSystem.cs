using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Fighting
{
    [Preserve]
    public unsafe class FightDamageSystem : SystemSignalsOnly, ISignalOnCollisionCharacterHitPunch
    {
        public void OnCollisionCharacterHitPunch(Frame f, TriggerInfo3D info, PlayerCharacter* character, PunchRef* punch)
        {
            if (f.Unsafe.TryGetPointer(info.Entity, out PhysicsBody3D* physicsBody3D))
            {
                if (f.Unsafe.TryGetPointer(info.Entity, out Transform3D* transform))
                {
                    var config = f.FindAsset(f.RuntimeConfig.GameConfig);
                    physicsBody3D->AddLinearImpulse(transform->Back * config.PunchPower);
                    character->PlayerHP -= config.PunchDamage;
                    Debug.LogError($"character{character->PlayerNumber}->PlayerHP : {character->PlayerHP}");
                    f.Events.Damage(info.Entity, config.MaxHP);
                    f.Destroy(info.Other);
                    if (character->PlayerHP <= FP._0)
                    {
                        f.Global->IsGameEnd = true;
                        f.Events.GameEnd(character->PlayerNumber);
                    }
                }
            }
        }
    }
}
