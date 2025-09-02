using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    //properties cannot be nested, therefore they are treated via static list (objects.cs)
    //since this makes any linking obsolete, this class is not requried to inherit from Node
    class Property
    {
        private static PropertyTransformer s_transformer = null;

        private Node m_name;
        private bool m_allowMerge;
        private bool m_allowMultiple;
        private List<Node> m_values;

        public static /*new*/ PropertyTransformer Transformer { get => s_transformer; set => s_transformer = value; }
        public string Name { get => m_name.Data; }
        public List<Node> Values { get => m_values; set => m_values = value; }
        public bool AllowMerge { get => m_allowMerge; }
        public bool AllowMultiple { get => m_allowMultiple; }

        public Property()
        {
            m_allowMerge = false;
            m_allowMultiple = false;
            m_name = new Node();
            m_values = new List<Node>();
        }

        public Property(Node property, List<Node> values) : this()
        {
            m_name = property;
            if (values != null)
                m_values.AddRange(values);
            //must be called after property values have been added!
            SetTypes();
            SetFlags();
        }

        public void Transform(object obj, Node owner)
        {
            s_transformer?.Transform(obj, m_name, m_values, owner);
        }

        private void SetTypes()
        {
            string sname = m_name.Data.ToLower(); //TEMP

            try
            {
                //TODO: add List length checks for explicite array access
                //TODO: add prefix patch for integer asset IDs for all asset types (really needed?)
                switch (sname)
                {
                    case "type":
                        m_values[0].NodeType = NodeType.SkillType;
                        break;

                    //collect all panel control definitions for generation of multi-dimensional array
                    case "digits":
                    case "hbar":
                    case "vbar":
                        m_values[5] = Util.UpdateSkill(m_values[5]);
                        break;

                    case "hslider":
                    case "vslider":
                        m_values[4] = Util.UpdateSkill(m_values[4]);
                        break;

                    case "picture":
                        m_values[3] = Util.UpdateSkill(m_values[3]);
                        break;

                    case "window":
                        m_values[5] = Util.UpdateSkill(m_values[5]);
                        m_values[6] = Util.UpdateSkill(m_values[6]);
                        break;

                    case "flags":
                        m_values.ForEach(x => x.NodeType = NodeType.Flag);
                        break;

                    case "bmap":
                    case "bmaps":
                        //PATCH: Asset ID can be integer numbers in WDL, make sure to prefix these 
                        m_values.ForEach(x => x.NodeType = (x.NodeType != NodeType.Null) ? NodeType.Identifier : NodeType.Null);
                        break;

                    case "ovlys":
                        //PATCH: Asset ID can be integer numbers in WDL, make sure to prefix these 
                        m_values.ForEach(x => x.NodeType = (x.NodeType != NodeType.Null) ? NodeType.Identifier : NodeType.Null);
                        break;

                    case "target":
                        m_values[0] = Util.UpdateActorTarget(m_values[0]);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("(E) PROPERTY SetTypes: " + e);
            }
        }

        private void SetFlags()
        {
            string sname = m_name.Data.ToLower();

            switch (sname)
            {
                //Apply patch for undocumented WDL syntax
                case "bmap":
                    Console.WriteLine("(W) PROPERTY patched invalid identifier " + m_name.Data);
                    m_name = new Node("bmaps", m_name.NodeType);
                    break;

                //All definitions must be merged into single one
                case "flags":
                    m_allowMerge = true;
                    break;

                //Multiple definitions per object allowed
                case "digits":
                case "hbar":
                case "vbar":
                case "hslider":
                case "vslider":
                case "picture":
                case "window":
                case "button":
                case "range":
                    m_allowMultiple = true;
                    break;

                default:
                    break;
            }

        }
    }
}
