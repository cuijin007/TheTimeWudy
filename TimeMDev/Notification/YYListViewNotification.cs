using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TimeMDev.Notification
{
    public class YYListViewNotification:HandleMessageBase
    {
        YYListView yyListView;
        Hashtable messageHandleFuntion = new Hashtable();
        delegate bool Function(params object[] parameter); 
        public YYListViewNotification(YYListView yyListView)
        {
            this.yyListView = yyListView;
            this.messageHandleFuntion.Add("EnsureVisibleByIndex", new Function(this.EnsureVisibleByIndex));
            this.messageHandleFuntion.Add("EnsureVisibleByShowPosition", new Function(this.EnsureVisibleByShowPosition));
            this.messageHandleFuntion.Add("Refresh", new Function(this.Refresh));
        }
        public override bool HandleMessage(string message, params object[] parameter)
        {
            Function func = (Function)this.messageHandleFuntion[message];
            if (func != null)
            {
                return func(parameter);
            }
            return false;
        }
        protected bool EnsureVisibleByIndex(params object[] parameter)
        {
            if (parameter.Length > 0)
            {
                int index = (int)parameter[0];
                int showPosition = this.yyListView.YYGetShowPosition(index);
                this.yyListView.YYEnsurVisible(showPosition);
                return true;
            }
            return false;
        }
        protected bool EnsureVisibleByShowPosition(params object[] parameter)
        {
            if (parameter.Length > 0)
            {
                int showPosition = (int)parameter[0];
                this.yyListView.YYEnsurVisible(showPosition);
                return true;
            }
            return false;
        }
        protected bool Refresh(params object[] parameter)
        {
            this.yyListView.YYRefresh();
            return true;
        }
    }
}
