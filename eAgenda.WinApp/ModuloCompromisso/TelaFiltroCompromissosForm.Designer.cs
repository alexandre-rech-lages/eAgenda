namespace eAgenda.WinApp.ModuloCompromisso
{
    partial class TelaFiltroCompromissosForm
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
            this.rdbTodosCompromissos = new System.Windows.Forms.RadioButton();
            this.rdbCompromissosFuturos = new System.Windows.Forms.RadioButton();
            this.rdbCompromissosPassados = new System.Windows.Forms.RadioButton();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdbTodosCompromissos
            // 
            this.rdbTodosCompromissos.AutoSize = true;
            this.rdbTodosCompromissos.Location = new System.Drawing.Point(31, 33);
            this.rdbTodosCompromissos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdbTodosCompromissos.Name = "rdbTodosCompromissos";
            this.rdbTodosCompromissos.Size = new System.Drawing.Size(204, 19);
            this.rdbTodosCompromissos.TabIndex = 4;
            this.rdbTodosCompromissos.TabStop = true;
            this.rdbTodosCompromissos.Text = "Visualizar todas os Compromissos";
            this.rdbTodosCompromissos.UseVisualStyleBackColor = true;
            // 
            // rdbCompromissosFuturos
            // 
            this.rdbCompromissosFuturos.AutoSize = true;
            this.rdbCompromissosFuturos.Location = new System.Drawing.Point(31, 73);
            this.rdbCompromissosFuturos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdbCompromissosFuturos.Name = "rdbCompromissosFuturos";
            this.rdbCompromissosFuturos.Size = new System.Drawing.Size(249, 19);
            this.rdbCompromissosFuturos.TabIndex = 5;
            this.rdbCompromissosFuturos.TabStop = true;
            this.rdbCompromissosFuturos.Text = "Visualizar somente Compromissos Futuros";
            this.rdbCompromissosFuturos.UseVisualStyleBackColor = true;
            // 
            // rdbCompromissosPassados
            // 
            this.rdbCompromissosPassados.AutoSize = true;
            this.rdbCompromissosPassados.Location = new System.Drawing.Point(31, 112);
            this.rdbCompromissosPassados.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdbCompromissosPassados.Name = "rdbCompromissosPassados";
            this.rdbCompromissosPassados.Size = new System.Drawing.Size(257, 19);
            this.rdbCompromissosPassados.TabIndex = 6;
            this.rdbCompromissosPassados.TabStop = true;
            this.rdbCompromissosPassados.Text = "Visualizar somente Compromissos Passados";
            this.rdbCompromissosPassados.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(343, 219);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 39);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(262, 219);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(72, 39);
            this.btnGravar.TabIndex = 8;
            this.btnGravar.Text = "Filtrar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // TelaFiltroCompromissosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 270);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.rdbCompromissosPassados);
            this.Controls.Add(this.rdbCompromissosFuturos);
            this.Controls.Add(this.rdbTodosCompromissos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaFiltroCompromissosForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Compromissos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rdbTodosCompromissos;
        private System.Windows.Forms.RadioButton rdbCompromissosFuturos;
        private System.Windows.Forms.RadioButton rdbCompromissosPassados;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
    }
}