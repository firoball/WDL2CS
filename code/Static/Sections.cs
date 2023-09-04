using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {

        private static SerializableData s_serializableData;
        private static string s_shadowDefinitions = string.Empty;

        public static string ShadowDefinitions { get => s_shadowDefinitions; set => s_shadowDefinitions = value; }

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

        public static string AddDummySection(string stream)
        {
            Console.WriteLine("(W) SECTIONS ignore invalid section: " + stream);
            return string.Empty;
        }

        public static string FormatInit()
        {
            return s_serializableData.InitSectionStream.Append(s_shadowDefinitions).ToString();
        }

        public static string Format()
        {
            return s_serializableData.SectionStream.ToString();
            //return string.Join(s_nl, s_sections.Select(x => x.Format()));
        }

        public static void Deserialize(ref string stream)
        {
            //Console.WriteLine(stream);
            List<ISerializable> sections = Section.DeserializeList(ref stream);
            ProcessSections(sections);
        }

        private static void ProcessSections(List<ISerializable> sections)
        {
            string p = string.Empty;
            PreProcessorStack<SerializableData> stack = new PreProcessorStack<SerializableData>();
            SerializableData serializableData = stack.Content;

            foreach (ISerializable section in sections)
            {
                //add property to active dataset
                AddSection(stack, section, serializableData.Sections);
            }

            serializableData = stack.Merge();

            //copy formatted data to static interface
            s_serializableData = serializableData;
        }

        private static void AddSection(PreProcessorStack<SerializableData> stack, ISerializable section, List<ISerializable> sections)
        {
            IEnumerable<string> sectionNamesTypes = sections.Select(x => x.Name + "@" + x.Type);
            IEnumerable<string> sectionNames = sections.Select(x => x.Name);
            if (sectionNamesTypes.Contains(section.Name + "@" + section.Type))
            {
                //TODO: find out whether 1st (delete) or last (move to Initialize routine) definition is the one evaluated by A3
                Console.WriteLine("(W) SECTIONS ignore double definition: " + section.Name + " (" + section.Type + ")");
            }
            else if (sectionNames.Contains(section.Name))
            {
                string collisions = string.Join(", ", sections.Where(x => x.Name.Equals(section.Name)).Select(x => x.Type).Reverse());
                //TODO: resolve ambiguous namings
                Console.WriteLine("(W) SECTIONS ambiguous definition: " + section.Name + " (" + section.Type + ", " + collisions+")");
                sections.Add(section);
            }
            else
            {
                sections.Add(section);
            }
        }
    }
}
