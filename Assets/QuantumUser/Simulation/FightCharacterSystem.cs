using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.Fighting
{
    [Preserve]
    public unsafe class FightCharacterSystem : SystemMainThreadFilter<FightCharacterSystem.Filter>//, ISignalOnCollisionCharacterHitPunch
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
            if (f.Global->IsGameEnd) return;
            if (f.Global->StartWaitTime > FP._0)
            {
                f.Global->StartWaitTime = f.Global->StartWaitTime - f.DeltaTime < FP._0 ? FP._0 : f.Global->StartWaitTime - f.DeltaTime;
                return;
            }
            
            Input* input = default;
            if(f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }

            UpdateCharacterMovement(f, ref filter, input);

            if (f.Global->IsGameStart && !f.Global->IsGameStartOnce)
            {
                f.Global->IsGameStartOnce = true;
                f.Global->StartWaitTime = FP._4;
                Transform3D* transform = f.Unsafe.GetPointer<Transform3D>(filter.Entity);
                transform->Teleport(f, new FPVector3(FP._0, FP._0_01, -FP._1_50));
            }
            
            UpdateCharacterPunch(f, ref filter, input);
        }

        private void UpdateCharacterMovement(Frame f, ref Filter filter, Input* input)
        {
            FP turnSpeed = 1;
            AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Backward", input->Left);
            if (input->Left)
            {
                filter.Transform->Position = new FPVector3(0, filter.Transform->Position.Y, filter.Transform->Position.Z - turnSpeed * f.DeltaTime);
            }

            AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Forward", input->Right);
            if (input->Right)
            {
                filter.Transform->Position = new FPVector3(0, filter.Transform->Position.Y, filter.Transform->Position.Z + turnSpeed * f.DeltaTime);
            }
        }
        
        private void UpdateCharacterPunch(Frame f, ref Filter filter, Input* input)
        {
            PunchRef* punch = f.Unsafe.GetPointer<PunchRef>(filter.Entity);
            PhysicsCollider3D* punchCollider = f.Unsafe.GetPointer<PhysicsCollider3D>(punch->Target);
            punch->RecoveryTime = punch->RecoveryTime - f.DeltaTime < FP._0 ? FP._0 : punch->RecoveryTime - f.DeltaTime;
            punch->AnimationRecoveryTime = punch->AnimationRecoveryTime - f.DeltaTime < FP._0
                ? FP._0
                : punch->AnimationRecoveryTime - f.DeltaTime;
            if (punch->AnimationRecoveryTime == FP._0)
            {
                if (punchCollider->Enabled)
                {
                    punchCollider->Enabled = false;
                }
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Punch", false);
            }

            if (input->Fire && punch->RecoveryTime == FP._0)
            {
                Transform3D* punchTransform = f.Unsafe.GetPointer<Transform3D>(punch->Target);
                FPVector3 punchPosition = filter.Transform->Position + filter.Transform->Forward * FP._0_50 +
                                          filter.Transform->Up * FP._1_10;
                punchTransform->Position = punchPosition;
                punchTransform->Teleport(f, punchPosition);
                if (f.Global->IsGameStart)
                {
                    punchCollider->Enabled = true;
                }
                
                punch->RecoveryTime = f.Global->PunchRecoveryMaxTime;
                punch->AnimationRecoveryTime = f.Global->PunchAnimationRecoveryMaxTime;
                AnimatorComponent.SetBoolean(f, filter.AnimatorComponent, "Punch", true);
            }
        }
    }
}