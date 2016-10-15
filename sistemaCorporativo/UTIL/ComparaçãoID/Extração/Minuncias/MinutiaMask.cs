using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ComparacaoDPCI.Geral;
using ComparacaoDPCI.Meta;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Extracao.Minuncia
{
    public sealed class MinutiaMask
    {
        [DpiAdjusted(Min = 0)]
        [Parameter(Lower = 0, Upper = 50)]
        public float DirectedExtension = 10;

        public DetailLogger.Hook Logger = DetailLogger.Null;

        public void Filter(TemplateBuilder template, BinaryMap mask)
        {
            template.Minutiae.RemoveAll(minutia =>
            {
                var arrow = Calc.Round(-DirectedExtension * Angle.ToVector(minutia.Direction));
                return !mask.GetBitSafe((Point)minutia.Position + new Size(arrow), false);
            });
            Logger.Log(template);
        }
    }
}
