namespace eAgenda.WinApp
{
    partial class TelaPrincipalForm
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
            this.toolbox = new System.Windows.Forms.MenuStrip();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contatosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compromissosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnInserir = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.btnAdicionarItens = new System.Windows.Forms.ToolStripButton();
            this.btnAtualizarItens = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.praDarPauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbox.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbox
            // 
            this.toolbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrosToolStripMenuItem});
            this.toolbox.Location = new System.Drawing.Point(0, 0);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(544, 24);
            this.toolbox.TabIndex = 0;
            this.toolbox.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            this.cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tarefasMenuItem,
            this.contatosMenuItem,
            this.compromissosMenuItem,
            this.praDarPauToolStripMenuItem});
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // tarefasMenuItem
            // 
            this.tarefasMenuItem.Name = "tarefasMenuItem";
            this.tarefasMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.tarefasMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tarefasMenuItem.Text = "Tarefas";
            this.tarefasMenuItem.Click += new System.EventHandler(this.tarefasMenuItem_Click);
            // 
            // contatosMenuItem
            // 
            this.contatosMenuItem.Name = "contatosMenuItem";
            this.contatosMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.contatosMenuItem.Size = new System.Drawing.Size(180, 22);
            this.contatosMenuItem.Text = "Contatos";
            this.contatosMenuItem.Click += new System.EventHandler(this.contatosMenuItem_Click);
            // 
            // compromissosMenuItem
            // 
            this.compromissosMenuItem.Name = "compromissosMenuItem";
            this.compromissosMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.compromissosMenuItem.Size = new System.Drawing.Size(180, 22);
            this.compromissosMenuItem.Text = "Compromissos";
            this.compromissosMenuItem.Click += new System.EventHandler(this.compromissosMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInserir,
            this.btnEditar,
            this.btnExcluir,
            this.btnAdicionarItens,
            this.btnAtualizarItens});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(544, 41);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnInserir
            // 
            this.btnInserir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInserir.Image = global::eAgenda.WinApp.Properties.Resources.outline_add_circle_outline_black_24dp;
            this.btnInserir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInserir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Padding = new System.Windows.Forms.Padding(5);
            this.btnInserir.Size = new System.Drawing.Size(38, 38);
            this.btnInserir.Text = "Inserir";
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::eAgenda.WinApp.Properties.Resources.outline_mode_edit_black_24dp;
            this.btnEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditar.Size = new System.Drawing.Size(38, 38);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluir.Image = global::eAgenda.WinApp.Properties.Resources.outline_delete_black_24dp;
            this.btnExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Padding = new System.Windows.Forms.Padding(5);
            this.btnExcluir.Size = new System.Drawing.Size(38, 38);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnAdicionarItens
            // 
            this.btnAdicionarItens.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdicionarItens.Image = global::eAgenda.WinApp.Properties.Resources.outline_list_alt_black_24dp;
            this.btnAdicionarItens.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdicionarItens.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdicionarItens.Name = "btnAdicionarItens";
            this.btnAdicionarItens.Padding = new System.Windows.Forms.Padding(5);
            this.btnAdicionarItens.Size = new System.Drawing.Size(38, 38);
            this.btnAdicionarItens.Click += new System.EventHandler(this.btnAdicionarItens_Click);
            // 
            // btnAtualizarItens
            // 
            this.btnAtualizarItens.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAtualizarItens.Image = global::eAgenda.WinApp.Properties.Resources.outline_list_alt_black_24dp;
            this.btnAtualizarItens.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAtualizarItens.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAtualizarItens.Name = "btnAtualizarItens";
            this.btnAtualizarItens.Padding = new System.Windows.Forms.Padding(5);
            this.btnAtualizarItens.Size = new System.Drawing.Size(38, 38);
            this.btnAtualizarItens.Click += new System.EventHandler(this.btnAtualizarItens_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(544, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panelRegistros
            // 
            this.panelRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegistros.Location = new System.Drawing.Point(0, 65);
            this.panelRegistros.Name = "panelRegistros";
            this.panelRegistros.Size = new System.Drawing.Size(544, 312);
            this.panelRegistros.TabIndex = 3;
            // 
            // praDarPauToolStripMenuItem
            // 
            this.praDarPauToolStripMenuItem.Name = "praDarPauToolStripMenuItem";
            this.praDarPauToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.praDarPauToolStripMenuItem.Text = "Pra dar pau";
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 399);
            this.Controls.Add(this.panelRegistros);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolbox);
            this.MainMenuStrip = this.toolbox;
            this.MinimumSize = new System.Drawing.Size(560, 438);
            this.Name = "TelaPrincipalForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e-Agenda 2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolbox.ResumeLayout(false);
            this.toolbox.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip toolbox;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contatosMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compromissosMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.ToolStripButton btnInserir;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripButton btnAdicionarItens;
        private System.Windows.Forms.ToolStripButton btnAtualizarItens;
        private System.Windows.Forms.ToolStripMenuItem praDarPauToolStripMenuItem;
    }
}