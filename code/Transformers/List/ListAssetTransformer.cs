using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.List
{
    class ListAssetTransformer : AssetTransformer
    {

        public override void Transform(object obj, Node name, Node type, Node file, List<Node> parameters)
        {
            PropertyList list = (PropertyList)obj;
            string stype = type.ToString();
            string sname = name.ToString();
            string sfile = file.ToString();

            var item = list.AddItem(stype, sname);
            list.AddProperty(item, "File", sfile);
            if (parameters != null && parameters.Count > 0)
            list.AddProperty(item, "Parameters", parameters.Select(x => x.ToString()).ToList());
        }
    }
}
