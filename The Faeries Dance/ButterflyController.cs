using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TheFaeriesDance
{
    class ButterflyController : ISpriteController
    {
        static List<GameSprite> Butterflies = new List<GameSprite>();
        static List<Texture2D> butterfliesTextures = new List<Texture2D>();
        static int frame = 0;

        public static void LoadContent(ContentManager Content, GraphicsDevice device)
        {
            Texture2D ButterflyTextures = Content.Load<Texture2D>("Butterflies");
            Color[] butterflyData = new Color[ButterflyTextures.Width * ButterflyTextures.Height];
            ButterflyTextures.GetData(butterflyData);
            Rectangle rect = new Rectangle(0, 0, 16, 16);

            for (int j = 0; j < ButterflyTextures.Height / 16; j++)
            {
                for (int i = 0; i < ButterflyTextures.Width / 16; i++)
                {
                    rect.X = i * 16;
                    rect.Y = j * 16;
                    Color[] data = new Color[16 * 16];

                    for (int x = 0; x < rect.Width; x++)
                        for (int y = 0; y < rect.Height; y++)
                            data[x + y * rect.Width] = butterflyData[x + rect.X + (y + rect.Y) * ButterflyTextures.Width];


                    Texture2D ButterflyTexture = new Texture2D(device, 16, 16);
                    ButterflyTexture.SetData(data);
                    butterfliesTextures.Add(ButterflyTexture);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                GameSprite butterflies = new GameSprite();
                butterflies.SpriteTexture = new Texture2D[4];
                for (int tex = 0; tex < 4; tex++)
                {
                    butterflies.SpriteTexture[tex] = butterfliesTextures[tex];
                }
               // SetupSprite(ref butterflies, 0.10f, 320.0f, 0, 0);
                butterflies.SpriteRectangle.Width = 32;
                butterflies.SpriteRectangle.Height = 32;
                Butterflies.Add(butterflies);
            }
            Butterflies[0].X = (19 << 5) + 16;
            Butterflies[0].Y = 19 << 5;
            Butterflies[1].X = (19 << 5) + 16;
            Butterflies[1].Y = 4 << 5;
           // Butterflies[2].X = (29 << 5) + 16;
           // Butterflies[2].Y = 12 << 5;
           // Butterflies[3].X = (1 << 5);
           // Butterflies[3].Y = 26 << 5;
        }
        public static void CheckCollision(PlayerSprite player)
        {
            foreach(var buttfly in Butterflies)
            {
                if (player.SpriteRectangle.Contains(buttfly.SpriteRectangle))
                {
                    player.JumpPower += 20;
                    Butterflies.Remove(buttfly);
                    Game1.soundBank.PlayCue("jump");
                    break;
                }
            }
        }

        public static void Update(Vector2 ScrollOffsets, GameTime gameTime)
        {
            if (((int)gameTime.TotalGameTime.Ticks & 0x7) == 0x7)
            {
                frame += 1;
                frame &= 0x3;

            }

            foreach (GameSprite butterfly in Butterflies)
            {
                butterfly.SpriteRectangle.X = (int)(butterfly.X - ScrollOffsets.X);
                butterfly.SpriteRectangle.Y = (int)(butterfly.Y - ScrollOffsets.Y);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameSprite bfly in Butterflies)
            {
                spriteBatch.Draw(
                    bfly.SpriteTexture[frame], 
                    bfly.SpriteRectangle, 
                    Color.White);
            }
        }
    }

}
