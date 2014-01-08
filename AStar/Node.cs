using System;
using System.Collections.Generic;

namespace AStar
{
    class Node
    {
        private Vector2D _coords;
        private Node _parent;
        private int _heuristic;
        private int _g;
        private int _f;
        private bool _hasParent;

        public Node()
        {
            _parent = null;
            _hasParent = false;
            _g = 0;
        }

        public Node(float x, float y)
        {
            _parent = null;
            _hasParent = false;
            _coords = new Vector2D(x, y);
            _g = 0;
        }

        public Node(Node parent, int heuristic)
        {
            _parent = parent;
            _hasParent = true;
            _g = _parent._g + 10;
            _heuristic = heuristic;
            _f = _g + _heuristic;
        }

        public Node(Node parent, float x, float y, int heuristic)
        {
            _parent = parent;
            _hasParent = true;
            _g = _parent._g + 10;
            _heuristic = heuristic;
            _f = _g + _heuristic;
            _coords = new Vector2D(x, y);
        }

        public int f
        {
            get { return _f; }
            set { _f = value; }
        }

        public int g
        {
            get { return _g; }
            set { _g = value; }
        }

        public bool HasParent
        {
            get { return _hasParent; }
            set { _hasParent = value; }
        }

        public Node Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int x
        {
            get { return (int)_coords.x; }
            set { _coords.x = value; }
        }

        public int y
        {
            get { return (int)_coords.y; }
            set { _coords.y = value; }
        }
    }
}