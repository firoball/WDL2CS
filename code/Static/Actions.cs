using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Actions
    {
        private static List<Node> s_parameters = new List<Node>();

        public static Node AddAction(Node name, Node stream)
        {
            name.NodeType = NodeType.Identifier;
            Registry.Register("Action", name.Data);
            Node a = new Action(name, stream);

            return a;
        }

        public static Node CreateMarker(Node label, Node stream)
        {
            label.NodeType = NodeType.GotoLabel;
            Node inst = new Instruction(label, false);
            Node s = new Node(inst, stream);

            return s;
        }

        public static Node CreateIfCondition(Node expr, Node stream)
        {
            Node parameters = new Node(new Node("("), expr, new Node(")"));

            Node i1 = new Instruction(new Node("if"), parameters);
            Node i2 = new Instruction(new Node("{"), false);
            Node i3 = new Instruction(new Node("}"), false);
            Node s = new Node(new[] { i1, i2, stream, i3});

            return s;
        }

        public static Node CreateElseCondition(Node stream)
        {
            Node i1 = new Instruction(new Node("else"));
            Node i2 = new Instruction(new Node("{"), false);
            Node i3 = new Instruction(new Node("}"), false);
            Node s = new Node(new[] { i1, i2, stream, i3 });

            return s;
        }

        public static Node CreateWhileCondition(Node expr, Node stream)
        {
            Node parameters = new Node(new Node("("), expr,  new Node(")"));

            Node i1 = new Instruction(new Node("while"), parameters);
            Node i2 = new Instruction(new Node("{"), false);
            Node i3 = new Instruction(new Node("}"), false);
            Node s = new Node(new[] { i1, i2, stream, i3 });

            return s;
        }

        public static Node CreateExpression(Node expr)
        {
            //ridiculous patch: A3 accepts RULE statements without assignment
            //TODO: find out real behaviour in A3, currently first identifier is treated as assignee
            //patch is derived from the behaviour of A3 for statements like "+ =" - seems like "=" is optional for WDL parser
            if ((expr.NodeType == NodeType.Container) && (expr.Children.Count > 2))
            {
                Console.WriteLine("(W) ACTIONS patched invalid rule");
                Node assignee = expr.Children[0];
                Node patchOp = new Node(expr.Children[1].Data.TrimEnd() + "= ", NodeType.Operator);
                expr.Children.RemoveAt(1); //remove operator from expression list
                expr.Children.RemoveAt(0); //remove assignee from expression list
                return CreateExpression(assignee, patchOp, expr);
            }
            else
            {
                return expr; //TODO: some strange Node was found... for now just forward it as is
            }
        }

        public static Node CreateExpression(Node assignee, Node op, Node expr)
        {
            //make sure Skills always have their "val" property
            assignee = Util.UpdateSkill(assignee);
            Node parameters = new Node(assignee, op, expr);

            Instruction inst = new Instruction(new Node("Rule", NodeType.Reserved), parameters);
            return inst;
        }

        public static Node CreateInstruction(Node command)
        {
            string scommand = command.Data;
            if (Identifier.IsCommand(ref scommand))
            {
                command.NodeType = NodeType.Reserved;
                Instruction inst = new Instruction(command, s_parameters);

                //Clean up
                s_parameters.Clear();
                return inst;
            }
            else if (s_parameters.Count == 0) //take care of wrongly defined goto marker (ends with ; instead of :)
            {
                Console.WriteLine("(W) ACTIONS crrect malformed goto marker: " + command.Data);
                command.NodeType = NodeType.GotoLabel;
                Instruction inst = new Instruction(command, false);
                return inst;
            }
            else
            {
                //Clean up
                s_parameters.Clear();

                Console.WriteLine("(W) ACTIONS ignore invalid command: " + command.Data);
                return null;
            }
        }

        public static Node AddInstructionParameter(Node param)
        {
            //unknown nodes must be actions and therefore are treated as identifier
            if (param.NodeType == NodeType.Default)
                param.NodeType = NodeType.Identifier;
            
            s_parameters.Insert(0, param);
            return null;
        }

    }
}
