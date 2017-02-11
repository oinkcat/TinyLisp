using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    public class SymbolObject : BaseObject
    {
        public SymbolObject(string Name)
        {
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return !this.Quoted ? Environment.GetObject(this.Name) : this;
        }
    }
}