using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sokoban
{
    public class SokobanGame : Game
    {
        private Level level; 
        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private char[,] warehouse =
            {
                { 'W', 'W', 'W', 'W', 'W', 'W', 'W' },
                { 'W', 'P', 'E', 'C', 'E', 'E', 'W' },
                { 'W', 'E', 'B', 'W', 'B', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'E', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'C', 'E', 'W' },
                { 'W', 'E', 'E', 'E', 'E', 'E', 'W' },
                { 'W', 'W', 'W', 'W', 'W', 'W', 'W' }
            };

        private SokobanViewModel viewModel;

        public SokobanGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = warehouse.GetLength(1) * 64; // Ширина окна
            graphics.PreferredBackBufferHeight = warehouse.GetLength(0) * 64; // Высота окна
            graphics.ApplyChanges();
            viewModel = new SokobanViewModel(warehouse, 1, 1);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level = new Level(this, viewModel); 
            Components.Add(level); 
            level.Initialize(); 
        }

        private float timeSinceLastMove;
        private float moveCooldown = 0.2f; // Время ожидания между ходами в секундах

        protected override void Update(GameTime gameTime)
        {
            // Обновляем таймер
            timeSinceLastMove += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var state = Keyboard.GetState();

            if (timeSinceLastMove >= moveCooldown)
            {
                if (state.IsKeyDown(Keys.Up))
                {
                    viewModel.MovePlayer(0, -1);
                    timeSinceLastMove = 0f; // Сброс таймера после перемещения
                }
                else if (state.IsKeyDown(Keys.Down))
                {
                    viewModel.MovePlayer(0, 1);
                    timeSinceLastMove = 0f; 
                }
                else if (state.IsKeyDown(Keys.Left))
                {
                    viewModel.MovePlayer(-1, 0);
                    timeSinceLastMove = 0f; 
                }
                else if (state.IsKeyDown(Keys.Right))
                {
                    viewModel.MovePlayer(1, 0);
                    timeSinceLastMove = 0f; 
                }
            }

            base.Update(gameTime);
        }        
    }
}
