using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    class VertexBuffer<TEllement> : VertexBufferBase where TEllement : struct 
    {
        public VertexBuffer(BufferTarget target) : base(target)
        {
        }

        public override int ElementSize 
        {
            get { return Marshal.SizeOf(typeof (TEllement)); }
        }

        public void Data(TEllement[] data)
        {
            Bind();
            GL.BufferData(_target, (IntPtr)(data.Length * ElementSize), data, BufferUsageHint.StaticDraw);
            Unbind();
        }
    }
}
