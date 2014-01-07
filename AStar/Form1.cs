﻿using System;
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
    public partial class Form1 : Form
    {

        private Rectangle _tile = new Rectangle(0, 0, 0, 0);
        public int[,] _grid;
        private Vector2D _mapOffset = new Vector2D(0,0);
        Graphics g;
        private int _tileSize;
        private Mobile _player;
        private Mobile _enemy;
        private bool _updateWorld = true;

        public Form1()
        {
            InitializeComponent();
            this.MaximumSize = new Size(800, 600);
            this.MinimumSize = new Size(800, 600);

            _tileSize = 50;

            _player = new Mobile(1,1);
            _enemy = new Mobile(2,2);

            _grid = new int[10, 10] {   {1,1,1,1,1,1,1,1,1,1},
                                        {1,0,1,0,1,0,1,0,1,1},
                                        {1,0,0,0,1,0,0,0,0,1},
                                        {1,0,1,1,1,1,1,1,0,1},
                                        {1,0,1,0,0,1,0,0,0,1},
                                        {1,0,1,0,0,1,0,1,1,1},
                                        {1,0,1,0,0,0,0,0,0,1},                                          
                                        {1,0,1,1,1,1,1,0,0,1},
                                        {1,0,0,0,0,0,0,0,0,1},
                                        {1,1,1,1,1,1,1,1,1,1}};
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            g = e.Graphics;

            if (_updateWorld)
            {
                int i;
                int j;

                for (i = 0; i < 10; i++)
                {
                    for (j = 0; j < 10; j++)
                    {
                        if (_grid[i, j] == 1)
                        {
                            _tile = new Rectangle(j * _tileSize + (int)_mapOffset.getX(), i * _tileSize + (int)_mapOffset.getY(), _tileSize, _tileSize);
                            formGraphics.FillRectangle(myBrush, _tile);
                        }
                        else if (_grid[i, j] == 0)
                        {
                            _tile = new Rectangle(j * _tileSize + (int)_mapOffset.getX(), i * _tileSize + (int)_mapOffset.getY(), _tileSize, _tileSize);
                            g.DrawRectangle(Pens.Blue, _tile);
                        }
                    }
                }
            }
            g.DrawRectangle(Pens.Green, _player.Draw(_mapOffset));
            g.DrawRectangle(Pens.Purple, _enemy.Draw(_mapOffset));

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //int PlayerX = _player.Location.X;
            //int PlayerY = _player.Location.Y;

            switch (e.KeyData)
            {
                case Keys.Up:
                    //if (YOffset <= 0)
                    //{
                    _mapOffset.setY(_mapOffset.getY() + 10);
                        this.Refresh();
                    //}
                    break;
                case Keys.Down:
                    if (_mapOffset.getY() > 0)
                    {
                        _mapOffset.setY(_mapOffset.getY() - 10);
                        this.Refresh();
                    }
                    else
                    {
                        _mapOffset.setY(0);
                        this.Refresh();
                    }
                    break;
                case Keys.Left:
                    _mapOffset.setX(_mapOffset.getX() + 10);
                    this.Refresh();
                    break;
                case Keys.Right:
                    if (_mapOffset.getX() > 0)
                    {
                        _mapOffset.setX(_mapOffset.getX() - 10);
                        this.Refresh();
                    }
                    else
                    {
                        _mapOffset.setX(0);
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
            _enemy.Update();

            //if (_enemy.GetCurrentCoords().getX() == 8 && _enemy.GetCurrentCoords().getY() == 4)
            //   _enemy.PathFinding(4, 4, _grid);

            this.Refresh();
            //MessageBox.Show("The form will now be closed.", "Time Elapsed");
            //this.Close();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int RealXPos = e.X - (int)_mapOffset.getX();
            int RealYPos = e.Y - (int)_mapOffset.getY();

            int GridXClick = RealXPos / _tileSize;
            int GridYClick = RealYPos / _tileSize;

            if (_grid[GridYClick, GridXClick] == 0)
                _player.PathFinding(GridXClick, GridYClick, _grid);
        }
    }
}
