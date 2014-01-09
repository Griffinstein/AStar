using System;
using System.Collections.Generic;
using System.Drawing;

namespace AStar
{
    class Counter
    {
        protected Vector2D _currentCoord = new Vector2D();
        protected Vector2D _targetCoord = new Vector2D();
        protected Vector2D _nextCoord = new Vector2D();
        protected Vector2D _currentPosition = new Vector2D();
        protected Rectangle _icon = new Rectangle(0, 0, 25, 25);
        protected List<Node> _openList = new List<Node>();
        protected int _speed = 5;
        public int[,] tempGrid = new int[20, 20];
        protected Node _path;
        protected bool _foundPath = false;

        public Counter(int x, int y)
        {
            _currentCoord.x = x;
            _currentCoord.y = y;
            _targetCoord.x = x;
            _targetCoord.y = y;
            _currentPosition.x = (float)(x * 50 + 12.5);
            _currentPosition.y = (float)(y * 50 + 12.5);
        }

        public Counter()
        {
            _currentCoord.x = 0;
            _currentCoord.y = 0;
            _targetCoord.x = 0;
            _targetCoord.y = 0;
            _currentPosition.x = (float)(12.5);
            _currentPosition.y = (float)(12.5);
        }

        public void Update(){
            if (_foundPath)
            {
                if (_currentPosition.x != (_path.x * 50 + (float)12.5) || _currentPosition.y != (_path.y * 50 + (float)12.5))
                    MoveToCoord((int)_path.x, (int)_path.y);
                else
                {
                    _currentCoord.x = _path.y;
                    _currentCoord.y = _path.x;

                    if (_path.HasParent)
                        _path = _path.Parent;
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

            for (i = 0; i < 20; i++)
            {
                for (j = 0; j < 20; j++)
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
            int x = node.x;
            int y = node.y;
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
                    if (next.f > _openList[i].f)
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
                        if (_openList[i].x == x && _openList[i].y == y)
                        {
                            if (_openList[i].g <= nextNewNode.g)
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