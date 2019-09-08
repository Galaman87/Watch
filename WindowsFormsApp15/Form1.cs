using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public double R(string time)
        {
            double k = 0;
            if (time == "sec")
                k = 0.85;
            else
            {
                if (time == "min") k = 0.8;
                else k = 0.65;
            }
            return ((Width > Height ? Height / 2 : Width / 2) * k)-10;
        }

        Color ColorSecArrow { get; set; } = Color.Yellow;
        Color ColorMinArrow { get; set; } = Color.Blue;
        Color ColorHourArrow { get; set; } = Color.Red;
        Color ColorBackArrow { get; set; } = Color.Black;
        Color ColorMark { get; set; } = Color.LightGreen;




        public double A(string time)
        {
            if (time == "sec")
                return (Math.PI / 30) * DateTime.Now.Second;
            if (time == "min")
                return (Math.PI / 30) * DateTime.Now.Minute;
            else
                return (Math.PI / 6) * DateTime.Now.Hour + (Math.PI / 360) * DateTime.Now.Minute;


        }

        public double GetX(string time)
        {
            return CentreX() + Math.Sin(A(time)) * R(time);
        }

        public double GetY(string time)
        {
            return CentreY() + Math.Cos(A(time)) * R(time);
        }

        public double CentreY()
        {
            return this.Height / 2 - 20;
        }
        public double CentreX()
        {
            return this.Width / 2;
        }
        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            
            Graphics graphics = e.Graphics;
            Matrix matrix = new Matrix(1, 0, 0, -1, 0, 0);
            matrix.Translate(0, -this.ClientRectangle.Height);
            graphics.Transform = matrix;


            Pen secPen = new Pen(ColorSecArrow, 3);
            secPen.EndCap = LineCap.ArrowAnchor;
            secPen.StartCap = LineCap.Round;

            Pen minPen = new Pen(ColorMinArrow, 4);
            minPen.EndCap = LineCap.ArrowAnchor;
            minPen.StartCap = LineCap.Round;

            Pen hourPen = new Pen(ColorHourArrow, 5);
            hourPen.EndCap = LineCap.ArrowAnchor;
            hourPen.StartCap = LineCap.Round;
            

            SolidBrush brush = new SolidBrush(ColorBackArrow);
            graphics.FillRectangle(brush, new Rectangle(0, 0, Width, Height));
            brush = new SolidBrush(ColorMark);

            double a = 0;
            for (int i = 0; i < 12; i++)
            {
                graphics.FillEllipse(brush,(float)( CentreX() + Math.Sin(a) * (R("sec") + 10)), (float)(CentreY() + Math.Cos(a) * (R("sec") + 10)),5,5);
                a += Math.PI / 6;
            }

            graphics.DrawLine(hourPen, (int)CentreX(), (int)CentreY(), (int)GetX("hour"), (int)GetY("hour"));
            graphics.DrawLine(minPen, (int)CentreX(), (int)CentreY(), (int)GetX("min"), (int)GetY("min"));
            graphics.DrawLine(secPen, (int)CentreX(), (int)CentreY(), (int)GetX("sec"), (int)GetY("sec"));


            Invalidate();
            System.Threading.Thread.Sleep(1000);

        }

    }
}
