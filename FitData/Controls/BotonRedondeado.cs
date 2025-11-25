using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FitData.Controls
{
    public class BotonRedondeado : Button
    {
        // Valores por defecto para TODA la app
        public int BorderRadius { get; set; } = 18;
        public Color BorderColor { get; set; } = Color.White;   // BORDE BLANCO DEFAULT
        public int BorderSize { get; set; } = 3;             

        public BotonRedondeado()
        {
            // Elimina bordes del sistema
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            // Color del texto por defecto
            ForeColor = Color.White;

            // Fondo gris moderno si no se especifica otro
            BackColor = Color.FromArgb(60, 60, 60);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width, Height);
            GraphicsPath path = GetRoundPath(rect, BorderRadius);

            this.Region = new Region(path);

            using (Pen pen = new Pen(BorderColor, BorderSize))
            {
                pen.Alignment = PenAlignment.Inset;
                pevent.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            float r = radius;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Width - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Width - r, rect.Height - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Height - r, r, r, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
