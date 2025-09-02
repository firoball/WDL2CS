using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Instruction : Node 
    {
        private static InstructionTransformer s_transformer = null;
        private Node m_command;
        private readonly bool m_count;
        private List<Node> m_parameters;

        public static new InstructionTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public NodeType CommandType { get => m_command.NodeType; }
        public string Command { get => m_command.Data; }
        public bool Count { get => m_count; }
        public List<Node> Parameters { get => m_parameters; }

        public Instruction()
        {
            m_command = new Node();
            m_count = false;
            m_parameters = new List<Node>();
        }

        public Instruction(Node command, List<Node> parameters, bool count) : this()
        {
            m_command = command;
            if (parameters != null)
                m_parameters.AddRange(parameters);
            m_count = count;
            Setup();
        }

        public Instruction(Node command, List<Node> parameters) : this(command, parameters, true) { }

        public Instruction(Node command, bool count) : this(command, new List<Node>(), count) { }
        public Instruction(Node command) : this(command, true) { }

        public Instruction(Node command, Node parameter, bool count) : this(command, new[] { parameter }.ToList(), count) { }
        public Instruction(Node command, Node parameter) : this(command, parameter, true) { }


        public string Format(string nul)
        {
            //TEMP
            StringBuilder sb = new StringBuilder();
            Transform(sb);

            return sb.ToString();
        }

        private void Setup()
        {

            try
            {
                string command = m_command.Data.ToLower();
                /* don't format any parameter of IFs, RULEs or GOTOs
                    * detect RULEs either by command or by assignment operator in expression
                    * IFs and RULEs already have their expression parameter formatted
                    * GOTO requires unformatted marker - ambiguous definition might result in wrong formatting otherwise
                    */

                switch (command)
                {
                    case "abs":
                        Node abs = new Node("abs", NodeType.Math);
                        m_parameters.Add(abs); //provide additional Node for optional use
                        break;


                    case "addt":
                        Node addt = Util.UpdateSkill(new Node("time_corr", NodeType.Skill));
                        m_parameters.Add(addt); //provide additional Node for optional use
                        break;

                    case "asin":
                        Node asin = new Node("asin", NodeType.Math);
                        m_parameters.Add(asin); //provide additional Node for optional use
                        break;

                    case "goto":
                        m_parameters[0] = Util.RemoveProperty(m_parameters[0]);
                        m_parameters[0].NodeType = NodeType.Identifier;
                        break;

                    case "if_above":
                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "if_below":
                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "if_equal":
                        if (Util.HasProperty(m_parameters[0], "target"))
                        {
                            m_parameters[1] = Util.UpdateActorTarget(m_parameters[1]);
                        }
                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "if_nequal":
                        if (Util.HasProperty(m_parameters[0], "target"))
                        {
                            m_parameters[1] = Util.UpdateActorTarget(m_parameters[1]);
                        }
                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "if_max":
                        //Remove .Val from Skill
                        Node if_max_prop = new Node("max", NodeType.Property);
                        Node if_max = m_parameters[0];
                        if (m_parameters[0].NodeType == NodeType.Container)
                        {
                            List<Node> children = m_parameters[0].Children;
                            if ((children.Count > 1) && (children.Last().NodeType == NodeType.Property))
                            {
                                List<Node> maxchildren = new List<Node>(children);
                                maxchildren.Remove(maxchildren.Last());
                                if_max = new Node(maxchildren);
                            }

                        }
                        m_parameters.Insert(1, new Node(if_max, if_max_prop)); //provide additional Node for optional use

                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "if_min":
                        //Remove .Val from Skill
                        Node if_min_prop = new Node("min", NodeType.Property);
                        Node if_min = m_parameters[0];
                        if (m_parameters[0].NodeType == NodeType.Container)
                        {
                            List<Node> children = m_parameters[0].Children;
                            if ((children.Count > 1) && (children.Last().NodeType == NodeType.Property))
                            {
                                List<Node> minchildren = new List<Node>(children);
                                minchildren.Remove(minchildren.Last());
                                if_min = new Node(minchildren);
                            }

                        }
                        m_parameters.Insert(1, new Node(if_min, if_min_prop)); //provide additional Node for optional use

                        if (m_parameters.Count > 2 && m_parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                        {
                            m_parameters[2] = Util.RemoveProperty(m_parameters[2]);
                            m_parameters[2].NodeType = NodeType.Identifier;
                        }
                        break;

                    case "level":
                        //patch very old WDL syntax
                        //it allowed map/level names without "", which therefore are captured as container node: identifier.property
                        if (m_parameters[0].NodeType == NodeType.Container && m_parameters[0].Children.Count == 2)
                        {
                            string levelfile = m_parameters[0].Children[0].Data + "." + m_parameters[0].Children[1].Data;
                            m_parameters[0] = new Node(levelfile, NodeType.File);
                        }
                        break;

                    case "map":
                        if (m_parameters.Count > 0)
                        {
                            //patch very old WDL syntax
                            //it allowed map/level names without "", which therefore are captured as container node: identifier.property
                            if (m_parameters[0].NodeType == NodeType.Container && m_parameters[0].Children.Count == 2)
                            {
                                string levelfile = m_parameters[0].Children[0].Data + "." + m_parameters[0].Children[1].Data;
                                m_parameters[0] = new Node(levelfile, NodeType.File);
                            }
                        }
                        break;

                    case "set":
                        //Special case "Target" property
                        if (Util.HasProperty(m_parameters[0], "target"))
                        {
                            m_parameters[1] = Util.UpdateActorTarget(m_parameters[1]);
                        }

                        //Not sure if special casing with 'if' is required...
                        /*if (
                            string.Equals(m_parameters[0].Data, "each_tick", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(m_parameters[0].Data, "each_sec", StringComparison.OrdinalIgnoreCase)
                            )
                        {

                        }
                        //special case: function assignments to each_tick and each_sec (not applicable for Set_all)
                        else if (m_parameters[0].StartsWith(Formatter.FormatGlobal("Each_")))
                        {
                            string p = Formatter.FormatIdentifier(m_parameters[1]);
                            //if function assigns itself, use existing instance
                            if (string.Compare(function, p) == 0)
                                o = $"{m_parameters[0]} = this;";
                            else
                                o = $"{m_parameters[0]} = {m_parameters[1]};";
                        }*/
                        break;

                    case "set_all":
                        //Special case "Target" property
                        if (Util.HasProperty(m_parameters[0], "target"))
                        {
                            m_parameters[1] = Util.UpdateActorTarget(m_parameters[1]);
                        }
                        break;

                    case "sin":
                        Node sin = new Node("sin", NodeType.Math);
                        m_parameters.Add(sin); //provide additional Node for optional use
                        break;

                    case "sqrt":
                        Node sqrt = new Node("sqrt", NodeType.Math);
                        m_parameters.Add(sqrt); //provide additional Node for optional use
                        break;

                    case "subt":
                        Node subt = Util.UpdateSkill(new Node("time_corr", NodeType.Skill));
                        m_parameters.Add(subt); //provide additional Node for optional use
                        break;

                    case "while":
                        //while(1) patch -> C# needs while(true)
                        if (m_parameters[0].NodeType == NodeType.Container)
                        {
                            List<Node> children = m_parameters[0].Children;
                            //searching for "(", container > 0, ")" 
                            if (
                                (children.Count == 3) && (children[1].NodeType == NodeType.Container))
                                {
                                    List<Node> subchildren = children[1].Children;
                                //searching for "(", number, ")" 
                                if (
                                    (subchildren.Count == 3) && (subchildren[1].NodeType == NodeType.Number) &&
                                    int.TryParse(subchildren[1].Data, out int n) && (n > 0)
                                    )
                                {
                                    //provide additional Node for optional use
                                    Node truenode = new Node(new Node("("), new Node("true"), new Node(")"));
                                    m_parameters.Add(truenode);
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch
            { 
                Console.WriteLine("(E) INSTRUCTION discard malformed instruction: " + m_command + " " + string.Join(", ", m_parameters.Select(x => x.Data)));
            }
        }

        private void Transform(StringBuilder sb)
        {
            s_transformer?.Transform(sb, m_command, m_parameters);
        }

    }
}
