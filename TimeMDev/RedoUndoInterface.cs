using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev
{
    public interface RedoUndoInterface
    {
        bool _Undo();
        void _Save();
        bool _Redo();
        void Clear();
    }
}
