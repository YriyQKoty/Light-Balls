using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player Player1;
    [SerializeField] private Player Player2;
    [SerializeField] private GraphicsController _graphicsController;
    private AudioSource _audioSource;

    [SerializeField] private ExitController _exitController;

    private Direction _deadDirection = Direction.None;

    private void Start()
    {
        _audioSource = AudioController.Instance.GetComponent<AudioSource>();
        var rotationRed = _graphicsController.RedAxisSprite.transform.rotation;
        Player1.SetInitialAxes(new Axes(rotationRed.z));
        Player1.Rotate(rotationRed);
        var rotationBlue = _graphicsController.BlueAxisSprite.transform.rotation;
        Player2.SetInitialAxes(new Axes(rotationBlue.z));
        Player2.Rotate(rotationBlue);
        DetermineDeadAxis();
    }

    void Update()
    {
        //if game not finished
        if (!_exitController.GameFinished && !_graphicsController.Paused)
        {
            //if W or S pushed and deadAxis not equal to Vertical
            if (Input.GetKeyDown(KeyCode.W) && _deadDirection != Direction.Up ||
                Input.GetKeyDown(KeyCode.S) && _deadDirection != Direction.Down)
            {
               
                if (Player1.enabled)
                {
                    Player1.MoveVertical();
                }

                if (Player2.enabled)
                {
                    Player2.MoveVertical();
                }

                //play sound 
                if (Player1.CanMoveVertically || Player2.CanMoveVertically)
                {
                    _audioSource.PlayOneShot(AudioController.Instance.StepSound);
                }
               
            } //else if A or D pushed and deadAxis not equal to Horizontal
            else if (Input.GetKeyDown(KeyCode.A) && _deadDirection != Direction.Left ||
                     Input.GetKeyDown(KeyCode.D) && _deadDirection != Direction.Right)
            {
                if (Player1.enabled)
                {
                    Player1.MoveHorizontal();
                }

                if (Player2.enabled)
                {
                    Player2.MoveHorizontal();
                }
                
                //play sound 
                if (Player1.CanMoveHorizontally || Player2.CanMoveHorizontally)
                {
                    _audioSource.PlayOneShot(AudioController.Instance.StepRightSound);
                }
            }
        }

        //Change axes rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangePlayersAxes();
        }
    }

    void DetermineDeadAxis()
    {
        _deadDirection = Player1.PlayerGlobalAxes.DetermineDeadAxis(Player2.PlayerGlobalAxes.AxesDictionary);
    }

    public void ChangePlayersAxes()
    {
        Player1.ChangeAxes();
        Player2.ChangeAxes();
        DetermineDeadAxis();
        _audioSource.PlayOneShot(AudioController.Instance.RotateAxis);
    }
}