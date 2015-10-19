using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    abstract class VertexBufferBase : GLObject
    {
        protected BufferTarget _target;

        public VertexBufferBase(BufferTarget target)
        {
            _target = target;
        }

        public override void Init()
        {
            _glId = GL.GenBuffer();
            // Bind for the first time to associate with the target
            Bind();
            Unbind();
            _initialized = true;
        }

        public void Bind()
        {
            GL.BindBuffer(_target, _glId);
        }

        public void Unbind()
        {
            GL.BindBuffer(_target, 0);
        }

        public abstract int ElementSize { get; }
    }
}
