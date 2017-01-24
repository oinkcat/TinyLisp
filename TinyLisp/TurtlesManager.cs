using System;
using System.Collections.Generic;
using System.Text;

static class TurtlesManager
{
    public static TinyLisp.frmGraphics GraphicForm;

    public static Turtle CurrentTurtle;

    private static void InitializeGraphics()
    {
        if (GraphicForm == null || GraphicForm.Visible == false)
        {
            GraphicForm = new TinyLisp.frmGraphics();
            TinyLisp.frmMain.ActiveForm.Invoke(TinyLisp.frmMain.ShowGraphicsForm);
        }
    }

    public static void CreateTurtle()
    {
        InitializeGraphics();
        CurrentTurtle = new Turtle(GraphicForm.GetGraphics());
    }
}
