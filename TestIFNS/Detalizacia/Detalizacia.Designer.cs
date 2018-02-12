using System.Windows.Forms;

namespace TestIFNSTools.Detalizacia
{
    partial class Detalizacia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (Application.OpenForms["Arhivator"] != null)
            {
                Application.OpenForms["Arhivator"].Show();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBarUL = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusUl = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarFl = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusFl = new System.Windows.Forms.ToolStripStatusLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.Mi = new System.Windows.Forms.Label();
            this.ErrorMI = new System.Windows.Forms.ComboBox();
            this.HostWpfXaml = new System.Windows.Forms.Integration.ElementHost();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarUL,
            this.StatusUl,
            this.StatusBarFl,
            this.StatusFl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 661);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1278, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBarUL
            // 
            this.StatusBarUL.Maximum = 10000;
            this.StatusBarUL.Name = "StatusBarUL";
            this.StatusBarUL.Size = new System.Drawing.Size(200, 16);
            // 
            // StatusUl
            // 
            this.StatusUl.Name = "StatusUl";
            this.StatusUl.Size = new System.Drawing.Size(98, 17);
            this.StatusUl.Text = "Состояние ЮЛ!!!";
            // 
            // StatusBarFl
            // 
            this.StatusBarFl.Maximum = 10000;
            this.StatusBarFl.Name = "StatusBarFl";
            this.StatusBarFl.Size = new System.Drawing.Size(200, 16);
            // 
            // StatusFl
            // 
            this.StatusFl.Name = "StatusFl";
            this.StatusFl.Size = new System.Drawing.Size(95, 17);
            this.StatusFl.Text = "Состояние ФЛ!!!";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(12, 623);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "Назад к Форме";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Mi
            // 
            this.Mi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mi.AutoSize = true;
            this.Mi.BackColor = System.Drawing.Color.Transparent;
            this.Mi.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Mi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Mi.Location = new System.Drawing.Point(179, 623);
            this.Mi.Name = "Mi";
            this.Mi.Size = new System.Drawing.Size(258, 29);
            this.Mi.TabIndex = 13;
            this.Mi.Text = "Состояние файла MI:";
            // 
            // ErrorMI
            // 
            this.ErrorMI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorMI.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ErrorMI.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ErrorMI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ErrorMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorMI.ForeColor = System.Drawing.Color.Red;
            this.ErrorMI.FormattingEnabled = true;
            this.ErrorMI.Location = new System.Drawing.Point(428, 623);
            this.ErrorMI.Name = "ErrorMI";
            this.ErrorMI.Size = new System.Drawing.Size(838, 28);
            this.ErrorMI.TabIndex = 14;
            // 
            // HostWpfXaml
            // 
            this.HostWpfXaml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HostWpfXaml.Location = new System.Drawing.Point(0, 0);
            this.HostWpfXaml.Name = "HostWpfXaml";
            this.HostWpfXaml.Size = new System.Drawing.Size(1278, 606);
            this.HostWpfXaml.TabIndex = 12;
            this.HostWpfXaml.Text = "elementHost1";
            this.HostWpfXaml.Child = null;
            // 
            // Detalizacia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1278, 683);
            this.Controls.Add(this.ErrorMI);
            this.Controls.Add(this.Mi);
            this.Controls.Add(this.HostWpfXaml);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Detalizacia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Детализация организаций";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.ToolStripProgressBar StatusBarUL;
        internal System.Windows.Forms.ToolStripStatusLabel StatusUl;
        internal System.Windows.Forms.StatusStrip statusStrip1;
        internal ToolStripProgressBar StatusBarFl;
        internal ToolStripStatusLabel StatusFl;
        public Label Mi;
        internal ComboBox ErrorMI;
        internal System.Windows.Forms.Integration.ElementHost HostWpfXaml;
    }
}