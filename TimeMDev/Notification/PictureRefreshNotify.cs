using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TimeMDev.Notification
{
    public class PictureRefreshNotify:HandleMessageBase
    {
        PictureRefresh pictureRefresh;
        Hashtable messageHandleFuntion = new Hashtable();
        delegate bool Function(params object[] parameter); 
        public PictureRefreshNotify(PictureRefresh pictureRefresh)
        {
            this.pictureRefresh = pictureRefresh;
            this.messageHandleFuntion.Add("SetNowTime", new Function(this.SetNowTime));
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

        public bool SetNowTime(params object[] parameter)
        {
            if(parameter.Length>0)
            {
                this.pictureRefresh.NowTime = (double)parameter[0];
                return true;
            }
            return false;
        }
    }
}
