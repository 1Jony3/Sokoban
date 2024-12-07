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
        private SokobanModel _model;

        public SokobanViewModel(char[,] warehouse, int startX, int startY)
        {
            IPlayer warehouseman = new Warehouseman(startX, startY);
            _model = new SokobanModel(warehouse, warehouseman);
        }

        public void MovePlayer(int deltaX, int deltaY)
        {
            _model.MovePlayer(deltaX, deltaY);
            OnPropertyChanged(nameof(Warehouse));
        }

        public char[,] Warehouse => _model.Warehouse;

        public IPlayer Warehouseman => _model.Warehouseman;
        public List<IPlayer> Boxes => _model.Boxes;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
