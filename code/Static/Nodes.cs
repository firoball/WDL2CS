using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Nodes
    {
        public static Node AddFile(Node file)
        {
            file.NodeType = NodeType.File;
            return file;
        }

        public static Node AddIdentifierPatch(Node keyword1, Node op, Node keyword2)
        {
            //workaround for identifiers like "identifier-name" / "identifier-123"
            return new Node(keyword1.Data + op.Data + keyword2.Data, NodeType.Identifier);
        }

        public static Node AddList(Node list)
        {
            list.NodeType = NodeType.List;
            return list;
        }

        public static Node AddKeyword(Node keyword)
        {
            IdentifyType(keyword, out string identifierType);
            return keyword;
        }

        public static Node AddKeywordProperty(Node keyword, Node property)
        {
            IdentifyType(keyword, out string type);
            //default keywords can't have properties - must be an identifier
            if (keyword.NodeType == NodeType.Default)
                keyword.NodeType = NodeType.Identifier;
            property.NodeType = NodeType.Property;

            //skill properties (skill1..8) must be accessed through sub-property, defaults to ".Val"
            if (property.Data.IndexOf("skill", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Node subProperty = new Node("VAL", NodeType.Property);
                return new Node(keyword, property, subProperty);
            }
            else
            {
                return new Node(keyword, property);
            }
        }

        public static Node AddMath(Node math, Node parameter)
        {
            math.NodeType = NodeType.Math;
            Node formula = AddParentheses(parameter);
            return new Node(math, formula);
        }

        public static Node AddNull()
        {
            return new Node("null", NodeType.Null);
        }

        public static Node AddNumber(Node number)
        {
            number.NodeType = NodeType.Number;
            return number;
        }

        public static Node AddNumber(Node op, Node number)
        {
            //avoid nested node for sign operator. This causes problems
            //separating the sign operator was necessary to avoid conflicts in grammar
            if (op.Data.Equals("-"))
                return new Node(op.Data + number.Data, NodeType.Number);
            else
            {
                number.NodeType = NodeType.Number;
                return number;
            }
        }

        public static Node AddNumberPatch(Node number, Node superfluous)
        {
            Console.WriteLine("(W) PARSER discarded superfluous token in expression: " + superfluous.ToString());
            number.NodeType = NodeType.Number;
            return number;
        }

        public static Node AddOperator(Node node1, string op, Node node2)
        {
            Node opNode = new Node(op, NodeType.Operator);
            return new Node(node1, opNode, node2);
        }

        public static Node AddParentheses(Node node)
        {
            Node n1 = new Node("(");
            Node n2 = new Node(")");
            return new Node(n1, node, n2);
        }

        public static Node AddString(Node str)
        {
            str.NodeType = NodeType.String;
            return str;
        }

        private static void IdentifyType(Node node, out string identifierType)
        {
            string identifier = node.Data;
            identifierType = string.Empty;

            //NodeType is already set - no action required
            if (node.NodeType == NodeType.Null) { }
            //known identifiers can override predefined keywords
            else if (Registry.Identify(out identifierType, identifier)) 
                node.NodeType = NodeType.Identifier;

            //predefined keywords
            else if (Identifier.IsSkill(ref identifier))
                node.NodeType = NodeType.Skill;
            else if (Identifier.IsGlobal(ref identifier)) 
                node.NodeType = NodeType.Global;
            else if (Identifier.IsEvent(ref identifier)) 
                node.NodeType = NodeType.Event;
            else if (Identifier.IsSynonym(ref identifier)) 
                node.NodeType = NodeType.GlobalSynonym;
            else if (Identifier.IsLocalSynonym(ref identifier)) 
                node.NodeType = NodeType.LocalSynonym;

            else
                //assume goto label or known identifier (null)
                //unregistered objects will also go here
                node.NodeType = NodeType.Default;
        }
    }
}
