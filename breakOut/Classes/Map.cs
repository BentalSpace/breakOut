﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breakOut {
    class Map {
        /*
         * 0 : 비어있는 곳
         * 1 : 빨
         * 2 : 주
         * 3 : 노
         * 4 : 초
         * 5 : 파
         * 6 : 남
         * 7 : 보
         */
        public int[,] brickMap = new int[,] {
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
            {0,2,2,2,2,2,2,2,2,2,2,2,2,2,0 },
            {0,3,3,3,3,3,3,3,3,3,3,3,3,3,0 },
            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,0 },
            {0,5,5,5,5,5,5,5,5,5,5,5,5,5,0 },
            {0,6,6,6,6,6,6,6,6,6,6,6,6,6,0 },
            {0,7,7,7,7,7,7,7,7,7,7,7,7,7,0 },
        };
    }
}