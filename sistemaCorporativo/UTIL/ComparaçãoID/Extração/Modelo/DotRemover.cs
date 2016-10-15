using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;

namespace ComparacaoDPCI.Extracao.Modelo
{
    public sealed class DotRemover : ISkeletonFilter
    {
        public DetailLogger.Hook Logger = DetailLogger.Null;

        public void Filter(SkeletonBuilder skeleton)
        {
            List<SkeletonBuilder.Minutia> removed = new List<SkeletonBuilder.Minutia>();
            foreach (SkeletonBuilder.Minutia minutia in skeleton.Minutiae)
                if (minutia.Ridges.Count == 0)
                    removed.Add(minutia);
            foreach (SkeletonBuilder.Minutia minutia in removed)
                skeleton.RemoveMinutia(minutia);
            Logger.Log(skeleton);
        }
    }
}
