using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    abstract class NodeTransformer
    {
        private delegate string TransformFunction(string input);
        private TransformFunction[] m_transformFunction;

        public NodeTransformer()
        {
            m_transformFunction = new TransformFunction[(int)NodeType.MaxValue];
            m_transformFunction[(int)NodeType.Default] = TransformDefault;
            m_transformFunction[(int)NodeType.File] = TransformFile;
            m_transformFunction[(int)NodeType.List] = TransformList;
            m_transformFunction[(int)NodeType.Skill] = TransformSkill;
            m_transformFunction[(int)NodeType.SkillType] = TransformSkillType;
            m_transformFunction[(int)NodeType.Global] = TransformGlobal;
            m_transformFunction[(int)NodeType.Event] = TransformEvent;
            m_transformFunction[(int)NodeType.LocalSynonym] = TransformLocalSynonym;
            m_transformFunction[(int)NodeType.GlobalSynonym] = TransformGlobalSynonym;
            m_transformFunction[(int)NodeType.Math] = TransformMath;
            m_transformFunction[(int)NodeType.Null] = TransformNull;
            m_transformFunction[(int)NodeType.Number] = TransformNumber;
            m_transformFunction[(int)NodeType.Operator] = TransformOperator;
            m_transformFunction[(int)NodeType.String] = TransformString;
            m_transformFunction[(int)NodeType.SimpleString] = TransformSimpleString;
            m_transformFunction[(int)NodeType.Property] = TransformProperty;
            m_transformFunction[(int)NodeType.Flag] = TransformFlag;
            m_transformFunction[(int)NodeType.Identifier] = TransformIdentifier;
            m_transformFunction[(int)NodeType.Reserved] = TransformReserved;
            m_transformFunction[(int)NodeType.ActorTarget] = TransformActorTarget;
            m_transformFunction[(int)NodeType.GotoLabel] = TransformGotoLabel;
        }

        public string Transform(string input, NodeType type)
        {
            if (type < NodeType.MaxValue)
            {
                return m_transformFunction[(int)type](input);
            }
            else
            {
                Console.WriteLine("(E) NODE NodeType " + type + " cannot be tranformed");
                return input;
            }
        }

        virtual protected string TransformDefault(string input)
        {
            return input;
        }

        virtual protected string TransformFile(string input)
        {
            return input;
        }

        virtual protected string TransformList(string input)
        {
            return input;
        }

        virtual protected string TransformSkill(string input)
        {
            return input;
        }

        virtual protected string TransformSkillType(string input)
        {
            return input;
        }

        virtual protected string TransformGlobal(string input)
        {
            return input;
        }

        virtual protected string TransformEvent(string input)
        {
            return input;
        }

        virtual protected string TransformLocalSynonym(string input)
        {
            return input;
        }

        virtual protected string TransformGlobalSynonym(string input)
        {
            return input;
        }

        virtual protected string TransformMath(string input)
        {
            return input;
        }

        virtual protected string TransformNull(string input)
        {
            return input;
        }

        virtual protected string TransformNumber(string input)
        {
            return input;
        }

        virtual protected string TransformOperator(string input)
        {
            return input;
        }

        virtual protected string TransformString(string input)
        {
            return input;
        }

        virtual protected string TransformSimpleString(string input)
        {
            return input;
        }

        virtual protected string TransformProperty(string input)
        {
            return input;
        }

        virtual protected string TransformFlag(string input)
        {
            return input;
        }

        virtual protected string TransformIdentifier(string input)
        {
            return input;
        }

        virtual protected string TransformReserved(string input)
        {
            return input;
        }

        virtual protected string TransformActorTarget(string input)
        {
            return input;
        }

        virtual protected string TransformGotoLabel(string input)
        {
            return input;
        }

    }
}
