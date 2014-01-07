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
            _currentCoord.setX(x);
            _currentCoord.setY(y);
            _targetCoord.setX(x);
            _targetCoord.setY(y);
            _currentPosition.setX(x * 50 + (float)12.5);
            _currentPosition.setY(y * 50 + (float)12.5);
        }

        public Mobile()
        {
            _currentCoord.setX(0);
            _currentCoord.setY(0);
            _targetCoord.setX(0);
            _targetCoord.setY(0);
            _currentPosition.setX(0 * 50 + (float)12.5);
            _currentPosition.setY(0 * 50 + (float)12.5);
        }

        public void Update(){
            //float x = _currentPosition.getX() + _speed;
            //_currentPosition.setX(x);
            //if (_currentCoord != _targetCoord)
            //{

            //}

            if (_foundPath)
            {
                if (_currentPosition.getX() != (_path.XCoord() * 50 + (float)12.5) || _currentPosition.getY() != (_path.YCoord() * 50 + (float)12.5))
                    MoveToCoord((int)_path.XCoord(), (int)_path.YCoord());
                else
                {
                    _currentCoord.setX(_path.YCoord());
                    _currentCoord.setY(_path.XCoord());

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

            _targetCoord.setX(x);
            _targetCoord.setY(y);

            _openList.Add(new Node(_targetCoord.getX(), _targetCoord.getY()));
            
            AddAdjacentNodesToOpenList(_openList[0]);
            
            _openList.Clear();
        }

        public void AddAdjacentNodesToOpenList(Node node)
        {
            int x = node.XCoord();
            int y = node.YCoord();
            int heuristic;
            _openList.Remove(node);
            tempGrid[x, y] = 3;

            if (x == (int)_currentCoord.getY() && y == (int)_currentCoord.getX())
            {
                _path = node;
                _foundPath = true;
            }

            if (!_foundPath)
            {
                if (tempGrid[x + 1, y] < 1)
                {
                    heuristic = (Math.Abs((int)_currentCoord.getY() - (x + 1)) + Math.Abs((int)_currentCoord.getX() - y)) * 10;
                    Node nextNewNode = new Node(node, x + 1, y, heuristic);

                    if (tempGrid[x + 1, y] == -1)//Checks to see if this is a faster route to this node
                    {
                        for (int i = 0; i > _openList.Count; i++)
                        {
                            if (_openList[i].XCoord() == x + 1 && _openList[i].YCoord() == y)
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
                        tempGrid[x + 1, y] = -1;
                        _openList.Add(nextNewNode);
                    }
                }
                if (tempGrid[x - 1, y] < 1)
                {
                    heuristic = (Math.Abs((int)_currentCoord.getY() - (x - 1)) + Math.Abs((int)_currentCoord.getX() - y)) * 10;
                    Node nextNewNode = new Node(node, x - 1, y, heuristic);

                    if (tempGrid[x - 1, y] == -1)
                    {
                        for (int i = 0; i > _openList.Count; i++)
                        {
                            if (_openList[i].XCoord() == x - 1 && _openList[i].YCoord() == y)
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
                        tempGrid[x - 1, y] = -1;
                        _openList.Add(nextNewNode);
                    }
                }
                if (tempGrid[x, y - 1] < 1)
                {
                    heuristic = (Math.Abs((int)_currentCoord.getY() - x) + Math.Abs((int)_currentCoord.getX() - (y - 1))) * 10;
                    Node nextNewNode = new Node(node, x, y - 1, heuristic);

                    if (tempGrid[x, y - 1] == -1)
                    {
                        for (int i = 0; i > _openList.Count; i++)
                        {
                            if (_openList[i].XCoord() == x && _openList[i].YCoord() == y - 1)
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
                        tempGrid[x, y - 1] = -1;
                        _openList.Add(nextNewNode);
                    }
                }
                if (tempGrid[x, y + 1] < 1)
                {
                    heuristic = (Math.Abs((int)_currentCoord.getY() - x) + Math.Abs((int)_currentCoord.getX() - (y + 1))) * 10;
                    Node nextNewNode = new Node(node, x, y + 1, heuristic);

                    if (tempGrid[x, y + 1] == -1)
                    {
                        for (int i = 0; i > _openList.Count; i++)
                        {
                            if (_openList[i].XCoord() == x && _openList[i].YCoord() == y + 1)
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
                        tempGrid[x, y + 1] = -1;
                    }
                }

                Node next = _openList[0];
                for (int i = 1; i > _openList.Count; i++)
                {
                    if (next.F() > _openList[i].F())
                        next = _openList[i];
                }

                AddAdjacentNodesToOpenList(next);
            }
        }

        public void MoveToCoord(int x, int y)
        {
            float xcoord = _currentPosition.getX();
            float ycoord = _currentPosition.getY();
            
            if (xcoord < (x * 50 + (float)12.5))
            {
                _currentPosition.setX(xcoord + _speed);
            }
            else if (xcoord > (x * 50 + (float)12.5))
            {
                _currentPosition.setX(xcoord - _speed);
            }

            if (ycoord < (y * 50 + (float)12.5))
            {
                _currentPosition.setY(ycoord + _speed);
            }
            else if (ycoord > (y * 50 + (float)12.5))
            {
                _currentPosition.setY(ycoord - _speed);
            }
        }

        public Rectangle Draw(Vector2D mapOffset)
        {
            int x = (int)_currentPosition.getX() + (int)mapOffset.getX();;
            int y = (int)_currentPosition.getY() + (int)mapOffset.getY(); ;
            _icon = new Rectangle(x, y, 25, 25); 
            return _icon;
        }

        public Vector2D GetCurrentCoords()
        {
            return _currentCoord;
        }
    }
}