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

        int killShip = 0;
        Boolean gotOne = false;

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
            int diameter = 64;
            int radius = diameter/2;
            int spriteID = 201;
            int id = 0;
            int space = 3;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    model.Update(new GraphicsObject(id, (diameter*(x+1))+space*x, (diameter*(y+1))+space*y, radius, 0, spriteID, id));
                    id++;
                }
            }

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
            int speed = 2;
            int rot = 0;
            int rotSpeed = 1;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                accelerate+=speed;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                accelerate-=speed;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                rot-=rotSpeed;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                rot+=rotSpeed;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.K) && !gotOne)
            {
                model.Remove(killShip);
                killShip++;
                gotOne = true;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.K) && gotOne)
            {
                gotOne = false;
            }

            List<GraphicsObject> list = model.GetAsList();
            for (int i = 0; i < list.Count; i++)
            {
                float angle = list[i].GetAngle() + rot;
                // this is to highlight that the Math.Sin and Math.Cos use the radian
                // for its "angle" It also demenstrates that we need to store our
                // locations using floats as it will allow for nice turning rather
                // than fixed 8-directional (N, NE, E, SE, S, SW, W, NW) movement
                float rad = (float)(Math.PI / 180) * list[i].GetAngle();
                float x = list[i].GetX() + ((float)Math.Sin(rad) * accelerate);
                float y = list[i].GetY() - ((float)Math.Cos(rad) * accelerate);

                model.Update(new GraphicsObject(list[i].GetID(), x, y, list[i].GetRadius(), angle, list[i].GetSpriteID()));
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
