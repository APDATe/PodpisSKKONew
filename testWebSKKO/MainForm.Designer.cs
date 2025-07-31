namespace testWebSKKO
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnGenerateAndSign;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnGenerateAndSign = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnGenerateAndSign
            // 
            this.btnGenerateAndSign.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGenerateAndSign.Location = new System.Drawing.Point(40, 30);
            this.btnGenerateAndSign.Name = "btnGenerateAndSign";
            this.btnGenerateAndSign.Size = new System.Drawing.Size(240, 40);
            this.btnGenerateAndSign.TabIndex = 0;
            this.btnGenerateAndSign.Text = "Сгенерировать и подписать";
            this.btnGenerateAndSign.UseVisualStyleBackColor = true;
            this.btnGenerateAndSign.Click += new System.EventHandler(this.btnGenerateAndSign_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 100);
            this.Controls.Add(this.btnGenerateAndSign);
            this.Name = "MainForm";
            this.Text = "JsonSignerApp";
            this.ResumeLayout(false);
        }
    }
}
