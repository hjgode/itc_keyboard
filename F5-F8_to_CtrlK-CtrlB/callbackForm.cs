using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogForm
{
    public interface ICallbackForm
    {
    }
    public abstract class CallBackForm:Form,ICallbackForm
    {
        public virtual void addLog(string text)
        {
            throw new NotImplementedException();
        }
    }
}
