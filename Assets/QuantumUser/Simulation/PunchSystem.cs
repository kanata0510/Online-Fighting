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
        
        public void PlayerPunch(Frame f, EntityRef owner, FPVector3 spawnPosition, AssetRef<EntityPrototype> punchPrototype)
        {
            var punchEntity = f.Create(punchPrototype);
            
            PunchRef* punchRef = f.Unsafe.GetPointer<PunchRef>(punchEntity);
            PlayerCharacter* playerCharacter = f.Unsafe.GetPointer<PlayerCharacter>(owner);
            punchRef->PlayerNumber = playerCharacter->PlayerNumber;
            
            PunchRef* punch = f.Unsafe.GetPointer<PunchRef>(punchEntity);
            punch->DestroyTime = f.Global->PunchDestroyTime;
            
            Transform3D* punchTransform = f.Unsafe.GetPointer<Transform3D>(punchEntity);
            punchTransform->Position = spawnPosition;
            punchTransform->Teleport(f, spawnPosition);
        }
    }
}
