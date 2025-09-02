using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.List
{
    class ListPropertyTransformer : PropertyTransformer
    {
        public override void Transform(object obj, Node name, List<Node> values, Node owner)
        {
            PropertyList list = (PropertyList)obj;
            list.AddProperty(null, name.ToString(), values.Select(x => x.ToString()).ToList());
        }

    }
}
