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
        private readonly int _increment = 5;
        private readonly Raytracer.Raytracer _raytracer;
        private readonly SynchronizationContext _synchronizationContext;
        private CancellationTokenSource _cancelSource;

        private int _lastX;
        private int _lastY;

        private bool _mousePressed;
        private bool _resized;
        private bool _viewChanged;
        private MouseButtons _mouseButtonPressed;

        public ChessForm()
        {
            InitializeComponent();
            _raytracer = new Raytracer.Raytracer();

            _synchronizationContext = SynchronizationContext.Current;
            lightX.Text = ((int) _raytracer.Light.Position.X).ToString();
            lightY.Text = ((int) _raytracer.Light.Position.Y).ToString();
            lightZ.Text = ((int) _raytracer.Light.Position.Z).ToString();

            cameraX.Text = ((int) _raytracer.Eye.Position.X).ToString();
            cameraY.Text = ((int) _raytracer.Eye.Position.Y).ToString();
            cameraZ.Text = ((int) _raytracer.Eye.Position.Z).ToString();

            _resized = true;
            _viewChanged = true;
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
            _viewChanged = true;
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

            // Repeat rendering until cancelled
            while (!_cancelSource.IsCancellationRequested)
            {
                var begin = DateTime.UtcNow;

                if (_resized)
                {
                    _raytracer.Resize(RenderView.Width, RenderView.Height);
                }
                _resized = false;

                if (_viewChanged)
                {
                    _raytracer.Light = new LightSource(int.Parse(lightX.Text), int.Parse(lightY.Text),
                        int.Parse(lightZ.Text));
                    _raytracer.Eye = new Camera(int.Parse(cameraX.Text), int.Parse(cameraY.Text),
                        int.Parse(cameraZ.Text));
                    _raytracer.BuildRayCache();
                }
                _viewChanged = false;

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
        
        private int _lastCamX;
        private int _lastCamY;
        private int _lastLightX;
        private int _lastLightY;

        private void RenderView_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePressed = true;
            _mouseButtonPressed = e.Button;
            _lastX = e.X;
            _lastY = e.Y;

            _lastCamX = int.Parse(cameraX.Text);
            _lastCamY = int.Parse(cameraY.Text);
            _lastLightX = int.Parse(lightX.Text);
            _lastLightY = int.Parse(lightY.Text);
        }

        private void RenderView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mousePressed) return;

            var dX = e.X - _lastX;
            var dY = e.Y - _lastY;        

            TextBox textBoxX;
            TextBox textBoxY;
            int lastCamOrLightX;
            int lastCamOrLightY;
        
            if (_mouseButtonPressed == System.Windows.Forms.MouseButtons.Left)
            {
                textBoxX = cameraX;
                textBoxY = cameraY;
                lastCamOrLightX = _lastCamX;
                lastCamOrLightY = _lastCamY;
            }
            else
            {
                textBoxX = lightX;
                textBoxY = lightY;
                lastCamOrLightX = _lastLightX;
                lastCamOrLightY = _lastLightY;
            }

            textBoxX.Text = (lastCamOrLightX + dX * _increment).ToString();
            textBoxY.Text = (lastCamOrLightY + dY * _increment).ToString();;

            _viewChanged = true;
        }

        private void RenderView_MouseUp(object sender, MouseEventArgs e)
        {
            _mousePressed = false;
            //_viewChanged = true;
        }

        private void RenderView_MouseWheel(object sender, MouseEventArgs e)
        {
            cameraZ.Text = (int.Parse(cameraZ.Text) + e.Delta/_increment).ToString();
            _viewChanged = true;
        }
    }
}