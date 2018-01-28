//çÏê¨é“ÅFîê
//çÏê¨ì˙ÅF2016.12.29
//ì‡Å@óeÅFÇ`ÅñåoòHíTç∏
//ç≈å„èCê≥é“ÅF
//èCÅ@ê≥Å@ì˙ÅF
//èCê≥ì‡óeÅF

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A_Star
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsManager;
        private Renderer renderer;
        private InputState inputState;
        private Stage stage;
        private Player player;
        private PathCalc pathCalc;

        public Game1()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            graphicsManager.PreferredBackBufferWidth = Parameter.ScreenWidth;
            graphicsManager.PreferredBackBufferHeight = Parameter.ScreenHeigth;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// èâä˙âª
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            renderer = new Renderer(Content, GraphicsDevice);
            inputState = new InputState();
            stage = new Stage();
            player = new Player(stage);
            pathCalc = new PathCalc();
            pathCalc.Initialize();

            Window.Title = "Ç`ÅñåoòHíTçı";
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            renderer.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            renderer.UnloadContent();
            
        }

        /// <summary>
        /// çXêV
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            // TODO: Add your update logic here
            inputState.Update();
            if (inputState.IsClickLeft()) {
                pathCalc.ClearMemory();
                pathCalc.SetTarget(inputState.MousePosition);
                pathCalc.SetStart(player.Position);
                pathCalc.SetCheckMap(stage.MapData);
                pathCalc.Calculate();
                pathCalc.GetPath();
            }
            else if (inputState.IsClickRight()) {
                pathCalc.ClearMemory();
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// ï`âÊ
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            renderer.Begin();
            stage.Draw(renderer);
            player.Draw(renderer);
            pathCalc.Draw(renderer);
            renderer.End();

            base.Draw(gameTime);
        }
    }
}
