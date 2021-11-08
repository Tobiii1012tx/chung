namespace TelevisonStore.DesignForm
{
    partial class HomeForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelViewProduct = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelViewSomething = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panelViewEmployee = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panelViewProduct.SuspendLayout();
            this.panelViewSomething.SuspendLayout();
            this.panelViewEmployee.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelViewProduct
            // 
            this.panelViewProduct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelViewProduct.AutoSize = true;
            this.panelViewProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panelViewProduct.Controls.Add(this.label1);
            this.panelViewProduct.Location = new System.Drawing.Point(25, 28);
            this.panelViewProduct.Name = "panelViewProduct";
            this.panelViewProduct.Size = new System.Drawing.Size(334, 137);
            this.panelViewProduct.TabIndex = 0;
            this.panelViewProduct.Click += new System.EventHandler(this.panelViewProduct_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(11, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Báo cáo sản phẩm theo khách hàng";
            // 
            // panelViewSomething
            // 
            this.panelViewSomething.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelViewSomething.BackColor = System.Drawing.Color.DarkGreen;
            this.panelViewSomething.Controls.Add(this.label3);
            this.panelViewSomething.Location = new System.Drawing.Point(394, 28);
            this.panelViewSomething.Name = "panelViewSomething";
            this.panelViewSomething.Size = new System.Drawing.Size(354, 137);
            this.panelViewSomething.TabIndex = 0;
            this.panelViewSomething.Click += new System.EventHandler(this.panelViewSomething_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(29, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Báo cáo danh sách hóa đơn nhập";
            // 
            // panelViewEmployee
            // 
            this.panelViewEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelViewEmployee.BackColor = System.Drawing.Color.Crimson;
            this.panelViewEmployee.Controls.Add(this.label2);
            this.panelViewEmployee.Location = new System.Drawing.Point(25, 192);
            this.panelViewEmployee.Name = "panelViewEmployee";
            this.panelViewEmployee.Size = new System.Drawing.Size(334, 137);
            this.panelViewEmployee.TabIndex = 0;
            this.panelViewEmployee.Click += new System.EventHandler(this.panelViewEmployee_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(11, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Báo cáo danh sách hóa đơn nhập theo quý";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.DarkGreen;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(394, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 137);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(29, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Top nhà cung cấp theo tháng";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelViewEmployee);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelViewSomething);
            this.Controls.Add(this.panelViewProduct);
            this.Name = "HomeForm";
            this.Size = new System.Drawing.Size(1038, 618);
            this.panelViewProduct.ResumeLayout(false);
            this.panelViewProduct.PerformLayout();
            this.panelViewSomething.ResumeLayout(false);
            this.panelViewSomething.PerformLayout();
            this.panelViewEmployee.ResumeLayout(false);
            this.panelViewEmployee.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelViewProduct;
        private System.Windows.Forms.Panel panelViewSomething;
        private System.Windows.Forms.Panel panelViewEmployee;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}
