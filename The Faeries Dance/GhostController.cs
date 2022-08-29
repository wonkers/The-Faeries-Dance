using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TheFaeriesDance
{
    class GhostSprite : GameSprite
    {
        enum State { 
            Idle,
            Chasing
        }
        private Vector2 Left = new Vector2(-1, 0);
        private Vector2 Right = new Vector2(1, 0);
        private Vector2 Up = new Vector2(0, -1);
        private Vector2 Down = new Vector2(0, 1);

        public Vector2 MapPosition = new Vector2();
        public Vector2 Target = Vector2.Zero;
        public Color GhostColor;
        State state = State.Idle;
        int Timer = 0;
        public void UpdateMapPosition()
        {
            MapPosition.X = X / 32;
            MapPosition.Y = Y / 32;
            SpriteRectangle.X = (int)X;
            SpriteRectangle.Y = (int)Y;
        }
        public void SetNextVector(Vector2 CookiePosition, NodePoint Node)
        {
            Vector2 target = Vector2.Zero;
            if (state == State.Chasing)
                target = CookiePosition - MapPosition;
            else
                target = Target - MapPosition;

            int x = 0;
            int y = 0;
            if (target.X < 0)
                x = -1;
            else if (target.X > 0)
                x = 1;
            if (target.Y < 0)
                y = -1;
            else if (target.Y > 0)
                y = 1;

            target.X = x;
            target.Y = y;

            if(target == Left)
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Right;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Down;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Right;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Right;
                }
            }
            else if (target == new Vector2(-1, -1))
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Down;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Down;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Right;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Right;
                }
            }
            else if (target == new Vector2(-1, 1))
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Up;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Up;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Up;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else
                        Direction = Right;
                }
            }

            else if (target == Right)
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Up;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Left;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Left;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Left;
                }
            }
            else if (target == new Vector2(1, -1))
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Down;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Left;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Left;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Left;
                }
            }
            else if (target == new Vector2(1, 1))
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Up;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Left;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else
                        Direction = Left;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else
                        Direction = Left;
                }
            }
            else if (target == Up)
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Down;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else
                        Direction = Down;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Up])
                        Direction = Up;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Left;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else
                        Direction = Down;
                }
            }
            else if (target == Down)
            {
                if (Direction == Right)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Up;
                }
                else if (Direction == Left)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else
                        Direction = Up;
                }
                else if (Direction == Up)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Left])
                        Direction = Left;
                    else
                        Direction = Up;
                }
                else if (Direction == Down)
                {
                    if (Node.Exits[(int)TheFaeriesDance.Direction.Down])
                        Direction = Down;
                    else if (Node.Exits[(int)TheFaeriesDance.Direction.Right])
                        Direction = Right;
                    else
                        Direction = Left;
                }
            }

            if (Direction == new Vector2(1, 0))
                effects = SpriteEffects.None;
            if (Direction == new Vector2(-1, 0))
                effects = SpriteEffects.FlipHorizontally;
        }

        public void Update()
        {
            Timer++;
            if (Timer > 1000)
            {
                Timer = 0;
                switch (state)
                {
                    case State.Chasing:
                        state = State.Idle;
                        break;
                    case State.Idle:
                        state = State.Chasing;
                        Direction *= new Vector2(-1, -1);
                        break;
                }

            }


            if (Direction.X < 0)
                effects = SpriteEffects.FlipHorizontally;
            else
                effects = SpriteEffects.None;
        }
    }

    class GhostController : ISpriteController
    {
        public static List<GhostSprite> Ghosts = new List<GhostSprite>();
        static Vector2[] Nodes = new Vector2[4]
        {
            new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1)
        };

        //left right up down
        static List<NodePoint> NodePoints = new List<NodePoint>
        {
            new NodePoint{ Location = new Vector2(1,    2), Exits = new bool[]{false, true, false, true } },
            new NodePoint{ Location = new Vector2(7,    2),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(12,   2),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(16,   2),Exits = new bool[]{true, false, false, true } },
            new NodePoint{ Location = new Vector2(22,   2),Exits = new bool[]{false, true, false, true } },
            new NodePoint{ Location = new Vector2(27,   2),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(32,   2),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(37,   2),Exits = new bool[]{true, false, false, true } },

            new NodePoint{ Location = new Vector2(12,   4),Exits = new bool[]{false, true, true, false } },//link
            new NodePoint{ Location = new Vector2(16,   4),Exits = new bool[]{true, true, true, true } },//link
            new NodePoint{ Location = new Vector2(22,   4),Exits = new bool[]{true, true, true, true } },
            new NodePoint{ Location = new Vector2(27,   4),Exits = new bool[]{true, false, true, false } },

            new NodePoint{ Location = new Vector2(1,    10), Exits = new bool[]{false, true, true, false}},
            new NodePoint{ Location = new Vector2(5,    10),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(7,    10),Exits = new bool[]{true, false, true, false } },
            new NodePoint{ Location = new Vector2(32,   10),Exits = new bool[]{false, true, true, false } },
            new NodePoint{ Location = new Vector2(33,   10),Exits = new bool[]{true, true, false, true } },
            new NodePoint{ Location = new Vector2(37,   10), Exits = new bool[]{true, false, true, false}},

            new NodePoint{ Location = new Vector2(13,   11),Exits = new bool[]{false, true, false, true } },
            new NodePoint{ Location = new Vector2(16,   11),Exits = new bool[]{true, true, true, false } }, //link
            new NodePoint{ Location = new Vector2(22,   11),Exits = new bool[]{true, true, true, false } }, //link
            new NodePoint{ Location = new Vector2(25,   11),Exits = new bool[]{true, false, false, true } },

            new NodePoint{ Location = new Vector2(5,    14),Exits = new bool[]{false, true, true, true } },
            new NodePoint{ Location = new Vector2(13,   14),Exits = new bool[]{true, false, true, true } },
            new NodePoint{ Location = new Vector2(25,   14),Exits = new bool[]{false, true, true, true } },
            new NodePoint{ Location = new Vector2(33,   14),Exits = new bool[]{true, false, true, true } },

            new NodePoint{ Location = new Vector2(13,   18),Exits = new bool[]{false, true, true, true } },// link
            new NodePoint{ Location = new Vector2(25,   18),Exits = new bool[]{true, false, true, true } },//link

            new NodePoint{ Location = new Vector2(5,    22),Exits = new bool[]{false, true, true, false } },
            new NodePoint{ Location = new Vector2(13,   22),Exits = new bool[]{true, false, true, false } },
            new NodePoint{ Location = new Vector2(25,   22),Exits = new bool[]{false, true, true, false } },
            new NodePoint{ Location = new Vector2(33,   22),Exits = new bool[]{true, false, true, false } },
        };

        public static void LoadContent(ContentManager Content, GraphicsDevice device)
        {
            for (int i = 0; i < 4; i++)
            {
                GameSprite ghost = new GhostSprite();
                ghost.SpriteTexture = new Texture2D[1];
                ghost.SpriteTexture[0] = Content.Load<Texture2D>("Ghost");
                //SetupSprite(ref ghost, 0.10f, 320.0f, 4 * 32, 13 * 32);
                ghost.SpriteRectangle.Width = 64;
                ghost.SpriteRectangle.Height = 80;
                ghost.XSpeed = 2;
                ghost.YSpeed = 2;
                ghost.Direction = Nodes[(int)Direction.Right];
                Ghosts.Add((GhostSprite)ghost);
            }

            Ghosts[0].X = 20 * 32;
            Ghosts[0].Y = 11 * 32;
            Ghosts[0].Target.X = 35;
            Ghosts[0].Target.Y = 6;

            Ghosts[1].X = 18 * 32;
            Ghosts[1].Y = 11 * 32;
            Ghosts[1].Direction = Nodes[(int)Direction.Left];
            Ghosts[1].effects = SpriteEffects.FlipHorizontally;
            Ghosts[1].Target.X = 4;
            Ghosts[1].Target.Y = 6;

            Ghosts[2].X = 18 * 32;
            Ghosts[2].Y = 18 * 32;
            Ghosts[2].Direction = Nodes[(int)Direction.Left];
            Ghosts[2].effects = SpriteEffects.FlipHorizontally;
            Ghosts[2].Target.X = 8;
            Ghosts[2].Target.Y = 20;

            Ghosts[3].X = 20 * 32;
            Ghosts[3].Y = 18 * 32;
            Ghosts[3].Target.X = 31;
            Ghosts[3].Target.Y = 20;
            Ghosts[3].XSpeed = 2.5f;
            Ghosts[3].YSpeed = 2.5f;

            Ghosts[0].GhostColor = Color.Orange;
            Ghosts[1].GhostColor = Color.Aqua;
            Ghosts[2].GhostColor = Color.Pink;
            Ghosts[3].GhostColor = Color.Red;

        }
        public static void CheckCollision(PlayerSprite player)
        {
            foreach(GhostSprite ghost in Ghosts)
            {
                if(player.CollisionRectangle.Intersects(ghost.SpriteRectangle))
                {

                }
            }
        }

        public static void Update(Vector2 ScrollOffsets, PlayerSprite player)
        {
            foreach (GhostSprite ghost in Ghosts)
            {
                ghost.Update();
                //ghost.Update(CookieSprite, collisionRectangles);
                foreach (var point in NodePoints)
                {
                    if (point.Location == ghost.MapPosition)
                    {
                        ghost.SetNextVector(new Vector2((ScrollOffsets.X + player.X) / 32, (ScrollOffsets.Y + player.Y) / 32), point);
                    }
                }

                ghost.X += ghost.Direction.X * (int)ghost.XSpeed;
                ghost.Y += ghost.Direction.Y * (int)ghost.YSpeed;
                ghost.UpdateMapPosition();

            }
        }

        public static void Draw(SpriteBatch spriteBatch, Vector2 ScrollOffsets)
        {
            foreach (GhostSprite Ghost in Ghosts)
            {

                spriteBatch.Draw(
                    Ghost.SpriteTexture[0],
                    new Rectangle((int)(Ghost.SpriteRectangle.X - ScrollOffsets.X),
                    (int)(Ghost.SpriteRectangle.Y - ScrollOffsets.Y), 64, 96),
                    null,
                    Ghost.GhostColor,
                    0.0f,
                    new Vector2(0, 0),
                    Ghost.effects, 1.0f);

               
            }
        }
    }
}
