﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Objects
    {
        private static List<string> s_values = new List<string>();

        public static string AddStringObject(string type, string name, string text)
        {
            type = Formatter.FormatObject(type);
            name = Formatter.FormatObjectId(name, type);
            Identifiers.Register(type, name);
            string o = new Object(type, name, text, true).Serialize();

            return o;
        }

        public static string AddObject(string type, string name)
        {
            return AddObject(type, name, string.Empty);
        }

        public static string AddObject(string type, string name, string stream)
        {
            type = Formatter.FormatObject(type);
            name = Formatter.FormatObjectId(name, type);
            Identifiers.Register(type, name);
            string o = new Object(type, name, stream).Serialize();

            return o;
        }

        public static string CreateProperty(string property)
        {
            Property prop = new Property(property, s_values);

            //Clean up
            s_values.Clear();
            return prop.Serialize();
        }

        public static void AddPropertyValue(string value)
        {
            s_values.Insert(0, value);
        }

    }
}
