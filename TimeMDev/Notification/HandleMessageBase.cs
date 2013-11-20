using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeMDev.Notification
{
    /// <summary>
    /// 消息处理基类，所有的都需要实现这个HandleMessage的函数
    /// </summary>
    public abstract class HandleMessageBase
    {
        public abstract bool HandleMessage(string message, params object[] parameter);
    }
}
