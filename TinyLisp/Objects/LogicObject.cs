using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    /// <summary>
    /// Логическое выражение (истина/ложь)
    /// </summary>
    public class LogicObject : BaseObject
    {
        /// <summary>
        /// Значение выражения
        /// </summary>
        public bool Value { get; set; }

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

        public override string ToString()
        {
            return Value ? "#t" : "#f";
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return this;
        }

        public LogicObject(bool Value)
        {
            this.Value = Value;
        }
    }
}