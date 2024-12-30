using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Fighting
{
    [Preserve]
    public unsafe class CharacterSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        public void OnPlayerAdded(Frame frame, PlayerRef player, bool firstTime)
        {
            {
                RuntimePlayer data = frame.GetPlayerData(player);
                
                // resolve the reference to the avatar prototype.
                var entityPrototypAsset = frame.FindAsset(data.PlayerAvatar);
                
                // Create a new entity for the player based on the prototype.
                var characterEntity = frame.Create(entityPrototypAsset);
                
                // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
                frame.Add(characterEntity, new PlayerLink { PlayerRef = player });
                
                var config = frame.FindAsset(frame.RuntimeConfig.GameConfig);
                
                frame.Global->PunchRecoveryMaxTime = config.PunchRecoveryTime;
                frame.Global->PunchAnimationRecoveryMaxTime = config.PunchAnimationRecoveryTime;
                
                Transform3D* transform3D = frame.Unsafe.GetPointer<Transform3D>(characterEntity);
                PlayerCharacter* character = frame.Unsafe.GetPointer<PlayerCharacter>(characterEntity);
                
                frame.Global->CurrentPlayerCount++;
                character->PlayerHP = config.MaxHP;
                character->PlayerNumber = frame.Global->CurrentPlayerCount;
                
                if (character->PlayerNumber == 1)
                {
                    transform3D->Position = new FPVector3(FP._0, FP._0_01, -FP._1_50);
                    frame.Events.PlayerAdd(1);
                }else if (character->PlayerNumber == 2)
                {
                    transform3D->Position = new FPVector3(FP._0, FP._0_01, FP._1_50);
                    transform3D->Rotation = FPQuaternion.Euler(FP._0, FP._180, FP._0);
                    
                    frame.Global->IsGameStart = true;
                    frame.Events.PlayerAdd(1);
                    frame.Events.PlayerAdd(2);
                }
            }
        }
    }
}