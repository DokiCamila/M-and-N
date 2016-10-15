﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComparacaoDPCI.Comparacao.Minuncia
{
    public struct EdgeLocation
    {
        public short Reference;
        public short Neighbor;

        public EdgeLocation(int reference, int neighbor)
        {
            Reference = (short)reference;
            Neighbor = (short)neighbor;
        }
    }
}
