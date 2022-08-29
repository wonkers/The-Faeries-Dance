using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheFaeriesDance
{
    class SparkleController : ISpriteController
    {
        static List<Texture2D> Sparkles = new List<Texture2D>();
        static List<GameSprite> Sparks = new List<GameSprite>();
        static int frame = 0;
        static List<Vector2> PositionData = new List<Vector2>
        {
            new Vector2(){X = (4 << 5) +16, Y = 4 << 5 },
            new Vector2(){X = (9 << 5) + 16, Y = (2 << 5) + 16},
            new Vector2(){X = (12 << 5) + 16, Y = (5 << 5) + 16},
            new Vector2(){X = (19 << 5) + 16, Y = (5 << 5) + 16},
            new Vector2(){X = (26 << 5) + 16, Y = (5 << 5) + 16},
            new Vector2(){X = (29 << 5) + 16,  Y = (2 << 5) + 16},
            new Vector2(){X = (34 << 5) + 16, Y= (4 << 5) + 15},

            new Vector2(){X = (5 << 5) + 16, Y = (12 << 5) + 16},
            new Vector2(){X = (13 << 5) + 16, Y= (12 << 5) + 16},
            new Vector2(){X = (19 << 5) + 16, Y= (12 << 5) + 16},
            new Vector2(){X = (25 << 5) + 16, Y = (12 << 5) + 16},
            new Vector2(){X = (33 << 5) + 16, Y = (12 << 5) + 16},

            new Vector2(){X = 5 << 5, Y = 19 << 5},
            new Vector2(){X = (10 << 5) + 16,Y = (16 << 5) + 16},
            new Vector2(){X = 17 << 5, Y = 19 << 5},
            new Vector2(){X = 22 << 5, Y = 19 << 5},
            new Vector2(){X = (28 << 5) + 15, Y =(16 << 5) + 16},
            new Vector2(){X = 33 << 5, Y = 19 << 5},
            new Vector2(){X = (10 << 5) + 16, Y = 23 << 5},
            new Vector2(){X = (28 << 5) + 16, Y = 23 << 5}

            
        };

        public static void LoadContent(ContentManager Content, GraphicsDevice device)
        {
            Sparkles.Add(Content.Load<Texture2D>("f1"));
            Sparkles.Add(Content.Load<Texture2D>("f2"));
            Sparkles.Add(Content.Load<Texture2D>("f3"));
            Sparkles.Add(Content.Load<Texture2D>("f4"));

            for (int s = 0; s < PositionData.Count; s++)
            {
                GameSprite sparkle = new GameSprite();
                sparkle.SpriteTexture = new Texture2D[4];
                for (int tex = 0; tex < 4; tex++)
                {
                    sparkle.SpriteTexture[tex] = Sparkles[tex];
                }
                Sparks.Add(sparkle);
            }
            int i = 0;
            foreach(Vector2 v in PositionData)
            {
                Sparks[i].SpriteRectangle = new Rectangle((int)v.X, (int)v.Y, 32, 32);
                Sparks[i].X = v.X;
                Sparks[i++].Y = v.Y;
            }
        }

        public static void CheckCollision(PlayerSprite player)
        {
            foreach (GameSprite spark in Sparks)
            {
                if (player.SpriteRectangle.Contains(spark.SpriteRectangle))
                {
                    Sparks.Remove(spark);
                    Game1.Score += 100;
                    Game1.light += 8;
                    Game1.soundBank.PlayCue("coin");
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
            foreach (GameSprite spark in Sparks)
            {
                spark.SpriteRectangle.X = (int)(spark.X - ScrollOffsets.X);
                spark.SpriteRectangle.Y = (int)(spark.Y - ScrollOffsets.Y);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameSprite sparkle in Sparks)
            {
                //spriteBatch.Draw(testTex, sparkle.SpriteRectangle, Color.OldLace);
                spriteBatch.Draw(sparkle.SpriteTexture[frame], sparkle.SpriteRectangle, Color.Yellow);
            }
        }
    }
}
