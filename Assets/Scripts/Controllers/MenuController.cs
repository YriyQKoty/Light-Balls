using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GraphicsController _graphicsController;
    [SerializeField] private Text _loadingProgress;
    [SerializeField] private GameObject _uiPanel;
    [FormerlySerializedAs("_tutorialMap")] [SerializeField] private GameObject _map;
    private AudioSource _audioSource;

    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = AudioController.Instance.GetComponent<AudioSource>();
        }
      
        if (IsMenuScene)
        {
            PlayMusic();
        }
       
    }

    void PlayMusic()
    {
        _audioSource.clip =AudioController.Instance.MenuTrack;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void StopMusic()
    {
        _audioSource.Stop();
    }
    IEnumerator LoadAsyncSceneLevel(int index)
    {
        yield return null;
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            _loadingProgress.text = "Loading progress: " + (asyncLoad.progress * 100) + "%";

            if (asyncLoad.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
               _loadingProgress.text = "Press the space bar to continue";
               //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _audioSource.Stop();
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// Reloads current level
    /// </summary>
    public void ReloadLevel()
    {
        //turning off button for replay and menu panel
        _graphicsController.Replay.SetActive(false);
        _graphicsController.MenuPanel.SetActive(false);
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Loads main menu scene
    /// </summary>
    public void LoadMenu()
    {
        _graphicsController.MenuPanel.SetActive(false);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        //main menu
        SceneManager.LoadScene(0);
    }


    public void LoadLevel(int levelIndex)
    {
        //IF NOT MAIN MENU SCENE
        if (!IsMenuScene)
        {
            if (_graphicsController.NextLevel.activeSelf)
            {
                _graphicsController.NextLevel.SetActive(false);
            }
       
            _graphicsController.MenuPanel.SetActive(false);
        }
      
        StartCoroutine(LoadAsyncSceneLevel(levelIndex));
        _uiPanel.SetActive(false);
        _map.SetActive(false);
        _loadingProgress.gameObject.SetActive(true);
    }

    private bool IsMenuScene => SceneManager.GetActiveScene().buildIndex == 0;

}