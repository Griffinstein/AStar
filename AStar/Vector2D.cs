using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Vector2D
    {
        private float _x;
        private float _y;

        public float x
        {
            get { return _x; }
            set { _x = value; }
        }

        public float y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Vector2D(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public Vector2D()
        {
            _x = 0;
            _y = 0;
        }
    }
}
