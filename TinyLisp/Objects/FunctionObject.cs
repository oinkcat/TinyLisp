using System;
using System.Collections.Generic;

public class FunctionObject : BaseObject, IApplyable
{
    public FunctionObject(string Name)
    {
        this.Name = Name;
    }

    public override string ToString()
    {
        return String.Format("<Function: {0}>", Name);
    }

    public BaseObject Apply(LispEnvironment Environment, List<BaseObject> Params)
    {
        switch (this.Name)
        {
            case "+":
                return BaseFunctions.NumberReduce(Environment, NumberObject.DoAdd, Params);
            case "-":
                return BaseFunctions.NumberReduce(Environment, NumberObject.DoSub, Params);
            case "*":
                return BaseFunctions.NumberReduce(Environment, NumberObject.DoMul, Params);
            case "/":
                return BaseFunctions.NumberReduce(Environment, NumberObject.DoDiv, Params);
            case "--":
                return BaseFunctions.InverseSign(Environment, Params);
            case "=":
                return BaseFunctions.ComparsionReduce(Environment, 0, 0, Params);
            case ">":
                return BaseFunctions.ComparsionReduce(Environment, 1, 1, Params);
            case "<":
                return BaseFunctions.ComparsionReduce(Environment, -1, -1, Params);
            case ">=":
                return BaseFunctions.ComparsionReduce(Environment, 1, 0, Params);
            case "<=":
                return BaseFunctions.ComparsionReduce(Environment, -1, 0, Params);
            case "or":
                return BaseFunctions.LogicReduce(Environment, LogicObject.DoOr, true, Params);
            case "and":
                return BaseFunctions.LogicReduce(Environment, LogicObject.DoAnd, false, Params);
            case "not":
                return BaseFunctions.Inverse(Environment, (LogicObject)Params[0].Eval(Environment, null));
            case "display":
                BaseFunctions.Print(Environment, Params);
                break;
            case "read":
                return BaseFunctions.Input();
            case "newline":
                BaseFunctions.NewLine();
                break;
            case "list":
                ListObject newList = new ListObject(Environment, Params);
                newList.Quoted = true;
                return newList;
            case "map":
                return BaseFunctions.MapFunction(Environment, Params);
            case "length":
                return BaseFunctions.ListItemsCount(Environment, Params);
            case "list-ref":
                return BaseFunctions.ListGetItem(Environment, Params);
            case "list-set!":
                return BaseFunctions.ListSetItem(Environment, Params);
            case "car":
                return BaseFunctions.FirstOfList(Environment, Params);
            case "cdr":
                return BaseFunctions.RestOfList(Environment, Params);
            case "cons":
                return BaseFunctions.ConstructList(Environment, Params);
            case "empty?":
                return BaseFunctions.IsEmptyList(Environment, (ListObject)Params[0].Eval(Environment, null));
            case "list?":
                return BaseFunctions.IsList(Environment, Params[0].Eval(Environment, null));
            case "abs":
                return BaseFunctions.SimpleMathFunction(Environment, Math.Abs, Params[0]);
            case "sqrt":
                return BaseFunctions.SimpleMathFunction(Environment, Math.Sqrt, Params[0]);
            case "log":
                return BaseFunctions.SimpleMathFunction(Environment, Math.Log, Params[0]);
            case "sin":
                return BaseFunctions.SimpleMathFunction(Environment, Math.Sin, Params[0]);
            case "cos":
                return BaseFunctions.SimpleMathFunction(Environment, Math.Cos, Params[0]);
            case "random":
                return BaseFunctions.GetRandom(Environment, Params);
            default:
                return ApplyTurtleFunction(Environment, Params);
        }

        return null;
    }

    public override BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params)
    {
        return this;
    }

    private BaseObject ApplyTurtleFunction(LispEnvironment Environment, List<BaseObject> Params)
    {
        switch (this.Name)
        {
            case "жди":
                TurtleFunctions.Wait(Environment, Params);
                break;
            case "открой-рисунок":
                TurtleFunctions.NewTurtle();
                TurtleFunctions.EraseGraphics();
                break;
            case "сотри-рисунок":
                TurtleFunctions.EraseGraphics();
                break;
            case "вперед":
                TurtleFunctions.Go(Environment, Params, 1);
                break;
            case "назад":
                TurtleFunctions.Go(Environment, Params, -1);
                break;
            case "налево":
                TurtleFunctions.Rotate(Environment, Params, -1);
                break;
            case "направо":
                TurtleFunctions.Rotate(Environment, Params, 1);
                break;
            case "новое-место":
                TurtleFunctions.Move(Environment, Params);
                break;
            case "новый-курс":
                TurtleFunctions.SetRotation(Environment, Params);
                break;
            case "перо-опусти":
                TurtleFunctions.TogglePainting(true);
                break;
            case "перо-подними":
                TurtleFunctions.TogglePainting(false);
                break;
            case "цвет":
                return TurtleFunctions.ColorFromRGB(Environment, Params);
            case "новый-цвет":
                TurtleFunctions.SetColor(Environment, Params);
                break;
        }

        return null;
    }
}