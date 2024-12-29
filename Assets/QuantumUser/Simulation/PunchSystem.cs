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
            Debug.Log(hits.Count);
            Debug.Log("playerNumber : "+playerNumber);
            if (hits.Count == 0)
            {
                return;
            }
            
            for (var i = 0; i < hits.Count; i++)
            {
                var target = hits[i].Entity;
                if (f.Unsafe.TryGetPointer<PlayerCharacter>(target, out var character))
                {
                    Debug.Log("character : " + character->PlayerNumber);
                    // Chara Hit Punch
                    if (playerNumber == character->PlayerNumber) continue;
                    
                    if (f.Unsafe.TryGetPointer(target, out PhysicsBody3D* physicsBody3D))
                    {
                        if (f.Unsafe.TryGetPointer(target, out Transform3D* transform))
                        {
                            var config = f.FindAsset(f.RuntimeConfig.GameConfig);
                            physicsBody3D->AddLinearImpulse(transform->Back * config.PunchPower);
                            character->PlayerHP -= config.PunchDamage;
                            f.Events.Damage(character->PlayerNumber, character->PlayerHP, config.MaxHP);
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
    }
}
