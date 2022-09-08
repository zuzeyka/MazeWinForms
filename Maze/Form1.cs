using System.Drawing;
using System.Windows.Forms;

namespace Maze
{
    public partial class Form1 : Form
    {
        internal Labirint maze;

        public Form1()
        {
            InitializeComponent();
            this.Icon = Maze.Properties.Resources.Music;
            maze = new Labirint(this, 40, 20);
            Options();
            StartGame();
        }

        public void Options()
        {
            Text = "Maze";

            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame()
        {
            maze.Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            statusStrip1.Items[0].Text = $"Healt:{maze.charapter.health_procent}";
            statusStrip1.Items[1].Text = $"Medals:{maze.charapter.medal_count}";
            maze.CharapterMovement (e);
            if (maze.CheckWin()) Application.Exit();
        }
    }
}
