using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Section
    {
        private readonly string m_type;
        private readonly string m_stream;

        private static readonly string s_sepSect = "#[S]#";
        private static readonly string s_assetType = "#[SA]#";
        private static readonly string s_defineType = "#[SD]#";
        private static readonly string s_actionType = "#[SF]#";
        private static readonly string s_globalType = "#[SG]#";
        private static readonly string s_objectType = "#[SO]#";

        public static string AssetType => s_assetType;
        public static string DefineType => s_defineType;
        public static string ActionType => s_actionType;
        public static string GlobalType => s_globalType;
        public static string ObjectType => s_objectType;

        public Section(string type, string stream)
        {
            m_type = type;
            m_stream = stream;
        }

        public string Serialize()
        {
            return s_sepSect + m_type + m_stream;
        }

        public static ISerializable Deserialize(string stream)
        {
            //kill any leading object seperator - it is used for serializing multiple instructions only
            string[] fragments = stream.Split(new[] { s_sepSect }, StringSplitOptions.RemoveEmptyEntries);

            string type = stream.Substring(0, 6);
            string data = stream.Substring(6);

            switch (type)
            {
                case "#[SA]#":
                    return Asset.Deserialize(data);

                case "#[SD]#":
                    return Define.Deserialize(data);

                case "#[SF]#":
                    return Action.Deserialize(data);

                case "#[SG]#":
                    return Global.Deserialize(data);

                case "#[SO]#":
                    return Object.Deserialize(data);

                default:
                    return null;
            }
        }

        public static List<ISerializable> DeserializeList(string stream)
        {
            List<ISerializable> sections = new List<ISerializable>();

            string[] fragments = stream.Split(new[] { s_sepSect }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string fragment in fragments)
            {
                sections.Add(Deserialize(fragment));
            }
            return sections;
        }
    }
}
