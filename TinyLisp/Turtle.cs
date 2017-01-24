using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

class Turtle
{
    private Graphics myGraphics;
    private Pen myPen;
    private const int WIDTH = 640, HEIGHT = 480;

    private double x, y;
    private double pX, pY;
    private double rot;

    private bool painting;

    public double X 
    {
        get { return this.x; }
    }

    public double Y
    {
        get { return this.y; }
    }

    public double Rotation
    {
        get { return this.rot; }
    }

    public bool Painting
    {
        get { return this.painting; }
        set 
        {
            this.pX = this.x;
            this.pY = this.y;
            this.painting = value;
        }
    }

    private double degToRad(double deg)
    {
        return deg / 180 * Math.PI;
    }

    public void Rotate(double byAngle)
    {
        this.rot += degToRad(byAngle);
    }

    public void SetRotation(double Angle)
    {
        this.rot = degToRad(Angle) - Math.PI / 2;
    }

    public void SetColor(int Color)
    {
        myPen.Color = System.Drawing.Color.FromArgb((int)((uint)Color | 0xFF000000));
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

    public void Erase()
    {
        myGraphics.FillRectangle(Brushes.White, 0, 0, WIDTH, HEIGHT);
    }

    private void ControlMoving()
    {
        bool outOfBounds = x < 0 || y < 0 || x > WIDTH || y > HEIGHT;
        if (x > WIDTH)
            x = 0;
        if (y > HEIGHT)
            y = 0;
        if (x < 0)
            x = WIDTH;
        if (y < 0)
            y = HEIGHT;
        if(outOfBounds)
            StorePreviousPosition();
        if (this.painting)
        {
            DrawTrack();
        }
    }

    public void MoveTo(double X, double Y)
    {
        this.x = X + WIDTH / 2;
        this.y = HEIGHT - (Y + HEIGHT / 2);
        ControlMoving();
    }

    public void MoveRelative(double distance)
    {
        int dst = (int)Math.Abs(distance);
        int direction = Math.Sign(distance);
        for (int i = 0; i < dst; i++)
        {
            this.x += Math.Cos(this.rot) * direction;
            this.y += Math.Sin(this.rot) * direction;
            // if(i % 2 == 0)
            //     System.Threading.Thread.Sleep(1);
            ControlMoving();
        }
    }

    public Turtle(Graphics g)
    {
        this.painting = false;
        this.myGraphics = g;
        this.myPen = new Pen(Color.Black, 1);
        rot = -Math.PI / 2;
        MoveTo(0, 0);
    }
}