using System;
using System.Text;
using System.Collections.Generic;

public class ListObject : BaseObject
{
    public List<BaseObject> List;

    public ListObject()
    {
        this.List = new List<BaseObject>();
    }

    public ListObject(List<BaseObject> List)
    {
        this.List = List;
    }

    public ListObject(LispEnvironment Environment, List<BaseObject> List)
    {
        this.List = new List<BaseObject>();
        foreach(BaseObject obj in List)
            this.List.Add(obj.Eval(Environment, null));
    }

    public void AddParameter(BaseObject obj)
    {
        List.Add(obj);
    }

    public override string ToString()
    {
        StringBuilder contents = new StringBuilder();
        foreach (BaseObject obj in List)
        {
            if(obj != null)
            {
                contents.Append(obj.ToString());
                contents.Append(' ');
            }
        }
        return String.Format("({0})", contents.ToString().Trim());
    }

    public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
    {
        if (this.Quoted)
            return this;
        BaseObject func = List[0];
        if (func.Quoted)
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
            List<BaseObject> ParamsList = List.GetRange(1, List.Count - 1);

            bool tailRecursionBegins = func is LambdaObject && (func as LambdaObject).TailRecursive;
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
            throw new ApplicationException(String.Format("Первый элемент списка - неприменимый объект: {0}", func.ToString()));;
    }
}