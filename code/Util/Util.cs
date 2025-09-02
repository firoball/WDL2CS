using System;
using System.Collections.Generic;
using System.Linq;

namespace WDL2CS
{
    static class Util
    {
        public static Node UpdateSkill(Node node)
        {
            //unary operators (-SkillX) cause an extra container wrapwround... dive into it as well
            if (node.NodeType == NodeType.Container && node.Children.Count == 2)
            {
                //be very strict here, otherwise min/max skill properties will break
                node.Children[1] = UpdateSkill(node.Children[1]);
                return node;
            }
            //standard path
            else if (
                //it's a known engine skill
                (node.NodeType == NodeType.Skill) || 
                // it's a user-defined identifier which was registered as skill
                ((node.NodeType == NodeType.Identifier) && Registry.Identify(out string identifierType, node.Data) && identifierType.Equals("Skill"))
            )
            {
                //Skills always need their "val" property
                Node property = new Node("VAL", NodeType.Property);
                return new Node(node, property);
            }
            //nothing to do
            else
            {
                return node;
            }
        }

        private static string[] s_targets = new[] { "move", "bullet", "drop", "stick", "straight", "follow", "repel", "vertex", "node0", "node1", "hold", "place", "null" };
        public static Node UpdateActorTarget(Node node)
        {
            if (s_targets.Any(x => x.Equals(node.Data, StringComparison.OrdinalIgnoreCase)))
                node.NodeType = NodeType.ActorTarget;

            return node;
        }

        public static bool HasProperty(Node node, string property)
        {
            if (node.NodeType == NodeType.Container)
            {
                List<Node> children = node.Children;
                if (
                    (children.Count > 1) && 
                    (children.Last().NodeType == NodeType.Property) && 
                    children.Last().Data.Equals(property, StringComparison.OrdinalIgnoreCase)
                    )
                    return true;
            }

            return false;
        }

        public static Node CopyReplaceProperty(Node node, string newProperty)
        {
            if (node.NodeType == NodeType.Container)
            {
                List<Node> children = node.Children;
                if ((children.Count > 1) && (children.Last().NodeType == NodeType.Property))
                {
                    List<Node> newchildren = new List<Node>(children);
                    newchildren.Remove(newchildren.Last());
                    newchildren.Add(new Node(newProperty, NodeType.Property));

                    return new Node(newchildren);
                }
            }

            return node;
        }

        public static bool SplitProperty(Node container, out Node target, out Node property)
        {
            target = null;
            property = null;
            if (container.NodeType == NodeType.Container)
            {
                List<Node> children = container.Children;
                if (children.Count > 1) //TODO: find out how porperty nodes (skill.var) are arranged. 
                {
                    target = children[0];
                    List<Node> properties = new List<Node>(children);
                    properties.RemoveAt(0);
                    property = new Node(properties);
                    return true;
                }

            }
            return false;
        }

        public static Node RemoveProperty(Node node)
        {
            if ((node.NodeType == NodeType.Container) && (node.Children.Count > 0))
                return node.Children[0];
            else
                return node;
        }
    }
}