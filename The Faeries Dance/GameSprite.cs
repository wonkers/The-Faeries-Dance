using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheFaeriesDance
{
    class GameSprite
    {
        public enum Facing
        {
            Right,
            Left
        }
        public Texture2D[] SpriteTexture;// = new Texture2D[2];
        public Rectangle SpriteRectangle;

        public float X;
        public float Y;
        public float XSpeed;
        public float YSpeed;
        public float WidthFactor;
        public float TicksToCrossScreen;
        public Vector2 Direction;
        public Facing facing;
        public SpriteEffects effects;


    }

    
}
