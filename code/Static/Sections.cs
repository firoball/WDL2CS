using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Sections
    {
        private static List<ISection> s_sections = new List<ISection>(10000);

        public static List<ISection> List { get => s_sections; }

        public static Node AddSection(Node section)
        {
            if (section != null && section is ISection)
                TryAddSection(section as ISection);
            return null;
        }

        public static Node AddDummySection(Node section)
        {
            Console.WriteLine("(W) SECTIONS ignore invalid section: " + section);
            return null;
        }

        private static void TryAddSection(ISection section)
        {
            IEnumerable<string> sectionNamesTypes = s_sections.Select(x => x.Name + "@" + x.Type);
            IEnumerable<string> sectionNames = s_sections.Select(x => x.Name);
            if (sectionNamesTypes.Contains(section.Name + "@" + section.Type))
            {
                //find position of identical named section and replace it with new section
                int idx = s_sections.FindIndex(x => x.Name == section.Name);
                if (idx != -1)
                    s_sections[idx] = section;
                Console.WriteLine("(W) SECTIONS ignore double definition: " + section.Name + " (" + section.Type + ")");
            }
            else if (sectionNames.Contains(section.Name))
            {
                string collisions = string.Join(", ", s_sections.Where(x => x.Name.Equals(section.Name)).Select(x => x.Type).Reverse());
                //TODO: resolve ambiguous namings
                Console.WriteLine("(W) SECTIONS ambiguous definition: " + section.Name + " (" + section.Type + ", " + collisions + ")");
                s_sections.Add(section);
            }
            else
            {
                s_sections.Add(section);
            }
        }

        private static string[] s_sectionTypes = {
            "bmap", "flic", "model", "music", "sound", "ovly", //Assets
            "overlay", "panel", "palette", "region", "skill", "string", "synonym", "texture", "text", "view", "wall", "way", //Objects
            "action" //Actions
        };

        public static List<ISection>[] GetSortedSections()
        {
            List<ISection>[] sections = new List<ISection>[s_sectionTypes.Length];
            for (int i = 0; i < s_sectionTypes.Length; i++)
            {
                sections[i] = s_sections.Where(x => x.Type.ToLower().Equals(s_sectionTypes[i])).OrderBy(y => y.Name).ToList();
            }
            return sections;
        }

    }
}
