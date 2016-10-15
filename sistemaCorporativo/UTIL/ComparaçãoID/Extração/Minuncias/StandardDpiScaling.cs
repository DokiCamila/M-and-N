using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComparacaoDPCI.Geral;
using ComparacaoDPCI.Meta;
using ComparacaoDPCI.Templates;

namespace ComparacaoDPCI.Extracao.Minuncia
{
    public sealed class StandardDpiScaling
    {
        [DpiAdjusted]
        [Parameter(Lower = 500, Upper = 500)]
        public int DpiScaling = 500;

        public DetailLogger.Hook Logger = DetailLogger.Null;

        public void Scale(TemplateBuilder template)
        {
            float dpiFactor = 500 / (float)DpiScaling;
            foreach (var minutia in template.Minutiae)
                minutia.Position = Calc.Round(Calc.Multiply(dpiFactor, minutia.Position));
            Logger.Log(template);
        }
    }
}
