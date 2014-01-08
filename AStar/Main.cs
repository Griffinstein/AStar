using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    public partial class Main : Form
    {

        private Rectangle _tile = new Rectangle(0, 0, 0, 0);
        public int[,] _grid;
        private Vector2D _mapOffset = new Vector2D(0,0);
        Graphics g;
        private int _tileSize;
        private int _WIDTH = 800;
        private int _HEIGHT = 600;
        private Counter _player;
        private bool _updateWorld = true;

        public Main()
        {
            InitializeComponent();
            this.MaximumSize = new Size(_WIDTH, _HEIGHT);
            this.MinimumSize = new Size(_WIDTH, _HEIGHT);

            _tileSize = 50;

            _player = new Counter(1,1);

            _grid = new int[20, 20] {   {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                        {1,0,1,0,0,0,1,0,1,1,0,0,0,0,0,0,0,0,1,1},
                                        {1,0,0,0,1,0,0,0,0,1,0,1,1,1,1,1,1,0,1,1},
                                        {1,0,1,1,1,1,1,1,0,1,0,1,0,0,0,0,1,0,1,1},
                                        {1,0,1,0,0,1,0,0,0,1,0,1,0,1,1,0,1,0,1,1},
                                        {1,0,1,0,0,1,0,1,1,1,0,1,1,1,1,0,1,0,1,1},
                                        {1,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,1,1},
                                        {1,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,1,1},
                                        {1,0,0,0,0,0,0,0,0,1,1,0,0,0,1,1,1,0,1,1},
                                        {1,0,0,0,0,0,0,0,0,1,1,0,1,0,1,1,1,0,1,1},
                                        {1,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
                                        {1,0,1,0,1,0,0,0,0,1,1,0,1,0,1,1,0,0,1,1},
                                        {1,0,1,1,1,0,0,0,0,1,1,0,0,0,1,1,0,0,1,1},
                                        {1,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,0,0,1,1},
                                        {1,0,1,0,0,1,1,1,0,1,1,1,1,1,1,0,0,0,1,1},
                                        {1,0,1,0,0,0,1,0,0,1,1,1,1,1,0,0,0,1,1,1},
                                        {1,0,1,0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,1,1},
                                        {1,0,1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,1,1},
                                        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush myPlayerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            g = e.Graphics;

            formGraphics.FillRectangle(myPlayerBrush, _player.Draw(_mapOffset));

            if (_updateWorld)
            {
                int i;
                int j;

                for (i = 0; i < 20; i++)
                {
                    for (j = 0; j < 20; j++)
                    {
                        if (_grid[i, j] == 1)
                        {
                            _tile = new Rectangle(j * _tileSize + (int)_mapOffset.x, i * _tileSize + (int)_mapOffset.y, _tileSize, _tileSize);
                            formGraphics.FillRectangle(myBrush, _tile);
                        }
                        else if (_grid[i, j] == 0)
                        {
                            _tile = new Rectangle(j * _tileSize + (int)_mapOffset.x, i * _tileSize + (int)_mapOffset.y, _tileSize, _tileSize);
                            g.DrawRectangle(Pens.Blue, _tile);
                        }
                    }
                }
            }

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.Up:
                    if (_mapOffset.y < 0)
                    {
                        _mapOffset.y = _mapOffset.y + 10;
                        this.Refresh();
                    }
                    else
                    {
                       _mapOffset.y = 0;
                       this.Refresh();
                    }
                    break;
                case Keys.Down:
                    if (_mapOffset.y > _HEIGHT - (20 * 50 + 40))
                    {
                        _mapOffset.y = _mapOffset.y - 10;
                        this.Refresh();
                    }
                    else
                    {
                        _mapOffset.y = _HEIGHT - (20 * 50 + 40);
                        this.Refresh();
                    }
                    break;
                case Keys.Left:
                    if (_mapOffset.x < 0)
                    {
                        _mapOffset.x = _mapOffset.x + 10;
                        this.Refresh();
                    }
                    else
                    {
                        _mapOffset.x = 0;
                        this.Refresh();
                    }
                    break;
                case Keys.Right:
                    if (_mapOffset.x > _WIDTH - (20 * 50))
                    {
                        _mapOffset.x = _mapOffset.x - 10;
                        this.Refresh();
                    }
                    else
                    {
                        _mapOffset.x = _WIDTH - (20 * 50);
                        this.Refresh();
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer MyTimer = new Timer();
            MyTimer.Interval = (1/*Minutes*/ * 1/*Second*/ * 33);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            _player.Update();

            this.Refresh();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int RealXPos = e.X - (int)_mapOffset.x;
            int RealYPos = e.Y - (int)_mapOffset.y;

            int GridXClick = RealXPos / _tileSize;
            int GridYClick = RealYPos / _tileSize;

            if (_grid[GridYClick, GridXClick] == 0)
                _player.PathFinding(GridXClick, GridYClick, _grid);
        }
    }
}
