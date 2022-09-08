using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maze
{
    class Labirint
    {
        internal int height; // высота лабиринта (количество строк)

        internal int width; // ширина лабиринта (количество столбцов в каждой строке)

        internal MazeObject[,] maze;

        internal PictureBox[,] images;

        internal static Random r = new Random();

        internal Form parent;

        internal Charapter charapter = new Charapter();

        internal int medal_count = 0;

        internal Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;

            maze = new MazeObject[height, width];
            images = new PictureBox[height, width];

            Generate();
        }

        private void Generate()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MazeObject.MazeObjectType current =
                        MazeObject.MazeObjectType.HALL;

                    // стены по периметру обязательны
                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }
                    else // в 1 случае из 5 - ставим стену
                    if (r.Next(5) == 0)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }
                    else // в 1 случае из 50 - размещаем медаль
                    if (r.Next(50) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDAL;
                        medal_count++;
                    }
                    else // в 1 случае из 25 - размещаем врага
                    if (r.Next(25) == 0)
                    {
                        current = MazeObject.MazeObjectType.ENEMY;
                    }
                    else // в 1 случае из 120 - размещаем лечение
                    if (r.Next(120) == 0)
                    {
                        current = MazeObject.MazeObjectType.HEAL;
                    }

                    // есть выход, и соседняя ячейка справа всегда свободна
                    if (
                        x == charapter.position.X + 1 &&
                        y == charapter.position.Y ||
                        x == width - 1 && y == height - 3
                    )
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }

                    // наш персонажик
                    if (x == charapter.position.X && y == charapter.position.Y)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                    }

                    maze[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location =
                        new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;
                }
            }
        }

        internal void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    images[y, x].Visible = true;
                }
            }
        }

        internal bool CheckWin()
        {
            if (
                charapter.position.X == width - 1 &&
                charapter.position.Y == height - 3 ||
                charapter.medal_count == this.medal_count ||
                charapter.health_procent <= 0
            )
            {
                if (charapter.health_procent <= 0)
                    MessageBox.Show("You have lose the game!");
                else
                    MessageBox.Show("You have win the game!");
                return true;
            }
            return false;
        }

        internal void CharapterMovement(KeyEventArgs e)
        {
            var x = charapter.position.X;
            var y = charapter.position.Y;

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    if (x - 1 >= 0 && CheckCharapterMovement(x - 1, y))
                        (
                            images[y, x].BackgroundImage,
                            images[y, x - 1].BackgroundImage
                        ) =
                            (
                                images[y, x - 1].BackgroundImage,
                                images[y, x].BackgroundImage
                            );
                    break;
                case Keys.Right:
                case Keys.D:
                    if (x + 1 >= 0 && CheckCharapterMovement(x + 1, y))
                        (
                            images[y, x].BackgroundImage,
                            images[y, x + 1].BackgroundImage
                        ) =
                            (
                                images[y, x + 1].BackgroundImage,
                                images[y, x].BackgroundImage
                            );
                    break;
                case Keys.Up:
                case Keys.W:
                    if (y - 1 < width && CheckCharapterMovement(x, y - 1))
                        (
                            images[y, x].BackgroundImage,
                            images[y - 1, x].BackgroundImage
                        ) =
                            (
                                images[y - 1, x].BackgroundImage,
                                images[y, x].BackgroundImage
                            );
                    break;
                case Keys.Down:
                case Keys.S:
                    if (y + 1 < height && CheckCharapterMovement(x, y + 1))
                        (
                            images[y, x].BackgroundImage,
                            images[y + 1, x].BackgroundImage
                        ) =
                            (
                                images[y + 1, x].BackgroundImage,
                                images[y, x].BackgroundImage
                            );
                    break;
            }
        }

        internal bool CheckCharapterMovement(int x, int y)
        {
            switch (maze[y, x].type)
            {
                case MazeObject.MazeObjectType.WALL:
                    return false;
                case MazeObject.MazeObjectType.HALL:
                    charapter.position = new Point(x, y);
                    images[y, x].BackgroundImage =
                        maze[height - 3, width - 1].texture;
                    return true;
                case MazeObject.MazeObjectType.MEDAL:
                    charapter.position = new Point(x, y);
                    charapter.medal_count++;
                    images[y, x].BackgroundImage =
                        maze[height - 3, width - 1].texture;
                    return true;
                case MazeObject.MazeObjectType.ENEMY:
                    charapter.position = new Point(x, y);
                    charapter.health_procent -= r.Next(20, 25);
                    images[y, x].BackgroundImage =
                        maze[height - 3, width - 1].texture;
                    return true;
                case MazeObject.MazeObjectType.HEAL:
                    charapter.position = new Point(x, y);
                    if (charapter.health_procent < 100)
                        charapter.health_procent += r.Next(5, 10);
                    images[y, x].BackgroundImage =
                        maze[height - 3, width - 1].texture;
                    return true;
                default:
                    return false;
            }
        }
    }
}
