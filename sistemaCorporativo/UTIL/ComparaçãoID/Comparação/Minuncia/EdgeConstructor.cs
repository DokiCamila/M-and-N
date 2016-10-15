using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Comparacao.Minuncia
{
    public sealed class EdgeConstructor
    {
        public EdgeShape Construct(Template template, int reference, int neighbor)
        {
            PolarPoint polar = Angle.ToPolar(Calc.Difference(template.Minutiae[neighbor].Position, template.Minutiae[reference].Position));
            EdgeShape edge;
            edge.Length = (short)polar.Distance;
            edge.ReferenceAngle = Angle.Difference(template.Minutiae[reference].Direction, polar.Angle);
            edge.NeighborAngle = Angle.Difference(template.Minutiae[neighbor].Direction, Angle.Opposite(polar.Angle));
            return edge;
        }
    }
}
