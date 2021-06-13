using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphicsController : MonoBehaviour
{
    [SerializeField] private RectTransform redAxisSprite;
    [SerializeField] private RectTransform blueAxisSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ExitController _exitController;

    [SerializeField] private GameObject PlayButton = null;
    
    
    [Header("Menu Buttons")] 
    [SerializeField] private GameObject _resume;
    [SerializeField] private GameObject _nextLevel;
    [SerializeField] private GameObject _replay;
    [SerializeField] private GameObject _menuPanel;

    public GameObject MenuPanel => _menuPanel;

    public GameObject NextLevel => _nextLevel;

    public GameObject Replay => _replay;

    private bool _paused;

    public RectTransform RedAxisSprite => redAxisSprite;

    public RectTransform BlueAxisSprite => blueAxisSprite;

    public bool Paused => _paused;
    
    private void Start()
    {
        _audioSource = AudioController.Instance.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Axes rotating
    /// </summary>
    public void RotateAxis()
    {
        RedAxisSprite.Rotate(0, 0, -90f, Space.Self);
        BlueAxisSprite.Rotate(0, 0, -90f, Space.Self);
    }

    /// <summary>
    /// Play button in main meny scene
    /// </summary>
    public void OnPlayButton()
    {
        _audioSource.PlayOneShot(AudioController.Instance.PlayButton);
    }

    private void Update()
    {
        //rotating axis
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateAxis();
        }

        //if not main menu
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //open/close pause menu
            if (!_exitController.GameFinished)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!_paused)
                    {
                        Pause();
                    }
                    else
                    {
                        Resume();
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Resumes and deactivates pause menu buttons
    /// </summary>
    public void Resume()
    {
        _paused = false;
        _resume.SetActive(false);
        _menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Pauses and activates pause menu buttons
    /// </summary>
    void Pause()
    {
        _paused = true;
        _resume.SetActive(true);
        _menuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
  

}