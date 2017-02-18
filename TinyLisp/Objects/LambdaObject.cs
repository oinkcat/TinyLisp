using System;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    /// <summary>
    /// Лямбда-выражение
    /// </summary>
    public class LambdaObject : BaseObject, IApplyable
    {
        public class TailRecursionException : ApplicationException
        {
            public List<BaseObject> Parameters;

            public TailRecursionException(List<BaseObject> Parameters)
            {
                this.Parameters = Parameters;
            }
        }

        public bool IsTailRecursive { get; set; }

        private int paramsCount;
        private string[] paramNames;

        private ListObject Body;
        private LispEnvironment localEnvironment;

        // Определить наличие хвостовой рекурсии
        private bool FindTailRecursion(ListObject List)
        {
            bool hasRecursion = false, hasTailRecursion = false;
            List<BaseObject> sExpr = List.Items;
            BaseObject first = null;
            if (sExpr.Count > 0)
            {
                first = sExpr[0];
                if (!(first is SyntaxObject || first is SymbolObject))
                {
                    return false;
                }
            }
            else
                return false;

            int i = 0;
            int foundAt = -1;
            foreach (BaseObject obj in sExpr)
            {
                i++;
                if (obj is SymbolObject)
                {
                    if (obj.Name == LispEnvironment.LastDefinition)
                        return true;
                }
                else if (obj is ListObject)
                {
                    hasRecursion = FindTailRecursion(obj as ListObject) && foundAt == -1;
                    if (hasRecursion)
                        foundAt = i;
                }
            }
            hasTailRecursion = foundAt == i || foundAt > 0 && first.Name == "if";
            return hasTailRecursion;
        }

        public BaseObject Apply(LispEnvironment Environment, List<BaseObject> Params)
        {
            LispEnvironment callEnvironment = localEnvironment.NewEnvironment(this);
            for (int i = 0; i < Params.Count; i++)
            {
                string symbolName = paramNames[i];
                BaseObject symbol = !(Params[i] is LambdaObject) ? Params[i].Eval(Environment, null) : Params[i];
                Params[i] = symbol;
                callEnvironment.PutLocalObject(symbolName, symbol);
            }
            return Body.Eval(callEnvironment, null);
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            return this;
        }

        public override string ToString()
        {
            return "<Lambda>";
        }

        public LambdaObject(ListObject Params, ListObject Body, LispEnvironment Environment)
        {
            this.localEnvironment = Environment.NewEnvironment(this);
            this.paramsCount = Params.Items.Count;
            if (paramsCount > 0)
            {
                this.paramNames = new string[paramsCount];
                for (int i = 0; i < Params.Items.Count; i++)
                {
                    paramNames[i] = Params.Items[i].Name;
                    localEnvironment.PutLocalObject(Params.Items[i].Name, Params.Items[i]);
                }
            }
            this.Body = Body;
            this.IsTailRecursive = FindTailRecursion(Body);
        }
    }
}