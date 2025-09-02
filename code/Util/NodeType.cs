namespace WDL2CS
{
    enum NodeType : int
    {
        Default = 0,

        File = 1,
        List = 2,
        Skill = 3,
        SkillType = 4,
        Global = 5,
        Event = 6,
        LocalSynonym = 7,
        GlobalSynonym = 8,
        Math = 9,
        Null = 10,
        Number = 11,
        Operator = 12,
        String = 13,
        SimpleString = 14,
        Property = 15,
        Flag = 16,
        Identifier = 17,
        Reserved = 18,
        ActorTarget = 19,
        GotoLabel = 20,

        MaxValue = 21,
        Container = 255
    }
}
