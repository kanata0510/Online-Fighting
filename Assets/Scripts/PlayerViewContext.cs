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
            public Image coolTime;
        }
        public View[] players;
        public AudioSource audioSource;
        public AudioClip[] audioClips;

        public Animator startAnimator;
        public TextMeshProUGUI waitingText;
        public GameObject readyText;
        public GameObject fightText;
        
        public Animator endAnimator;
        public GameObject endText;
        public TextMeshProUGUI winnerText;

        public GameObject returnMenuButton;
    }
}