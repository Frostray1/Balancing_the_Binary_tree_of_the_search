namespace BalancedBinaryTree
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.button_add = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_showRB = new System.Windows.Forms.Button();
            this.button_addRange = new System.Windows.Forms.Button();
            this.numericUpDown_min = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_max = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button_saveRB = new System.Windows.Forms.Button();
            this.button_showAVL = new System.Windows.Forms.Button();
            this.button_saveAVL = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown.Location = new System.Drawing.Point(592, 12);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(156, 20);
            this.numericUpDown.TabIndex = 1;
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.Location = new System.Drawing.Point(592, 38);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 2;
            this.button_add.Text = "Добавить";
            this.button_add.UseVisualStyleBackColor = true;
       //   this.button_add.Click += new System.EventHandler(this.Button_add_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(12, 170);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(756, 344);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // button_remove
            // 
            this.button_remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_remove.Location = new System.Drawing.Point(673, 38);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(75, 23);
            this.button_remove.TabIndex = 5;
            this.button_remove.Text = "Удалить";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.Button_remove_Click);
            // 
            // button_showRB
            // 
            this.button_showRB.Location = new System.Drawing.Point(27, 80);
            this.button_showRB.Name = "button_showRB";
            this.button_showRB.Size = new System.Drawing.Size(267, 30);
            this.button_showRB.TabIndex = 6;
            this.button_showRB.Text = "Просмотр RB дерева";
            this.button_showRB.UseVisualStyleBackColor = true;
//            this.button_showRB.Click += new System.EventHandler(this.Button_showRB_Click);
            // 
            // button_addRange
            // 
            this.button_addRange.Location = new System.Drawing.Point(27, 35);
            this.button_addRange.Name = "button_addRange";
            this.button_addRange.Size = new System.Drawing.Size(267, 23);
            this.button_addRange.TabIndex = 7;
            this.button_addRange.Text = "Добавить последовательность";
            this.button_addRange.UseVisualStyleBackColor = true;
           // this.button_addRange.Click += new System.EventHandler(this.Button_addRange_Click);
            // 
            // numericUpDown_min
            // 
            this.numericUpDown_min.Location = new System.Drawing.Point(27, 12);
            this.numericUpDown_min.Maximum = new decimal(new int[] {
            499,
            0,
            0,
            0});
            this.numericUpDown_min.Name = "numericUpDown_min";
            this.numericUpDown_min.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_min.TabIndex = 8;
            this.numericUpDown_min.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_max
            // 
            this.numericUpDown_max.Location = new System.Drawing.Point(174, 12);
            this.numericUpDown_max.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDown_max.Name = "numericUpDown_max";
            this.numericUpDown_max.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_max.TabIndex = 9;
            this.numericUpDown_max.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(153, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "-";
            // 
            // button_saveRB
            // 
            this.button_saveRB.Location = new System.Drawing.Point(27, 116);
            this.button_saveRB.Name = "button_saveRB";
            this.button_saveRB.Size = new System.Drawing.Size(267, 30);
            this.button_saveRB.TabIndex = 11;
            this.button_saveRB.Text = "Сохранить изображение RB дерева";
            this.button_saveRB.UseVisualStyleBackColor = true;
          //  this.button_saveRB.Click += new System.EventHandler(this.Button_saveRB_Click);
            // 
            // button_showAVL
            // 
            this.button_showAVL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_showAVL.Location = new System.Drawing.Point(481, 80);
            this.button_showAVL.Name = "button_showAVL";
            this.button_showAVL.Size = new System.Drawing.Size(267, 30);
            this.button_showAVL.TabIndex = 12;
            this.button_showAVL.Text = "Просмотр AVL дерева";
            this.button_showAVL.UseVisualStyleBackColor = true;
           // this.button_showAVL.Click += new System.EventHandler(this.Button_showAVL_Click);
            // 
            // button_saveAVL
            // 
            this.button_saveAVL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_saveAVL.Location = new System.Drawing.Point(481, 116);
            this.button_saveAVL.Name = "button_saveAVL";
            this.button_saveAVL.Size = new System.Drawing.Size(267, 30);
            this.button_saveAVL.TabIndex = 13;
            this.button_saveAVL.Text = "Сохранить изображение AVL дерева";
            this.button_saveAVL.UseVisualStyleBackColor = true;
          //  this.button_saveAVL.Click += new System.EventHandler(this.Button_saveAVL_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(344, 13);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(193, 45);
            this.button_clear.TabIndex = 14;
            this.button_clear.Text = "Очистить деревья";
            this.button_clear.UseVisualStyleBackColor = true;
          //  this.button_clear.Click += new System.EventHandler(this.Button_clear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 526);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_saveAVL);
            this.Controls.Add(this.button_showAVL);
            this.Controls.Add(this.button_saveRB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_max);
            this.Controls.Add(this.numericUpDown_min);
            this.Controls.Add(this.button_addRange);
            this.Controls.Add(this.button_showRB);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.numericUpDown);
            this.Name = "MainForm";
            this.Text = "Черно-красное дерево";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Button button_showRB;
        private System.Windows.Forms.Button button_addRange;
        private System.Windows.Forms.NumericUpDown numericUpDown_min;
        private System.Windows.Forms.NumericUpDown numericUpDown_max;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_saveRB;
        private System.Windows.Forms.Button button_showAVL;
        private System.Windows.Forms.Button button_saveAVL;
        private System.Windows.Forms.Button button_clear;
    }
}

