using TinyLisp;

/// <summary>
/// Управляет созданием окна графики
/// </summary>
public static class TurtlesManager
{
    /// <summary>
    /// Текущее открытое графическое окно
    /// </summary>
    public static frmGraphics GraphicForm { get; set; }

    /// <summary>
    /// Текущая созданная "черепашка"
    /// </summary>
    public static Turtle CurrentTurtle { get; set; }

    /// <summary>
    /// Показать окно графики и создать "черепашку"
    /// </summary>
    public static void CreateTurtle()
    {
        if (GraphicForm == null || GraphicForm.Visible == false)
        {
            GraphicForm = new frmGraphics();
            frmMain.ActiveForm.Invoke(frmMain.ShowGraphicsForm);
        }

        CurrentTurtle = new Turtle(GraphicForm.GetGraphics());
    }
}
