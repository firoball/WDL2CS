using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {
        private static List<ISerializable> s_sections = new List<ISerializable>();

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

        public static void Format(StringBuilder sb, bool isInitialized)
        {
            foreach (ISerializable section in s_sections)
            {
                if (section.IsInitialized() == isInitialized)
                {
                    section.Format(sb);
                    sb.AppendLine();
                }
            }
        }

        public static void Deserialize(ref string stream)
        {
            //Console.WriteLine(stream);
            List<ISerializable> sections = Section.DeserializeList(ref stream);

            //Verify section list
            foreach (ISerializable section in sections)
            {
                AddSection(section, s_sections);
            }
        }

        private static void AddSection(ISerializable section, List<ISerializable> sections)
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
