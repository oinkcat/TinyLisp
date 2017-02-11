using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TinyLisp.Objects;

/// <summary>
/// Парсер исходного кода
/// </summary>
public class Parser
{
    // Известные функции
    private string[] functions = new string[] { 
        "+", "-", "*", "/", "--", "=", "<", ">", "<=", ">=", 
        "not", "or", "and",
        "list", "map", "list-ref", "list-set!", "length",
        "display", "newline", "read",
        "car", "cdr", "cons",
        "empty?", "number?", "bool?", "string?", "list?", "lambda?",
        "abs", "sqrt", "log", "sin", "cos", "random",
        "открой-рисунок", "сотри-рисунок", "вперед", "назад", "налево", "направо",
        "новый-курс", "новое-место", "новый-цвет",
        "перо-опусти", "перо-подними", "цвет"
    };

    // Известные синтаксические конструкции
    public static string[] syntax = new string[] { 
        "begin", "define", "set!", "if", "lambda" 
    };

    private Stack<BaseObject> nestedObjects;

    public Parser()
    {
        this.nestedObjects = new Stack<BaseObject>();
    }

    // TODO: Вынести
    public static bool TryParseDouble(string value, out double result)
    {
        try     
        { 
            result = double.Parse(value);
        }
        catch   
        { 
            result = 0; 
            return false; 
        }
        return true;
    }

    private BaseObject GetObject(string value, bool quoted)
    {
        double dummy;
        BaseObject newObject = null;
        if (Array.IndexOf(functions, value) > -1)
            newObject = new FunctionObject(value);
        else if (Array.IndexOf(syntax, value) > -1)
            newObject = new SyntaxObject(value);
        else if (TryParseDouble(value.Replace('.', ','), out dummy))
            newObject = new NumberObject(dummy);
        else if (value == "#t" || value == "#f")
            newObject = new LogicObject(value == "#t");
        else if (value.StartsWith("\"") && value.EndsWith("\""))
            newObject = new StringObject(value.Substring(1, value.Length - 2));
        else if (value == "nil")
            newObject = null;
        else
            newObject = new SymbolObject(value);
        newObject.Quoted = quoted;
        quoted = false;
        return newObject;
    }

    /// <summary>
    /// Выполнить разбор исходного кода в AST
    /// </summary>
    /// <param name="source">Исходный код программы</param>
    /// <returns>Список транслированных действий</returns>
    public ListObject Parse(string source)
    {
        ListObject rootObject = new ListObject();
        rootObject.AddParameter(new SyntaxObject("begin"));

        StringBuilder newString = new StringBuilder();

        ListObject currentList = rootObject;
        ListObject childList = null;

        // Разбор лексем с помощью конечного автомата
        bool isString = false;
        bool isQuoted = false;
        bool isComment = false;
        for (int i = 0; i < source.Length; i++)
        {
            char c = source[i];
            if (c == ';' && !isString)
                isComment = true;
            else if (c == '\n')
                isComment = false;
            if (isComment)
                continue;
            if (c == '(')
            {
                nestedObjects.Push(currentList);
                currentList = new ListObject();
                if (isQuoted)
                {
                    currentList.Quoted = true;
                    isQuoted = false;
                }
            }
            else if (c == ')')
            {
                if (newString.Length > 0)
                {
                    currentList.AddParameter(GetObject(newString.ToString(), isQuoted));
                }
                newString = new StringBuilder();
                childList = currentList;
                currentList = (ListObject)nestedObjects.Pop();
                currentList.AddParameter(childList);
            }
            else if (c == ' ' && !isString && newString.Length > 0)
            {
                string value = newString.ToString();
                currentList.AddParameter(GetObject(value, isQuoted));
                newString = new StringBuilder();
            }
            else if (c == '"')
            {
                isString ^= true;
                newString.Append(c);
            }
            else if (c == '\'')
            {
                isQuoted = true;
            }
            else
            {
                if (!Char.IsWhiteSpace(c) || isString)
                    newString.Append(c);
            }
        }

        return rootObject;
    }
}