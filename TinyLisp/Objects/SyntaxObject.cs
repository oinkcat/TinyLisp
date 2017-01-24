using System;
using System.Collections.Generic;

public class SyntaxObject : BaseObject, IApplyable
{
    public SyntaxObject(string Name)
    {
        this.Name = Name;
    }

    public BaseObject Apply(LispEnvironment Environment, List<BaseObject> Params)
    {
        switch (Name)
        {
            case "begin":
                foreach (BaseObject obj in Params)
                    Environment.LastResult = obj.Eval(Environment, null);
                return Environment.LastResult;
            case "define":
            case "set!":
                string symbolName = null;
                BaseObject symbol = Params[0];
                BaseObject definition = null;
                if (symbol is SymbolObject)
                {
                    symbolName = symbol.Name;
                    LispEnvironment.CurrentDefinition = symbolName;
                    definition = Params[1].Eval(Environment, null);
                }
                else if (symbol is ListObject) // Упрощенное определение функции
                {
                    List<BaseObject> headList = ((ListObject)symbol).List;
                    symbolName = headList[0].Name;
                    LispEnvironment.CurrentDefinition = symbolName;
                    ListObject funcParameters = new ListObject();
                    if(headList.Count > 1)
                        funcParameters.List = headList.GetRange(1, headList.Count - 1);
                    List<BaseObject> bodyList = Params.GetRange(1, Params.Count - 1);
                    ListObject funcBody = bodyList.Count > 1 ? new ListObject(bodyList) : (ListObject)bodyList[0];
                    if (bodyList.Count > 1)
                    {
                        funcBody.List.Insert(0, new SyntaxObject("begin"));
                    }
                    definition = new LambdaObject(funcParameters, funcBody, Environment);
                }
                
                if(Name == "define")
                    Environment.PutGlobalObject(symbolName, definition);
                else
                    Environment.PutLocalObject(symbolName, definition);
                return definition;
            case "if":
                BaseObject cond = ((ListObject)Params[0]).Eval(Environment, null);
                int branchIndex = (cond as LogicObject).Value ? 1 : 2;
                if (!(branchIndex == 2 && Params.Count == 2))
                {
                    BaseObject result = Params[branchIndex].Eval(Environment, null);
                    Environment.LastResult = result;
                    return result;
                }
                break;
            case "lambda":
                ListObject parameters = (ListObject)Params[0];
                ListObject lambdaBody = (ListObject)Params[1];
                return new LambdaObject(parameters, lambdaBody, Environment);
        }
        return null;
    }

    public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
    {
        return this;
    }

    public override string ToString()
    {
        return "Syntax";
    }
}