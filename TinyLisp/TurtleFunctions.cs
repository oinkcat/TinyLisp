using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

public static class TurtleFunctions
{
    public static void Wait(LispEnvironment Environment, List<BaseObject> Params)
    {
        int howMuch = BaseFunctions.GetInteger(Environment, Params, 0);
        Thread.Sleep(howMuch);
    }

    public static void Go(LispEnvironment Environment, List<BaseObject> Params, int Direction)
    {
        int distance = BaseFunctions.GetInteger(Environment, Params, 0) * Direction;
        TurtlesManager.CurrentTurtle.MoveRelative(distance);
    }

    public static void Move(LispEnvironment Environment, List<BaseObject> Params)
    {
        int x = BaseFunctions.GetInteger(Environment, Params, 0);
        int y = BaseFunctions.GetInteger(Environment, Params, 1);
        TurtlesManager.CurrentTurtle.MoveTo(x, y);
    }

    public static void Rotate(LispEnvironment Environment, List<BaseObject> Params, int Direction)
    {
        int delta = BaseFunctions.GetInteger(Environment, Params, 0) * Direction;
        TurtlesManager.CurrentTurtle.Rotate(delta);
    }

    public static void SetRotation(LispEnvironment Environment, List<BaseObject> Params)
    {
        int angle = BaseFunctions.GetInteger(Environment, Params, 0);
        TurtlesManager.CurrentTurtle.SetRotation(angle);
    }

    public static void NewTurtle()
    {
        TurtlesManager.CreateTurtle();
    }

    public static void TogglePainting(bool Enabled)
    {
        TurtlesManager.CurrentTurtle.Painting = Enabled;
    }

    public static void EraseGraphics()
    {
        TurtlesManager.CurrentTurtle.Erase();
    }

    public static NumberObject ColorFromRGB(LispEnvironment Environment, List<BaseObject> Params)
    {
        int r = (BaseFunctions.GetInteger(Environment, Params, 0) & 0xFF) << 16;
        int g = (BaseFunctions.GetInteger(Environment, Params, 1) & 0xFF) << 8;
        int b = BaseFunctions.GetInteger(Environment, Params, 2) & 0xFF;

        int color = r | g | b;
        return new NumberObject(color);
    }

    public static void SetColor(LispEnvironment Environment, List<BaseObject> Params)
    {
        int color = BaseFunctions.GetInteger(Environment, Params, 0);
        TurtlesManager.CurrentTurtle.SetColor(color);
    }
}