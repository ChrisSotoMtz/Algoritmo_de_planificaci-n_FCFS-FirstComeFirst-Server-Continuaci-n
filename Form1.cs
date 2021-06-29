using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad_6
{
    public partial class Form1 : Form
    {
        bool start = false;
        bool pause = false;
        bool bcpActive = false;
        int contadorGeneral = 0;
        int contProceso = 0;
        int idcont = 1;

        Proceso ProcesoActual = null;
        Proceso ProcesoAux = null;
        Proceso Procesoaux2 = null;
        Proceso Procesoaux3 = null;
        Proceso ProcesoBloqAux = null;
        static Random rnd = new Random();
        List<string> Operando = new List<string>();
        public ArrayList ProcesosNuevos = new ArrayList();
        public ArrayList ProcesosBloqueados = new ArrayList();
        public ArrayList ProcesosMemoria = new ArrayList();
        public ArrayList procesosTerminados = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Operando.Add("+");
            Operando.Add("/");
            Operando.Add("X");
            Operando.Add("-");
            Operando.Add("%");
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        #region Funciones
        public void CrearProceso(int cantidadProcesos)
        {
            
            for (int i = 0; i < System.Convert.ToInt32(cantidadProcesos); i++)
            {

                //Separamos los lotes en 5
                int r = rnd.Next(Operando.Count);
                int firstNumber = rnd.Next(1, 1000);
                int secondNumber = rnd.Next(1, 1000);
                int tiempo = rnd.Next(6, 15);

                Proceso addprocces = new Proceso();
                addprocces.id = idcont.ToString();
                idcont++;
                addprocces.tdo = Operando[r].ToString();
                addprocces.numero1 = firstNumber;
                addprocces.numero2 = secondNumber;
                addprocces.tiempoRestante = tiempo;
                addprocces.tiempoEstimado = tiempo;
                addprocces.blocked = false;
                addprocces.calculated = false;
                addprocces.tiempoBloqueo = 0;
                addprocces.operacion = firstNumber.ToString() + " " + Operando[r] + " " + secondNumber.ToString();
                switch (Operando[r])
                {
                    case "+":
                        addprocces.resultado = (firstNumber + secondNumber).ToString();
                        break;
                    case "-":
                        addprocces.resultado = (firstNumber - secondNumber).ToString();
                        break;
                    case "/":
                        addprocces.resultado = (firstNumber / secondNumber).ToString();
                        break;
                    case "X":
                        addprocces.resultado = (firstNumber * secondNumber).ToString();
                        break;
                    case "%":
                        addprocces.resultado = (firstNumber % secondNumber).ToString();
                        break;
                }
                if (ProcesosMemoria.Count < 5)
                {
                    //addprocces.tiempoLlegada = 0;
                    ProcesosMemoria.Add(addprocces);
                }
                else
                    ProcesosNuevos.Add(addprocces);


            }
        }



        #endregion

        #region Listados
        public void ListarMemoria(ListView listView, ArrayList array)
        {
            listView.Items.Clear();
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.operacion.ToString());
                fila.SubItems.Add(pros.tiempoRestante.ToString());
                fila.SubItems.Add(pros.tiempoEstimado.ToString());
                listView.Items.Add(fila);
            }
        }


        public void ListarBloqueados(ListView listView, ArrayList array)
        {
            listView.Items.Clear();
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.tiempoBloqueo.ToString());
                listView.Items.Add(fila);
            }

        }

        public void ListarTerminados(ListView listView, ArrayList array)
        {
            listView.Items.Clear();
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.operacion);
                fila.SubItems.Add(pros.resultado);
                listView.Items.Add(fila);
            }

        }
        public void ListarMemoriaBCP(ListView listView, ArrayList array)
        {
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.operacion);
                fila.SubItems.Add(pros.tiempoLlegada.ToString());
                fila.SubItems.Add(pros.tiempoRespuesta.ToString());
                fila.SubItems.Add(pros.tiempoEspera.ToString());
                fila.SubItems.Add(pros.tiempoServicio.ToString());
                listView.Items.Add(fila);
            }
        }

        public void ListarBloqueadosBCP(ListView listView, ArrayList array)
        {
            listView.Items.Clear();
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.operacion);
                fila.SubItems.Add(pros.tiempoLlegada.ToString());
                fila.SubItems.Add(pros.tiempoRespuesta.ToString());
                fila.SubItems.Add(pros.tiempoEspera.ToString());
                fila.SubItems.Add(pros.tiempoServicio.ToString());
                fila.SubItems.Add(pros.tiempoBloqueo.ToString());
                listView.Items.Add(fila);
            }
        }
        public void ListarTerminadosBCP(ListView listView, ArrayList array)
        {
            listView.Items.Clear();
            foreach (Proceso pros in array)
            {
                ListViewItem fila = new ListViewItem(pros.id.ToString());
                fila.SubItems.Add(pros.operacion);
                fila.SubItems.Add(pros.resultado);
                fila.SubItems.Add(pros.tiempoLlegada.ToString());
                fila.SubItems.Add(pros.tiempoFinalizacion.ToString());
                fila.SubItems.Add(pros.tiempoRetorno.ToString());
                fila.SubItems.Add(pros.tiempoRespuesta.ToString());
                fila.SubItems.Add(pros.tiempoEspera.ToString());
                fila.SubItems.Add(pros.tiempoServicio.ToString());
                listView.Items.Add(fila);
            }
           
        }
        #endregion

        #region botones


        private void buttonStart_Click(object sender, EventArgs e)
        {
            start = true;
            if (ProcesosNuevos.Count >= 1 || ProcesosMemoria.Count >= 0)
            {
  
                buttonStart.Enabled = false;

                timer1.Enabled = true;
            }
            else
                MessageBox.Show("No hay procesos en cola", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            nuevos_tb.Text = ProcesosNuevos.Count.ToString();
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        #endregion

        #region botonesVentana
        private void botonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void botonMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        #endregion





        #region timer
        private void timer1_Tick(object sender, EventArgs e)
        {

            nuevos_tb.Text = ProcesosNuevos.Count.ToString();
            contadorGeneral++;
            contProceso++;
            ListarMemoria(tablaMemoria, ProcesosMemoria);
            ListarBloqueados(tablaBloqueados, ProcesosBloqueados);
            if (ProcesosMemoria.Count <= 0 && ProcesosNuevos.Count <= 0 && ProcesosBloqueados.Count<=0)
            {
                timer1.Stop();
                tbTiempo.Text = "";
                tbID.Text = "";
                tbOp.Text = "";
               contadorGeneral--;
            }
            //Asigna el proceso actual 
            else
            {
                if (ProcesosMemoria.Count > 0)
                {
                    ProcesoActual = (Proceso)ProcesosMemoria[0];
                    tbTiempo.Text = (ProcesoActual.tiempoRestante - contProceso).ToString();
                    ProcesoActual.tiempoServicio++;
                    tbID.Text = ProcesoActual.id;
                    tbOp.Text = ProcesoActual.operacion;

                }

                for (int i = 0; i < ProcesosMemoria.Count; i++)
                {
                    Procesoaux3 = (Proceso)ProcesosMemoria[i];
                    if (Procesoaux3.id != tbID.Text && Procesoaux3.blocked == false)
                    {
                        Procesoaux3.tiempoRespuesta++;
                    }


                }
                if (tbID.Text == ProcesoActual.id && tablaMemoria.Items.Count > 0)
                    tablaMemoria.Items.RemoveAt(0);
            }
            //Elimina los procesos completados

                if (contProceso == ProcesoActual.tiempoRestante)
                {
                    if (procesosTerminados.Count <= 0)
                        ProcesoActual.tiempoFinalizacion = System.Convert.ToInt32(tiempoTranscurrido.Text) + 1;
                    else
                        ProcesoActual.tiempoFinalizacion = System.Convert.ToInt32(tiempoTranscurrido.Text);

                    ProcesoActual.tiempoRetorno = ProcesoActual.tiempoFinalizacion - ProcesoActual.tiempoLlegada;
                    ProcesoActual.tiempoEspera = ProcesoActual.tiempoRetorno - ProcesoActual.tiempoServicio;
                    procesosTerminados.Add(ProcesoActual);
                    ListarTerminados(tablaTerminados, procesosTerminados);
                
                    if (ProcesosMemoria.Count >= 0 && ProcesosNuevos.Count >= 0)
                    {
                        ProcesosMemoria.RemoveAt(0);
                        if (ProcesosNuevos.Count > 0 && ProcesosMemoria.Count + ProcesosBloqueados.Count <= 4)
                        {

                            Procesoaux2 = (Proceso)ProcesosNuevos[0];
                            Procesoaux2.tiempoLlegada = System.Convert.ToInt32(tiempoTranscurrido.Text);
                            ProcesosMemoria.Add(Procesoaux2);
                            ProcesosNuevos.RemoveAt(0);

                        }
                        tbTiempo.Text = "";
                        tbID.Text = "";
                        tbOp.Text = "";

                    }
                    contProceso = 0;
                }

            if(ProcesosBloqueados.Count == 5) 
            {
                tbTiempo.Text = "NULL";
                tbID.Text ="NULL";
                tbOp.Text = "NULL";

            }
            tiempoTranscurrido.Text = contadorGeneral.ToString();
        }

        #endregion

        #region keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.P && start == true && pause == false)
            {
                timer1.Stop();
                if(ProcesosBloqueados.Count>0)
                    bloqueo.Stop();
                estado_Tb.Text = "Pause";
                pause = true;

            }

            if (e.KeyData == Keys.C && start == true &&( pause == true || bcpActive == true))
            {
                if (ProcesosBloqueados.Count > 0)
                    bloqueo.Start();
                timer1.Start();
                estado_Tb.Text = "On going";
                pause = false;
                bcpActive = false;
                tabControl1.SelectedTab = tabPage1;

            }

            if (e.KeyData == Keys.I && start == true && pause == false && ProcesosMemoria.Count > 0)
            {
                contProceso = 0;
                ProcesoBloqAux = (Proceso)ProcesosMemoria[0];
                ProcesoBloqAux.tiempoRestante = System.Convert.ToInt32(tbTiempo.Text);
                ProcesosBloqueados.Add(ProcesoBloqAux);
                ProcesosMemoria.RemoveAt(0);
                bloqueo.Enabled = true;
                

            }

            if(e.KeyData == Keys.E)
            {
                if (ProcesosMemoria.Count > 0)
                {
                    ProcesoActual.resultado = "ERROR";
                    ProcesoActual.tiempoFinalizacion = System.Convert.ToInt32(tiempoTranscurrido.Text);
                    ProcesoActual.tiempoRetorno = ProcesoActual.tiempoFinalizacion - ProcesoActual.tiempoLlegada;
                    ProcesoActual.tiempoEspera = ProcesoActual.tiempoRetorno - ProcesoActual.tiempoServicio;
                    

                    procesosTerminados.Add(ProcesoActual);
                    ListarTerminados(tablaTerminados, procesosTerminados);
                    ProcesosMemoria.RemoveAt(0);
                    contProceso = 0;

                    if(ProcesosNuevos.Count >0 && ProcesosMemoria.Count < 5)
                    {
                        ProcesoAux = null;
                        ProcesoAux = (Proceso)ProcesosNuevos[0];
                        ProcesoAux.tiempoLlegada = System.Convert.ToInt32(tiempoTranscurrido.Text);
                        ProcesosMemoria.Add(ProcesosNuevos[0]);
                        ProcesosNuevos.RemoveAt(0);
                    }
                }
            }

            if(e.KeyData == Keys.N && pause == false) 
            {
                CrearProceso(1);
                ListarMemoria(tablaMemoria, ProcesosMemoria);
                nuevos_tb.Text = ProcesosNuevos.Count.ToString();
            }

            if(e.KeyData == Keys.B && bcpActive ==false )
            {
                bcpActive = true;
                tabControl1.SelectedTab = tabPage2;
                timer1.Stop();
                ListarTerminadosBCP(bcp_terminados, procesosTerminados);
                ListarBloqueadosBCP(bcp_bloqueados, ProcesosBloqueados);
                ListarMemoriaBCP(bcp_memoria, ProcesosMemoria);
            }

        }

        #endregion

        private void bloqueo_Tick(object sender, EventArgs e)
        {
            for(int i =0; i<ProcesosBloqueados.Count; i++) 
            {
                ProcesoAux = (Proceso)ProcesosBloqueados[i];
                ProcesoAux.tiempoBloqueo++;
                if(ProcesoAux.tiempoBloqueo == 5)
                {
                    ProcesoAux.tiempoBloqueo = 0;
                    ProcesoAux.blocked = true;
                    ProcesosMemoria.Add(ProcesoAux);
                    ProcesosBloqueados.Remove(ProcesoAux);

                }
            
            }

            if (ProcesosBloqueados.Count == 0)
                bloqueo.Enabled = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}