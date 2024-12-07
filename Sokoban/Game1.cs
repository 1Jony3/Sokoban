using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sokoban
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SokobanViewModel _viewModel;

        private Texture2D _playerTexture;
        private Texture2D _boxTexture;
        private Texture2D _boxInPlaceTexture;
        private Texture2D _brickTexture;
        private Texture2D _crystalTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            char[,] warehouse =
            {
                { 'W', 'W', 'W', 'W', 'W', 'W', 'W' },
                { 'W', 'P', 'E', 'C', 'E', 'E', 'W' },
                { 'W', 'E', 'B', 'W', 'B', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'E', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'C', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'E', 'E', 'W' },
                { 'W', 'W', 'W', 'W', 'W', 'W', 'W' }
            };
            _graphics.PreferredBackBufferWidth = warehouse.GetLength(1) * 64; // Ширина окна
            _graphics.PreferredBackBufferHeight = warehouse.GetLength(0) * 64; // Высота окна
            _graphics.ApplyChanges();
            _viewModel = new SokobanViewModel(warehouse, 1, 1);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerTexture = Content.Load<Texture2D>("player");
            _boxTexture = Content.Load<Texture2D>("boxNotActive");
            _boxInPlaceTexture = Content.Load<Texture2D>("box");
            _brickTexture = Content.Load<Texture2D>("wall");
            _crystalTexture = Content.Load<Texture2D>("crystal");
        }

        private float _timeSinceLastMove;
        private float _moveCooldown = 0.2f; // Время ожидания между ходами в секундах

        protected override void Update(GameTime gameTime)
        {
            // Обновляем таймер
            _timeSinceLastMove += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var state = Keyboard.GetState();

            if (_timeSinceLastMove >= _moveCooldown)
            {
                if (state.IsKeyDown(Keys.Up))
                {
                    _viewModel.MovePlayer(0, -1);
                    _timeSinceLastMove = 0f; // Сброс таймера после перемещения
                }
                else if (state.IsKeyDown(Keys.Down))
                {
                    _viewModel.MovePlayer(0, 1);
                    _timeSinceLastMove = 0f; 
                }
                else if (state.IsKeyDown(Keys.Left))
                {
                    _viewModel.MovePlayer(-1, 0);
                    _timeSinceLastMove = 0f; 
                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    _viewModel.MovePlayer(1, 0);
                    _timeSinceLastMove = 0f; 
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            // Отрисовка уровня
            for (var y = 0; y < _viewModel.Warehouse.GetLength(0); y++)
            {
                for (var x = 0; x < _viewModel.Warehouse.GetLength(1); x++)
                {
                    switch (_viewModel.Warehouse[y, x])
                    {
                        case 'E': // Пустое место
                            break;
                        case 'W': // Стена
                            _spriteBatch.Draw(_brickTexture, new Vector2(x * 64, y * 64), Color.White);
                            break;
                        case 'P': // Игрок
                            _spriteBatch.Draw(_playerTexture, new Vector2(x * 64, y * 64), Color.White);
                            break;
                        case 'B': // Ящик
                            _spriteBatch.Draw(_boxTexture, new Vector2(x * 64, y * 64), Color.White);
                            break;
                        case 'C': // Кристалл
                            _spriteBatch.Draw(_crystalTexture, new Vector2(x * 64, y * 64), Color.White);
                            break;
                        case 'A': // Ящик в нужном месте
                            _spriteBatch.Draw(_boxInPlaceTexture, new Vector2(x * 64, y * 64), Color.White);
                            break;

                    }


                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
