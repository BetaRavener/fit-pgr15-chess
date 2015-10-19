using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    class ShaderObject : GLObject
    {
        private string _source;
        private ShaderType _type;
        public bool Compiled { get; private set; }

        protected ShaderObject()
        {
            _source = null;
            _glId = -1;
            Compiled = false;
        }

        public static ShaderObject Load(string path, ShaderType type)
        {
            var shaderObject = new ShaderObject();
            try
            {
                shaderObject._source = File.ReadAllText(path);
                shaderObject._type = type;
            }
            catch (IOException e)
            {
                return null;
            }

            return shaderObject;
        }

        public override void Init()
        {
            _glId = GL.CreateShader(_type);
            GL.ShaderSource(_glId, _source);
            _initialized = true;
        }

        public void Compile()
        {
            GL.CompileShader(_glId);
            Compiled = true;
        }
    }
}
