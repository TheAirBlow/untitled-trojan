using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UntitledTrojan.Tools
{
    internal static class Failsafe
    {
        internal static bool FailMain()
        {
            string inputText = "";
            MessageBox.Show("WARNING! This is not a joke." +
                "\nThis can trash your pc. Use VM for testing this malware." +
                "\nUse only for educational purposes." +
                "\nNext window will be a confirmation of your actions." +
                "\n\nCreated by TheAirBlow 2020 (https://github.com/theairblow)", "UntitledTrojan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult result = InputBox("UntitledTrojan", "Enter \"I agree\" if you are agreed with anything showed before.", ref inputText);
            if (result == DialogResult.OK)
            {
                if (inputText == "I agree")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Trojan actions was cancelled.", "UntitledTrojan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Trojan actions was cancelled.", "UntitledTrojan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        internal static void AlreadyRan()
        {

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
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
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
