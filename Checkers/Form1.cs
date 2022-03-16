using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Testing commits

namespace Checkers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Board board = new Board();

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (PictureBox p in board.GetPictureBoxGrid())
            {
                this.Controls.Add(p);
            }
            board.UpdateGrid();
        }
    }
    class Board
    {
        PictureBox[,] grid;
        char[,] backend;

        public Board()
        {
            SetupGrid();
            CreatePictureBoxGrid();
        }

        public void SetupGrid()
        {
            //Update character array to be the same values as a beginning checkers board
            //j is x-axies
            //i is y-axies
            backend = new char[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if((i + j) % 2 == 0)
                    {
                        if (j == 0 || j == 1)
                        {
                            backend[i, j] = 'r';
                        }
                        else if (j == 6 || j == 7)
                        {
                            backend[i, j] = 'b';
                        }
                    }
                }
            }
            CreatePictureBoxGrid();
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            //Inserts player pieces onto the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (backend[i, j] == 'r')
                    {
                        grid[i, j].Image = cropImage(Image.FromFile("Sprite.jpg"), new Rectangle(175, 10, 175, 175));
                        grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    if (backend[i, j] == 'b')
                    {
                        grid[i, j].Image = cropImage(Image.FromFile("Sprite.jpg"), new Rectangle(15, 185, 165, 160));
                        grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            //Allows us to use a sprite sheet
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public void CreatePictureBoxGrid()
        {
            //Creates the board
            grid = new PictureBox[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    grid[i, j] = new PictureBox();
                    grid[i, j].Width = 50;
                    grid[i, j].Height = 50;
                    grid[i, j].Location = new Point(i * 50, j * 50);
                    grid[i, j].BorderStyle = BorderStyle.Fixed3D;
                    grid[i, j].Click += ClickTile;
                }
            }
        }
        public void ClickTile(object sender, EventArgs e)
        {
            PictureBox s = (PictureBox)sender;
            int i = s.Location.X / 50;
            int j = s.Location.Y / 50;
            if (grid[i, j].Image != null) //Checks if square has counter
            {
                //Removes image of counter if the counter image is present
                grid[i, j].Image = null;
            }
            else
            {
                grid[i, j].BackColor = Color.Black;
            }
        }
        public PictureBox[,] GetPictureBoxGrid()
        {
            return grid;
        }
    }
}
