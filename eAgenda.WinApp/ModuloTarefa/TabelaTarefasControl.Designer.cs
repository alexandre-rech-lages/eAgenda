namespace eAgenda.WinApp.ModuloTarefa
{
    partial class TabelaTarefasControl
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
            this.gridTarefas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTarefas)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTarefas
            // 
            this.gridTarefas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTarefas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTarefas.Location = new System.Drawing.Point(0, 0);
            this.gridTarefas.Name = "gridTarefas";
            this.gridTarefas.RowTemplate.Height = 25;
            this.gridTarefas.Size = new System.Drawing.Size(476, 411);
            this.gridTarefas.TabIndex = 0;
            // 
            // TabelaTarefasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridTarefas);
            this.Name = "TabelaTarefasControl";
            this.Size = new System.Drawing.Size(476, 411);
            ((System.ComponentModel.ISupportInitialize)(this.gridTarefas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTarefas;
    }
}
