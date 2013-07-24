using System;
using System.Collections.Generic;
using System.Text;

namespace TextBoxSync
{
    partial class zzyTextBox : System.Windows.Forms.TextBox
    {
        public zzyTextBox() : base()
        {
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys KeyData)
        {
            if (KeyData == System.Windows.Forms.Keys.Up || KeyData == System.Windows.Forms.Keys.Down)
                return true;
            return base.IsInputKey(KeyData);
        }
    }
}
