using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.HandleRecord
{
    /// <summary>
    /// 命令模式操作管理类
    /// </summary>
    public class CommandManage
    {
        //做了和没做的堆栈
        List<List<HandleRecordBass>> stackHaveDone;
        List<List<HandleRecordBass>> stackNeedDone;
        int stackSize;
        public CommandManage(int stackSize)
        {
            this.stackSize = stackSize;
            this.stackHaveDone = new List<List<HandleRecordBass>>();
            this.stackNeedDone = new List<List<HandleRecordBass>>();
        }
        public void CommandRun(params HandleRecordBass[] command)
        {
            List<HandleRecordBass> functions = new List<HandleRecordBass>();
            for (int i = 0; i < command.Length; i++)
            {
                functions.Add(command[i]);
                command[i].Execute();
            }
            this.stackHaveDone.Add(functions);
            if (stackHaveDone.Count > this.stackSize)
            {
                this.stackHaveDone.RemoveAt(0);
            }
        }
        public void CommandRunNoRedo(params HandleRecordBass[] command)
        {
            for (int i = 0; i < command.Length; i++)
            {
                command[i].Execute();
            }
        }
        public void Undo()
        {
            if(this.stackHaveDone.Count>0)
            {
                List<HandleRecordBass> functions = this.stackHaveDone[this.stackHaveDone.Count - 1];
                for (int i = 0; i < functions.Count; i++)
                {
                    functions[i].UnExecute();
                }
                this.stackNeedDone.Add(functions);
                if (stackNeedDone.Count > this.stackSize)
                {
                    this.stackNeedDone.RemoveAt(0);
                }
                this.stackHaveDone.RemoveAt(this.stackHaveDone.Count - 1);
            }
        }
        public void Redo()
        {
            if (this.stackHaveDone.Count > 0)
            {
                List<HandleRecordBass> functions = this.stackNeedDone[this.stackNeedDone.Count - 1];
                for (int i = 0; i < functions.Count; i++)
                {
                    functions[i].Execute();
                }
                this.stackHaveDone.Add(functions);
                if (stackHaveDone.Count > this.stackSize)
                {
                    this.stackHaveDone.RemoveAt(0);
                }
                this.stackNeedDone.RemoveAt(this.stackNeedDone.Count - 1);
            }
        }
    }
}
