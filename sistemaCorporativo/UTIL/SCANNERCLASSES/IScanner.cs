using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sistemaCorporativo.UTIL.FingerGraph.Scanner
{
    internal delegate void ImageReceivedHandler(object sender, Image img);

    internal interface IScanner {
        event ImageReceivedHandler ImageReceived;
    }
}
