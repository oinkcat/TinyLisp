using System;
using System.Collections.Generic;

public class LogicObject : BaseObject
{
    public bool Value;

    #region Логические функции

    public delegate LogicObject LogicFunction(LogicObject First, LogicObject Other);

    public static LogicObject DoOr(LogicObject First, LogicObject Other)
    {
        return new LogicObject(First.Value || Other.Value);
    }

    public static LogicObject DoAnd(LogicObject First, LogicObject Other)
    {
        return new LogicObject(First.Value && Other.Value);
    }

    public static LogicObject DoNot(LogicObject Value)
    {
        return new LogicObject(!Value.Value);
    }

    #endregion

    public LogicObject(bool Value)
    {
        this.Value = Value;
    }

    public override string ToString()
    {
        return Value ? "#t" : "#f";
    }

    public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
    {
        return this;
    }
}