using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class Parser
{
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
    public static string[] syntax = new string[] { "begin", "define", "set!", "if", "lambda" };
    private Stack<BaseObject> NestedObjects;

    public Parser()
    {
        this.NestedObjects = new Stack<BaseObject>();
    }

    public static bool TryParseDouble(string Value, out double Result)
    {
        try     { Result = double.Parse(Value); }
        catch   { Result = 0; return false; }
        return true;
    }

    private BaseObject GetObject(string Value, ref bool Quoted)
    {
        double dummy;
        BaseObject newObject = null;
        if (Array.IndexOf(functions, Value) > -1)
            newObject = new FunctionObject(Value);
        else if (Array.IndexOf(syntax, Value) > -1)
            newObject = new SyntaxObject(Value);
        else if (TryParseDouble(Value.Replace('.', ','), out dummy))
            newObject = new NumberObject(dummy);
        else if (Value == "#t" || Value == "#f")
            newObject = new LogicObject(Value == "#t");
        else if (Value.StartsWith("\"") && Value.EndsWith("\""))
            newObject = new StringObject(Value.Substring(1, Value.Length - 2));
        else if (Value == "nil")
            newObject = null;
        else
            newObject = new SymbolObject(Value);
        newObject.Quoted = Quoted;
        Quoted = false;
        return newObject;
    }

    public ListObject Parse(string Source)
    {
        ListObject rootObject = new ListObject();
        rootObject.AddParameter(new SyntaxObject("begin"));

        StringBuilder newString = new StringBuilder();

        ListObject currentList = rootObject;
        ListObject childList = null;

        bool isString = false;
        bool isQuoted = false;
        bool isComment = false;
        for (int i = 0; i < Source.Length; i++)
        {
            char c = Source[i];
            if (c == ';' && !isString)
                isComment = true;
            else if (c == '\n')
                isComment = false;
            if (isComment)
                continue;
            if (c == '(')
            {
                NestedObjects.Push(currentList);
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
                    currentList.AddParameter(GetObject(newString.ToString(), ref isQuoted));
                }
                newString = new StringBuilder();
                childList = currentList;
                currentList = (ListObject)NestedObjects.Pop();
                currentList.AddParameter(childList);
            }
            else if (c == ' ' && !isString && newString.Length > 0)
            {
                string value = newString.ToString();
                currentList.AddParameter(GetObject(value, ref isQuoted));
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