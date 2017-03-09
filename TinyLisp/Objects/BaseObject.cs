using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

using NumObjFunc = TinyLisp.Objects.NumberObject.NumberFunction;
using LogObjFunc = TinyLisp.Objects.LogicObject.LogicFunction;
using NumFunc = System.Func<double, double>;

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

        public abstract BaseObject Eval(LispEnvironment env, List<BaseObject> args);
    }

    /// <summary>
    /// Набор базовых функций языка
    /// </summary>
    public static class BaseFunctions
    {
        public static AutoResetEvent InputCompleted = new AutoResetEvent(false);

        private static Random randomGenerator = new Random();

        public static void RestrictParametersNumber(List<BaseObject> args, string funcName, int nParams)
        {
            if (args != null && args.Count != nParams)
            {
                string msg = String.Format("Функция {0} требует {1} параметра(ов)", funcName, nParams);
                throw new ApplicationException(msg);
            }
        }

        public static int GetInteger(LispEnvironment env, List<BaseObject> args, int Index)
        {
            NumberObject evald = (NumberObject)args[Index].Eval(env, null);
            int number = (int)evald.Value;
            return number;
        }

        private static ListObject GetList(LispEnvironment env, BaseObject args, string funcName)
        {
            ListObject paramsList = args as ListObject;
            if (args is SymbolObject)
                paramsList = env.GetObject(args.Name) as ListObject;
            if (!(paramsList is ListObject))
                throw new Exception(String.Format("Не задан список в функции {0}!", funcName));
            return (ListObject)paramsList.Eval(env, null);
        }

        public static BaseObject FirstOfList(LispEnvironment env, List<BaseObject> list)
        {
            RestrictParametersNumber(list, "car", 1);
            ListObject slice = GetList(env, list[0], "car");
            return slice.Items[0] as BaseObject;
        }

        public static ListObject RestOfList(LispEnvironment env, List<BaseObject> list)
        {
            RestrictParametersNumber(list, "cdr", 1);
            ListObject slice = GetList(env, list[0], "cdr");
            List<BaseObject> newList = new List<BaseObject>();
            for (int i = 1; i < slice.Items.Count; i++)
                newList.Add(slice.Items[i]);
            return new ListObject(env, newList);
        }

        public static ListObject ConstructList(LispEnvironment env, List<BaseObject> list)
        {
            RestrictParametersNumber(list, "cons", 2);
            BaseObject newElement = list[0].Eval(env, null);
            ListObject slice = GetList(env, list[1], "cons");
            ListObject newList = new ListObject(slice.Items);
            newList.Items.Insert(0, newElement);
            return newList;
        }

        public static BaseObject MapFunction(LispEnvironment env, List<BaseObject> args)
        {
            RestrictParametersNumber(args, "map", 2);
            ListObject output = new ListObject();
            BaseObject func = args[0];
            if (func is SymbolObject || func is ListObject)
                func = func.Eval(env, null);
            IApplyable applyableFunc = (IApplyable)func;
            BaseObject list = args[1].Eval(env, null);
            ListObject evaluatedList = GetList(env, list, "map");
            for (int i = 0; i < evaluatedList.Items.Count; i++)
            {
                List<BaseObject> funcParameter = new List<BaseObject>(1);
                funcParameter.Add(evaluatedList.Items[i]);
                BaseObject result = applyableFunc.Apply(env, funcParameter);
                output.AddParameter(result);
            }
            return output;
        }

        public static NumberObject ListItemsCount(LispEnvironment env, List<BaseObject> args)
        {
            RestrictParametersNumber(args, "length", 1);
            ListObject targetList = GetList(env, args[0], "length");
            int listCount = targetList.Items.Count;
            return new NumberObject(listCount);
        }

        public static BaseObject ListGetItem(LispEnvironment env, List<BaseObject> args)
        {
            RestrictParametersNumber(args, "list-ref", 2);
            ListObject targetList = GetList(env, args[0], "list-ref");
            int index = GetInteger(env, args, 1);
            return targetList.Items[index].Eval(env, null);
        }

        public static BaseObject ListSetItem(LispEnvironment env, List<BaseObject> args)
        {
            RestrictParametersNumber(args, "list-set!", 3);
            BaseObject newItem = args[1].Eval(env, null);
            ListObject targetList = GetList(env, args[0], "lest-set");
            int index = GetInteger(env, args, 2);
            targetList.Items[index] = newItem;
            return newItem;
        }

        public static void Print(LispEnvironment env, List<BaseObject> list)
        {
            RestrictParametersNumber(list, "print", 1);
            BaseObject EvalResult = list[0].Eval(env, null);
            if (EvalResult != null)
            {
                frmMain.UI.AppendOutputText(EvalResult.ToString());
            }
        }

        public static void NewLine()
        {
            frmMain.UI.AppendOutputText(Environment.NewLine);
        }

        private static void BeginInputAndWait()
        {
            RestrictParametersNumber(null, "read", 0);
            frmMain.UI.BeginInput();
            InputCompleted.Reset();
            InputCompleted.WaitOne();
        }

        public static BaseObject Input()
        {
            BeginInputAndWait();
            string inString = TinyLisp.frmMain.LastEnteredString;
            double possibleNumber;
            if (Parser.TryParseDouble(inString, out possibleNumber))
                return new NumberObject(possibleNumber);
            else
                return new StringObject(inString);
        }

        public static NumberObject NumberReduce(LispEnvironment env, NumObjFunc func, List<BaseObject> args)
        {
            NumberObject temp = args[0].Eval(env, null) as NumberObject;
            for (int i = 1; i < args.Count; i++)
                temp = func(temp, args[i].Eval(env, null) as NumberObject);
            return temp;
        }

        public static NumberObject InverseSign(LispEnvironment env, List<BaseObject> args)
        {
            NumberObject temp = args[0].Eval(env, null) as NumberObject;
            NumberObject inverted = new NumberObject(-temp.Value);
            return inverted;
        }

        public static LogicObject ComparsionReduce(LispEnvironment env, int[] expect, List<BaseObject> args)
        {
            bool resultValue = true;
            for (int i = 1; i < args.Count; i++)
            {
                NumberObject first = (args[i - 1] as BaseObject).Eval(env, null) as NumberObject;
                NumberObject second = (args[i] as BaseObject).Eval(env, null) as NumberObject;

                int result = first.Value.CompareTo(second.Value);
                if (!expect.Contains(result))
                {
                    resultValue = false;
                    break;
                }
            }
            return new LogicObject(resultValue);
        }

        public static LogicObject LogicReduce(LispEnvironment env, LogObjFunc func, bool stopAt, List<BaseObject> arg)
        {
            for (int i = 1; i < arg.Count; i++)
            {
                LogicObject first = (arg[i - 1] as BaseObject).Eval(env, null) as LogicObject;
                LogicObject second = (arg[i] as BaseObject).Eval(env, null) as LogicObject;
                LogicObject Result = func(first, second);
                if (Result.Value == stopAt)
                    return Result;
            }
            return new LogicObject(!stopAt);
        }

        public static LogicObject Inverse(LispEnvironment env, LogicObject cond)
        {
            return new LogicObject(!cond.Value);
        }

        public static LogicObject IsEmptyList(LispEnvironment env, ListObject element)
        {
            return new LogicObject(element.Items.Count == 0);
        }

        public static LogicObject IsList(LispEnvironment env, BaseObject element)
        {
            return new LogicObject(element is ListObject);
        }

        public static NumberObject ApplyMath(LispEnvironment env, NumFunc func, BaseObject number)
        {
            double inValue = ((NumberObject)number.Eval(env, null)).Value;
            double result = func(inValue);
            return new NumberObject(result);
        }

        public static NumberObject GetRandom(LispEnvironment env, List<BaseObject> args)
        {
            RestrictParametersNumber(args, "random", 2);
            int from = GetInteger(env, args, 0);
            int to = GetInteger(env, args, 1);
            double result = randomGenerator.Next(from, to);
            return new NumberObject(result);
        }
    }
}