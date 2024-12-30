using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Fighting
{
    [Preserve]
    public unsafe class PunchSystem : SystemMainThreadFilter<PunchSystem.Filter>, ISignalPlayerPunch
    {
        public struct Filter
        {
            public EntityRef Entity;
            public PunchRef* PunchRef;
        }
        
        public override void Update(Frame f, ref Filter filter)
        {
            filter.PunchRef->DestroyTime -= f.DeltaTime;
            if (filter.PunchRef->DestroyTime <= 0)
            {
                f.Destroy(filter.Entity);
            }
        }
        
        public void PlayerPunch(Frame f, int playerNumber, FPVector3 spawnPosition, PunchSpecAsset punchSpecAsset)
        {
            var hits = f.Physics3D.OverlapShape(spawnPosition, FPQuaternion.Identity, punchSpecAsset.AttackShape.CreateShape(f), punchSpecAsset.AttackLayers,
                QueryOptions.ComputeDetailedInfo | QueryOptions.HitKinematics | QueryOptions.HitDynamics);
            
            if (hits.Count == 0)
            {
                return;
            }

            for (var i = 0; i < hits.Count; i++)
            {
                var target = hits[i].Entity;
                if (!f.Has<PlayerCharacter>(target)) continue;

                var character = f.Unsafe.GetPointer<PlayerCharacter>(target);
                // Chara Hit Punch
                if (playerNumber == character->PlayerNumber) continue;
                if (!f.Has<PhysicsBody3D>(target)) continue;
                if (!f.Has<Transform3D>(target)) continue;

                PhysicsBody3D* physicsBody3D = f.Unsafe.GetPointer<PhysicsBody3D>(target);
                Transform3D* transform = f.Unsafe.GetPointer<Transform3D>(target);

                var config = f.FindAsset(f.RuntimeConfig.GameConfig);
                physicsBody3D->AddLinearImpulse(transform->Back * config.PunchPower);
                character->PlayerHP -= config.PunchDamage;
                f.Events.Damage(character->PlayerNumber, character->PlayerHP, config.MaxHP);
                if (character->PlayerHP <= FP._0)
                {
                    f.Global->IsGameEnd = true;
                    f.Events.GameEnd(character->PlayerNumber);
                    return;
                }
            }
        }
    }
}
