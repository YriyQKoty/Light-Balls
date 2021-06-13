using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public struct Axes
    {
        private Dictionary<Direction, bool> _axesDictionary;

        public Dictionary<Direction, bool> AxesDictionary => _axesDictionary;

        public Direction DetermineDeadAxis(Dictionary<Direction, bool> secondAxes)
        {
            return _axesDictionary.Intersect(secondAxes).FirstOrDefault(a => a.Value == false).Key;
        }

        public Axes(bool up, bool right)
        {
            _axesDictionary = new Dictionary<Direction, bool>
            {
                [Direction.Up] = up, [Direction.Right] = right, [Direction.Down] = !up, [Direction.Left] = !right
            };
        }

        public Axes(float zAngle)
        {
            if (Math.Abs(zAngle- Quaternion.Euler(0,0,90).z ) < 0.1f)
            {
                this = new Axes(true, false);
            }
            else if (Math.Abs(zAngle - Quaternion.Euler(0,0,-90).z ) < 0.1f)
            {
                this = new Axes(false, true);
            }
            else if (Math.Abs(zAngle - Quaternion.Euler(0,0,180).z) < 0.1f)
            {
                this = new Axes(false, false);
            }
            else
            {
                this = new Axes(true, true);
            }
            
        }
        
        
    }

}