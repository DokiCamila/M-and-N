using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;
using ComparacaoDPCI.Meta;
using ComparacaoDPCI.Extracao.Modelo;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Extracao.Minuncia
{
    public sealed class MinutiaCollector
    {
        [DpiAdjusted(Min = 2)]
        [Parameter(Lower = 3, Upper = 50)]
        public int DirectionSegmentLength = 21;
        [DpiAdjusted(Min = 0)]
        [Parameter(Lower = 0, Upper = 20)]
        public int DirectionSegmentSkip = 3;

        public DetailLogger.Hook Logger = DetailLogger.Null;

        byte ComputeDirection(SkeletonBuilder.Ridge ridge)
        {
            int first = DirectionSegmentSkip;
            int last = DirectionSegmentSkip + DirectionSegmentLength - 1;
            
            if (last >= ridge.Points.Count)
            {
                int shift = last - ridge.Points.Count + 1;
                last -= shift;
                first -= shift;
            }
            if (first < 0)
                first = 0;

            return Angle.AtanB(ridge.Points[first], ridge.Points[last]);
        }

        public void Collect(SkeletonBuilder skeleton, TemplateBuilder.MinutiaType type, TemplateBuilder template)
        {
            foreach (SkeletonBuilder.Minutia skeletonMinutia in skeleton.Minutiae)
            {
                if (skeletonMinutia.Valid && skeletonMinutia.Ridges.Count == 1)
                {
                    TemplateBuilder.Minutia templateMinutia = new TemplateBuilder.Minutia();
                    templateMinutia.Type = type;
                    templateMinutia.Position = skeletonMinutia.Position;
                    templateMinutia.Direction = ComputeDirection(skeletonMinutia.Ridges[0]);
                    template.Minutiae.Add(templateMinutia);
                }
            }
            Logger.Log(template);
        }
    }
}
