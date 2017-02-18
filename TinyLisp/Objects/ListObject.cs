using System;
using System.Text;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    /// <summary>
    /// Список
    /// </summary>
    public class ListObject : BaseObject
    {
        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<BaseObject> Items { get; set; }

        public void AddParameter(BaseObject obj)
        {
            Items.Add(obj);
        }

        public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
        {
            if (this.IsQuoted)
                return this;
            BaseObject func = Items[0];
            if (func.IsQuoted)
                return func;
            if (func is SymbolObject)
            {
                func = func.Eval(Environment, null);
            }
            if (func is ListObject)
            {
                func = func.Eval(Environment, null);
            }
            if (func is IApplyable)
            {
                BaseObject applyResult = null;
                List<BaseObject> ParamsList = Items.GetRange(1, Items.Count - 1);

                bool tailRecursionBegins = func is LambdaObject && (func as LambdaObject).IsTailRecursive;
                if (tailRecursionBegins)
                {
                    bool tailRecursionContinues = Environment.Caller == func;
                    if (tailRecursionContinues)
                    {
                        for (int i = 0; i < ParamsList.Count; i++)
                            ParamsList[i] = ParamsList[i].Eval(Environment, null);
                        // Пипец конечно...
                        throw new LambdaObject.TailRecursionException(ParamsList);
                    }

                    bool tailRecursion = false;
                    do
                    {
                        try
                        {
                            tailRecursion = false;
                            applyResult = (func as IApplyable).Apply(Environment, ParamsList);
                        }
                        catch (LambdaObject.TailRecursionException e)
                        {
                            ParamsList = e.Parameters;
                            tailRecursion = true;
                        }
                    }
                    while (tailRecursion);
                }
                else
                    applyResult = (func as IApplyable).Apply(Environment, ParamsList);

                return applyResult;
            }
            else
                throw new ApplicationException(String.Format("Первый элемент списка - неприменимый объект: {0}", func.ToString())); ;
        }

        public override string ToString()
        {
            StringBuilder contents = new StringBuilder();
            foreach (BaseObject obj in Items)
            {
                if (obj != null)
                {
                    contents.Append(obj.ToString());
                    contents.Append(' ');
                }
            }
            return String.Format("({0})", contents.ToString().Trim());
        }

        public ListObject(LispEnvironment Environment, List<BaseObject> List)
        {
            this.Items = new List<BaseObject>();
            foreach (BaseObject obj in List)
                this.Items.Add(obj.Eval(Environment, null));
        }

        public ListObject(List<BaseObject> List)
        {
            this.Items = List;
        }

        public ListObject()
        {
            this.Items = new List<BaseObject>();
        }
    }
}