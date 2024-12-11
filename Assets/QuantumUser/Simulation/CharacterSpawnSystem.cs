using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum.Asteroids
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
                var targetEntity = frame.Create(config.AsteroidPrototype);
                
                frame.Add(characterEntity, new PunchRef { Target = targetEntity });
                
                frame.Global->PunchRecoveryMaxTime = config.PunchRecoveryTime;
                frame.Global->PunchAnimationRecoveryMaxTime = config.PunchAnimationRecoveryTime;
                Transform3D* transform3D = frame.Unsafe.GetPointer<Transform3D>(characterEntity);
                if (frame.Global->PlayerCount == 0)
                {
                    frame.Global->PlayerCount = 1;
                    if (frame.Unsafe.TryGetPointer<PlayerCharacter>(characterEntity, out var character))
                    {
                        character->PlayerNumber = 1;
                        character->PlayerHP = config.MaxHP;
                    }
                    transform3D->Position = new FPVector3(FP._0, FP._0_01, -FP._1_50);
                }else if (frame.Global->PlayerCount == 1)
                {
                    frame.Global->PlayerCount = 2;
                    if (frame.Unsafe.TryGetPointer<PlayerCharacter>(characterEntity, out var character))
                    {
                        character->PlayerNumber = 2;
                        character->PlayerHP = config.MaxHP;
                    }
                    transform3D->Position = new FPVector3(FP._0, FP._0_01, FP._1_50);
                    transform3D->Rotation = FPQuaternion.Euler(FP._0, FP._180, FP._0);
                }
            }
        }
    }
}