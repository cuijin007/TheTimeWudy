using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TimeMDev.Notification
{
    public class NotificationCenter
    {
        public static Hashtable notificationHashtable = new Hashtable();
        /// <summary>
        /// 增加一个通知中心的cell
        /// </summary>
        /// <param name="cellName">控件名</param>
        /// <param name="cell">控件消息处理单元</param>
        /// <returns></returns>
        public static bool AddNotificationCell(string cellName,HandleMessageBase cell)
        {
            if (notificationHashtable[cellName] == null)
            {
                notificationHashtable.Add(cellName, cell);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cellName">控件名</param>
        /// <param name="message">消息名</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static bool SendMessage(string cellName, string message, params object[] parameter)
        {
            HandleMessageBase cell = (HandleMessageBase)notificationHashtable[cellName];
            if (cell == null)
            {
                return false;
            }
            else
            {
                return cell.HandleMessage(message, parameter);
            }
        }
    }
}
