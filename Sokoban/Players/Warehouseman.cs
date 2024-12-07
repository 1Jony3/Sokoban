using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Players
{
    public class Warehouseman : IPlayer
    {
        public int LocalX { get; private set; }
        public int LocalY { get; private set; }

        public Warehouseman(int startX, int startY)
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
