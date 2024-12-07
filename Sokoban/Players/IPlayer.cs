using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Players
{
    public interface IPlayer
    {
        int LocalX { get; }
        int LocalY { get; }
        void Move(int deltaX, int deltaY);
    }
}
