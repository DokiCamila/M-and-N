using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComparacaoDPCI.Geral;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Extracao.Minuncia
{
    public sealed class MinutiaShuffler
    {
        public void Shuffle(TemplateBuilder template)
        {
            int seed = 0;
            foreach (var minutia in template.Minutiae)
                seed += minutia.Direction + minutia.Position.X + minutia.Position.Y + (int)minutia.Type;
            template.Minutiae = Calc.Shuffle(template.Minutiae, new Random(seed)).ToList();
        }
    }
}
