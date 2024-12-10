using Photon.Deterministic;
using UnityEngine.Scripting;
using Quantum;
using UnityEngine;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class FightCharacterSystem : SystemMainThreadFilter<FightCharacterSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PhysicsBody3D* Body;
            public PlayerCharacter* PlayerCharacter;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            Input* input = default;
            if(f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                Debug.Log(*playerLink);
                Debug.Log(playerLink->PlayerRef);
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }
            
            UpdateCharacterMovement(f, ref filter, input);
        }
        
        private void UpdateCharacterMovement(Frame f, ref Filter filter, Input* input)
        {
            FP shipAcceleration = 1;
            FP turnSpeed = 1;

            if (input->Up)
            {
                filter.Body->AddForce(filter.Transform->Up * shipAcceleration);
            }

            if (input->Left)
            {
                filter.Transform->Teleport(f, filter.Transform->Position + filter.Transform->Left * turnSpeed * f.DeltaTime);
            }

            if (input->Right)
            {
                filter.Transform->Teleport(f, filter.Transform->Position + filter.Transform->Right * turnSpeed * f.DeltaTime);
            }
        }
    }
}