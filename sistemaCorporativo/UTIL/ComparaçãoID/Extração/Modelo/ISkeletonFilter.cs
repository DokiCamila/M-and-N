using System;
using System.Collections.Generic;
using System.Text;

namespace ComparacaoDPCI.Extracao.Modelo
{
    public interface ISkeletonFilter
    {
        void Filter(SkeletonBuilder skeleton);
    }
}
