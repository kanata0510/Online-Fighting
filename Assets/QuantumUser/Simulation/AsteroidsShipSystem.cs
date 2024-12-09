using Photon.Deterministic;
using UnityEngine.Scripting;
using Quantum;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class AsteroidsShipSystem : SystemMainThreadFilter<AsteroidsShipSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PhysicsBody3D* Body;
            public AsteroidsShip* AsteroidsShip;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            Input* input = default;
            if(f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }

            UpdateShipMovement(f, ref filter, input);
        }
        
        private void UpdateShipMovement(Frame f, ref Filter filter, Input* input)
        {
            FP shipAcceleration = 7;
            FP turnSpeed = 8;

            if (input->Up)
            {
                filter.Body->AddForce(filter.Transform->Up * shipAcceleration);
            }

            if (input->Left)
            {
                filter.Body->AddTorque(new FPVector3(turnSpeed, 0, 0));
            }

            if (input->Right)
            {
                filter.Body->AddTorque(new FPVector3(turnSpeed, 0, 0));
            }

            //filter.Body->AngularVelocity = FPMath.Clamp(filter.Body->AngularVelocity, -turnSpeed, turnSpeed);
        }
    }
}