using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private GameObject[] blocks;

        private bool blocked = true;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (blocked)
            {
                blocked = false;
                AudioController.Instance.AudioSource.PlayOneShot(AudioController.Instance.ButtonOpen);
                foreach (var block in blocks)
                {
                    Destroy(block);
                }
            }
           
        }
    }
}