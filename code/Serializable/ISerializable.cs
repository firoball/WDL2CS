﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    interface ISerializable
    {
        string Name
        {
            get;
        }

        string Type
        {
            get;
        }
        string Serialize();
        void Format(StringBuilder sb);
        bool IsInitialized();
    }
}
