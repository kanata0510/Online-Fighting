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
                var entityPrototypAsset = frame.FindAsset<EntityPrototype>(data.PlayerAvatar);
                
                // Create a new entity for the player based on the prototype.
                var characterEntity = frame.Create(entityPrototypAsset);
                Debug.Log(player);
                // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
                frame.Add(characterEntity, new PlayerLink { PlayerRef = player });
            }
        }
    }
}