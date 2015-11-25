using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RayTracer;

namespace Chess.Gui
{
    public partial class ChessForm : Form, IProgress<Tuple<int, int>>
    {
        private readonly int _increment = 10;
        private readonly Raytracer.Raytracer _raytracer;
        private readonly SynchronizationContext _synchronizationContext;
        private CancellationTokenSource _cancelSource;

        private int _lastX;
        private int _lastY;


        private bool _mousePressed;
        private bool _resized;

        public ChessForm()
        {
            InitializeComponent();
            _raytracer = new Raytracer.Raytracer();
            _resized = true;
            _synchronizationContext = SynchronizationContext.Current;
            lightX.Text = ((int) _raytracer.Light.Position.X).ToString();
            lightY.Text = ((int) _raytracer.Light.Position.Y).ToString();
            lightZ.Text = ((int) _raytracer.Light.Position.Z).ToString();

            cameraX.Text = ((int) _raytracer.Eye.Position.X).ToString();
            cameraY.Text = ((int) _raytracer.Eye.Position.Y).ToString();
            cameraZ.Text = ((int) _raytracer.Eye.Position.Z).ToString();
        }

        /// <summary>
        ///     Method used to report progress of rendering.
        /// </summary>
        /// <param name="value"></param>
        public void Report(Tuple<int, int> value)
        {
            _synchronizationContext.Post(@object =>
            {
                var valMax = (Tuple<int, int>) @object;
                RenderProgressBar.Value = valMax.Item1;
                RenderProgressBar.Maximum = valMax.Item2;
            }, value);
        }

        private void RenderView_Resize(object sender, EventArgs e)
        {
            _resized = true;
        }

        /// <summary>
        ///     Method that does call actual rendering. This should run in seprate thread so it
        ///     doesn't block GUI.
        /// </summary>
        /// <returns>Image of rendered scene.</returns>
        private Image Redraw()
        {
            var img = _raytracer.RenderImage(_cancelSource.Token, Report);
            return !_cancelSource.Token.IsCancellationRequested ? img : null;
        }

        private void RenderButton_Click(object sender, EventArgs e)
        {
            if (_cancelSource == null)
            {
                Render();
            }
            else
            {
                _cancelSource.Cancel();
            }
        }

        private async void Render()
        {
            RenderButton.Text = "Cancel";
            _cancelSource = new CancellationTokenSource();

            _raytracer.NumberOfThreads = (int) ThreadsNumber.Value;
            _raytracer.Resize(RenderView.Width, RenderView.Height);

            _raytracer.Light = new LightSource(int.Parse(lightX.Text), int.Parse(lightY.Text), int.Parse(lightZ.Text));
            _raytracer.Eye = new Camera(int.Parse(cameraX.Text), int.Parse(cameraY.Text), int.Parse(cameraZ.Text));
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
                var begin = DateTime.UtcNow;

                Image img = null;
                await Task.Run(() => img = Redraw(), _cancelSource.Token);

                if (img != null)
                {
                    var oldImg = RenderView.Image;
                    RenderView.Image = img;
                    oldImg?.Dispose();
                    Refresh();
                }
                var end = DateTime.UtcNow;
                // Calculate time it took to render scene
                var elapsed = (end - begin).TotalMilliseconds;

                FPSlabel.Text = (1000.0/elapsed).ToString("#.##");

                // If scene drawn under 30 miliseconds, wait with next rendering
                if (elapsed < 30)
                    await Task.Delay((int) (30 - elapsed));
            }

            _cancelSource = null;
            RenderProgressBar.Value = RenderProgressBar.Minimum;
            RenderButton.Text = "Render";
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

        private void RenderView_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePressed = true;
            _lastX = e.X;
            _lastY = e.Y;
        }

        private void RenderView_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousePressed)
            {
                var dX = e.X - _lastX;
                var dY = e.Y - _lastY;

                if (Math.Abs(dX) > Math.Abs(dY))
                    cameraX.Text = (int.Parse(cameraX.Text) + (dX < 0 ? -1 : 1)*_increment).ToString();
                else
                    cameraY.Text = (int.Parse(cameraY.Text) + (dY < 0 ? -1 : 1)*_increment).ToString();
            }
        }

        private void RenderView_MouseUp(object sender, MouseEventArgs e)
        {
            _mousePressed = false;
            // TODO solve so that it will render and finishes
            if (_cancelSource == null)
            {
                Render();
            }
            else
            {
                _cancelSource.Cancel();
            }
        }

        private void RenderView_MouseWheel(object sender, MouseEventArgs e)
        {
            cameraZ.Text = (int.Parse(cameraZ.Text) + e.Delta/_increment).ToString();
            // TODO solve so that it will render and finishes
            if (_cancelSource == null)
            {
                Render();
            }
            else
            {
                _cancelSource.Cancel();
            }
        }
    }
}