using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
   [SerializeField] private AudioClip menuTrack;
   [SerializeField]private AudioClip stepSound;
   [SerializeField] private AudioClip stepRightSound;
   [SerializeField] private AudioClip victorySound;
   [SerializeField] private AudioClip failSound;
   [SerializeField] private AudioClip rotateAxis;
   [SerializeField] private AudioClip playButton;
   [SerializeField] private AudioClip buttonOpen;

   private AudioSource _audioSource;

   public AudioSource AudioSource => _audioSource;

   private void OnEnable()
   {
      DontDestroyOnLoad(this.gameObject);
      _audioSource = GetComponent<AudioSource>();
   }

   public AudioClip PlayButton => playButton;
   public AudioClip StepSound => stepSound;

   public AudioClip StepRightSound => stepRightSound;

   public AudioClip VictorySound => victorySound;

   public AudioClip FailSound => failSound;

   public AudioClip MenuTrack => menuTrack;

   public AudioClip RotateAxis => rotateAxis;

   public AudioClip ButtonOpen => buttonOpen;

   public void Mute(bool muted)
   {
      if (muted)
      {
         _audioSource.mute = false;
      }
      else
      {
         _audioSource.mute = true;
      }
      
   }
}
