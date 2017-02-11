using System;
using System.Collections.Generic;
using TinyLisp.Objects;

namespace TinyLisp
{
    public class LispEnvironment
    {
        public static LispEnvironment MainEnvironment = null;

        public static Dictionary<string, BaseObject> globals = new Dictionary<string, BaseObject>();

        private LispEnvironment prevEnvironment = null;
        private Dictionary<string, BaseObject> locals;
        public LambdaObject Caller;
        public BaseObject LastResult;

        public static string CurrentDefinition;

        public LispEnvironment()
        {
            globals = new Dictionary<string, BaseObject>();
            locals = new Dictionary<string, BaseObject>();
            PutGlobalObject("pi", new NumberObject(Math.PI));
            PutGlobalObject("e", new NumberObject(Math.E));
        }

        public LispEnvironment(LispEnvironment Previous, LambdaObject Caller)
        {
            locals = new Dictionary<string, BaseObject>();
            this.Caller = Caller;
            prevEnvironment = Previous;
        }

        public LispEnvironment NewEnvironment(LambdaObject Caller)
        {
            LispEnvironment newEnv = new LispEnvironment(this, Caller);
            return newEnv;
        }

        public BaseObject GetLocalRecursive(string Symbol)
        {
            if (locals.ContainsKey(Symbol))
            {
                return locals[Symbol];
            }
            else
            {
                if (prevEnvironment != null)
                    return prevEnvironment.GetLocalRecursive(Symbol);
                else
                    throw new Exception(String.Format("Символ '{0}' не определен", Symbol));
            }
        }

        public static BaseObject GetGlobalObject(string Symbol)
        {
            return globals.ContainsKey(Symbol) ? globals[Symbol] : null;
        }

        public BaseObject GetObject(string Symbol)
        {
            return globals.ContainsKey(Symbol) ? globals[Symbol] : GetLocalRecursive(Symbol);
        }

        public void PutLocalObject(string Symbol, BaseObject LispObject)
        {
            locals[Symbol] = LispObject;
        }

        public void PutGlobalObject(string Symbol, BaseObject LispObject)
        {
            globals[Symbol] = LispObject;
        }
    }
}