using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestIFNSTools
{
    public partial class DialogForm : Form
    {

        public DialogForm()
        {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(textBox1.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                    e.Cancel = true;
                    textBox1.Select(0, textBox1.Text.Length);
                    // Set the ErrorProvider error with the text to display. 
                    errorProvider1.SetError(textBox1, errorMsg);
            }
        }
        private void textBox1_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(textBox1, "");
        }

        public bool ValidEmailAddress(string FileName, out string errorMessage)
        {
            // Если текст 0
            if (FileName.Length == 0)
            {
                errorMessage = "Поле не может содержать пустое значение!!!";
                return false;
            }
            // Если содержат запрещенные символы в имени файла то плохо в противном случае хоролшо!
            foreach (char Simvol in new[] { '?', '\\', '/', ':', '"', '*', '>', '<', '|' })  //Перебор запрещенных символов в имени файла
            {
                if (FileName.IndexOf(Simvol) != -1)
                {
                    errorMessage = "Поле содержит недопустимый символ [" + Simvol + "] !";
                    return false;

                }
            }
                // Если все хорошо
             errorMessage = "";
             return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;  //Установлено свойство button2.CausesValidation = false;
        }

        //private bool ValidateAll()
        //{
        //    ValidateControl(0, this);
        //    return false;
        //}

        //private void ValidateControl(int recursionDeep, Control ctl)
        //{
        //    const string eventName = "Validating";
        //    if (recursionDeep > 10)
        //        throw new InvalidOperationException("Бесконечный цикл");
        //    var cancelEventArgs = new CancelEventArgs();
        //    foreach (Control childControl in ctl.Controls)
        //    {
        //        childContr
        //        var eventInfo = childControl.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //        var eventDelegate = childControl.GetType().GetField(eventName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(childControl) as MulticastDelegate;
        //        if (eventDelegate != null)
        //        {
        //            foreach (var handler in eventDelegate.GetInvocationList())
        //            {
        //                handler.Method.Invoke(handler.Target, new object[] { childControl, cancelEventArgs });
        //            }
        //        }

   
        //        ValidateControl(++recursionDeep, childControl);
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren(ValidationConstraints.Visible))
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
        }
        }
    }
