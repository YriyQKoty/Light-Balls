using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        private Axes _initialGlobalAxes;
        private Axes _playerGlobalAxes;
        private Transform _transform;
        private float offset = 0.5f;

        private Direction _xLocalDirection;
        private Direction _yLocalDirection;
 
        public Axes PlayerGlobalAxes => _playerGlobalAxes;

        private void Awake()
        {
            _transform = transform;
        }

        public void Rotate(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        public void SetInitialAxes(Axes newAxes)
        {
            _initialGlobalAxes = newAxes;
            _playerGlobalAxes = new Axes(newAxes.AxesDictionary[Direction.Up], newAxes.AxesDictionary[Direction.Right]);

            DetermineLocalDirections();
        }

        void DetermineLocalDirections()
        {
            if (_transform.rotation.z == 0)
            {
                _xLocalDirection = Direction.Right;
                _yLocalDirection = Direction.Up;
            }
            else if (Math.Abs(_transform.rotation.z - Quaternion.Euler(0,0,90).z ) < 0.1f)
            {
                _xLocalDirection = Direction.Up;
                _yLocalDirection = Direction.Left;
            }
            else if (Math.Abs(_transform.rotation.z - Quaternion.Euler(0,0,-90).z ) < 0.1f)
            {
                _xLocalDirection = Direction.Down;
                _yLocalDirection = Direction.Right;
            }
            else if (Math.Abs(_transform.rotation.z - Quaternion.Euler(0,0,180).z) < 0.1f)
            {
                _xLocalDirection = Direction.Left;
                _yLocalDirection = Direction.Down;
            }
        }
        public void ChangeAxes()
        {
            Dictionary<Direction, bool> previousAxes = new Dictionary<Direction, bool>();
            foreach (var kv in _initialGlobalAxes.AxesDictionary)
            {
                previousAxes.Add(kv.Key, kv.Value);
            }

            _initialGlobalAxes.AxesDictionary[Direction.Up] = _initialGlobalAxes.AxesDictionary[Direction.Left];
            _initialGlobalAxes.AxesDictionary[Direction.Right] = previousAxes[Direction.Up];
            _initialGlobalAxes.AxesDictionary[Direction.Down] = previousAxes[Direction.Right];
            _initialGlobalAxes.AxesDictionary[Direction.Left] = previousAxes[Direction.Down];

            _playerGlobalAxes = new Axes(_initialGlobalAxes.AxesDictionary[Direction.Up], _initialGlobalAxes.AxesDictionary[Direction.Right]);

            RotateСlockwise();
            
            DetermineLocalDirections();
        }

        void RotateСlockwise()
        {
            var prevEuler = _transform.eulerAngles;
            _transform.eulerAngles =
                new Vector3(prevEuler.x, prevEuler.y, prevEuler.z - (int)(Mathf.PI/2 * Mathf.Rad2Deg));
        }

        public bool CanMoveVertically => _playerGlobalAxes.AxesDictionary[Direction.Up] ||
                                         _playerGlobalAxes.AxesDictionary[Direction.Down];
        
        public bool CanMoveHorizontally => _playerGlobalAxes.AxesDictionary[Direction.Right] ||
                                         _playerGlobalAxes.AxesDictionary[Direction.Left];
        
        public void MoveVertical()
        {
            if (_playerGlobalAxes.AxesDictionary[Direction.Up])
            {
                var position = _transform.position;
                _transform.position = new Vector2(position.x, position.y + offset);
            }
            else if (_playerGlobalAxes.AxesDictionary[Direction.Down])
            {
                var position = _transform.position;
                _transform.position = new Vector2(position.x, position.y - offset);
            }
        }

        public void MoveHorizontal()
        {
            if (_playerGlobalAxes.AxesDictionary[Direction.Right])
            {
                var position = _transform.position;
                _transform.position = new Vector2(position.x + offset, position.y);
            }
            else if (_playerGlobalAxes.AxesDictionary[Direction.Left])
            {
                var position = _transform.position;
                _transform.position = new Vector2(position.x - offset, position.y);
            }
        }

        private void FixedUpdate()
        {
            RaycastHit2D hitRight = Physics2D.Raycast(_transform.position,
               _transform.right, offset, LayerMask.GetMask("Wall")); 
            RaycastHit2D hitUp = Physics2D.Raycast(_transform.position,
                _transform.up, offset, LayerMask.GetMask("Wall")); 
            
            Debug.DrawRay(_transform.position,
                _transform.right);
            Debug.DrawRay(_transform.position,
                _transform.up);
            
            if (hitRight.collider)
            {
                if (_playerGlobalAxes.AxesDictionary[_xLocalDirection])
                {
                    _playerGlobalAxes.AxesDictionary[_xLocalDirection] = false;
                }
            }
            else
            {
                if (!_playerGlobalAxes.AxesDictionary[_xLocalDirection])
                {
                    _playerGlobalAxes.AxesDictionary[_xLocalDirection] = true;
                }
            }
            
            
            if (hitUp.collider)
            {
                if (_playerGlobalAxes.AxesDictionary[_yLocalDirection])
                {
                    _playerGlobalAxes.AxesDictionary[_yLocalDirection] = false;
                }
            }
            else
            {
                if (!_playerGlobalAxes.AxesDictionary[_yLocalDirection])
                {
                    _playerGlobalAxes.AxesDictionary[_yLocalDirection] = true;
                }
            }
        }
    }


}