using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class PropertyList
    {
        private Dictionary<string, List<List<string>>> m_lastAddeditem;

        /* Generic types only for avoiding any type dependencies on transpiler code
         *  Object/Asset type
         *      Object/Asset name
         *          Object/Asset property ID
         *              property values (multiple sets)
         */
        private Dictionary<string, Dictionary<string, Dictionary<string, List<List<string>>>>> m_list = new Dictionary<string, Dictionary<string, Dictionary<string, List<List<string>>>>>();

        public PropertyList()
        {
            m_list.Add("Model", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Sound", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Music", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Flic", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Bmap", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Ovly", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Font", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Palette", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Texture", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Wall", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Region", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Thing", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Actor", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Way", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Text", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Overlay", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("Panel", new Dictionary<string, Dictionary<string, List<List<string>>>>());
            m_list.Add("View", new Dictionary<string, Dictionary<string, List<List<string>>>>());
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<List<string>>>>> List { get => m_list; }

        public Dictionary<string, List<List<string>>> AddItem(string type, string name)
        {
            Dictionary<string, List<List<string>>> item = new Dictionary<string, List<List<string>>>();
            if (m_list.ContainsKey(type))
            {
                m_list[type].Add(name, item);
            }
            m_lastAddeditem = item; //store reference of new item inside class
            return item;
        }

        public void AddProperty(Dictionary<string, List<List<string>>> item, string property, string value)
        {
            AddProperty(item, property, new List<string> { value });
        }

        public void AddProperty(Dictionary<string, List<List<string>>> item, string property, List<string> values)
        {
            if (item == null && m_lastAddeditem != null)
                item = m_lastAddeditem;

            if (item != null)
            {
                if (!item.ContainsKey(property))
                {
                    item.Add(property, new List<List<string>>());
                }
                item[property].Add(values);
            }
        }

    }
}
