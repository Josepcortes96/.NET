namespace FitData.Forms
{
    partial class FormCliente
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbActividad;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.DataGridView dataGridViewHorarios;
        private System.Windows.Forms.DataGridView dataGridViewReservas;
        private System.Windows.Forms.DataGridView dataGridViewLista;
        private System.Windows.Forms.Button btnMakeReservation;
        private System.Windows.Forms.Button btnCancelReservation;
        private System.Windows.Forms.Button btnEditReservation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLeaveWaiting;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbActividad = new System.Windows.Forms.ComboBox();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.dataGridViewHorarios = new System.Windows.Forms.DataGridView();
            this.dataGridViewReservas = new System.Windows.Forms.DataGridView();
            this.dataGridViewLista = new System.Windows.Forms.DataGridView();
            this.btnMakeReservation = new System.Windows.Forms.Button();
            this.btnCancelReservation = new System.Windows.Forms.Button();
            this.btnEditReservation = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLeaveWaiting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLista)).BeginInit();
            this.SuspendLayout();
            // cmbActividad
            this.cmbActividad.Location = new System.Drawing.Point(12, 12);
            this.cmbActividad.Size = new System.Drawing.Size(350, 23);
            this.cmbActividad.SelectedIndexChanged += new System.EventHandler(this.cmbActividad_SelectedIndexChanged);
            // monthCalendar
            this.monthCalendar.Location = new System.Drawing.Point(380, 12);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // dataGridViewHorarios
            this.dataGridViewHorarios.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewHorarios.Size = new System.Drawing.Size(740, 180);
            this.dataGridViewHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHorarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // dataGridViewReservas
            this.dataGridViewReservas.Location = new System.Drawing.Point(12, 250);
            this.dataGridViewReservas.Size = new System.Drawing.Size(740, 120);
            this.dataGridViewReservas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReservas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // dataGridViewLista
            this.dataGridViewLista.Location = new System.Drawing.Point(12, 380);
            this.dataGridViewLista.Size = new System.Drawing.Size(740, 100);
            this.dataGridViewLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // buttons
            this.btnMakeReservation.Location = new System.Drawing.Point(770, 50);
            this.btnMakeReservation.Size = new System.Drawing.Size(150, 30);
            this.btnMakeReservation.Text = "Reservar";
            this.btnMakeReservation.Click += new System.EventHandler(this.btnMakeReservation_Click);
            this.btnCancelReservation.Location = new System.Drawing.Point(770, 250);
            this.btnCancelReservation.Size = new System.Drawing.Size(150, 30);
            this.btnCancelReservation.Text = "Cancelar Reserva";
            this.btnCancelReservation.Click += new System.EventHandler(this.btnCancelReservation_Click);
            this.btnEditReservation.Location = new System.Drawing.Point(770, 290);
            this.btnEditReservation.Size = new System.Drawing.Size(150, 30);
            this.btnEditReservation.Text = "Editar Reserva";
            this.btnEditReservation.Click += new System.EventHandler(this.btnEditReservation_Click);
            this.btnRefresh.Location = new System.Drawing.Point(770, 12);
            this.btnRefresh.Size = new System.Drawing.Size(150, 30);
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnLeaveWaiting.Location = new System.Drawing.Point(770, 380);
            this.btnLeaveWaiting.Size = new System.Drawing.Size(150, 30);
            this.btnLeaveWaiting.Text = "Salir Lista Espera";
            this.btnLeaveWaiting.Click += new System.EventHandler(this.btnLeaveWaiting_Click);
            // FormCliente
            this.ClientSize = new System.Drawing.Size(940, 500);
            this.Controls.Add(this.cmbActividad);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.dataGridViewHorarios);
            this.Controls.Add(this.dataGridViewReservas);
            this.Controls.Add(this.dataGridViewLista);
            this.Controls.Add(this.btnMakeReservation);
            this.Controls.Add(this.btnCancelReservation);
            this.Controls.Add(this.btnEditReservation);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLeaveWaiting);
            this.Text = "Cliente - Reservas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLista)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
