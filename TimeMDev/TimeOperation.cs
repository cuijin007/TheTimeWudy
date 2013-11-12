using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev
{
    /// <summary>
    /// 命令模式，用命令模式进行undo，redo的重构
    /// </summary>
    public class TimeOperation
    {
        OpreationInterface op = new AddRecord();
        
    }
    /// <summary>
    /// 执行，和撤销
    /// </summary>
    public interface OpreationInterface
    {
         void Execute();
         void NoExecute();
    }
    /// <summary>
    /// 增加一条记录
    /// </summary>
    public class AddRecord:OpreationInterface
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void NoExecute()
        {
            throw new NotImplementedException();
        }
    }
}
