namespace ClientApp
{
    partial class FrmStream
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gr1 = new System.Windows.Forms.GroupBox();
            this.pic4 = new System.Windows.Forms.PictureBox();
            this.pic3 = new System.Windows.Forms.PictureBox();
            this.pic2 = new System.Windows.Forms.PictureBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridDacnhSach = new System.Windows.Forms.DataGridView();
            this.gr1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDacnhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // gr1
            // 
            this.gr1.Controls.Add(this.pic4);
            this.gr1.Controls.Add(this.pic3);
            this.gr1.Controls.Add(this.pic2);
            this.gr1.Controls.Add(this.pic1);
            this.gr1.Location = new System.Drawing.Point(12, 12);
            this.gr1.Name = "gr1";
            this.gr1.Size = new System.Drawing.Size(953, 546);
            this.gr1.TabIndex = 0;
            this.gr1.TabStop = false;
            this.gr1.Text = "Camera";
            // 
            // pic4
            // 
            this.pic4.Location = new System.Drawing.Point(473, 277);
            this.pic4.Name = "pic4";
            this.pic4.Size = new System.Drawing.Size(463, 251);
            this.pic4.TabIndex = 0;
            this.pic4.TabStop = false;
            // 
            // pic3
            // 
            this.pic3.Location = new System.Drawing.Point(7, 277);
            this.pic3.Name = "pic3";
            this.pic3.Size = new System.Drawing.Size(460, 251);
            this.pic3.TabIndex = 0;
            this.pic3.TabStop = false;
            // 
            // pic2
            // 
            this.pic2.Location = new System.Drawing.Point(473, 20);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(463, 251);
            this.pic2.TabIndex = 0;
            this.pic2.TabStop = false;
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(7, 20);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(460, 251);
            this.pic1.TabIndex = 0;
            this.pic1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridDacnhSach);
            this.groupBox2.Location = new System.Drawing.Point(971, 338);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 218);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tình trạng";
            // 
            // gridDacnhSach
            // 
            this.gridDacnhSach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDacnhSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridDacnhSach.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridDacnhSach.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDacnhSach.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridDacnhSach.EnableHeadersVisualStyles = false;
            this.gridDacnhSach.GridColor = System.Drawing.Color.White;
            this.gridDacnhSach.Location = new System.Drawing.Point(1, 20);
            this.gridDacnhSach.MultiSelect = false;
            this.gridDacnhSach.Name = "gridDacnhSach";
            this.gridDacnhSach.RowHeadersVisible = false;
            this.gridDacnhSach.ShowEditingIcon = false;
            this.gridDacnhSach.Size = new System.Drawing.Size(415, 192);
            this.gridDacnhSach.TabIndex = 0;
            // 
            // FrmStream
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1395, 568);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gr1);
            this.Name = "FrmStream";
            this.Text = "FrmStream";
            this.Load += new System.EventHandler(this.FrmStream_Load);
            this.gr1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDacnhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gr1;
        private System.Windows.Forms.PictureBox pic4;
        private System.Windows.Forms.PictureBox pic3;
        private System.Windows.Forms.PictureBox pic2;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gridDacnhSach;
    }
}