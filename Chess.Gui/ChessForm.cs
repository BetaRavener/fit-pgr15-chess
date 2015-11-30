using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using RayTracer;

namespace Chess.Gui
{
    public partial class ChessForm : Form, IProgress<Tuple<int, int>>
    {
        private readonly Raytracer.Raytracer _raytracer;
        private readonly SynchronizationContext _synchronizationContext;
        private CancellationTokenSource _cancelSource;

        private const double ZoomSensitivity = 0.05;
        private const double RotationSensitivity = 0.02;
        private const double MoveSensitivity = 5;
        private const double LightSensitivity = 5;
        private int _lastX;
        private int _lastY;

        OpenTK.Vector3d _lightPos;
        private bool _resized;
        private bool _viewChanged;
        private bool _rotating;
        private bool _lightChanged;

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

            _lightPos = _raytracer.Light.Position;

            _resized = true;
            _viewChanged = true;
            _lightChanged = true;
            _rotating = false;
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
            _raytracer.ReflectionDepth = (int) reflectionDepth.Value;

            // Repeat rendering until cancelled
            while (!_cancelSource.IsCancellationRequested)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    await Task.Delay(100);
                    continue;
                }

                var begin = DateTime.UtcNow;

                if (_resized)
                {
                    _raytracer.Resize(RenderView.Width, RenderView.Height);
                }
                _resized = false;

                if (_lightChanged)
                {
                    _raytracer.Light.Position = _lightPos;
                    lightX.Text = ((int) _lightPos.X).ToString();
                    lightY.Text = ((int) _lightPos.Y).ToString();
                    lightZ.Text = ((int) _lightPos.Z).ToString();
                }
                if (_viewChanged)
                {
                    var camPos = _raytracer.Eye.Position;
                    cameraX.Text = ((int) camPos.X).ToString();
                    cameraY.Text = ((int) camPos.Y).ToString();
                    cameraZ.Text = ((int) camPos.Z).ToString();

                    _raytracer.BuildRayCache();
                }
                _lightChanged = false;
                _viewChanged = false;

                _raytracer.OnlyBoundingBoxes = _rotating;

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

        private void RenderView_MouseDown(object sender, MouseEventArgs e)
        {
            _lastX = e.X;
            _lastY = e.Y;
        }

        private void RenderView_MouseMove(object sender, MouseEventArgs e)
        {
            var dX = e.X - _lastX;
            var dY = e.Y - _lastY;

            if ((ModifierKeys == Keys.Control && e.Button == MouseButtons.Left) || e.Button == MouseButtons.Right)
            {
                _lightPos += new OpenTK.Vector3d(LightSensitivity * dX, LightSensitivity * dY, 0);
                _lightChanged = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Invert Y axis
                _raytracer.Eye.RotateRelative(new OpenTK.Vector2d(RotationSensitivity * dY, -RotationSensitivity * dX));
                _viewChanged = true;
                _rotating = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                _raytracer.Eye.MoveRelative(new OpenTK.Vector3d(MoveSensitivity * dX, -MoveSensitivity * dY, 0));
                _viewChanged = true;
                _rotating = true;
            }

            _lastX = e.X;
            _lastY = e.Y;
        }

        private void RenderView_MouseWheel(object sender, MouseEventArgs e)
        {
            _raytracer.Eye.OrbitDistance += e.Delta * ZoomSensitivity;
            _viewChanged = true;
        }

        private void RenderView_MouseUp(object sender, MouseEventArgs e)
        {
            _rotating = false;
        }

        private void lightX_TextChanged(object sender, EventArgs e)
        {
            if (!lightX.Focused)
                return;

            _lightPos.X = double.Parse(lightX.Text);
            _lightChanged = true;
        }

        private void lightY_TextChanged(object sender, EventArgs e)
        {
            if (!lightY.Focused)
                return;

            _lightPos.Y = double.Parse(lightY.Text);
            _lightChanged = true;
        }

        private void lightZ_TextChanged(object sender, EventArgs e)
        {
            if (!lightZ.Focused)
                return;

            _lightPos.Z = double.Parse(lightZ.Text);
            _lightChanged = true;
        }

        private void cameraX_TextChanged(object sender, EventArgs e)
        {
            if (!cameraX.Focused)
                return;

            var pos = _raytracer.Eye.Position;
            _raytracer.Eye.Position = new Vector3d(double.Parse(cameraX.Text), pos.Y, pos.Z);
            _viewChanged = true;
        }

        private void cameraY_TextChanged(object sender, EventArgs e)
        {
            if (!cameraY.Focused)
                return;

            var pos = _raytracer.Eye.Position;
            _raytracer.Eye.Position = new Vector3d(pos.X, double.Parse(cameraY.Text), pos.Z);
            _viewChanged = true;
        }

        private void cameraZ_TextChanged(object sender, EventArgs e)
        {
            if (!cameraZ.Focused)
                return;

            var pos = _raytracer.Eye.Position;
            _raytracer.Eye.Position = new Vector3d(pos.X, pos.Y, double.Parse(cameraZ.Text));
            _viewChanged = true;
        }

        private void reflectionDepth_ValueChanged(object sender, EventArgs e)
        {
            if (!reflectionDepth.Focused)
                return;

            _raytracer.ReflectionDepth = (int) reflectionDepth.Value;
            _viewChanged = true;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"Chess state JSON file|*.JSON";
            openFileDialog.InitialDirectory = @".";

            string filename = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
            }

            if (filename != "")
            {
                
            }

            //Stream fileStream = null;
            //if (openFileDialog.ShowDialog() == DialogResult.OK) && (fileStream = selectFileDialog.OpenFile()) != null){
            //    string fileName = selectFileDialog.FileName;
            //    using (fileStream)
            //    {
            //        // TODO
            //    }
            //}

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (RenderView.Image == null)
            {
                MessageBox.Show(@"You need to render scene first!");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = @"Bitmap Image|*.bmp|Jpeg Image|*.jpg|Gif Image|*.gif",
                Title = @"Export to image file"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            ImageFormat imageType;
            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    imageType = ImageFormat.Bmp;
                    break;

                case 2:
                    imageType = ImageFormat.Jpeg;
                    break;

                case 3:
                    imageType = ImageFormat.Gif;
                    break;

                default:
                    MessageBox.Show(@"Allowed types ar only JPG, BMP, GIF");
                    return;
            }

            RenderView.Image.Save(saveFileDialog.FileName, imageType);
        }

        private void antialiasingFactor_ValueChanged(object sender, EventArgs e)
        {
            if (!antialiasingFactor.Focused)
                return;

            _raytracer.AntialiasFactor = (int) antialiasingFactor.Value;
            _viewChanged = true;

            // TODO when resize && rendering is on, crashes because ray cache not found
        }
    }
}