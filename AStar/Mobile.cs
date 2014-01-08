using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AStar
{
    class Mobile
    {
        private Vector2D _currentCoord = new Vector2D();
        private Vector2D _targetCoord = new Vector2D();
        private Vector2D _nextCoord = new Vector2D();
        private Vector2D _currentPosition = new Vector2D();
        private Rectangle _icon = new Rectangle(0, 0, 25, 25);
        private List<Node> _openList = new List<Node>();
        private int _speed = 5;
        public int[,] tempGrid = new int[10, 10];
        private Node _path;
        private bool _foundPath = false;

        public Mobile(int x, int y)
        {
            _currentCoord.x = x;
            _currentCoord.y = y;
            _targetCoord.x = x;
            _targetCoord.y = y;
            _currentPosition.x = (float)(x * 50 + 12.5);
            _currentPosition.y = (float)(y * 50 + 12.5);
        }

        public Mobile()
        {
            _currentCoord.x = 0;
            _currentCoord.y = 0;
            _targetCoord.x = 0;
            _targetCoord.y = 0;
            _currentPosition.x = (float)(12.5);
            _currentPosition.y = (float)(12.5);
        }

        public void Update(){
            //float x = _currentPosition.getX() + _speed;
            //_currentPosition.setX(x);
            //if (_currentCoord != _targetCoord)
            //{

            //}

            if (_foundPath)
            {
                if (_currentPosition.x != (_path.XCoord() * 50 + (float)12.5) || _currentPosition.y != (_path.YCoord() * 50 + (float)12.5))
                    MoveToCoord((int)_path.XCoord(), (int)_path.YCoord());
                else
                {
                    _currentCoord.x = _path.YCoord();
                    _currentCoord.y = _path.XCoord();

                    if (_path.HasParent())
                        _path = _path.Parent();
                    else
                    {
                        _foundPath = false;
                        _path = null;
                    }
                }
            }
        }

        public void PathFinding(int x, int y, int[,] grid)
        {
            int i;
            int j;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    tempGrid[i,j] = grid[j,i];
                }
            }

            _path = null;
            _foundPath = false;

            _targetCoord.x = x;
            _targetCoord.y = y;

            _openList.Add(new Node(_targetCoord.x, _targetCoord.y));
            
            AddAdjacentNodesToOpenList(_openList[0]);
            
            _openList.Clear();
        }

        public void AddAdjacentNodesToOpenList(Node node)
        {
            int x = node.XCoord();
            int y = node.YCoord();
            _openList.Remove(node);
            tempGrid[x, y] = 3;

            if (x == (int)_currentCoord.y && y == (int)_currentCoord.x)
            {
                _path = node;
                _foundPath = true;
            }

            if (!_foundPath)
            {
                CheckAdjacentTile((x + 1), y, node);
                CheckAdjacentTile((x - 1), y, node);
                CheckAdjacentTile(x, (y + 1), node);
                CheckAdjacentTile(x, (y - 1), node);

                Node next = _openList[0];
                for (int i = 1; i > _openList.Count; i++)
                {
                    if (next.F() > _openList[i].F())
                        next = _openList[i];
                }

                AddAdjacentNodesToOpenList(next);
            }
        }

        public void CheckAdjacentTile(int x, int y, Node node)
        {
            int heuristic;

            if (tempGrid[x, y] < 1)
            {
                heuristic = (Math.Abs((int)_currentCoord.y - x) + Math.Abs((int)_currentCoord.x - y)) * 10;
                Node nextNewNode = new Node(node, x, y, heuristic);

                if (tempGrid[x, y] == -1)
                {
                    for (int i = 0; i > _openList.Count; i++)
                    {
                        if (_openList[i].XCoord() == x && _openList[i].YCoord() == y)
                        {
                            if (_openList[i].G() <= nextNewNode.G())
                                break;
                            else
                                _openList[i] = nextNewNode;
                        }

                    }
                }
                else
                {
                    _openList.Add(nextNewNode);
                    tempGrid[x, y] = -1;
                }
            }
        }

        public void MoveToCoord(int x, int y)
        {
            float xcoord = _currentPosition.x;
            float ycoord = _currentPosition.y;
            
            if (xcoord < (x * 50 + (float)12.5))
            {
                _currentPosition.x = xcoord + _speed;
            }
            else if (xcoord > (x * 50 + (float)12.5))
            {
                _currentPosition.x = xcoord - _speed;
            }

            if (ycoord < (y * 50 + (float)12.5))
            {
                _currentPosition.y = ycoord + _speed;
            }
            else if (ycoord > (y * 50 + (float)12.5))
            {
                _currentPosition.y = ycoord - _speed;
            }
        }

        public Rectangle Draw(Vector2D mapOffset)
        {
            int x = (int)_currentPosition.x + (int)mapOffset.x;
            int y = (int)_currentPosition.y + (int)mapOffset.y;
            _icon = new Rectangle(x, y, 25, 25);
            return _icon;
        }
    }
}