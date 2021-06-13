using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SoundChecker : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        private void Start()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(AudioController.Instance.Mute);
        }
    }
}