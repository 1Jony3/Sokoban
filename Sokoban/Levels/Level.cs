using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban;


internal class Level : Microsoft.Xna.Framework.DrawableGameComponent
{
    private SokobanGame sokobanGame;
    private char[,] warehouse;
    private SokobanViewModel viewModel;
    private Texture2D playerTexture, boxTexture, boxInPlaceTexture, brickTexture, crystalTexture;

    public Level(SokobanGame sokobanGame, SokobanViewModel viewModel) : base(sokobanGame)
    {
        this.sokobanGame = sokobanGame;
        this.viewModel = viewModel; 
    }

    public override void Initialize() => base.Initialize();

    protected override void LoadContent()
    {
        playerTexture = sokobanGame.Content.Load<Texture2D>("player");
        boxTexture = sokobanGame.Content.Load<Texture2D>("boxNotActive");
        boxInPlaceTexture = sokobanGame.Content.Load<Texture2D>("box");
        brickTexture = sokobanGame.Content.Load<Texture2D>("wall");
        crystalTexture = sokobanGame.Content.Load<Texture2D>("crystal");
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        sokobanGame.GraphicsDevice.Clear(Color.Black);
        sokobanGame.spriteBatch.Begin();

        // Отрисовка уровня
        for (var y = 0; y < viewModel.Warehouse.GetLength(0); y++)
        {
            for (var x = 0; x < viewModel.Warehouse.GetLength(1); x++)
            {
                DrawLevel(x, y, viewModel.Warehouse[y, x]);
            }
        }
        sokobanGame.spriteBatch.End();
        base.Draw(gameTime);
    }

    private void DrawLevel(int x, int y, char level)
    {
        Vector2 position = new Vector2(x * 64, y * 64);

        switch (level)
        {
            case 'E': // Пустое место
                break;
            case 'W': // Стена
                sokobanGame.spriteBatch.Draw(brickTexture, position, Color.White);
                break;
            case 'P': // Игрок
                sokobanGame.spriteBatch.Draw(playerTexture, position, Color.White);
                break;
            case 'B': // Ящик
                sokobanGame.spriteBatch.Draw(boxTexture, position, Color.White);
                break;
            case 'C': // Кристалл
                sokobanGame.spriteBatch.Draw(crystalTexture, position, Color.White);
                break;
            case 'A': // Ящик в нужном месте
                sokobanGame.spriteBatch.Draw(boxInPlaceTexture, position, Color.White);
                break;
        }
    }
}