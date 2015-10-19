using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess.Gui
{
    public partial class ChessForm : Form
    {
        private Raytracer.Raytracer _raytracer;
        private bool _needRedraw;

        public ChessForm()
        {
            InitializeComponent();
            _raytracer = new Raytracer.Raytracer();
            _raytracer.Resize(RenderView.Width, RenderView.Height);
            Redraw();
        }

        private void RenderView_Paint(object sender, PaintEventArgs e)
        {
            if (!_needRedraw)
                return;

            var oldImg = RenderView.Image;
            RenderView.Image = _raytracer.RenderImage();
            oldImg?.Dispose();
            _needRedraw = false;
        }

        private void RenderView_Resize(object sender, EventArgs e)
        {
            var control = (Control)sender;
            _raytracer.Resize(control.Size.Width, control.Size.Height);
            Redraw();
        }

        public void Redraw()
        {
            _needRedraw = true;
        }
    }
}
