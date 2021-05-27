using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UntitledTrojan2.Tools
{
    internal static class Failsafe
    {
        internal static bool FailMain()
        {
            string inputText = "";

            MessageBox.Show("Это не шутка. Это настоящий троян." +
                    "\nЕсли ты продолжишь, то твой MBR будет стёрт." +
                    "\nТОЛЬКО ДЛЯ ИСПОЛЬЗОВАНИЯ В ИССЛЕДОВАТЕЛЬСКИХ ЦЕЛЯХ.", "Amogus Trojan 2.0", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            DialogResult result = InputBox("Amogus Trojan 2.0", "Напиши \"Согласен\" если ты хочешь продолжить. ЭТО ОПАСНО, ЧЁРТ ВОЗЬМИ!", ref inputText);
            if (result == DialogResult.OK)
            {
                if (inputText.ToLower() == "согласен") return true;
                else MessageBox.Show("Вы ввели слово неправильно!", "Amogus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("АХТУНГ! Отмена действий.", "Amogus", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return false;
        }

        internal static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "АХТУНГ!";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 40, 372, 35);
            buttonOk.SetBounds(228, 72, 75, 30);
            buttonCancel.SetBounds(309, 72, 75, 30);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 110);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
