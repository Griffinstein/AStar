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

        public void setX(float x) { _x = x; }
        public void setY(float y) { _y = y; }
        public float getX() { return _x; }
        public float getY() { return _y; }
    }
}
