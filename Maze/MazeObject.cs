using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType
        {
            HALL,
            WALL,
            MEDAL,
            ENEMY,
            HEAL,
            CHAR
        }

        public Bitmap[]
            images =
            {
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\hall.png"),
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\wall.png"),
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\medal.png"),
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\enemy.png"),
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\heal.png"),
                new Bitmap(@"C:\Users\stepa\OneDrive\MyProjects\C#\Maze\pics\player.png")
            };

        public MazeObjectType type;

        public int width;

        public int height;

        public Image texture;

        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            texture = images[(int) type];
        }
    }
}
