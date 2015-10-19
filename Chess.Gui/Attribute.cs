using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    static class ActiveAttribTypeExtender
    {
        private static void GetInfo(ActiveAttribType type, out int columns, out int columnSize, out int elementSize)
        {
            switch (type)
            {
                case ActiveAttribType.Int:
                    columns = 1; columnSize = 1; elementSize = 4;
                    return;

                case ActiveAttribType.IntVec2:
                    columns = 1; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.IntVec3:
                    columns = 1; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.IntVec4:
                    columns = 1; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.UnsignedInt:
                    columns = 1; columnSize = 1; elementSize = 4;
                    return;

                case ActiveAttribType.UnsignedIntVec2:
                    columns = 1; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.UnsignedIntVec3:
                    columns = 1; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.UnsignedIntVec4:
                    columns = 1; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.Float:
                    columns = 1; columnSize = 1; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat2:
                    columns = 2; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat2x3:
                    columns = 2; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat2x4:
                    columns = 2; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat3:
                    columns = 3; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat3x2:
                    columns = 3; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat3x4:
                    columns = 3; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat4:
                    columns = 4; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat4x2:
                    columns = 4; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.FloatMat4x3:
                    columns = 4; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.FloatVec2:
                    columns = 1; columnSize = 2; elementSize = 4;
                    return;

                case ActiveAttribType.FloatVec3:
                    columns = 1; columnSize = 3; elementSize = 4;
                    return;

                case ActiveAttribType.FloatVec4:
                    columns = 1; columnSize = 4; elementSize = 4;
                    return;

                case ActiveAttribType.Double:
                    columns = 1; columnSize = 1; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat2:
                    columns = 2; columnSize = 2; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat2x3:
                    columns = 2; columnSize = 3; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat2x4:
                    columns = 2; columnSize = 4; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat3:
                    columns = 3; columnSize = 3; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat3x2:
                    columns = 3; columnSize = 2; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat3x4:
                    columns = 3; columnSize = 4; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat4:
                    columns = 4; columnSize = 4; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat4x2:
                    columns = 4; columnSize = 2; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleMat4x3:
                    columns = 4; columnSize = 3; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleVec2:
                    columns = 1; columnSize = 2; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleVec3:
                    columns = 1; columnSize = 3; elementSize = 8;
                    return;

                case ActiveAttribType.DoubleVec4:
                    columns = 1; columnSize = 4; elementSize = 8;
                    return;

                default:
                    columns = 0; columnSize = 0; elementSize = 0;
                    return;
            }
        }

        public static int ElementSize(this ActiveAttribType type)
        {
            int columns, columnSize, elementSize;
            GetInfo(type, out columns, out columnSize, out elementSize);
            return elementSize;
        }

        public static int ColumnSize(this ActiveAttribType type)
        {
            int columns, columnSize, elementSize;
            GetInfo(type, out columns, out columnSize, out elementSize);
            return columnSize;
        }

        public static int Columns(this ActiveAttribType type)
        {
            int columns, columnSize, elementSize;
            GetInfo(type, out columns, out columnSize, out elementSize);
            return columns;
        }
    }

    class Attribute
    {
        private ShaderProgram _program;
        private string _name;

        private VertexBufferBase _buffer;
        private int _offset;
        private int _stride;

        private int _glLocation;
        private ActiveAttribType _type;
        private VertexAttribPointerType _pointerType;

        Attribute(ShaderProgram program, string name)
        {
            _program = program;
            _name = name;
            _buffer = null;

            _glLocation = -1;
        }

        public void ConnectBuffer(VertexBufferBase buffer, int offset, int stride)
        {
            _buffer = buffer;
            _offset = offset;
            _stride = stride;
        }

        public VertexAttribPointerType PointerType
        {
            get { return _pointerType; }
            set { _pointerType = value; }
        }

        public void Prepare()
        {
            _glLocation = GL.GetAttribLocation(_program.GlId, _name);

            int activeCount = 0;

            GL.GetProgram(_program.GlId, GetProgramParameterName.ActiveAttributes, out activeCount);
            for (int i = 0; i < activeCount; i++)
            {
                var size = 0;
                ActiveAttribType type;
                
                var name = GL.GetActiveAttrib(_program.GlId, i, out size, out type);
                if (!name.Equals(_name)) continue;

                _type = type;
            }
        }

        public void Activate()
        {
            Prepare();

            for (var i = 0; i < _type.Columns(); i++)
            {
                GL.EnableVertexAttribArray(_glLocation+i);
                var offset = i * _type.ColumnSize() * _type.ElementSize() + _offset;

                _buffer.Bind();
                //TODO: Finish if required
                throw new NotImplementedException();
                _buffer.Unbind();
            }
        }
    }
}
