﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheFaeriesDance
{

    static class MapData
    {
        public static Texture2D tiles;
        public static Color[] tileData;
        public static Texture2D Background;
        public static Texture2D Paralax;
        public static Rectangle BackgroundRectangle = new Rectangle(0, 0, 640, 512);
        public static Rectangle ParalaxRectangle = new Rectangle(0, 0, 640, 512);
        public static byte[] TheMap = new byte[40 * 30]{
            4 ,33,33,33,33,33,33,33,33,33,33,33,33,34,17,17,17,17,32,33,33,34,17,17,17,42,28,33,33,33,33,33,33,33,33,33,33,33,33,33,
            33,33,33,33,33,33,33,33,33,33,33,33,27,42,7 ,7 ,7 ,7 ,40,41 ,41,42,7 ,7 ,7 ,7 ,40,28,33,33,33,33,33,33,33,33,33,33,33,33,
            33,33,33,33,33,33,33,33,33,33,33,27,42,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,40,28,33,33,33,33,33,33,33,33,33,33,33,
            33,33,33,33,33,33,33,33,33,33,27,42,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,40,28,33,33,33,33,33,33,33,33,33,33,
            33,33,33,33,33,33,33,33,33,27,42,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,40,28,33,33,33,33,33,33,33,33,33,
            41,28,33,33,33,33,33,33,27,42,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,40,28,33,33,33,33,33,33,27,42,
            10,40,28,33,33,33,33,27,42,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,40,28,33,33,33,33,27,42,8,
            10,7 ,40,28,33,33,27,42,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,40,28,33,33,27,42,7 ,8,
            10,7 ,7 ,32,33,33,34,7 ,7 ,8 ,11,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,12,10,7 ,7 ,32,33,33,34,7 ,7 ,8,
            10,7 ,7 ,32,33,33,34,7 ,7 ,8 ,3 ,17,17,17,18,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,16,17,17,17,4 ,10,7 ,7 ,32,33,33,34,7 ,7 ,8,
            10,7 ,7 ,32,33,33,34,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,32,33,33,34,7 ,7 ,8,
            10,7 ,7 ,32,33,27,42,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,40,28,33,34,7 ,7 ,8,
            10,7 ,7 ,32,33,34,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,32,33,34,7 ,7 ,8,
            10,7 ,7 ,32,33,34,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,32,33,34 ,7 ,7 ,8,
            11,1 ,1 ,32,33,35,26,7 ,7 ,7 ,7 ,7 ,7 ,24,25,25,25,1 ,1 ,1 ,1 ,1 ,1 ,25,25,25,26,7 ,7 ,7 ,7 ,7 ,7 ,24,36,33,34,1 ,1 ,12,
            9 ,9 ,9 ,32,28,33,35,26,7 ,7 ,7 ,7 ,24,36,33,33,33,33,9 ,9 ,9 ,9 ,9 ,33,33,33,35,26,7 ,7 ,7 ,7 ,24,36,33,27,42,9 ,9 ,9,
            9 ,9 ,9 ,32,40,28,33,35,26,7 ,7 ,24,36,33,33,33,33,33,9 ,9 ,9 ,9 ,9 ,33,28,33,33,35,26,7 ,7 ,24,36,33,27,42,41 ,9 ,9 ,9,
            3 ,33,33,18,7 ,40,28,33,34,7 ,7 ,32,33,33,33,27,42,32,33,33,33,33,34,40,28,33,33,33,34,7 ,24 ,36,33,27,42,7 ,16,32,33,33,
            33,27,42,7 ,7 ,7 ,40,28,33,9 ,9 ,33,33,33,27,42,7 ,40,28,33,33,27,42,7 ,40,28,33,33,33,33,33,33,27,42,7 ,7 ,7 ,40,28,33,
            33,34,7 ,7 ,7 ,7 ,7 ,40,28,33,33,33,33,27,42,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,40,28,33,33,33,33,27,42,7 ,7 ,7 ,7 ,7 ,32,33,
            33,34,7 ,7 ,7 ,7 ,7 ,7 ,40,28,33,33,27,42,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,40,28,33,33,27,42,7 ,7 ,7 ,7 ,7 ,7 ,32,33,
            33,34,7 ,7 ,7 ,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,7 ,7 ,7 ,32,33,
            33,35,26,7 ,7 ,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,24,33,33,33,33,26,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,7 ,7 ,24,36,33,
            33,1 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,40,28,33,33,27,42,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,7 ,7 ,32,33,33,
            33,33,10,7 ,7 ,7 ,7 ,7 ,7 ,32,33,33,34,7,7 ,7 ,7 ,7 ,40,41,41,42,7 ,7 ,7 ,7 ,7 ,32,33,33,34,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9,
            33,33,18,7 ,7 ,7 ,7 ,24,25,36,33,33,35,25,26,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,24,25,36,33,33,35,25,26,7 ,7 ,7 ,7 ,16,32,33,
            33,42,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,1 ,1 ,1, 1,1,2 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,0 ,33,12,33,33,33,33,33,34,2 ,7 ,7 ,7 ,7 ,40,41,
            33,26,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,24,25,
            33,35,26,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,25,26,7 ,24,25,25,26,7 ,24,25,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,24,36,33,
            33,33,33,1 ,1 ,1 ,12,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,11,33,1 ,33,12,11,33,1 ,33,12,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,11,1 ,1 ,1 ,32,33,33
            };

        public static byte[] FrontMap = new byte[40 * 30]{
            4 ,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,5 ,4 ,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,17,5,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,11,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,0 ,2 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,12,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,16,18,7 ,7 ,7 ,8 ,3 ,17,17,17,18,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,16,17,17,17,4 ,10,7 ,7 ,7 ,16,18,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            11,1 ,1 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,12,
            9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9,
            9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9,
            3 ,17,17,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,17,17,17,17,17,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,17,4,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,2 , 7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,2 ,7,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7, 8 ,9 ,9 ,10 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,10,7,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,17,18 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,17,18 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,4 ,3 ,17,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8,
            11,1 ,2 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,0 ,1 ,12,
            9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9,
            3 ,17,18,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,7 ,16,17,4,
            10,7 ,7 ,7 ,7 ,7 ,0 ,1 ,1 ,1 ,1 ,1 ,1, 1,1 ,2 ,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,0 ,1 ,1, 1,1 ,1 ,1 ,1 ,1 ,2 ,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,8,
            10,7 ,7 ,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,8 ,10,7 ,7 ,7 ,8 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,10,7 ,7 ,7 ,7 ,7 ,8,
            11,1 ,1 ,1 ,1 ,1 ,12,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,11,1 ,1 ,1 ,12,11,1 ,1 ,1 ,12,9 ,9 ,9 ,9 ,9 ,9 ,9 ,9 ,11,1 ,1 ,1 ,1 ,1 ,12

            };
    }
}
