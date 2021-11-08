namespace TelevisonStore.DesignForm.Messeage
{
    partial class YesNo
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.btnYes = new FontAwesome.Sharp.IconButton();
            this.btnNo = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.ForeColor = System.Drawing.Color.DimGray;
            this.labelMessage.Location = new System.Drawing.Point(66, 74);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(424, 36);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "Bạn có chắc chắn xóa không ?";
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.SystemColors.Control;
            this.btnYes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(119)))));
            this.btnYes.FlatAppearance.BorderSize = 4;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnYes.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnYes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(119)))));
            this.btnYes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnYes.IconSize = 20;
            this.btnYes.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnYes.Location = new System.Drawing.Point(240, 154);
            this.btnYes.Name = "btnYes";
            this.btnYes.Rotation = 0D;
            this.btnYes.Size = new System.Drawing.Size(95, 39);
            this.btnYes.TabIndex = 1;
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNo.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnNo.FlatAppearance.BorderSize = 4;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnNo.IconColor = System.Drawing.Color.Red;
            this.btnNo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNo.IconSize = 20;
            this.btnNo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNo.Location = new System.Drawing.Point(360, 154);
            this.btnNo.Name = "btnNo";
            this.btnNo.Rotation = 0D;
            this.btnNo.Size = new System.Drawing.Size(95, 39);
            this.btnNo.TabIndex = 1;
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // YesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(502, 216);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "YesNo";
            this.Text = "YesNo";
            this.Load += new System.EventHandler(this.YesNo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private FontAwesome.Sharp.IconButton btnYes;
        private FontAwesome.Sharp.IconButton btnNo;
    }
}