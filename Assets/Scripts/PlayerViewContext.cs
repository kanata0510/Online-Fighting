using System;
using TMPro;
using UnityEngine.UI;

namespace Quantum {
    using UnityEngine;

    public class PlayerViewContext : MonoBehaviour, IQuantumViewContext {
        [Serializable]
        public struct View
        {
            public TextMeshProUGUI nameLabel;
            public Image slider;
        }

        public View player1;
        public View player2;
    }
}