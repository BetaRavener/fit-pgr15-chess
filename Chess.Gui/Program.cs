using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileFormatWavefront;
using FileFormatWavefront.Model;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Chess.Gui
{
    class Program
    {
        static float[] Unpack(IReadOnlyCollection<Vertex> vertices)
        {
            float[] ret = new float[vertices.Count()*3];
            for (int i = 0; i < vertices.Count(); i++)
            {
                ret[i*3] = vertices.ElementAt(i).x;
                ret[i*3+1] = vertices.ElementAt(i).y;
                ret[i*3+2] = vertices.ElementAt(i).z;
            }
            return ret;
        }

        static void Main(string[] args)
        {
            Scene scene = null;
            ShaderObject vertexShaderObject = null;
            ShaderObject fragmentShaderObject = null;
            ShaderProgram renderProgram = null;
            VertexBuffer<float> vertices = new VertexBuffer<float>(BufferTarget.ArrayBuffer);
            VertexBuffer<float> normals = new VertexBuffer<float>(BufferTarget.ArrayBuffer);

            using (var game = new GameWindow())
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                    var result = FileFormatObj.Load("model/teapot.obj", true);
                    scene = result.Model;

                    vertices.Init();
                    normals.Init();

                    vertices.Data(Unpack(scene.Vertices));
                    normals.Data(Unpack(scene.Normals));

                    vertexShaderObject = ShaderObject.Load("shaders/render.vert", ShaderType.VertexShader);
                    vertexShaderObject.Init();
                    fragmentShaderObject = ShaderObject.Load("shaders/render.frag", ShaderType.FragmentShader);
                    fragmentShaderObject.Init();
                    renderProgram = new ShaderProgram();
                    renderProgram.AddShader(vertexShaderObject);
                    renderProgram.AddShader(fragmentShaderObject);
                    renderProgram.Link();
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    renderProgram.Activate();

                    renderProgram.Deactivate();
                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
