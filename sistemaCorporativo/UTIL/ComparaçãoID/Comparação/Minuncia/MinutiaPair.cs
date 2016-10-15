using System;
using System.Collections.Generic;
using System.Text;

namespace ComparacaoDPCI.Comparacao.Minuncia
{
    public struct MinutiaPair
    {
        public short Probe;
        public short Candidate;

        public MinutiaPair(int probe, int candidate)
        {
            Probe = (short)probe;
            Candidate = (short)candidate;
        }
    }
}
