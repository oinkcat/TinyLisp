using System;
using System.Collections.Generic;
using TinyLisp.Objects;

namespace TinyLisp
{
    /// <summary>
    /// Среда выполнения функции (переменные, информация о вызове)
    /// </summary>
    public class LispEnvironment
    {
        /// <summary>
        /// Текущая среда выполнения
        /// </summary>
        public static LispEnvironment Current { get; set; }

        /// <summary>
        /// Глобальные переменные
        /// </summary>
        public static Dictionary<string, BaseObject> Variables { get; set; }

        // Среда, в которую вложена текущая
        private LispEnvironment parent = null;

        /// <summary>
        /// Локальные переменные
        /// </summary>
        private Dictionary<string, BaseObject> locals;

        /// <summary>
        /// Текущая выполняющаяся функция
        /// </summary>
        public LambdaObject Caller { get; set; }

        /// <summary>
        /// Результат выполнения последнего выражения
        /// </summary>
        public BaseObject LastResult { get; set; }

        /// <summary>
        /// Последняя установленная переменная
        /// </summary>
        public static string LastDefinition { get; set; }

        /// <summary>
        /// Создать новую среду
        /// </summary>
        /// <param name="caller">Выполняющаяся функция</param>
        /// <returns>Новая среда</returns>
        public LispEnvironment NewEnvironment(LambdaObject caller)
        {
            LispEnvironment newEnv = new LispEnvironment(this, caller);
            return newEnv;
        }

        /// <summary>
        /// Выдать локальную переменную
        /// </summary>
        /// <param name="symbol">Имя объекта</param>
        /// <returns>Запрошенный объект</returns>
        public BaseObject GetLocalRecursive(string symbol)
        {
            if (locals.ContainsKey(symbol))
            {
                return locals[symbol];
            }
            else
            {
                if (parent != null)
                    return parent.GetLocalRecursive(symbol);
                else
                    throw new Exception(String.Format("Символ '{0}' не определен", symbol));
            }
        }

        /// <summary>
        /// Выдать глобальную переменную
        /// </summary>
        /// <param name="symbol">Имя переменной</param>
        /// <returns>Объект-значение переменной</returns>
        public static BaseObject GetGlobalObject(string symbol)
        {
            return Variables.ContainsKey(symbol) ? Variables[symbol] : null;
        }

        /// <summary>
        /// Выдать глобальную/локальную переменную
        /// </summary>
        /// <param name="symbol">Имя переменной</param>
        /// <returns>Объект-значение переменной</returns>
        public BaseObject GetObject(string symbol)
        {
            return Variables.ContainsKey(symbol) ? Variables[symbol] : GetLocalRecursive(symbol);
        }

        /// <summary>
        /// Поместить объект в локальную переменную
        /// </summary>
        /// <param name="symbol">Имя переменной</param>
        /// <param name="lispObject">Помещаемый объект</param>
        public void PutLocalObject(string symbol, BaseObject lispObject)
        {
            locals[symbol] = lispObject;
        }

        /// <summary>
        /// Поместить объект в глобальную переменную
        /// </summary>
        /// <param name="symbol">Имя переменной</param>
        /// <param name="lispObject">Помещаемый объект</param>
        public void PutGlobalObject(string symbol, BaseObject lispObject)
        {
            Variables[symbol] = lispObject;
        }

        public LispEnvironment(LispEnvironment parent, LambdaObject caller)
        {
            locals = new Dictionary<string, BaseObject>();
            this.Caller = caller;
            this.parent = parent;
        }

        public LispEnvironment()
        {
            Variables = new Dictionary<string, BaseObject>();
            locals = new Dictionary<string, BaseObject>();
            PutGlobalObject("pi", new NumberObject(Math.PI));
            PutGlobalObject("e", new NumberObject(Math.E));
        }
    }
}