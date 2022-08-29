using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using SharpDX.Direct3D11;


namespace TheFaeriesDance
{
    enum GameState
    { 
        Splash,
        Menu,
        Begin,
        Play,
        Die,
        End,
        Score
    }
    enum Shape
    {
        Sphere,
        Box,
        Grid
    }
    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    struct Playfield
    {
        int width;
        int height;
        Tile[,] tiles;

        public Playfield(int w, int h, Vector2 tileSize)
        {
            width = w;
            height = h;
            int tilesX = width / (int)tileSize.X;
            int tilesY = height /(int) tileSize.Y;
            tiles = new Tile[tilesY, tilesX];
        }
    }


    struct Tile
    {
        Rectangle rectangle;
        Texture2D texture;
        public Tile(int x, int y, int width, int height, Texture2D tex)
        {
            rectangle = new Rectangle(x, y, width, height);
            texture = tex;
        }
        public Tile(Texture2D tex)
        {
            rectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            texture = tex;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public bool Intersects(Rectangle target)
        {
            return rectangle.Intersects(target);
        }
        public bool Contains(Rectangle target)
        {
            return rectangle.Contains(target);
        }
        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            rectangle.Deconstruct(out x, out y, out width, out height);
        }
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            rectangle.Inflate(horizontalAmount, verticalAmount);
        }
        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            rectangle.Inflate(horizontalAmount, verticalAmount);
        }
        public void Offset(int offsetX, int offsetY)
        {
            rectangle.Offset(offsetX, offsetY);
        }
        public void Offset(float offsetX, float offsetY)
        {
            rectangle.Offset(offsetX, offsetY);
        }
        public void Offset(Point amount)
        {
            rectangle.Offset(amount);
        }
        public void Offset(Vector2 amount)
        {
            rectangle.Offset(amount);
        }
        public bool IsEmpty
        {
            get { return rectangle.IsEmpty; }
        }
        public int X
        {
            get { return rectangle.X; }
            set { rectangle.X = value; }
        }
        public int Y
        {
            get { return rectangle.Y; }
            set { rectangle.Y = value; }
        }
        public int Width
        {
            get { return rectangle.Width; }
            set { rectangle.Width = value; }
        }
        public int Height
        {
            get { return rectangle.Height; }
            set { rectangle.Height = value; }
        }
        public int Left
        {
            get { return rectangle.Left; }
        }
        public int Right
        {
            get { return rectangle.Right; }
        }
        public int Top
        {
            get { return rectangle.Top; }
        }
        public int Bottom
        {
            get { return rectangle.Bottom; }
        }
        public Point Center
        {
            get { return rectangle.Center; }
        }
        public Point Location
        {
            get { return rectangle.Location; }
            set { rectangle.Location = value; }
        }
        public Point Size
        {
            get { return rectangle.Size; }
            set { rectangle.Size = value; }
        }
    }


    class PlayerSprite : GameSprite
    {
        enum State
        {
            Alive,
            NotAlive
        }

        public Rectangle CollisionRectangle;
        public int JumpPower = 100;
        private int lives = 0;
        public void SetLives(int value) { lives = value; }
        public int GetLIves() { return lives; }
    }    

    class NodePoint
    {
        public Vector2 Location = Vector2.Zero;
        public bool[] Exits;// = new bool[4];
    }

    struct DisplaySettings
    {
        public float Width;
        public float Height;
        public float OverScanPercentage;
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;

        public float GetPercentage(float percentage, float inputValue)
        {
            return (inputValue * percentage) / 100;
        }

        public void SetupScreen(GraphicsDevice device)
        {
            OverScanPercentage = 10.0f;
            Width = device.Viewport.Width;
            Height = device.Viewport.Height;
            float xOverscanMargin = GetPercentage(OverScanPercentage, Width) / 2.0f;
            float yOverscanMargin = GetPercentage(OverScanPercentage, Height) / 2.0f;
            MinX = xOverscanMargin;
            MinY = yOverscanMargin;
            MaxX = Width - xOverscanMargin;
            MaxY = Height - yOverscanMargin;
        }
    }

    public class Game1 : Game
    {
        public static int Score = 0;
        public static int light = 0;
        Color Intensity = new Color(95, 95, 95);
        Texture2D testTex;
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private DisplaySettings displaySettings;
        int frame = 0;
        bool bump = false;
        Rectangle JumpMeter = new Rectangle(0, 512 - 48, 100, 32);
        GameState gameState = GameState.Play;

        AudioEngine audioEngine;
        WaveBank waveBank;
        public static SoundBank soundBank;

        float jump = 100;

        PlayerSprite CookieSprite;
        
        List<Texture2D> tileTextures = new List<Texture2D>();

        List<Rectangle> collisionRectangles = new List<Rectangle>();

        List<Texture2D> objectTextures = new List<Texture2D>();

        Vector2 ScrollOffsets = new Vector2(0, 0);
        Texture2D AmigaFont;
       // int Score = 0;

        bool Release = true;

        int increment = 1;


        void SetupSprite(
           ref GameSprite sprite, float widthFactor, float ticksToCrossScreen, float x, float y)
        {
            sprite.WidthFactor = widthFactor;
            sprite.TicksToCrossScreen = ticksToCrossScreen;
            sprite.SpriteRectangle.Width = (int)((displaySettings.Width * widthFactor) + 0.5f);
            float aspectRatio = (float)sprite.SpriteTexture[0].Width / sprite.SpriteTexture[0].Height;
            sprite.SpriteRectangle.Height = (int)((sprite.SpriteRectangle.Width / aspectRatio) + 0.5f);
            sprite.X = x;
            sprite.Y = y;
            sprite.XSpeed = (int)displaySettings.Width / ticksToCrossScreen;
            sprite.YSpeed = (int)sprite.XSpeed;

            
            sprite.effects = SpriteEffects.None;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here

            Window.Title = "The Faeries Dance";
            device = _graphics.GraphicsDevice;
           

            _graphics.PreferredBackBufferHeight = 512;
            _graphics.PreferredBackBufferWidth = 640;
            //_graphics.IsFullScreen = true;
            //_graphics.SynchronizeWithVerticalRetrace = false;

            _graphics.ApplyChanges();

            displaySettings.SetupScreen(device);

            IsFixedTimeStep = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(device);

            // TODO: use this.Content to load your game content here

            spriteFont = Content.Load<SpriteFont>("Arial");
            AmigaFont = Content.Load<Texture2D>("R");
            //spriteFont = new SpriteFont(AmigaFont, )
            List<Rectangle> glyphBounds = new List<Rectangle>();
            List<Rectangle> croppings = new List<Rectangle>();
            List<char> characters = new List<char>();
            int linespacing = spriteFont.LineSpacing;
            Single spacing = spriteFont.Spacing;
            List<Vector3> kerning = new List<Vector3>();
            
            foreach(var glyph in spriteFont.Glyphs)
            {
                glyphBounds.Add(glyph.BoundsInTexture);
                croppings.Add(new Rectangle());// glyph.Cropping);
                characters.Add(glyph.Character);
                kerning.Add(new Vector3(2, 0, 1));// new Vector3(glyph.LeftSideBearing, glyph.Width, glyph.RightSideBearing));
            }
            int a = 0;
            
            glyphBounds.Add(new Rectangle());
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 32; x++)
                {
                    glyphBounds[a] = new Rectangle(x * 8, y * 18, 8, 18);

                    a++;
                }
            spriteFont = new SpriteFont(AmigaFont, glyphBounds, croppings, characters, linespacing, spacing, kerning, null);

            MapData.Background = Content.Load<Texture2D>("ForestBackground1");
            MapData.Paralax = Content.Load<Texture2D>("ForestBackground2");

            CookieSprite = new PlayerSprite();
            CookieSprite.SpriteTexture = new Texture2D[2];
            CookieSprite.SpriteTexture[0] = Content.Load<Texture2D>("cookie1");
            CookieSprite.SpriteTexture[1] = Content.Load<Texture2D>("cookie2");
            CookieSprite.X = 32;
            CookieSprite.Y = 0;
            CookieSprite.SpriteRectangle.Width = 64;
            CookieSprite.SpriteRectangle.Height = 96;
            CookieSprite.CollisionRectangle.X = CookieSprite.SpriteRectangle.X;
            CookieSprite.CollisionRectangle.Y = CookieSprite.SpriteRectangle.Y;
            CookieSprite.CollisionRectangle.Width = 32;
            CookieSprite.CollisionRectangle.Height = 92;
            CookieSprite.XSpeed = 2.0f;
            CookieSprite.SetLives(3);

            GhostController.LoadContent(Content, device);
            MapData.tiles = Content.Load<Texture2D>("tiles");
            MapData.tileData = new Color[MapData.tiles.Width * MapData.tiles.Height];
            MapData.tiles.GetData(MapData.tileData);
            
            Rectangle rect = new Rectangle(0, 0, 16, 16);

            for(int j = 0; j < MapData.tiles.Height/16; j++)
            {
                for(int i = 0; i < MapData.tiles.Width/16; i++)
                {
                    rect.X = i*16;
                    rect.Y = j*16;
                    Color[] data = new Color[16 * 16];

                    for (int x = 0; x < rect.Width; x++)
                        for (int y = 0; y < rect.Height; y++)
                            data[x + y * rect.Width] = MapData.tileData[x + rect.X + (y + rect.Y) * MapData.tiles.Width];


                    Texture2D tileTexture = new Texture2D(device, 16, 16);
                    tileTexture.SetData(data);
                    tileTextures.Add(tileTexture);
                }
            }
            //320 x 96
            Texture2D forestObjects = Content.Load<Texture2D>("ForestObjects");
            Color[] objectData = new Color[forestObjects.Width * forestObjects.Height];
            forestObjects.GetData(objectData);
            Rectangle ObjectRect = new Rectangle(0, 0, 80, 96);
            for(int j = 0; j < forestObjects.Height/96; j++)
            {
                for(int i = 0; i < forestObjects.Width/80; i++)
                {
                    ObjectRect.X = i * 80;
                    ObjectRect.Y = j * 96;
                    Color[] data = new Color[96 * 80];
                    for (int x = 0; x < ObjectRect.Width; x++)
                        for (int y = 0; y < ObjectRect.Height; y++)
                            data[x + y * ObjectRect.Width] = objectData[x + ObjectRect.X + (y + ObjectRect.Y) * forestObjects.Width];

                    Texture2D objectTexture = new Texture2D(device, 80, 96);
                    objectTexture.SetData(data);
                    objectTextures.Add(objectTexture);
                }
            }

            ButterflyController.LoadContent(Content, device);
            
            //Create Collision Tiles (platforms/walls)
            int z = 0;
            for(int y = 0; y < 30; y++)
            {
                for(int x = 0; x < 40; x++)
                {
                    if (MapData.FrontMap[z] != 7)
                    {
                        //16 x 16?
                        collisionRectangles.Add(new Rectangle(x * 32, y * 32, 32, 32));
                    }
                    z++;
                }
            }
            Rectangle testRect = new Rectangle((8 << 5), (19 << 5), 32, 32);
            Rectangle testRect2 = new Rectangle((31 << 5), (19 << 5), 32, 32);
            collisionRectangles.Add(testRect);
            collisionRectangles.Add(testRect2);

            CookieSprite.Y = 512 - 192;
            CookieSprite.X += 64;
            
            ScrollOffsets.Y = 512;

            CookieSprite.CollisionRectangle.X = (int)CookieSprite.X+16;
            CookieSprite.CollisionRectangle.Y = (int)(CookieSprite.Y - ScrollOffsets.Y);
            CookieSprite.CollisionRectangle.Width = 42;
            CookieSprite.CollisionRectangle.Height = 80;

            testTex = new Texture2D(device, 640, 64);
            Color[] colours = new Color[testTex.Width * testTex.Height];
            testTex.GetData(colours);
            for (int c = 0; c < colours.Length; c++)
            {
                colours[c] = Color.White;
            }
            testTex.SetData(colours);

            SparkleController.LoadContent(Content, device);

            audioEngine = new AudioEngine("Content\\Sounds\\Win\\GameSounds.xgs");
            waveBank = new WaveBank(audioEngine,"Content\\Sounds\\Win\\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Content\\Sounds\\Win\\Sound Bank.xsb");
            soundBank.PlayCue("music");

            GC.Collect();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Splash:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Play:
                    break;
                case GameState.End:
                    break;
                case GameState.Score:
                    break;

            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float oldy = CookieSprite.Y;
            float oldScrolly = ScrollOffsets.Y;
            int result = (int)(ScrollOffsets.Y - (int)CookieSprite.Y) - 64;
            //ScrollOffsets.Y -= (int)result;
           
            // if (CookieSprite.CollisionRectangle.Y < 512+256)
            //     ScrollOffsets.Y--;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                float old = CookieSprite.X;
                CookieSprite.Direction.X = -CookieSprite.XSpeed;
                CookieSprite.X -= CookieSprite.XSpeed;
                CookieSprite.facing = GameSprite.Facing.Left;
                CookieSprite.CollisionRectangle.X = (int)CookieSprite.X +10;

                foreach (var block in collisionRectangles)
                {
                    if (CookieSprite.CollisionRectangle.Intersects(block))
                    {
                        CookieSprite.X = old;
                        CookieSprite.CollisionRectangle.X = (int)old+10;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                float old = CookieSprite.X;
                CookieSprite.Direction.X = CookieSprite.XSpeed;
                CookieSprite.X += CookieSprite.XSpeed;
                CookieSprite.facing = GameSprite.Facing.Right;
                CookieSprite.CollisionRectangle.X = (int)CookieSprite.X +10;

                foreach (var block in collisionRectangles)
                {
                    if (CookieSprite.CollisionRectangle.Intersects(block))
                    {
                        CookieSprite.X = old;
                        CookieSprite.CollisionRectangle.X = (int)old+10;
                    }
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Release && jump > 19)
            { 
                //float old = CookieSprite.Y;
                CookieSprite.Direction.Y = -5.5f;
                CookieSprite.YSpeed -= 5.5f;
                if (CookieSprite.YSpeed < -5.5f) CookieSprite.YSpeed = -5.5f;
                Release = false;
                jump -= 20;
                if (jump < 0)
                    jump = 0;

                if (frame < 2)
                    soundBank.PlayCue("wing2");
                else
                    soundBank.PlayCue("wing1");
            }
            if(Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                
                Release = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CookieSprite.Direction.Y = 1.0f;
                CookieSprite.YSpeed = 2.0f;
               // soundBank.PlayCue("sound1");
            }

            if (CookieSprite.facing == GameSprite.Facing.Left)
            {
                CookieSprite.effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                CookieSprite.effects = SpriteEffects.None;
            }                  

            if (CookieSprite.X > 320 - 32 && CookieSprite.X < 960-32)
            {
                ScrollOffsets.X = CookieSprite.X - (320 - 32);
            }
            else
            {
                CookieSprite.SpriteRectangle.X = (int)(CookieSprite.X-ScrollOffsets.X);
            }


            CookieSprite.Y += CookieSprite.YSpeed;// / 2;
            CookieSprite.CollisionRectangle.Y = (int)(CookieSprite.Y + ScrollOffsets.Y)+16;

            int countHits = 0;
            foreach (Rectangle rect in collisionRectangles)
            {
                //672
                if (CookieSprite.CollisionRectangle.Intersects(rect))
                {
                    countHits++;
                    ScrollOffsets.Y = oldScrolly;
                    if (CookieSprite.YSpeed > 0 && bump == false)
                    {
                        
                        CookieSprite.Y = rect.Top - 96 - ScrollOffsets.Y;
                    }
                    else
                    {
                        CookieSprite.Y = rect.Bottom - ScrollOffsets.Y;
                        bump = true;
                    }

                    CookieSprite.Y = oldy;
                    CookieSprite.CollisionRectangle.Y = (int)(CookieSprite.Y + ScrollOffsets.Y)+16;
                    CookieSprite.YSpeed = 0.5f;
                    break;
                }
                
            }
            if (countHits == 0)
                bump = false;

            CookieSprite.SpriteRectangle.Y = (int)CookieSprite.Y;           

            CookieSprite.YSpeed += 0.5f;
            if (CookieSprite.YSpeed > 1) CookieSprite.YSpeed = 1;

            if (((int)gameTime.TotalGameTime.Ticks & 0x7) == 0x7)
            {
                frame += increment;
                frame &= 0x3;
                
            }
            if(((int)gameTime.TotalGameTime.Ticks &0x1f) == 0x1f)
                soundBank.PlayCue("wing1");


            MapData.ParalaxRectangle.X = -(int)ScrollOffsets.X/4;

            GhostController.CheckCollision(CookieSprite);
            GhostController.Update(ScrollOffsets, CookieSprite);

            if(CookieSprite.Y < 176)
            {
                if(ScrollOffsets.Y > 0)
                {
                    ScrollOffsets.Y-=2;
                    CookieSprite.Y += 2;
                }
            }
            if(CookieSprite.Y > 176)
            {
                if(ScrollOffsets.Y < 512)
                {
                    ScrollOffsets.Y+=2;
                    CookieSprite.Y -= 2;
                }
            }
            ButterflyController.Update(ScrollOffsets, gameTime);
            ButterflyController.CheckCollision(CookieSprite);

            SparkleController.Update(ScrollOffsets, gameTime);
            SparkleController.CheckCollision(CookieSprite);

            Intensity.R = (byte)(95 + light);
            Intensity.G = (byte)(95 + light);
            Intensity.B = (byte)(95 + light);

            if (jump < CookieSprite.JumpPower)
                jump += 0.75f;
            JumpMeter.Width = (int)jump;

            audioEngine.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            switch (gameState)
            {
                case GameState.Splash:
                    break;
                case GameState.Menu:
                    break;
                case GameState.Begin:
                case GameState.Die:
                case GameState.Play:
                    spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateTranslation(0, 0, 0) * Matrix.CreateScale(1, 1, 1));
                    spriteBatch.Draw(MapData.Background, MapData.BackgroundRectangle, Intensity);
                    spriteBatch.Draw(MapData.Paralax, MapData.ParalaxRectangle, Intensity);
                    //spriteBatch.Draw(objectTextures[1], new Rectangle(-32 - (int)ScrollOffsets.X, 384+128  - (int)ScrollOffsets.Y, 160, 224), Color.White);

                    int i = 0;
                    for (int y = 0; y < 30; y++)
                    {
                        for (int x = 0; x < 40; x++)
                        {
                            Vector2 range = new Vector2();
                            range.X = Math.Abs(CookieSprite.CollisionRectangle.X - (x * 32 ));
                            range.Y = Math.Abs(CookieSprite.CollisionRectangle.Y - (x * 32 ));
                            range.Normalize();
                            if (range.X < 0.5f && range.Y < 0.5f)
                            {
                                spriteBatch.Draw(tileTextures[MapData.FrontMap[i++]], new Rectangle(x * 32 - (int)ScrollOffsets.X, y * 32 - (int)ScrollOffsets.Y, 32, 32), Color.White);
                            }
                            else
                            //spriteBatch.Draw(tileTextures[MapData.TheMap[i]], new Rectangle(x * 32 - (int)ScrollOffsets.X, y * 32 - (int)ScrollOffsets.Y, 32, 32), Color.White);
                            spriteBatch.Draw(tileTextures[MapData.FrontMap[i++]], new Rectangle(x * 32 - (int)ScrollOffsets.X, y * 32 - (int)ScrollOffsets.Y, 32, 32), Intensity);
                        }
                    }

                    //spriteBatch.Draw(tileTextures[46], new Rectangle((1 << 5) - (int)ScrollOffsets.X, (26 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    //spriteBatch.Draw(tileTextures[46], new Rectangle((19 << 5) + 16 - (int)ScrollOffsets.X, (18 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[31], new Rectangle((2 << 5) - (int)ScrollOffsets.X, (22 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);

                    spriteBatch.Draw(tileTextures[31], new Rectangle((8 << 5) - (int)ScrollOffsets.X, (25 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[29], new Rectangle((17 << 5) - (int)ScrollOffsets.X, (28 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[31], new Rectangle((9 << 5) - (int)ScrollOffsets.X, (17 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[30], new Rectangle((18 << 5) - (int)ScrollOffsets.X, (20 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);

                    spriteBatch.Draw(tileTextures[31], new Rectangle((9 << 5) + 16 - (int)ScrollOffsets.X, (4 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    //spriteBatch.Draw(tileTextures[46], new Rectangle((19 << 5) + 16 - (int)ScrollOffsets.X, (4 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[30], new Rectangle((19 << 5) + 16 - (int)ScrollOffsets.X, (13 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    //spriteBatch.Draw(tileTextures[46], new Rectangle((29 << 5) + 16 - (int)ScrollOffsets.X, (12 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);
                    spriteBatch.Draw(tileTextures[31], new Rectangle((36 << 5) + 16 - (int)ScrollOffsets.X, (13 << 5) - (int)ScrollOffsets.Y, 32, 32), Color.White);

                    Rectangle root1 = new Rectangle(collisionRectangles[collisionRectangles.Count - 2].X - (int)ScrollOffsets.X,
                        collisionRectangles[collisionRectangles.Count - 2].Y - (int)ScrollOffsets.Y+32, 32, 32);
                    Rectangle root2 = new Rectangle(collisionRectangles[collisionRectangles.Count - 1].X - (int)ScrollOffsets.X+32,
                        collisionRectangles[collisionRectangles.Count - 1].Y - (int)ScrollOffsets.Y, 32, 32);

                    spriteBatch.Draw(tileTextures[46], root1, null, Color.White, MathHelper.ToRadians(90), new Vector2(root1.Width/2, root1.Height/2), SpriteEffects.None, 1.0f);
                    spriteBatch.Draw(tileTextures[46], root2, null, Color.White, MathHelper.ToRadians(-90), new Vector2(root2.Width/2, root2.Height/2), SpriteEffects.None, 1.0f);

                    spriteBatch.Draw(objectTextures[3], new Rectangle(32 - (int)ScrollOffsets.X, 384+256+64 - (int)ScrollOffsets.Y, 160, 224), Color.White);

                    Rectangle temp = new Rectangle(CookieSprite.CollisionRectangle.X - (int)ScrollOffsets.X, CookieSprite.CollisionRectangle.Y - (int)ScrollOffsets.Y,
                        CookieSprite.CollisionRectangle.Width, CookieSprite.CollisionRectangle.Height);


                    foreach (GhostSprite ghost in GhostController.Ghosts)
                    {
                        Rectangle rect = new Rectangle(ghost.SpriteRectangle.X - (int)ScrollOffsets.X, ghost.SpriteRectangle.Y - (int)ScrollOffsets.Y, ghost.SpriteRectangle.Width, ghost.SpriteRectangle.Height);
                       // spriteBatch.Draw(testTex, rect, Color.Blue);
                    }

                    //spriteBatch.Draw(testTex, temp, Color.Red);

                    GhostController.Draw(spriteBatch, ScrollOffsets);

                    ButterflyController.Draw(spriteBatch);

                    
                    spriteBatch.Draw(CookieSprite.SpriteTexture[frame / 2], CookieSprite.SpriteRectangle, null, Color.White, 0.0f, new Vector2(0, 0), CookieSprite.effects, 1.0f);

                    spriteBatch.Draw(testTex, new Vector2(0, 512 - 64), Color.Black);

                    spriteBatch.DrawString(spriteFont, String.Format("Lives : {0}", CookieSprite.GetLIves().ToString()), new Vector2(260, 470), Color.White);
                    spriteBatch.DrawString(spriteFont, String.Format("Score : {0}", Score), new Vector2(460, 470), Color.White);

                    SparkleController.Draw(spriteBatch);
                    foreach(Rectangle r in collisionRectangles)
                    {
                        Rectangle r1 = new Rectangle(r.X - (int)ScrollOffsets.X, r.Y - (int)ScrollOffsets.Y, 32, 32);

                       // spriteBatch.Draw(testTex, r1, Color.AliceBlue);
                    }


                    spriteBatch.Draw(testTex, JumpMeter, Color.Green);
                    spriteBatch.End();
                    break;
                case GameState.End:
                    break;
                case GameState.Score:
                    break;

            }
           
            
            
            base.Draw(gameTime);
        }

       
    }
}
