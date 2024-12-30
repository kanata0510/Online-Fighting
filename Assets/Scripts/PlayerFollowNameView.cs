using Quantum;
using UnityEngine;

public class PlayerFollowNameView : QuantumEntityViewComponent
{
    public GameObject you;
    public override void OnActivate(Frame frame)
    {
        var player = frame.Get<PlayerLink>(EntityRef).PlayerRef;
        you.transform.rotation = Quaternion.Euler(0, -90, 0);
        you.SetActive(QuantumRunner.Default.Game.PlayerIsLocal(player));
    }
}
