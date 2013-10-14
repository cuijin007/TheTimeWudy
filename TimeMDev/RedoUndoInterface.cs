using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev
{
    public interface RedoUndoInterface
    {
        bool Undo();
        void Save();
        bool Redo();
        void Clear();
    }
}
