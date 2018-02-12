using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestIFNSTools.Arhivator.DialogForm
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

        public bool ValidEmailAddress(string fileName, out string errorMessage)
        {
            // Если текст 0
            if (fileName.Length == 0)
            {
                errorMessage = "Поле не может содержать пустое значение!!!";
                return false;
            }
            // Если содержат запрещенные символы в имени файла то плохо в противном случае хоролшо!
            foreach (char simvol in new[] { '?', '\\', '/', ':', '"', '*', '>', '<', '|' })  //Перебор запрещенных символов в имени файла
            {
                if (fileName.IndexOf(simvol) != -1)
                {
                    errorMessage = "Поле содержит недопустимый символ [" + simvol + "] !";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Visible))
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
        }
        }
    }
