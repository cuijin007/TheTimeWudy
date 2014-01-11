using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace TimeMDev.Notification
{
   public class TextBoxNotify:HandleMessageBase
    {
       TextBox textBox;
       Hashtable messageHandleFuntion = new Hashtable();
       delegate bool Function(params object[] parameter); 
       public TextBoxNotify(TextBox textBox)
        {
            this.textBox = textBox;
            this.messageHandleFuntion.Add("SetText", new Function(this.SetText));
            this.messageHandleFuntion.Add("Refresh", new Function(this.Refresh));
            this.messageHandleFuntion.Add("SetFocus", new Function(this.SetFocus));
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
        public bool SetText(params object[] parameter)
        {
            if (parameter.Length > 0)
            {
                this.textBox.Text = (string)parameter[0];
                return true;
            }
            return false;
        }
        public bool Refresh(params object[] parameter)
        {
            if (parameter.Length > 0)
            {
                this.textBox.Refresh();
                return true;
            }
            return false;
        }
        public bool SetFocus(params object[] parameter)
        {
            this.textBox.Focus();
            return true;
        }
   }
}
