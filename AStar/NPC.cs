using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class NPC : Counter
    {
        private Vector2D _start;
        private Vector2D _end;
        private bool _forward = true;
        private bool _back = false;
        public int[,] NPCGrid;

        public NPC(Vector2D start, Vector2D end, int[,] grid)
        {
            _start = start;
            _end = end;
            _currentCoord = start;
            _currentPosition.x = (float)(start.x * 50 + 12.5);
            _currentPosition.y = (float)(start.y * 50 + 12.5);
            NPCGrid = grid;
        }

        public void UpdateNPC()
        {/*
            if (_currentCoord.x == 1 && _currentCoord.y == 1 && !_back && _forward)
            {
                PathFinding((int)2, (int)2, NPCGrid);
                _forward = false;
                _back = true;
            }
            else if (_currentCoord.x == 2 && _currentCoord.y == 2 && _back && !_forward)
            {
                PathFinding((int)1, (int)1, tempGrid);
                _back = false;
                _forward = true;
            }

            base.Update();*/
        }
    }
}
