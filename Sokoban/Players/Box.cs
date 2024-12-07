using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Players
{
    internal class Box : IPlayer
    {
        public int LocalX { get; private set; }
        public int LocalY { get; private set; }

        public Box(int startX, int startY)
        {
            LocalX = startX;
            LocalY = startY;
        }

        public void Move(int deltaX, int deltaY)
        {
            LocalX += deltaX;
            LocalY += deltaY;
        }
    }
}
