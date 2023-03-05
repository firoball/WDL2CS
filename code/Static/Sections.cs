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

            //wrap preprocessor statement into section object
            string pre = new Preprocessor("#if", "!(" + expr + ")").Serialize();
            s += new Section(Section.PreProcType, pre).Serialize();
            s += stream;

            return s;
        }

        public static string CreatePreProcIfCondition(string expr, string stream)
        {
            string s = string.Empty;

            //wrap preprocessor statement into section object
            string pre = new Preprocessor("#if", expr).Serialize();
            s += new Section(Section.PreProcType, pre).Serialize();
            s += stream;

            return s;
        }

        public static string CreatePreProcElseCondition(string ifstream, string elsestream)
        {
            string s = string.Empty;
            string pre = string.Empty;

            //wrap preprocessor statements into section object
            s += ifstream;
            pre = new Preprocessor("#else").Serialize();
            s += new Section(Section.PreProcType, pre).Serialize();
            s += elsestream;
            pre = new Preprocessor("#endif").Serialize();
            s += new Section(Section.PreProcType, pre).Serialize();

            return s;
        }

        public static string CreatePreProcEndCondition(string stream)
        {
            string s = string.Empty;

            //wrap preprocessor statement into section object
            s += stream;
            string pre = new Preprocessor("#endif").Serialize();
            s += new Section(Section.PreProcType, pre).Serialize();

            return s;
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
