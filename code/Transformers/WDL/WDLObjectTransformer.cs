using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSObjectTransformer : ObjectTransformer
    {
        private static readonly string s_indent = "\t\t";
        private static readonly string s_indentProperty = "\t\t\t";
        private static readonly string s_indentInit = "\t\t\t";
        private static string s_scope = "public static";

        private static StringBuilder s_propertyBuilder = new StringBuilder();
        private static StringBuilder s_rangeBuilder = new StringBuilder();
        private static StringBuilder s_controlBuilder = new StringBuilder();

        public CSObjectTransformer(bool excludeProperties) : base(excludeProperties) { }

        public override void Transform(object obj, Node name, Node type, Node str, List<Property> properties, bool isInitialized)
        {
            StringBuilder sb = (StringBuilder)obj;
            string stype = type.ToString();
            string sname = name.ToString();
            bool isSkill = false;

            s_propertyBuilder.Clear();
            s_rangeBuilder.Clear();
            s_controlBuilder.Clear();

            switch (stype)
            {
                case "Synonym":
                    sb.Append($"{s_indent}{s_scope} ");
                    ProcessSynonym(sb, properties, sname);
                    sb.Append(";");
                    break;

                case "String":
                    sb.Append($"{s_indent}{s_scope} string {sname}");
                    string sproperties = str.ToString(); //string carries text instead of properties //TEMP
                    if (!string.IsNullOrEmpty(sproperties))
                        sb.Append($" = {sproperties}");
                    sb.Append(";");
                    break;

                case "Skill":
                    isSkill = true;
                    goto default;

                default:
                    string indent = s_indent;
                    if (!isInitialized)
                    {
                        indent = s_indent;
                        sb.Append($"{indent}{s_scope} {stype} ");
                    }
                    else
                    {
                        indent = s_indentInit;
                        sb.Append(indent);
                    }

                    sb.Append(sname + " = new " + stype);
                    if (isSkill) //Skills are treated like variable instances, thus no name identifier is passed on construction
                        sb.Append("()");
                    else
                        sb.Append("(\"" + sname + "\")");

                    if (isSkill || !m_excludeProperties) //Skills always need their properties and thus must not be skipped
                    {
                        if (properties.Count > 0)
                        {
                            sb.AppendLine();
                            sb.Append($"{indent}{{");
                            ProcessProperties(sb, properties, type, indent);
                            sb.AppendLine();
                            sb.Append($"{indent}}}");
                        }
                    }
                    sb.Append(";");
                    break;
            }

        }

        private void FormatProperties(StringBuilder sb)
        {
            //copy standard properties to output
            sb.Append(s_propertyBuilder);

            //handle palette range definitions
            if (s_rangeBuilder.Length > 0)
            {
                sb.AppendLine();
                sb.AppendLine(s_indentProperty + "Range = new[,]");
                sb.AppendLine(s_indentProperty + "{" + s_rangeBuilder);
                sb.Append(s_indentProperty + "}");
            }

            //handle UI control definitions
            if (s_controlBuilder.Length > 0)
            {
                sb.AppendLine();
                sb.AppendLine(s_indentProperty + "Controls = new UIControl[]");
                sb.AppendLine(s_indentProperty + "{ " + s_controlBuilder);
                sb.Append(s_indentProperty + "}");
            }
        }

        private void ProcessSynonym(StringBuilder sb, List<Property> properties, string name)
        {
            //Current implementation is not compatible with preprocessor directives - most likely not relevant for any A3 game ever created
            Property property;
            //workaround: build synonym definition in property
            //Type declares datatype of Synonym
            property = properties.Where(x => x.Name.ToLower().Equals("type")).FirstOrDefault(); //TEMP
            if (property != null)
            {
                string synType = property.Values[0].ToString();
                string stype = property.Values[0].Data.ToLower();
                //use C# strings -> convert to "string"
                if (stype.Equals("string"))
                    synType = "string";
                //"Action" keyword is reserved in C# -> use "Function" instead (mandatory)
                if (stype.Equals("action"))
                    synType = "Function";
                //Scripts don't distinguish between object types and just use BaseObject which just carries all properties - like WDL
                //Trying to keep this more strict results in all kind of type problems in WDL actions
                if (stype.Equals("wall") || stype.Equals("thing") || stype.Equals("actor"))
                    synType = "BaseObject";

                s_propertyBuilder.Append($"{synType} {name}");

                //"Default" declares default assignment of Synonym (optional)
                property = properties.Where(x => x.Name.ToLower().Equals("default")).FirstOrDefault();//TEMP
                if (property != null)
                {
                    if (property.Values[0].NodeType != NodeType.Null) //Discard null-assignments for synonyms
                    {
                        s_propertyBuilder.Append($" = {property.Values[0]}");
                    }
                }
            }
            FormatProperties(sb);
        }

        private void ProcessProperties(StringBuilder sb, List<Property> properties, Node type, string indent)
        {
            foreach (Property property in properties)
            {
                switch (property.Name.ToLower()) //TEMP
                {
                    case "range":
                        s_rangeBuilder.AppendLine();
                        s_rangeBuilder.Append(indent + "\t\t");
                        property.Transform(s_rangeBuilder, type);
                        s_rangeBuilder.Append(",");
                        break;

                    case "digits":
                    case "hbar":
                    case "vbar":
                    case "hslider":
                    case "vslider":
                    case "picture":
                    case "window":
                    case "button":
                        s_controlBuilder.AppendLine();
                        s_controlBuilder.Append(indent + "\t\t");
                        property.Transform(s_controlBuilder, type);
                        s_controlBuilder.Append(",");
                        break;

                    case "flags":
                    default:
                        s_propertyBuilder.AppendLine();
                        s_propertyBuilder.Append(indent + "\t");
                        property.Transform(s_propertyBuilder, type);
                        s_propertyBuilder.Append(",");
                        break;
                }
            }
            FormatProperties(sb);
        }

    }
}
