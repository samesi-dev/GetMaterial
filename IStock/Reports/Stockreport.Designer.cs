namespace IStock.Reports
{
    partial class Stockreport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stockreport));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CrossButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.SystemName = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.st = new IStock.st();
            this.stockBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockTableAdapter = new IStock.stTableAdapters.StockTableAdapter();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CrossButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.st)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(4)))), ((int)(((byte)(118)))));
            this.panel2.Controls.Add(this.CrossButton);
            this.panel2.Controls.Add(this.SystemName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 37);
            this.panel2.TabIndex = 53;
            // 
            // CrossButton
            // 
            this.CrossButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CrossButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(4)))), ((int)(((byte)(118)))));
            this.CrossButton.Image = ((System.Drawing.Image)(resources.GetObject("CrossButton.Image")));
            this.CrossButton.ImageActive = null;
            this.CrossButton.Location = new System.Drawing.Point(767, 6);
            this.CrossButton.Name = "CrossButton";
            this.CrossButton.Size = new System.Drawing.Size(25, 25);
            this.CrossButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CrossButton.TabIndex = 3;
            this.CrossButton.TabStop = false;
            this.CrossButton.Zoom = 10;
            this.CrossButton.Click += new System.EventHandler(this.CrossButton_Click);
            // 
            // SystemName
            // 
            this.SystemName.AutoSize = true;
            this.SystemName.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SystemName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SystemName.Location = new System.Drawing.Point(11, 8);
            this.SystemName.Name = "SystemName";
            this.SystemName.Size = new System.Drawing.Size(76, 28);
            this.SystemName.TabIndex = 0;
            this.SystemName.Text = "Reports";
            this.SystemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "st";
            reportDataSource1.Value = this.stockBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "IStock.Reports.streport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 44);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 403);
            this.reportViewer1.TabIndex = 54;
            // 
            // st
            // 
            this.st.DataSetName = "st";
            this.st.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stockBindingSource
            // 
            this.stockBindingSource.DataMember = "Stock";
            this.stockBindingSource.DataSource = this.st;
            // 
            // stockTableAdapter
            // 
            this.stockTableAdapter.ClearBeforeFill = true;
            // 
            // Stockreport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stockreport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stockreport";
            this.Load += new System.EventHandler(this.Stockreport_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CrossButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.st)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuImageButton CrossButton;
        private Bunifu.Framework.UI.BunifuCustomLabel SystemName;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private st st;
        private System.Windows.Forms.BindingSource stockBindingSource;
        private stTableAdapters.StockTableAdapter stockTableAdapter;
    }
}