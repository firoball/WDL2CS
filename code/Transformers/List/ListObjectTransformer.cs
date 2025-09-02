using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.List
{
    class ListObjectTransformer : ObjectTransformer
    {

        public override void Transform(object obj, Node name, Node type, Node str, List<Property> properties, bool isInitialized)
        {
            PropertyList list = (PropertyList)obj;
            string stype = type.ToString();

            switch (stype.ToLower())
            {
                case "synonym":
                case "string":
                case "skill":
                    break; //ignore - these objects are not exported in property list

                default:
                    string sname = name.ToString();
                    var item = list.AddItem(stype, sname);
                    foreach (Property property in properties)
                    {
                        property.Transform(list, type);
                    }
                    break;
            }
            
        }

    }
}
