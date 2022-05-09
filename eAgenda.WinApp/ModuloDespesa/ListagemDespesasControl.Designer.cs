namespace eAgenda.WinApp.ModuloDespesa
{
    partial class ListagemDespesasControl
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
            this.listDespesas = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listDespesas
            // 
            this.listDespesas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDespesas.FormattingEnabled = true;
            this.listDespesas.ItemHeight = 15;
            this.listDespesas.Location = new System.Drawing.Point(0, 0);
            this.listDespesas.Name = "listDespesas";
            this.listDespesas.Size = new System.Drawing.Size(277, 253);
            this.listDespesas.TabIndex = 0;
            // 
            // ListagemDespesasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listDespesas);
            this.Name = "ListagemDespesasControl";
            this.Size = new System.Drawing.Size(277, 253);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listDespesas;
    }
}
