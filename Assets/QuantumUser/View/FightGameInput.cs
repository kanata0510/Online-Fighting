namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine;

    public class FightGameInput : MonoBehaviour
    {
        private void OnEnable()
        {
            QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
        }

        public void PollInput(CallbackPollInput callback)
        {
            Quantum.Input i = new Quantum.Input();

            // Note: Use GetKey() instead of GetKeyDown/Up. Quantum calculates up/down internally.
            i.Left = UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.LeftArrow);
            i.Right = UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.RightArrow);
            i.Fire = UnityEngine.Input.GetKeyDown(KeyCode.Space) || UnityEngine.Input.GetKeyDown(KeyCode.Return);
            
            callback.SetInput(i, DeterministicInputFlags.Repeatable);
        }
    }
}
