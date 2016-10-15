using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;

namespace ComparacaoDPCI.Extracao.Modelo
{
    public sealed class BranchMinutiaRemover : ISkeletonFilter
    {
        public DetailLogger.Hook Logger = DetailLogger.Null;

        public void Filter(SkeletonBuilder skeleton)
        {
            foreach (SkeletonBuilder.Minutia minutia in skeleton.Minutiae)
            {
                if (minutia.Ridges.Count > 2)
                    minutia.Valid = false;
            }
            Logger.Log(skeleton);
        }
    }
}
