using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private GraphicsController _graphicsController;
    private AudioSource _audioSource;
    
    private int _playersInZone;
    private float _timer = 0f;

    private bool _gameFinished = false;

    public bool GameFinished => _gameFinished;
    

    private void Start()
    {
        _audioSource = AudioController.Instance.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_timer > 2f)
        {
            OnFail();
            _timer = 0;
            _gameFinished = true;
        }
        
        if (_playersInZone == 1 && !_gameFinished)
        {
            _timer += Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _playersInZone++;
        
        DeactivatePlayer(other.gameObject);

        if (_playersInZone > 1)
        {
            _gameFinished = true;
            OnVictory();
        }
    }

    private void OnFail()
    {
        Debug.Log("You failed!");
        _audioSource.PlayOneShot(AudioController.Instance.FailSound);
        _graphicsController.Replay.SetActive(true);
        _graphicsController.MenuPanel.SetActive(true);
    }

    private void OnVictory()
    {
        Debug.Log("You won!");
        _audioSource.PlayOneShot(AudioController.Instance.VictorySound);
        _graphicsController.NextLevel.SetActive(true);
        _graphicsController.MenuPanel.SetActive(true);
    }

    private void DeactivatePlayer(GameObject gameObj)
    {
        var player = gameObj.GetComponent<Player>();

        player.enabled = false;
    }
}
