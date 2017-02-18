using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    /// <summary>
    /// Строка
    /// </summary>
    public class StringObject : BaseObject
    {
        /// <summary>
        /// Текст строки
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return this;
        }

        public StringObject(string Value)
        {
            this.Value = Value;
        }
    }
}