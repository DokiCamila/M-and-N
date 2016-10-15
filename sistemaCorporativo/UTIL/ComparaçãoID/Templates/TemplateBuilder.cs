using System;
using System.Collections.Generic;
using System.Text;
using ComparacaoDPCI.Geral;

namespace ComparacaoDPCI.Templates
{
    public sealed class TemplateBuilder : ICloneable
    {
        public enum MinutiaType
        {
            Extremidade = 0,
            Bifurcação = 1
        }

        public class Minutia
        {
            public Point Position;
            public byte Direction;
            public MinutiaType Type;
        }

        public int OriginalDpi;
        public int OriginalWidth;
        public int OriginalHeight;

        public int StandardDpiWidth
        {
            get { return Calc.DivRoundUp(OriginalWidth * 500, OriginalDpi); }
            set { OriginalWidth = value * OriginalDpi / 500; }
        }
        public int StandardDpiHeight
        {
            get { return Calc.DivRoundUp(OriginalHeight * 500, OriginalDpi); }
            set { OriginalHeight = value * OriginalDpi / 500; }
        }

        public List<Minutia> Minutiae = new List<Minutia>();

        public TemplateBuilder Clone()
        {
            return new Template(this).ToTemplateBuilder();
        }

        object ICloneable.Clone() { return Clone(); }
    }
}
