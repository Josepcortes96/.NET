using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FitData.Datos.Repositorios;
using FitData.Datos;
using FitData.Entidades;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Forms
{
    public partial class FormMain : Form
    {
        private readonly UsuarioRepository _repo;

        public FormMain()
        {
            InitializeComponent();
            var context = new FitDataContext();
            _repo = new UsuarioRepository(context);
            LoadData();

            // conectar eventos (si no lo hace el Designer)
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnImport.Click += btnImport_Click;
            btnExport.Click += btnExport_Click;
        }

        private void LoadData()
        {
            dataGridViewUsuarios.DataSource = _repo.GetAll();
            dataGridViewUsuarios.AutoResizeColumns();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new FormUsuario();
            if (f.ShowDialog() == DialogResult.OK)
            {
                _repo.Add(f.Usuario);
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow?.DataBoundItem is Usuario u)
            {
                var f = new FormUsuario(u);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _repo.Update(f.Usuario);
                    LoadData();
                }
            }
            else MessageBox.Show("Selecciona un usuario.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow?.DataBoundItem is Usuario u)
            {
                var confirm = MessageBox.Show($"Eliminar {u.Nombre} {u.Apellido}?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _repo.Delete(u.IdUsuario);
                    LoadData();
                }
            }
            else MessageBox.Show("Selecciona un usuario.");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "XML Files|*.xml", FileName = "usuarios.xml" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _repo.ExportToXml(sfd.FileName);
                MessageBox.Show("Exportado correctamente.");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "XML Files|*.xml" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _repo.ImportFromXml(ofd.FileName);
                LoadData();
                MessageBox.Show("Importado correctamente.");
            }
        }
    }
}
