using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    public class NumberObject : BaseObject
    {
        public double Value;

        #region Числовые функции

        public delegate NumberObject NumberFunction(NumberObject First, NumberObject Other);

        public static NumberObject DoAdd(NumberObject First, NumberObject Other)
        {
            return new NumberObject(First.Value + Other.Value);
        }

        public static NumberObject DoSub(NumberObject First, NumberObject Other)
        {
            return new NumberObject(First.Value - Other.Value);
        }

        public static NumberObject DoMul(NumberObject First, NumberObject Other)
        {
            return new NumberObject(First.Value * Other.Value);
        }

        public static NumberObject DoDiv(NumberObject First, NumberObject Other)
        {
            return new NumberObject(First.Value / Other.Value);
        }

        public static NumberObject DoCompare(NumberObject First, NumberObject Other)
        {
            return new NumberObject(First.Value.CompareTo(Other.Value));
        }

        #endregion

        public NumberObject(NumberObject obj)
        {
            Value = obj.Value;
        }

        public NumberObject(double Number)
        {
            Value = Number;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return this;
        }
    }
}