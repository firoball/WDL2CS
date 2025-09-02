using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Object : Node, ISection
    {
        private static ObjectTransformer s_transformer = null;

        private Node m_name;
        private Node m_type;
        private readonly bool m_isInitialized;
        private readonly Node m_string;
        private List<Property> m_properties;

        public static new ObjectTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public string Name { get => m_name.Data; } 
        public string Type { get => m_type.Data; }

        public Object(Node type, Node name, bool isInitialized)
        {
            m_type = type;
            m_name = name;
            m_isInitialized = isInitialized;
            m_properties = new List<Property>();
            Setup();
        }

        public Object() : this(new Node(), new Node()) { }

        public Object(Node type, Node name) : this(type, name, false) { }


        public Object(Node type, Node name, bool isInitialized, List<Property> properties) : this(type, name, isInitialized)
        {
            if (properties != null)
            {
                SetupProperties(properties);
            }
        }

        public Object(Node type, Node name, bool isInitialized, Node str) : this(type, name, isInitialized)
        {
            m_string = str;
        }


        public bool IsInitialized()
        {
            return m_isInitialized;
        }

        public void Transform(object obj)
        {
            s_transformer?.Transform(obj, m_name, m_type, m_string, m_properties, m_isInitialized);
        }

        private void Setup()
        {
            m_type.NodeType = NodeType.Reserved;
            if (m_isInitialized) //format predefined Skill
                m_name.NodeType = NodeType.Skill;
            else
                m_name.NodeType = NodeType.Identifier;
        }

        private void SetupProperties(List<Property> unprocessedProperties)
        {
            //sort properties alphabetically
            List<Property> properties = unprocessedProperties.OrderBy(x => x.Name).ToList(); //TODO: sort AFTER formatting (as previously)

            foreach (Property property in properties)
            {
                //add property to active dataset
                AddProperty(property, m_properties);

                //Synonyms need special treatment: convert from WDL object with properties to C# object reference
                if (m_type.Data.ToLower().Equals("synonym") && property.Name.ToLower().Equals("type")) //TEMP
                {
                    property.Values[0].NodeType = NodeType.Reserved;
                }
            }

            //find inlined "way"s and build their nodes and sections
            foreach (Property property in m_properties)
            {
                if (property.Name.ToLower() == "way")
                {
                    if (!Registry.Is("Way", property.Values[0].Data)) //way is unknown
                    {
                        Console.WriteLine("(I) OBJECT add missing Way definition for: " + property.Values[0].Data);
                        Node way = new Object(new Node("Way", NodeType.Reserved), property.Values[0]);
                        Sections.AddSection(way); //add new way on the fly
                        Registry.Register("WAY", property.Values[0].Data); //make way known
                    }
                }

                //synonyms might be declared before the corresponding referenced object/action. Therefore set identifier for property value
                //can initially not detected as such. Address this manually here by enforcing a node of identifer type.
                foreach (Node v in property.Values.Where(x => x.NodeType == NodeType.Default))
                {
                    v.NodeType = NodeType.Identifier;
                }
            }

        }

        private void AddProperty(Property property, List<Property> properties)
        {
            List<string> propertyNames = properties.Select(x => x.Name).ToList();
            if (propertyNames.Contains(property.Name))
            {
                //Eliminate double definitions of properties only where their values can be merged
                if (property.AllowMerge)
                {
                    int i = propertyNames.IndexOf(property.Name);
                    properties[i].Values.AddRange(property.Values);
                }
                else if (property.AllowMultiple)
                {
                    properties.Add(property);
                }
                else
                {
                    //TODO: take first or last definition?
                    Console.WriteLine("(W) OBJECT ignore double definition of property: " + property.Name);
                }
            }
            else
            {
                properties.Add(property);
            }
        }

    }
}
