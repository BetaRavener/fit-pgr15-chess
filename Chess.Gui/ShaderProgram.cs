using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    class ShaderProgram : GLObject
    {
        private List<ShaderObject> _shaders;
        public bool Linked { get; private set; }

        public ShaderProgram()
        {
            _shaders = new List<ShaderObject>();
            Linked = false;
        }

        public void AddShader(ShaderObject shader)
        {
            _shaders.Add(shader);
        }

        public override void Init()
        {
            _glId = GL.CreateProgram();
            _initialized = true;
        }

        public void Link()
        {
            if (_shaders.Count == 0)
                throw new InvalidOperationException("Cannot link empty program");

            foreach (var shader in _shaders)
            {
                if (!shader.Compiled)
                    shader.Compile();

                GL.AttachShader(_glId, shader.GlId);
            }

            GL.LinkProgram(_glId);
            Linked = true;
        }

        public void Activate()
        {
            GL.UseProgram(_glId);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }
    }
}
