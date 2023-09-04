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
        private static readonly string s_actionType = "#[SF]#";
        private static readonly string s_globalType = "#[SG]#";
        private static readonly string s_objectType = "#[SO]#";

        public static string AssetType => s_assetType;
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

        public static ISerializable Deserialize(ref string stream)
        {
            //kill any leading object seperator - it is used for serializing multiple instructions only
            string[] fragments = stream.Split(new[] { s_sepSect }, StringSplitOptions.RemoveEmptyEntries);

            string type = stream.Substring(0, 6);
            string data = stream.Substring(6);

            switch (type)
            {
                case "#[SA]#":
                    return Asset.Deserialize(ref data);

                case "#[SF]#":
                    return Action.Deserialize(ref data);

                case "#[SG]#":
                    return Global.Deserialize(ref data);

                case "#[SO]#":
                    return Object.Deserialize(ref data);

                default:
                    return null;
            }
        }

        public static List<ISerializable> DeserializeList(ref string stream)
        {
            List<ISerializable> sections = new List<ISerializable>();

            string[] fragments = stream.Split(new[] { s_sepSect }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < fragments.Length; i++)
            {
                sections.Add(Deserialize(ref fragments[i]));
            }
            return sections;
        }
    }
}
