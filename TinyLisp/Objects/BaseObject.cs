using System;
using System.Threading;
using System.Collections.Generic;

namespace TinyLisp.Objects
{
    /// <summary>
    /// Объект, который может быть применен к параметрам
    /// </summary>
    public interface IApplyable
    {
        BaseObject Apply(LispEnvironment Environment, List<BaseObject> Params);
    }

    /// <summary>
    /// Базовый класс объекта языка
    /// </summary>
    public abstract class BaseObject
    {
        public string Name { get; set; }

        public bool IsQuoted { get; set; }

        public abstract BaseObject Eval(LispEnvironment Environment, List<BaseObject> Params);
    }

    public static class BaseFunctions
    {
        public delegate double MathFunction(double value);
        public delegate void OutputFunction(string Text);
        public static OutputFunction Output = null;
        public static AutoResetEvent InputCompleted = new AutoResetEvent(false);

        private static Random randomGenerator = new Random();

        public static void RestrictParametersNumber(List<BaseObject> Params, string FunctionName, int RestrictNumberTo)
        {
            if (Params != null && Params.Count != RestrictNumberTo)
            {
                throw new ApplicationException(String.Format("Функция {0} требует {1} параметра(ов)", FunctionName, RestrictNumberTo));
            }
        }

        public static int GetInteger(LispEnvironment Environment, List<BaseObject> Params, int Index)
        {
            NumberObject evald = (NumberObject)Params[Index].Eval(Environment, null);
            int number = (int)evald.Value;
            return number;
        }

        private static ListObject GetList(LispEnvironment Environment, BaseObject Param, string FuncName)
        {
            ListObject paramsList = Param as ListObject;
            if (Param is SymbolObject)
                paramsList = Environment.GetObject(Param.Name) as ListObject;
            if (!(paramsList is ListObject))
                throw new Exception(String.Format("Не задан список в функции {0}!", FuncName));
            return (ListObject)paramsList.Eval(Environment, null);
        }

        public static BaseObject FirstOfList(LispEnvironment Environment, List<BaseObject> List)
        {
            RestrictParametersNumber(List, "car", 1);
            ListObject list = GetList(Environment, List[0], "car");
            return list.Items[0] as BaseObject;
        }

        public static ListObject RestOfList(LispEnvironment Environment, List<BaseObject> List)
        {
            RestrictParametersNumber(List, "cdr", 1);
            ListObject list = GetList(Environment, List[0], "cdr");
            List<BaseObject> newList = new List<BaseObject>();
            for (int i = 1; i < list.Items.Count; i++)
                newList.Add(list.Items[i]);
            return new ListObject(Environment, newList);
        }

        public static ListObject ConstructList(LispEnvironment Environment, List<BaseObject> List)
        {
            RestrictParametersNumber(List, "cons", 2);
            BaseObject newElement = List[0].Eval(Environment, null);
            ListObject list = GetList(Environment, List[1], "cons");
            ListObject newList = new ListObject(list.Items);
            newList.Items.Insert(0, newElement);
            return newList;
        }

        public static BaseObject MapFunction(LispEnvironment Environment, List<BaseObject> Params)
        {
            RestrictParametersNumber(Params, "map", 2);
            ListObject output = new ListObject();
            BaseObject func = Params[0];
            if (func is SymbolObject || func is ListObject)
                func = func.Eval(Environment, null);
            IApplyable applyableFunc = (IApplyable)func;
            BaseObject list = Params[1].Eval(Environment, null);
            ListObject evaluatedList = GetList(Environment, list, "map");
            for (int i = 0; i < evaluatedList.Items.Count; i++)
            {
                List<BaseObject> funcParameter = new List<BaseObject>(1);
                funcParameter.Add(evaluatedList.Items[i]);
                BaseObject result = applyableFunc.Apply(Environment, funcParameter);
                output.AddParameter(result);
            }
            return output;
        }

        public static NumberObject ListItemsCount(LispEnvironment Environment, List<BaseObject> Params)
        {
            RestrictParametersNumber(Params, "length", 1);
            ListObject targetList = GetList(Environment, Params[0], "length");
            int listCount = targetList.Items.Count;
            return new NumberObject(listCount);
        }

        public static BaseObject ListGetItem(LispEnvironment Environment, List<BaseObject> Params)
        {
            RestrictParametersNumber(Params, "list-ref", 2);
            ListObject targetList = GetList(Environment, Params[0], "list-ref");
            int index = GetInteger(Environment, Params, 1);
            return targetList.Items[index].Eval(Environment, null);
        }

        public static BaseObject ListSetItem(LispEnvironment Environment, List<BaseObject> Params)
        {
            RestrictParametersNumber(Params, "list-set!", 3);
            BaseObject newItem = Params[1].Eval(Environment, null);
            ListObject targetList = GetList(Environment, Params[0], "lest-set");
            int index = GetInteger(Environment, Params, 2);
            targetList.Items[index] = newItem;
            return newItem;
        }

        public static void Print(LispEnvironment Environment, List<BaseObject> List)
        {
            RestrictParametersNumber(List, "print", 1);
            BaseObject EvalResult = List[0].Eval(Environment, null);
            if (EvalResult != null)
            {
                if (Output != null)
                    Output(EvalResult.ToString());
            }
        }

        public static void NewLine()
        {
            if (Output != null)
                Output("\r\n");
        }

        private static void BeginInputAndWait()
        {
            RestrictParametersNumber(null, "read", 0);
            TinyLisp.frmMain.ActiveForm.Invoke(TinyLisp.frmMain.BeginUserInput);
            InputCompleted.Reset();
            InputCompleted.WaitOne();
        }

        public static BaseObject Input()
        {
            BeginInputAndWait();
            string inString = TinyLisp.frmMain.EnteredString;
            double possibleNumber;
            if (Parser.TryParseDouble(inString, out possibleNumber))
                return new NumberObject(possibleNumber);
            else
                return new StringObject(inString);
        }

        public static NumberObject NumberReduce(LispEnvironment Environment, NumberObject.NumberFunction Func, List<BaseObject> Params)
        {
            NumberObject temp = Params[0].Eval(Environment, null) as NumberObject;
            for (int i = 1; i < Params.Count; i++)
                temp = Func(temp, Params[i].Eval(Environment, null) as NumberObject);
            return temp;
        }

        public static NumberObject InverseSign(LispEnvironment Environment, List<BaseObject> Params)
        {
            NumberObject temp = Params[0].Eval(Environment, null) as NumberObject;
            NumberObject inverted = new NumberObject(-temp.Value);
            return inverted;
        }

        public static LogicObject ComparsionReduce(LispEnvironment Environment, int Expected, int AlsoExpected, List<BaseObject> Params)
        {
            bool resultValue = true;
            for (int i = 1; i < Params.Count; i++)
            {
                NumberObject first = (Params[i - 1] as BaseObject).Eval(Environment, null) as NumberObject;
                NumberObject second = (Params[i] as BaseObject).Eval(Environment, null) as NumberObject;
                int result = first.Value.CompareTo(second.Value);
                if (result != Expected && result != AlsoExpected)
                {
                    resultValue = false;
                    break;
                }
            }
            return new LogicObject(resultValue);
        }

        public static LogicObject LogicReduce(LispEnvironment Environment, LogicObject.LogicFunction Func, bool Expected, List<BaseObject> Params)
        {
            for (int i = 1; i < Params.Count; i++)
            {
                LogicObject first = (Params[i - 1] as BaseObject).Eval(Environment, null) as LogicObject;
                LogicObject second = (Params[i] as BaseObject).Eval(Environment, null) as LogicObject;
                LogicObject Result = Func(first, second);
                if (Result.Value == Expected)
                    return Result;
            }
            return new LogicObject(!Expected);
        }

        public static LogicObject Inverse(LispEnvironment Environment, LogicObject Condition)
        {
            return new LogicObject(!Condition.Value);
        }

        public static LogicObject IsEmptyList(LispEnvironment Environment, ListObject Element)
        {
            return new LogicObject(Element.Items.Count == 0);
        }

        public static LogicObject IsList(LispEnvironment Environment, BaseObject Element)
        {
            return new LogicObject(Element is ListObject);
        }

        public static NumberObject SimpleMathFunction(LispEnvironment Environment, MathFunction Func, BaseObject Number)
        {
            double inValue = ((NumberObject)Number.Eval(Environment, null)).Value;
            double result = Func(inValue);
            return new NumberObject(result);
        }

        public static NumberObject GetRandom(LispEnvironment Environment, List<BaseObject> Params)
        {
            RestrictParametersNumber(Params, "random", 2);
            int from = GetInteger(Environment, Params, 0);
            int to = GetInteger(Environment, Params, 1);
            double result = randomGenerator.Next(from, to);
            return new NumberObject(result);
        }
    }
}