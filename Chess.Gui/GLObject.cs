using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Gui
{
    internal abstract class GLObject
    {
        protected int _glId;
        protected bool _initialized;

        protected GLObject()
        {
            _glId = -1;
            _initialized = false;
        }

        public int GlId
        {
            get { return _glId; }
        }

        public abstract void Init();

        public bool IsInit()
        {
            return _initialized;
        }
    }
}
