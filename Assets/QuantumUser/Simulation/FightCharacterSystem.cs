using Photon.Deterministic;
using UnityEngine.Scripting;
using UnityEngine;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class FightCharacterSystem : SystemMainThreadFilter<FightCharacterSystem.Filter>, ISignalOnCollisionCharacterHitPunch, ISignalOnCollisionPunchHitCharacter
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PhysicsBody3D* Body;
            public PhysicsCollider3D* PhysicsCollider3D;
            public AnimatorComponent* AnimatorComponent;
            public PlayerCharacter* PlayerCharacter;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            Input* input = default;
            if(f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }

            UpdateCharacterMovement(f, ref filter, input);
        }
        
        private void UpdateCharacterMovement(Frame f, ref Filter filter, Input* input)
        {
            FP turnSpeed = 1;
            if (f.Unsafe.TryGetPointer(filter.Entity, out PunchRef* p))
            {
                PhysicsCollider3D* punchCollider = f.Unsafe.GetPointer<PhysicsCollider3D>(p->Target);
                if (punchCollider->Enabled)
                {
                    punchCollider->Enabled = false;
                }
            }

            if (f.Unsafe.TryGetPointer(filter.Entity, out PunchRef* punch) && punch->RecoveryTime == FP._0 && input->Fire)
            {
                Transform3D* punchTransform = f.Unsafe.GetPointer<Transform3D>(punch->Target);
                punchTransform->Teleport(f,
                    filter.Transform->Position + filter.Transform->Forward * FP._0_50 +
                    filter.Transform->Up * FP._1_10);
                PhysicsCollider3D* punchCollider = f.Unsafe.GetPointer<PhysicsCollider3D>(punch->Target);
                punchCollider->Enabled = true;
                punch->RecoveryTime = f.Global->PunchRecoveryMaxTime;
                punch->AnimationRecoveryTime = f.Global->PunchAnimationRecoveryMaxTime;
                Debug.Log("Punch");
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Punch", true);
                PunchRecovery(f, filter);
                return;
            }

            if (input->Left)
            {
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Backward", true);
                filter.Transform->Position += FPVector3.Back * turnSpeed * f.DeltaTime;
            }
            else
            {
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Backward", false);
            }
            if (input->Right)
            {
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Forward", true);
                filter.Transform->Position += FPVector3.Forward * turnSpeed * f.DeltaTime;
            }
            else
            {
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Forward", false);
            }
            PunchRecovery(f, filter);
        }

        private void PunchRecovery(Frame f, Filter filter)
        {
            if (f.Unsafe.TryGetPointer(filter.Entity, out PunchRef* p))
            {
                if (p->RecoveryTime > 0)
                {
                    p->RecoveryTime = p->RecoveryTime - f.DeltaTime < FP._0 ? FP._0 : p->RecoveryTime - f.DeltaTime;
                }
                if (p->AnimationRecoveryTime > 0)
                {
                    p->AnimationRecoveryTime = p->AnimationRecoveryTime - f.DeltaTime < FP._0 ? FP._0 : p->AnimationRecoveryTime - f.DeltaTime;
                }
                else
                {
                    AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Punch", false);
                }
            }
        }
        
        public void OnCollisionCharacterHitPunch(Frame f, TriggerInfo3D info, PlayerCharacter* character, Punch* punch)
        {
            Debug.Log("OnCollisionCharacterHitPunch");
            var config = f.FindAsset(f.RuntimeConfig.GameConfig);
            character->PlayerHP -= config.PunchDamage;
            if (f.Unsafe.TryGetPointer(info.Entity, out PhysicsBody3D* physicsBody3D))
            {
                Transform3D* transform = f.Unsafe.GetPointer<Transform3D>(info.Entity);
                physicsBody3D->AddLinearImpulse(transform->Back * config.PunchPower);
            }
        }
        
        public void OnCollisionPunchHitCharacter(Frame f, TriggerInfo3D info, Punch* punch, PlayerCharacter* character)
        {
            Debug.Log("OnCollisionPunchHitCharacter");
        }
    }
}