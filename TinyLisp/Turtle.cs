using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// "Черепашка", рисующая графику
/// </summary>
public class Turtle
{
    private Graphics myGraphics;
    private Pen myPen;

    // Размеры области рисования
    private const int Width = 640;
    private const int Height= 480;

    private double x, y;
    private double pX, pY;
    private double rot;

    private bool isPainting;

    /// <summary>
    /// Горизонтальная координата
    /// </summary>
    public double X 
    {
        get { return this.x; }
    }

    /// <summary>
    /// Вертикальная координата
    /// </summary>
    public double Y
    {
        get { return this.y; }
    }

    /// <summary>
    /// Угол поворота
    /// </summary>
    public double Rotation
    {
        get { return this.rot; }
    }

    /// <summary>
    /// Выполняетсяли рисование
    /// </summary>
    public bool Painting
    {
        get { return this.isPainting; }
        set 
        {
            this.pX = this.x;
            this.pY = this.y;
            this.isPainting = value;
        }
    }

    /// <summary>
    /// Повернуть "черепашку"
    /// </summary>
    /// <param name="byAngle">Угол поворота</param>
    public void Rotate(double byAngle)
    {
        this.rot += degToRad(byAngle);
    }

    /// <summary>
    /// Установить угол поворота
    /// </summary>
    /// <param name="angle">Угол поворота</param>
    public void SetRotation(double angle)
    {
        this.rot = degToRad(angle) - Math.PI / 2;
    }

    /// <summary>
    /// Установить цвет рисования
    /// </summary>
    /// <param name="color">Значение цвета</param>
    public void SetColor(int color)
    {
        myPen.Color = Color.FromArgb((int)((uint)color | 0xFF000000));
    }

    /// <summary>
    /// Очистить область вывода графики
    /// </summary>
    public void Erase()
    {
        myGraphics.FillRectangle(Brushes.White, 0, 0, Width, Height);
    }

    /// <summary>
    /// Передвинуть "черепашку"
    /// </summary>
    /// <param name="x">Горизонтальная координата</param>
    /// <param name="y">Вертикальная координата</param>
    public void MoveTo(double x, double y)
    {
        this.x = x + Width / 2;
        this.y = Height - (y + Height / 2);
        ControlMoving();
    }

    /// <summary>
    /// Передвинуть черепашку вперед
    /// </summary>
    /// <param name="distance">Расстояние перемещение</param>
    public void MoveRelative(double distance)
    {
        int dst = (int)Math.Abs(distance);
        int direction = Math.Sign(distance);
        for (int i = 0; i < dst; i++)
        {
            this.x += Math.Cos(this.rot) * direction;
            this.y += Math.Sin(this.rot) * direction;
            ControlMoving();
        }
    }

    private double degToRad(double deg)
    {
        return deg / 180 * Math.PI;
    }

    private void StorePreviousPosition()
    {
        this.pX = this.x;
        this.pY = this.y;
    }

    private void DrawTrack()
    {
        myGraphics.DrawLine(myPen, (int)pX, (int)pY, (int)x, (int)y);
        StorePreviousPosition();
    }

    private void ControlMoving()
    {
        bool outOfBounds = x < 0 || y < 0 || x > Width || y > Height;
        if (x > Width)
            x = 0;
        if (y > Height)
            y = 0;
        if (x < 0)
            x = Width;
        if (y < 0)
            y = Height;
        if(outOfBounds)
            StorePreviousPosition();
        if (this.isPainting)
        {
            DrawTrack();
        }
    }

    public Turtle(Graphics g)
    {
        this.isPainting = false;
        this.myGraphics = g;
        this.myPen = new Pen(Color.Black, 1);
        rot = -Math.PI / 2;
        MoveTo(0, 0);
    }
}