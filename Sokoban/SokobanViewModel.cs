using Sokoban.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class SokobanViewModel : INotifyPropertyChanged
    {
        private SokobanModel model;

        public SokobanViewModel(char[,] warehouse, int startX, int startY)
        {
            IPlayer warehouseman = new Warehouseman(startX, startY);
            model = new SokobanModel(warehouse, warehouseman);
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            model.MovePlayer(deltaX, deltaY);
            OnPropertyChanged(nameof(Warehouse));
        }

        public char[,] Warehouse => model.Warehouse;

        public IPlayer Warehouseman => model.Warehouseman;
        public List<IPlayer> Boxes => model.Boxes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
