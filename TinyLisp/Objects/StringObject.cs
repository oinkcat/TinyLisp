using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    public class StringObject : BaseObject
    {
        public string Value;

        public StringObject(string Value)
        {
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return this;
        }
    }
}