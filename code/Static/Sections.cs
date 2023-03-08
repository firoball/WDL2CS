using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {

        private static readonly string s_nl = Environment.NewLine;
        private static SerializableData s_serializableData;

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
            return s_serializableData.InitSectionStream.ToString();
        }

        public static string Format()
        {
            return s_serializableData.SectionStream.ToString();
            //return string.Join(s_nl, s_sections.Select(x => x.Format()));
        }

        public static void Deserialize(string stream)
        {
            //Console.WriteLine(stream);
            List<ISerializable> sections = Section.DeserializeList(stream);
            ProcessSections(sections); //TODO: right place to call?
        }

        private static void ProcessSections(List<ISerializable> sections)
        {
            string p = string.Empty;
            PreProcessorStack<SerializableData> stack = new PreProcessorStack<SerializableData>();
            SerializableData serializableData = stack.Content;

            foreach (ISerializable section in sections)
            {
                if (section is Preprocessor)
                {
                    Preprocessor pre = section as Preprocessor;
                    switch (pre.Name)
                    {
                        case "#if":
                            //update preprocessor stack and obtain new dataset
                            serializableData = stack.Update(pre.Name, pre.Expression);
                            break;

                        case "#else":
                            //move all previously collected sections into data list
                            ProcessSectionData(serializableData);
                            //update preprocessor stack and move to else branch of active dataset
                            serializableData = stack.Update(pre.Name);
                            break;

                        case "#endif":
                            //move all previously collected properties into data list
                            ProcessSectionData(serializableData);
                            //update preprocessor stack, get previous dataset
                            serializableData = stack.Update(pre.Name);
                            break;

                        default:
                            //this should never be reached - just do nothing
                            break;
                    }
                }
                else
                {
                    //add property to active dataset
                    AddSection(section, serializableData.Sections);
                }
            }

            //take care of properties not enclosed by any preprocessor directive
            if (string.IsNullOrEmpty(stack.Condition))
            {
                ProcessSectionData(serializableData);
            }

            serializableData = stack.Merge();

            //copy formatted data to static interface
            s_serializableData = serializableData;
        }

        private static void ProcessSectionData(SerializableData active)
        {
            List<string> sections = new List<string>();
            List<string> initSections = new List<string>();

            foreach (ISerializable section in active.Sections)
            {
                if (section.IsInitialized())
                    initSections.Add(section.Format());
                else
                    sections.Add(section.Format());
            }

            //sections.Sort(); //TODO: this is dangerous - introduce sort by type
            active.SectionStream.Append(string.Join(s_nl, sections));

            //initSections.Sort(); //TODO: this is dangerous - introduce sort by type
            active.InitSectionStream.Append(string.Join(s_nl, initSections));
        }

        private static void AddSection(ISerializable section, List<ISerializable> sections)
        {
            List<string> sectionNames = sections.Select(x => x.Name).ToList();
            if (sectionNames.Contains(section.Name))
            {
                Console.WriteLine("(W) SECTIONS ignore double definition: " + section.Name);
            }
            else
            {
                sections.Add(section);
            }
        }
    }
}
