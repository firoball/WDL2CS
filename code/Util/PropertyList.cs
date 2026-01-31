using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class PropertyList
    {
        private Dictionary<string, List<List<Tuple<string, string>>>> m_lastAddeditem;

        /* Generic types only for avoiding any type dependencies on transpiler code
         *  Object/Asset type
         *      Object/Asset name
         *          Object/Asset property ID
         *              property value/type pairs (multiple sets)
         */
        private Dictionary<string, Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>> m_list = new Dictionary<string, Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>>();

        public PropertyList()
        {
            m_list.Add("Model", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Sound", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Music", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Flic", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Bmap", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Ovly", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Font", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Palette", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Texture", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Wall", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Region", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Thing", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Actor", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Way", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Text", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Overlay", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("Panel", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            m_list.Add("View", new Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>());
            //TODO: Add Action, String for convenience here - although not yet used?
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<List<Tuple<string, string>>>>>> List { get => m_list; }

        public Dictionary<string, List<List<Tuple<string, string>>>> AddItem(string type, string name)
        {
            Dictionary<string, List<List<Tuple<string, string>>>> item = new Dictionary<string, List<List<Tuple<string, string>>>>();
            if (m_list.ContainsKey(type))
            {
                m_list[type].Add(name, item);
            }
            m_lastAddeditem = item; //store reference of new item inside class
            return item;
        }

        public void AddProperty(Dictionary<string, List<List<Tuple<string, string>>>> item, string property, Tuple<string, string> value)
        {
            AddProperty(item, property, new List<Tuple<string, string>> { value });
        }

        public void AddProperty(Dictionary<string, List<List<Tuple<string, string>>>> item, string property, List<Tuple<string, string>> values)
        {
            if (item == null && m_lastAddeditem != null)
                item = m_lastAddeditem;

            if (item != null)
            {
                if (!item.ContainsKey(property))
                {
                    item.Add(property, new List<List<Tuple<string, string>>>());
                }
                item[property].Add(values);
            }
        }

    }
}
