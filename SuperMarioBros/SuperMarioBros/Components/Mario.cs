using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SuperMarioBros.Components;
using SuperMarioBros.ScreenManagers;

namespace SuperMarioBros.Components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Mario : GameComponent
    {
        enum State
        {
            Walking, Jumping
        }

        State currentState = State.Walking;
        
        private Rectangle _marioRect = new Rectangle(18, 903, 33, 32);
        public Mario(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            this.Position = new Vector2(ScreenManager.GetInstance().ScreenSize.X / 8, ScreenManager.GetInstance().ScreenSize.Y - 96);
            this.Texture = GameContentManager.GetInstance().GetTexture("sprite_sheet");
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, KeyboardState current, KeyboardState previous)
        {
            // TODO: Add your update code here
            if (current.IsKeyDown(Keys.Left))
                Position.X -= 3;
            else if (current.IsKeyDown(Keys.Right))
                Position.X += 3;
            else if (current.IsKeyDown(Keys.Space) && !previous.IsKeyDown(Keys.Space) && currentState == State.Walking)
                Jump();
            base.Update(gameTime);
        }

        public void Jump()
        {
            GameContentManager.GetInstance().GetSound("jump").Play();
            currentState = State.Jumping;

            float maxHeight = Position.Y;
            while (Position.Y > maxHeight - 20) Position.Y--;
            while (Position.Y < maxHeight) Position.Y++;
            
            currentState = State.Walking;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.GetInstance().SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Position, _marioRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
