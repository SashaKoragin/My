﻿using System;
using System.Reflection;
using System.Reflection.Emit;

namespace TestIFNSTools.Arhivator.Arhiv
{
    partial class Arhivator
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
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Arhivator));
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Sovpad = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.col0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button3 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.colu0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьПутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView5 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateFinish = new System.Windows.Forms.TextBox();
            this.DateStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listView6 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView3 = new System.Windows.Forms.ListView();
            this.colum0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colum1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colum2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.загрузитьQBEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьQBEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button6 = new System.Windows.Forms.Button();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.загрузитьQBEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьQBEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.открытьОТЧЕТToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотретьKVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьОТЧЕТToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьОТЧЕТToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button7 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Stat = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.открытьОтчетToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьОтчетToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Location = new System.Drawing.Point(502, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выберете папку для поиска";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Sovpad);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 144);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры для ввода!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Признак совпадения";
            // 
            // Sovpad
            // 
            this.Sovpad.Location = new System.Drawing.Point(126, 97);
            this.Sovpad.Name = "Sovpad";
            this.Sovpad.Size = new System.Drawing.Size(111, 20);
            this.Sovpad.TabIndex = 4;
            this.Sovpad.Text = "MI_2NDFLMF*";
            this.Sovpad.Enter += new System.EventHandler(this.Sovpad_Enter);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(9, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(490, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(490, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(502, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "Выберете путь для архивов";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.listView2);
            this.groupBox2.Location = new System.Drawing.Point(740, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 286);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Работа с папками.";
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.BackColor = System.Drawing.SystemColors.Info;
            this.listView1.BackgroundImageTiled = true;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col0});
            this.listView1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listView1.ForeColor = System.Drawing.Color.Black;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(15, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(611, 115);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // col0
            // 
            this.col0.Text = "Пути для выбора файлов!";
            this.col0.Width = 185;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button3.Location = new System.Drawing.Point(15, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 20);
            this.button3.TabIndex = 3;
            this.button3.Text = "Save QBE";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listView2
            // 
            this.listView2.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView2.BackgroundImageTiled = true;
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colu0});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(15, 177);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(611, 96);
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            // 
            // colu0
            // 
            this.colu0.Text = "Сохраненнные пути!";
            this.colu0.Width = 191;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьПутьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // удалитьПутьToolStripMenuItem
            // 
            this.удалитьПутьToolStripMenuItem.Name = "удалитьПутьToolStripMenuItem";
            this.удалитьПутьToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.удалитьПутьToolStripMenuItem.Text = "Удалить путь!";
            this.удалитьПутьToolStripMenuItem.Click += new System.EventHandler(this.удалитьПутьToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.listView5);
            this.groupBox3.Controls.Add(this.DateFinish);
            this.groupBox3.Controls.Add(this.DateStart);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.monthCalendar1);
            this.groupBox3.Location = new System.Drawing.Point(12, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 448);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметры даты.";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Fuchsia;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(88, 293);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 22);
            this.button5.TabIndex = 6;
            this.button5.Text = "Reset";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Lime;
            this.button4.Location = new System.Drawing.Point(22, 293);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 22);
            this.button4.TabIndex = 6;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listView5
            // 
            this.listView5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView5.BackgroundImageTiled = true;
            this.listView5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView5.FullRowSelect = true;
            this.listView5.GridLines = true;
            this.listView5.Location = new System.Drawing.Point(22, 321);
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(248, 109);
            this.listView5.TabIndex = 7;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            this.listView5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView5_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Выборки дат:";
            this.columnHeader1.Width = 93;
            // 
            // DateFinish
            // 
            this.DateFinish.Enabled = false;
            this.DateFinish.Location = new System.Drawing.Point(156, 240);
            this.DateFinish.Name = "DateFinish";
            this.DateFinish.Size = new System.Drawing.Size(100, 20);
            this.DateFinish.TabIndex = 6;
            // 
            // DateStart
            // 
            this.DateStart.Enabled = false;
            this.DateStart.Location = new System.Drawing.Point(156, 206);
            this.DateStart.Name = "DateStart";
            this.DateStart.Size = new System.Drawing.Size(100, 20);
            this.DateStart.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Дата окончания поиска:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Дата начала поиска:";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.monthCalendar1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.monthCalendar1.Location = new System.Drawing.Point(50, 27);
            this.monthCalendar1.MaxSelectionCount = 366;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.TitleBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.monthCalendar1.TitleForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.monthCalendar1.TrailingForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox4.Controls.Add(this.listView6);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.listView4);
            this.groupBox4.Controls.Add(this.listView3);
            this.groupBox4.Location = new System.Drawing.Point(740, 304);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(671, 425);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Отчет отобранных значений!";
            // 
            // listView6
            // 
            this.listView6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView6.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView6.FullRowSelect = true;
            this.listView6.GridLines = true;
            this.listView6.Location = new System.Drawing.Point(15, 282);
            this.listView6.Name = "listView6";
            this.listView6.Size = new System.Drawing.Size(611, 130);
            this.listView6.TabIndex = 12;
            this.listView6.UseCompatibleStateImageBehavior = false;
            this.listView6.View = System.Windows.Forms.View.Details;
            this.listView6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView6_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Отчет по квитанциям Excel:";
            this.columnHeader2.Width = 166;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Динамический отчет!!!";
            // 
            // listView4
            // 
            this.listView4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0});
            this.listView4.FullRowSelect = true;
            this.listView4.GridLines = true;
            this.listView4.Location = new System.Drawing.Point(15, 165);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(611, 111);
            this.listView4.TabIndex = 1;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            this.listView4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView4_MouseClick);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Отчеты по Архивам Excel:";
            this.columnHeader0.Width = 166;
            // 
            // listView3
            // 
            this.listView3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colum0,
            this.colum1,
            this.colum2,
            this.column3});
            this.listView3.GridLines = true;
            this.listView3.Location = new System.Drawing.Point(15, 35);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(611, 124);
            this.listView3.TabIndex = 0;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // colum0
            // 
            this.colum0.Text = "Номер";
            this.colum0.Width = 47;
            // 
            // colum1
            // 
            this.colum1.Text = "Дата";
            this.colum1.Width = 89;
            // 
            // colum2
            // 
            this.colum2.Text = "Имя файла";
            this.colum2.Width = 196;
            // 
            // column3
            // 
            this.column3.Text = "Путь";
            this.column3.Width = 268;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьQBEToolStripMenuItem,
            this.удалитьQBEToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(154, 48);
            // 
            // загрузитьQBEToolStripMenuItem
            // 
            this.загрузитьQBEToolStripMenuItem.Name = "загрузитьQBEToolStripMenuItem";
            this.загрузитьQBEToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.загрузитьQBEToolStripMenuItem.Text = "Загрузить QBE";
            this.загрузитьQBEToolStripMenuItem.Click += new System.EventHandler(this.загрузитьQBEToolStripMenuItem_Click);
            // 
            // удалитьQBEToolStripMenuItem
            // 
            this.удалитьQBEToolStripMenuItem.Name = "удалитьQBEToolStripMenuItem";
            this.удалитьQBEToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.удалитьQBEToolStripMenuItem.Text = "Удалить QBE";
            this.удалитьQBEToolStripMenuItem.Click += new System.EventHandler(this.удалитьQBEToolStripMenuItem_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button6.Location = new System.Drawing.Point(315, 189);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(129, 41);
            this.button6.TabIndex = 6;
            this.button6.Text = "Архивировать!!!";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьQBEToolStripMenuItem1,
            this.удалитьQBEToolStripMenuItem1});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(154, 48);
            // 
            // загрузитьQBEToolStripMenuItem1
            // 
            this.загрузитьQBEToolStripMenuItem1.Name = "загрузитьQBEToolStripMenuItem1";
            this.загрузитьQBEToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.загрузитьQBEToolStripMenuItem1.Text = "Загрузить QBE";
            this.загрузитьQBEToolStripMenuItem1.Click += new System.EventHandler(this.загрузитьQBEToolStripMenuItem1_Click);
            // 
            // удалитьQBEToolStripMenuItem1
            // 
            this.удалитьQBEToolStripMenuItem1.Name = "удалитьQBEToolStripMenuItem1";
            this.удалитьQBEToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.удалитьQBEToolStripMenuItem1.Text = "Удалить QBE";
            this.удалитьQBEToolStripMenuItem1.Click += new System.EventHandler(this.удалитьQBEToolStripMenuItem1_Click);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьОТЧЕТToolStripMenuItem,
            this.просмотретьKVToolStripMenuItem,
            this.загрузитьОТЧЕТToolStripMenuItem,
            this.удалитьОТЧЕТToolStripMenuItem});
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(169, 92);
            // 
            // открытьОТЧЕТToolStripMenuItem
            // 
            this.открытьОТЧЕТToolStripMenuItem.Name = "открытьОТЧЕТToolStripMenuItem";
            this.открытьОТЧЕТToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.открытьОТЧЕТToolStripMenuItem.Text = "Открыть ОТЧЕТ";
            this.открытьОТЧЕТToolStripMenuItem.Click += new System.EventHandler(this.открытьОТЧЕТToolStripMenuItem_Click);
            // 
            // просмотретьKVToolStripMenuItem
            // 
            this.просмотретьKVToolStripMenuItem.Name = "просмотретьKVToolStripMenuItem";
            this.просмотретьKVToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.просмотретьKVToolStripMenuItem.Text = "KV Отчет";
            this.просмотретьKVToolStripMenuItem.Click += new System.EventHandler(this.просмотретьKVToolStripMenuItem_Click);
            // 
            // загрузитьОТЧЕТToolStripMenuItem
            // 
            this.загрузитьОТЧЕТToolStripMenuItem.Name = "загрузитьОТЧЕТToolStripMenuItem";
            this.загрузитьОТЧЕТToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.загрузитьОТЧЕТToolStripMenuItem.Text = "Загрузить ОТЧЕТ";
            this.загрузитьОТЧЕТToolStripMenuItem.Click += new System.EventHandler(this.загрузитьОТЧЕТToolStripMenuItem_Click);
            // 
            // удалитьОТЧЕТToolStripMenuItem
            // 
            this.удалитьОТЧЕТToolStripMenuItem.Name = "удалитьОТЧЕТToolStripMenuItem";
            this.удалитьОТЧЕТToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.удалитьОТЧЕТToolStripMenuItem.Text = "Удалить ОТЧЕТ";
            this.удалитьОТЧЕТToolStripMenuItem.Click += new System.EventHandler(this.удалитьОТЧЕТToolStripMenuItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button7.Location = new System.Drawing.Point(547, 189);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(158, 41);
            this.button7.TabIndex = 9;
            this.button7.Text = "Проверка соединение с БД";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Stat,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 736);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1423, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Stat
            // 
            this.Stat.Maximum = 10000;
            this.Stat.Name = "Stat";
            this.Stat.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // contextMenuStrip5
            // 
            this.contextMenuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьОтчетToolStripMenuItem1,
            this.удалитьОтчетToolStripMenuItem1});
            this.contextMenuStrip5.Name = "contextMenuStrip5";
            this.contextMenuStrip5.Size = new System.Drawing.Size(157, 48);
            // 
            // открытьОтчетToolStripMenuItem1
            // 
            this.открытьОтчетToolStripMenuItem1.Name = "открытьОтчетToolStripMenuItem1";
            this.открытьОтчетToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.открытьОтчетToolStripMenuItem1.Text = "Открыть Отчет";
            this.открытьОтчетToolStripMenuItem1.Click += new System.EventHandler(this.открытьОтчетToolStripMenuItem1_Click);
            // 
            // удалитьОтчетToolStripMenuItem1
            // 
            this.удалитьОтчетToolStripMenuItem1.Name = "удалитьОтчетToolStripMenuItem1";
            this.удалитьОтчетToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.удалитьОтчетToolStripMenuItem1.Text = "Удалить Отчет";
            this.удалитьОтчетToolStripMenuItem1.Click += new System.EventHandler(this.удалитьОтчетToolStripMenuItem1_Click);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(34, 665);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(98, 28);
            this.button8.TabIndex = 12;
            this.button8.Text = "К детализации";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Arhivator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1423, 758);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Arhivator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = String.Format("Архиватор версии {0}", version);
            this.Load += new System.EventHandler(this.LoadReport);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader col0;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem удалитьПутьToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colu0;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem загрузитьQBEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьQBEToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem загрузитьQBEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьQBEToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem открытьОТЧЕТToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьОТЧЕТToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьОТЧЕТToolStripMenuItem;
        internal System.Windows.Forms.ListView listView1;
        internal System.Windows.Forms.TextBox Sovpad;
        internal System.Windows.Forms.TextBox DateFinish;
        internal System.Windows.Forms.TextBox DateStart;
        internal System.Windows.Forms.ListView listView4;
        internal System.Windows.Forms.ListView listView3;
        internal System.Windows.Forms.ColumnHeader colum0;
        internal System.Windows.Forms.ColumnHeader colum1;
        internal System.Windows.Forms.ColumnHeader colum2;
        internal System.Windows.Forms.ColumnHeader column3;
        internal System.Windows.Forms.TextBox textBox2;
        internal System.Windows.Forms.ListView listView5;
        internal System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripProgressBar Stat;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem просмотретьKVToolStripMenuItem;
        internal System.Windows.Forms.Button button7;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip5;
        private System.Windows.Forms.ToolStripMenuItem открытьОтчетToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьОтчетToolStripMenuItem1;
        internal System.Windows.Forms.ListView listView6;
        internal System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button button8;
        internal System.Windows.Forms.ListView listView2;
       
    }
}

