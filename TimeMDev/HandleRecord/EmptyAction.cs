using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 空运行
    /// </summary>
    public class EmptyAction:HandleRecordBass
    {
        public override void Execute()
        {
        }

        public override void UnExecute()
        {
        }
    }
}
