using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using RayTracer;

namespace Chess.Gui
{
    public partial class ChessForm : Form, IProgress<Tuple<int, int>>
    {
        private Raytracer.Raytracer _raytracer;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _synchronizationContext;
        private bool _resized;

        public ChessForm()
        {
            InitializeComponent();
            _raytracer = new Raytracer.Raytracer();
            _resized = true;
            _synchronizationContext = SynchronizationContext.Current;
        }

        private void RenderView_Resize(object sender, EventArgs e)
        {
            _resized = true;
        }

        /// <summary>
        /// Method that does call actual rendering. This should run in seprate thread so it
        /// doesn't block GUI.
        /// </summary>
        /// <returns>Image of rendered scene.</returns>
        private Image Redraw()
        {
            var img = _raytracer.RenderImage(_cancelSource.Token, Report);
            return !_cancelSource.Token.IsCancellationRequested ? img : null;
        }

        private async void RenderButton_Click(object sender, EventArgs e)
        {
            if (_cancelSource == null)
            {
                RenderButton.Text = "Cancel";
                _cancelSource = new CancellationTokenSource();

                _raytracer.NumberOfThreads = (int)ThreadsNumber.Value;
                _raytracer.Resize(RenderView.Width, RenderView.Height);

                _raytracer.Light = new LightSource(Int32.Parse(lightX.Text), Int32.Parse(lightY.Text), Int32.Parse(lightZ.Text));
                _raytracer.Eye = new Camera(Int32.Parse(cameraX.Text), Int32.Parse(cameraY.Text), Int32.Parse(cameraZ.Text));
                _resized = false;
                _raytracer.BuildRayCache();

                //Image img = null;
                //await Task.Run(() => img = Redraw(), _cancelSource.Token);

                //if (img != null)
                //{
                //    var oldImg = RenderView.Image;
                //    RenderView.Image = img;
                //    oldImg?.Dispose();
                //    Refresh();
                //}

                // Repeat rendering until cancelled
                while (!_cancelSource.IsCancellationRequested)
                {
                    DateTime begin = DateTime.UtcNow;

                    Image img = null;
                    await Task.Run(() => img = Redraw(), _cancelSource.Token);

                    if (img != null)
                    {
                        var oldImg = RenderView.Image;
                        RenderView.Image = img;
                        oldImg?.Dispose();
                        Refresh();
                    }
                    DateTime end = DateTime.UtcNow;
                    // Calculate time it took to render scene
                    var elapsed = (end - begin).TotalMilliseconds;

                    FPSlabel.Text = (1000.0 / elapsed).ToString("#.##");

                    // If scene drawn under 30 miliseconds, wait with next rendering
                    if (elapsed < 30)
                        await Task.Delay((int)(30 - elapsed));                    
                }

                _cancelSource = null;
                RenderProgressBar.Value = RenderProgressBar.Minimum;
                RenderButton.Text = "Render";
            }
            else
            {
                _cancelSource.Cancel();
            }
        }

        /// <summary>
        /// Method used to report progress of rendering.
        /// </summary>
        /// <param name="value"></param>
        public void Report(Tuple<int, int> value)
        {
            _synchronizationContext.Post((@object) =>
            {
                var valMax = (Tuple<int, int>)@object;
                RenderProgressBar.Value = valMax.Item1;
                RenderProgressBar.Maximum = valMax.Item2;
            }, value);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
