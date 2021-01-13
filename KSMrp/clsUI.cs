using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using System.Drawing;
using nsKXMSUC;

namespace KSMrp
{
    public class clsUI
    {
        public void SetButtonStatus(RadioButton rb, Boolean Check)
    {
        if (Check == true)
        {
            rb.Enabled = true;
        }
        else
        {
            rb.Enabled = false;
        }
    }

    public bool ShowPanel(Panel MainPanel, Panel SubPanel, string PanelName)
    {

        bool IsExist = false;
        string Desc = PanelName;
        foreach (Panel SPanel in MainPanel.Controls)
        {
            if (SPanel.Tag.ToString() == Desc)
            {
                IsExist = true;
                SPanel.Visible = true;
                break;
            }
            else
            {
                SPanel.Visible = false;
            }
        }
        if (IsExist == false)
        {
            MainPanel.Controls.Add(SubPanel);
            SubPanel.Dock = DockStyle.Fill;
            SubPanel.Tag = Desc;
        }
        return !IsExist;
    }
    public bool ClearPanel(Panel MainPanel)
    {

        bool IsExist = false;
        foreach (Panel SPanel in MainPanel.Controls)
        {
            SPanel.Visible = false;
        }
        return !IsExist;
    }

    }
    public class InputBox
    {
        public static DialogResult Inputbox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            textBox.Font = new Font("新細明體",12);
            textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 50, 372, 20);
            buttonOk.SetBounds(228, 80, 75, 23);
            buttonCancel.SetBounds(309, 80, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 117);
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
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public ComboboxItem()
        {
        }
        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
