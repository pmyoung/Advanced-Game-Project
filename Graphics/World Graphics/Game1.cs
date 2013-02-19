/*
 * Note that the following code is for testing pourposes only!
 */
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
using GameGraphics;

namespace Comp4432_AGP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WorldGraphics world;
        GraphicsModel model;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world = new WorldGraphics();

            model = new GraphicsModel();
            model.Update(new GraphicsObject(1, 16, 16, 16, 0, 1, 0));
            model.Update(new GraphicsObject(2, 48, 16, 16, 0, 1, 1));
            model.Update(new GraphicsObject(3, 80, 16, 16, 0, 1, 2));
            model.Update(new GraphicsObject(4, 16, 48, 16, 0, 1, 3));
            model.Update(new GraphicsObject(5, 48, 48, 16, 0, 1, 4));
            model.Update(new GraphicsObject(6, 80, 48, 16, 0, 1, 5));
            model.Update(new GraphicsObject(7, 16, 80, 16, 0, 1, 6));
            model.Update(new GraphicsObject(8, 48, 80, 16, 0, 1, 7));
            model.Update(new GraphicsObject(9, 80, 80, 16, 0, 1, 8));

            world.SetGraphicsModel(model);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteStore sp = new SpriteStore();
            sp.LoadSprites(Content);
            world.SetSpriteStore(sp);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int accelerate = 0;
            int rot = 0;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                accelerate++;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                accelerate--;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                rot--;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                rot++;
            }

            List<GraphicsObject> list = model.GetAsList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SetAngle(list[i].GetAngle() + rot);
                // this is to highlight that the Math.Sin and Math.Cos use the radian
                // for its "angle" It also demenstrates that we need to store our
                // locations using floats as it will allow for nice turning rather
                // than fixed 8-directional (N, NE, E, SE, S, SW, W, NW) movement
                float rad = (float)(Math.PI / 180) * list[i].GetAngle();
                list[i].SetX(list[i].GetX() + ((float)Math.Sin(rad) * accelerate));
                list[i].SetY(list[i].GetY() - ((float)Math.Cos(rad) * accelerate));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            world.Render(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
