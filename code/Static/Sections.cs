using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {

        private static readonly string s_nl = Environment.NewLine;
        private static List<ISerializable> s_sections;

        public static string CreatePreProcIfNotCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += "#if !(" + expr + ")" + s_nl;
            s += stream;

//            return s;
            return stream; //TODO: proper serialization
        }


        public static string CreatePreProcIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            s += "#if " + expr + s_nl;
            s += stream;

//            return s;
            return stream; //TODO: proper serialization
        }

        public static string CreatePreProcElseCondition(string ifstream, string elsestream)
        {
            string s = string.Empty;

            s += ifstream;
            s += "#else" + s_nl;
            s += elsestream;
            s += "#endif";// + s_nl;

//            return s;
            return ifstream+elsestream; //TODO: proper serialization
        }

        public static string CreatePreProcEndCondition(string stream)
        {
            string s = string.Empty;

            s += stream + s_nl;
            s += "#endif";

//            return s;
            return string.Empty; //TODO: proper serialization
        }

        public static string AddActionSection(string stream)
        {
            if (!string.IsNullOrEmpty(stream))
                return new Section(Section.ActionType, stream).Serialize();
            else
                return string.Empty;
        }

        public static string AddAssetSection(string stream)
        {
            if (!string.IsNullOrEmpty(stream))
                return new Section(Section.AssetType, stream).Serialize();
            else
                return string.Empty;
        }

        public static string AddDefineSection(string stream)
        {
            if (!string.IsNullOrEmpty(stream))
                return new Section(Section.DefineType, stream).Serialize();
            else
                return string.Empty;
        }

        public static string AddGlobalSection(string stream)
        {
            if (!string.IsNullOrEmpty(stream))
                return new Section(Section.GlobalType, stream).Serialize();
            else
                return string.Empty;
        }

        public static string AddObjectSection(string stream)
        {
            if (!string.IsNullOrEmpty(stream))
                return new Section(Section.ObjectType, stream).Serialize();
            else
                return string.Empty;
        }

        public static string FormatInit()
        {
            return "//TODO: add init sections here";
        }

        public static string Format()
        {
            return string.Join(s_nl, s_sections.Select(x => x.Format()));
        }

        public static void Deserialize(string stream)
        {
            //Console.WriteLine(stream);
            s_sections = Section.DeserializeList(stream);
        }
    }
}
