using Sokoban.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class SokobanModel
    {
        public char[,] Warehouse { get; private set; }
        public IPlayer Warehouseman { get; private set; }
        public List<IPlayer> Boxes { get; private set; }

        public SokobanModel(char[,] warehouse, IPlayer warehouseman)
        {
            Warehouse = warehouse;
            Warehouseman = warehouseman;
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            var newX = Warehouseman.LocalX + deltaX;
            var newY = Warehouseman.LocalY + deltaY;

            var canMove = false;

            // Проверка на границы
            if (CanMoveObject(newX, newY))
            {
                canMove = true;
                // Если игрок перемещается в ящик
                if (Warehouse[newY, newX] == 'B')
                {
                    canMove = false;
                    var boxNewX = newX + deltaX;
                    var boxNewY = newY + deltaY;

                    // Проверка, можно ли переместить ящик
                    if (CanMoveObject(boxNewX, boxNewY))
                    {
                        canMove = true;
                        // Перемещение ящика
                        Warehouse[newY, newX] = 'E'; // Освобождаем место под ящиком
                        BoxInTheRightPlace(boxNewX, boxNewY);                                                
                    }
                } 
                //if (Warehouse[newY, newX] == 'C') 
            }
            if (canMove == true) MovingPlayer(deltaX, deltaY);
        }

        private bool CanMoveObject(int x, int y)
        {
            // Проверка на границы
            if (x < 0 || x >= Warehouse.GetLength(1) || y < 0 || y >= Warehouse.GetLength(0))
                return false;

            // Проверка на стены
            if (Warehouse[y, x] == 'W' || Warehouse[y, x] == 'A')
                return false;

            return true;
        }

        private void MovingPlayer(int deltaX, int deltaY)
        {
            // Перемещение игрока
            Warehouse[Warehouseman.LocalY, Warehouseman.LocalX] = 'E';
            Warehouseman.Move(deltaX, deltaY);
            Warehouse[Warehouseman.LocalY, Warehouseman.LocalX] = 'P'; 
        }
        
        private void BoxInTheRightPlace(int boxNewX, int boxNewY)
        {
            if (Warehouse[boxNewY, boxNewX] == 'C') Warehouse[boxNewY, boxNewX] = 'A'; // Перемещаем ящик и активируем
            else Warehouse[boxNewY, boxNewX] = 'B'; // Перемещаем ящик
        }
    }
}
