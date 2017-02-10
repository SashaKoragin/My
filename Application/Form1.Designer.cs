namespace Application
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Заявки = new System.Windows.Forms.Button();
            this.Добавить = new System.Windows.Forms.Button();
            this.Выборка = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Выборка.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Заявки
            // 
            this.Заявки.Location = new System.Drawing.Point(36, 65);
            this.Заявки.Name = "Заявки";
            this.Заявки.Size = new System.Drawing.Size(150, 39);
            this.Заявки.TabIndex = 0;
            this.Заявки.Text = "Заявки";
            this.Заявки.UseVisualStyleBackColor = true;
            this.Заявки.Click += new System.EventHandler(this.button1_Click);
            // 
            // Добавить
            // 
            this.Добавить.Location = new System.Drawing.Point(36, 136);
            this.Добавить.Name = "Добавить";
            this.Добавить.Size = new System.Drawing.Size(150, 39);
            this.Добавить.TabIndex = 1;
            this.Добавить.Text = "Добавить в БД ";
            this.Добавить.UseVisualStyleBackColor = true;
            this.Добавить.Click += new System.EventHandler(this.button2_Click);
            // 
            // Выборка
            // 
            this.Выборка.Controls.Add(this.label1);
            this.Выборка.Controls.Add(this.button1);
            this.Выборка.Controls.Add(this.dataGridView1);
            this.Выборка.Location = new System.Drawing.Point(234, 26);
            this.Выборка.Name = "Выборка";
            this.Выборка.Size = new System.Drawing.Size(749, 445);
            this.Выборка.TabIndex = 2;
            this.Выборка.TabStop = false;
            this.Выборка.Text = "Выборка";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 321);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(749, 124);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Выбрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите номер";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 483);
            this.Controls.Add(this.Выборка);
            this.Controls.Add(this.Добавить);
            this.Controls.Add(this.Заявки);
            this.Name = "Form1";
            this.Text = "БД";
            this.Выборка.ResumeLayout(false);
            this.Выборка.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Заявки;
        private System.Windows.Forms.Button Добавить;
        private System.Windows.Forms.GroupBox Выборка;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

