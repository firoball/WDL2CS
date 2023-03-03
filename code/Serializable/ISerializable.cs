using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    interface ISerializable
    {
        string Serialize();
        string Format();
        bool IsInitialized();
    }
}
