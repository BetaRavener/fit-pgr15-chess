using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Chess.Gui
{
    class ShaderInput : GLObject
    {
        private List<Attribute> _attributes;

        ShaderInput()
        {
            _attributes = new List<Attribute>();
        }

        public override void Init()
        {
            _glId = GL.GenVertexArray();
            _initialized = true;
        }

        public void Add(Attribute attribute)
        {
            _attributes.Add(attribute);
        }

        public void Prepare()
        {
            foreach (var attribute in _attributes)
            {
                attribute.Prepare();
            }
        }

        public void Activate()
        {
            
        }
    }
}
