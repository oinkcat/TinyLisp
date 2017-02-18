using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using TinyLisp;
using TinyLisp.Objects;

/// <summary>
/// Реализация функций "черепашьей" графики
/// </summary>
public static class TurtleFunctions
{
    /// <summary>
    /// Ждать некоторое кол-во миллисекунд
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (число: время)</param>
    public static void Wait(LispEnvironment Environment, List<BaseObject> Params)
    {
        int howMuch = BaseFunctions.GetInteger(Environment, Params, 0);
        Thread.Sleep(howMuch);
    }

    /// <summary>
    /// Передвинуть перо вперед
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (число: расстояние)</param>
    /// <param name="Direction">Направление: вперед/назад</param>
    public static void Go(LispEnvironment Environment, List<BaseObject> Params, int Direction)
    {
        int distance = BaseFunctions.GetInteger(Environment, Params, 0) * Direction;
        TurtlesManager.CurrentTurtle.MoveRelative(distance);
    }

    /// <summary>
    /// Передвинуть перо в точку с заданными координатами
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (числа: новые координаты)</param>
    public static void Move(LispEnvironment Environment, List<BaseObject> Params)
    {
        int x = BaseFunctions.GetInteger(Environment, Params, 0);
        int y = BaseFunctions.GetInteger(Environment, Params, 1);
        TurtlesManager.CurrentTurtle.MoveTo(x, y);
    }

    /// <summary>
    /// Изменить направление движения пера
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (число: угол поворота пера)</param>
    /// <param name="Direction">Направление: влево/вправо</param>
    public static void Rotate(LispEnvironment Environment, List<BaseObject> Params, int Direction)
    {
        int delta = BaseFunctions.GetInteger(Environment, Params, 0) * Direction;
        TurtlesManager.CurrentTurtle.Rotate(delta);
    }

    /// <summary>
    /// Установить угол поворота пера
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (число: заданный угол поворота)</param>
    public static void SetRotation(LispEnvironment Environment, List<BaseObject> Params)
    {
        int angle = BaseFunctions.GetInteger(Environment, Params, 0);
        TurtlesManager.CurrentTurtle.SetRotation(angle);
    }

    /// <summary>
    /// Создать перо ("Черепашку"), рисующую графику
    /// </summary>
    public static void NewTurtle()
    {
        TurtlesManager.CreateTurtle();
    }

    /// <summary>
    /// Установить режим рисования
    /// </summary>
    /// <param name="Enabled">Рисование разрешено</param>
    public static void TogglePainting(bool Enabled)
    {
        TurtlesManager.CurrentTurtle.Painting = Enabled;
    }

    /// <summary>
    /// Очистить область вывода графики
    /// </summary>
    public static void EraseGraphics()
    {
        TurtlesManager.CurrentTurtle.Erase();
    }

    /// <summary>
    /// Получить значение цвета из значений его компонент
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (числа: компоненты цвета)</param>
    /// <returns>Объект-число со значением цвета</returns>
    public static NumberObject ColorFromRGB(LispEnvironment Environment, List<BaseObject> Params)
    {
        int r = (BaseFunctions.GetInteger(Environment, Params, 0) & 0xFF) << 16;
        int g = (BaseFunctions.GetInteger(Environment, Params, 1) & 0xFF) << 8;
        int b = BaseFunctions.GetInteger(Environment, Params, 2) & 0xFF;

        int color = r | g | b;
        return new NumberObject(color);
    }

    /// <summary>
    /// Установить цвет для рисования
    /// </summary>
    /// <param name="Environment">Состояние среды выполнения</param>
    /// <param name="Params">Параметры (число: значение цвета)</param>
    public static void SetColor(LispEnvironment Environment, List<BaseObject> Params)
    {
        int color = BaseFunctions.GetInteger(Environment, Params, 0);
        TurtlesManager.CurrentTurtle.SetColor(color);
    }
}