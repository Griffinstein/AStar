using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int F()
        {
            return _f;
        }

        public int G()
        {
            return _g;
        }

        public Node Parent()
        {
            return _parent;
        }

        public bool HasParent()
        {
            return _hasParent;
        }

        public int XCoord()
        {
            int i = (int)_coords.x;
            return i;
        }

        public int YCoord()
        {
            int i = (int)_coords.y;
            return i;
        }
    }
}