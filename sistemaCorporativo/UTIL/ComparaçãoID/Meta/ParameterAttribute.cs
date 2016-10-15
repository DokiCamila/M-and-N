using System;
using System.Collections.Generic;
using System.Text;

namespace ComparacaoDPCI.Meta
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ParameterAttribute : Attribute
    {
        // Padrões:
        // Int32: Inferior = 1, Superior = 1000
        // Uníco: Inferior = 0, Superior = 1, Precisão = 2
        // Byte (angulo): Inferior = 0, Superior = Angle.PIB

        bool LowerIsDefaultValue = true;
        double LowerValue;
        public double Lower
        {
            get { return LowerValue; }
            set { LowerValue = value; LowerIsDefaultValue = false; }
        }
        public bool LowerIsDefault { get { return LowerIsDefaultValue; } }

        bool UpperIsDefaultValue = true;
        double UpperValue;
        public double Upper
        {
            get { return UpperValue; }
            set { UpperValue = value; UpperIsDefaultValue = false; }
        }
        public bool UpperIsDefault { get { return UpperIsDefaultValue; } }

        int PrecisionValue = 2;
        public int Precision
        {
            get { return PrecisionValue; }
            set { PrecisionValue = value; }
        }

        public bool TuningDisabled { get { return !LowerIsDefaultValue && !UpperIsDefaultValue && LowerValue == UpperValue; } }
    }
}
