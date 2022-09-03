using System.Drawing;

namespace Maze
{
    internal class Charapter
    {
        internal Point position { get; set; }
        internal int medal_count { get; set; }
        internal int health_procent { get; set; }
        internal Charapter()
        {
            position = new Point(0, 2);
            medal_count = 0;
            health_procent = 100;
        }
    }
}
