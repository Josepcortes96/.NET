using System;
using System.Linq;
using System.Windows.Forms;
using FitData.Datos.Repositorios;
using FitData.Datos;
using FitData.Entidades;

    namespace FitData.Forms
    {
        partial class FormMain
        {
            private System.ComponentModel.IContainer components = null;
            private System.Windows.Forms.DataGridView dataGridViewUsuarios;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnEdit;
            private System.Windows.Forms.Button btnDelete;
            private System.Windows.Forms.Button btnImport;
            private System.Windows.Forms.Button btnExport;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null)) components.Dispose();
                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
                this.dataGridViewUsuarios = new System.Windows.Forms.DataGridView();
                this.btnAdd = new System.Windows.Forms.Button();
                this.btnEdit = new System.Windows.Forms.Button();
                this.btnDelete = new System.Windows.Forms.Button();
                this.btnImport = new System.Windows.Forms.Button();
                this.btnExport = new System.Windows.Forms.Button();
                ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsuarios)).BeginInit();
                this.SuspendLayout();
                // 
                // dataGridViewUsuarios
                // 
                this.dataGridViewUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Bottom) |
                    System.Windows.Forms.AnchorStyles.Left) |
                    System.Windows.Forms.AnchorStyles.Right)));
                this.dataGridViewUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dataGridViewUsuarios.Location = new System.Drawing.Point(12, 12);
                this.dataGridViewUsuarios.MultiSelect = false;
                this.dataGridViewUsuarios.Name = "dataGridViewUsuarios";
                this.dataGridViewUsuarios.ReadOnly = true;
                this.dataGridViewUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                this.dataGridViewUsuarios.Size = new System.Drawing.Size(760, 380);
                this.dataGridViewUsuarios.TabIndex = 0;
                // 
                // btnAdd
                // 
                this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
                this.btnAdd.Location = new System.Drawing.Point(12, 405);
                this.btnAdd.Name = "btnAdd";
                this.btnAdd.Size = new System.Drawing.Size(90, 30);
                this.btnAdd.TabIndex = 1;
                this.btnAdd.Text = "Añadir";
                this.btnAdd.UseVisualStyleBackColor = true;
                // 
                // btnEdit
                // 
                this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
                this.btnEdit.Location = new System.Drawing.Point(108, 405);
                this.btnEdit.Name = "btnEdit";
                this.btnEdit.Size = new System.Drawing.Size(90, 30);
                this.btnEdit.TabIndex = 2;
                this.btnEdit.Text = "Editar";
                this.btnEdit.UseVisualStyleBackColor = true;
                // 
                // btnDelete
                // 
                this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
                this.btnDelete.Location = new System.Drawing.Point(204, 405);
                this.btnDelete.Name = "btnDelete";
                this.btnDelete.Size = new System.Drawing.Size(90, 30);
                this.btnDelete.TabIndex = 3;
                this.btnDelete.Text = "Eliminar";
                this.btnDelete.UseVisualStyleBackColor = true;
                // 
                // btnImport
                // 
                this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
                this.btnImport.Location = new System.Drawing.Point(536, 405);
                this.btnImport.Name = "btnImport";
                this.btnImport.Size = new System.Drawing.Size(110, 30);
                this.btnImport.TabIndex = 4;
                this.btnImport.Text = "Importar XML";
                this.btnImport.UseVisualStyleBackColor = true;
                // 
                // btnExport
                // 
                this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
                this.btnExport.Location = new System.Drawing.Point(652, 405);
                this.btnExport.Name = "btnExport";
                this.btnExport.Size = new System.Drawing.Size(120, 30);
                this.btnExport.TabIndex = 5;
                this.btnExport.Text = "Exportar XML";
                this.btnExport.UseVisualStyleBackColor = true;
                // 
                // FormMain
                // 
                this.ClientSize = new System.Drawing.Size(784, 450);
                this.Controls.Add(this.btnExport);
                this.Controls.Add(this.btnImport);
                this.Controls.Add(this.btnDelete);
                this.Controls.Add(this.btnEdit);
                this.Controls.Add(this.btnAdd);
                this.Controls.Add(this.dataGridViewUsuarios);
                this.Name = "FormMain";
                this.Text = "Gestión Usuarios - FitData";
                ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsuarios)).EndInit();
                this.ResumeLayout(false);

            }
        }
    }


