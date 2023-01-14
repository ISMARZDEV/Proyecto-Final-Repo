using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition; //Contiene tipos de tecnología de voz de escritorio de Windows para implementar el reconocimiento de voz.
using System.Speech.Synthesis; //Contiene clases para inicializar y configurar un motor de síntesis de voz para generar voz.
using System.IO.Ports; //Contiene clases para controlar puertos serie (Serial Port)


namespace PROTOTIPO_3
{
    public partial class Form1 : Form
    {
        //SENSOR DE TEMPERATURA
        double temperature = 0, humidity = 0;
        bool updateData = false;

        //RECONOCIMIENTO DE VOZ
        SpeechRecognitionEngine escucha = new SpeechRecognitionEngine();

        //GENERADOR DE VOZ
        SpeechSynthesizer hablar = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        //INICIALIZA EL RECONOCIMIENTO DE VOZ
        private void button1_Click(object sender, EventArgs e)
        {
            escucha.SetInputToDefaultAudioDevice();
            escucha.LoadGrammar(new DictationGrammar());
            escucha.SpeechRecognized += Deteccion;
            escucha.RecognizeAsync(RecognizeMode.Multiple);
            serialPort1.Write("Q"); //BUZZER (SONIDO)
            hablar.SpeakAsync($"Reconocimiento de voz iniciado, Hola soy 42 ¿Comó puedo ayudarte?");
            // Cambio ismael
        }

                private void button2_Click(object sender, EventArgs e)
        {
            escucha.RecognizeAsyncStop();
        }

        //RECONOCIMIENTO DE VOZ
        private void Deteccion(object sender, SpeechRecognizedEventArgs e)
        {
            textBox1.Text = e.Result.Text; //MOSTRAR VOZ A TEXTO (TEXBOX)

            string nombre = "Mesa 9";

            /*FUNCIONES BASICAS DE LA IA 42 (Saludos,Preguntas y Respuestas)------------------*/

            if (e.Result.Text == "hola 42" || e.Result.Text == "42")
            {   
                hablar.SpeakAsync($"Hola {nombre} ¿Comó puedo ayudarte?");
            }

            if (e.Result.Text == "quién eres" || e.Result.Text == "hola")
            {
                hablar.SpeakAsync($"Hola {nombre}, soy tu asistente virtual opiripiropi 42, ¿En que puedo ayudarte?");
            }

            /*TEMPERATURA ACTUAL-------------------------------*/

            if (e.Result.Text == "temperatura" || e.Result.Text == "temperatura actual")
            {
                hablar.SpeakAsync($"La temperatura actual es de {temperature} grados centigrados");
            }
            /*HUMEDAD ACTUAL-----------------------------------*/

            if (e.Result.Text == "humedad" || e.Result.Text == "humedad actual")
            {
                hablar.SpeakAsync($"El porcentaje actual de humedad es de {humidity}");
            }

            /*ENCENDER LUCES ----------------------------------*/

            /*RELAY 1 LUZ NORMAL*/

            if (e.Result.Text == "42 enciende la luz" || e.Result.Text == "enciende la luz")
            {
                serialPort1.Write("7");
                hablar.SpeakAsync($"Listo {nombre}, luz encendida");
            }

            /*RELAY 2 LUCES DE FIESTA*/

            if (e.Result.Text == "42 enciende luz de fiesta" || e.Result.Text == "luz de fiesta")
            {
                hablar.SpeakAsync($"Listo {nombre}, luz de fiesta encendida");
            }

            /*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA*/

            if (e.Result.Text == "42 enciende las luces" || e.Result.Text == "enciende luces")
            {
                hablar.SpeakAsync($"Listo {nombre}, luces encendidas");
            }

            /*APAGAR LUCES ------------------------------------ */

            /*RELAY 1 LUZ NORMAL*/

            if (e.Result.Text == "42 apagar la luz" || e.Result.Text == "apagar la luz")
            {
                serialPort1.Write("8");
                hablar.SpeakAsync($"Luz apagada {nombre}");
            }

            /*RELAY 2 LUCES DE FIESTA*/

            if (e.Result.Text == "42 apaga la luz de fiesta" || e.Result.Text == "apaga la luz de fiesta")
            {
                hablar.SpeakAsync($"Luz de fiesta apagada {nombre}");
            }

            /*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA*/

            if (e.Result.Text == "42 apaga las luces" || e.Result.Text == "apaga luces")
            {
                hablar.SpeakAsync($"Luces apagas {nombre}");
            }

            /*ENCENDER LEDS ----------------------------------*/

            if (e.Result.Text == "42 enciende rojo" || e.Result.Text == "enciende rojo")
            {
                serialPort1.Write("1");
                hablar.SpeakAsync($"Led rojo encendido {nombre}");
            }

            if (e.Result.Text == "42 enciende verde" || e.Result.Text == "enciende verde")
            {
                serialPort1.Write("2");
                hablar.SpeakAsync($"Led verde encendido {nombre}");
            }

            if (e.Result.Text == "42 enciende azul" || e.Result.Text == "enciende azul")
            {
                serialPort1.Write("3");
                hablar.SpeakAsync($"Led azul encendido {nombre}");
            }

            /*APAGAR LEDS ----------------------------------*/

            if (e.Result.Text == "42 apagar rojo" || e.Result.Text == "apagar rojo" || e.Result.Text == "a pagar rojo")
            {
                serialPort1.Write("4");
                hablar.SpeakAsync($"Led rojo apagado {nombre}");
            }

            if (e.Result.Text == "42 apagar verde" || e.Result.Text == "apagar verde" || e.Result.Text == "a pagar verde")
            {
                serialPort1.Write("5");
                hablar.SpeakAsync($"Led verde apagado {nombre}");
            }

            if (e.Result.Text == "42 apagar azul" || e.Result.Text == "apagar azul" || e.Result.Text == "a pagar azul")
            {
                serialPort1.Write("6");
                hablar.SpeakAsync($"Led azul apagado {nombre}");
            }

            if (e.Result.Text == "fiesta" || e.Result.Text == "baile")
            {
                hablar.SpeakAsync($"Hora de la fiesta {nombre}");
                string url = "https://www.youtube.com/watch?v=ApXoWvfEYVU&ab_channel=PostMaloneVEVO";
                System.Diagnostics.Process.Start(url);
            }
        }


        //CONTROL DE DISPOSITIVOS EN EN LA GUI (Botones...)
        private bool botonPresionado = true;

        private void button5_Click(object sender, EventArgs e)
        {
            if (botonPresionado)
            {
                // Segunda función
                serialPort1.Write("1");
                hablar.SpeakAsync($"Led rojo encendido");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = false;
            }
            else
            {
                // Primera función
                serialPort1.Write("4");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (botonPresionado)
            {
                // Segunda función
                serialPort1.Write("2");
                hablar.SpeakAsync($"Led verde encendido");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = false;
            }
            else
            {
                // Primera función
                serialPort1.Write("5");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (botonPresionado)
            {
                // Segunda función
                serialPort1.Write("3");
                hablar.SpeakAsync($"Led azul encendido");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = false;
            }
            else
            {
                // Primera función
                serialPort1.Write("6");
                // Cambiamos el estado del botón para la próxima vez que sea presionado
                botonPresionado = true;
            }
        }
         
        //

        private void Form1_Load(object sender, EventArgs e)
        {
            button_open.Enabled = true;
            button_close.Enabled = false;
            //SE DEFINEN LAS 
            chart1.Series["TEMP"].Points.AddXY(1, 1);
            chart1.Series["HUM"].Points.AddXY(1, 1);

        }
        //OBTIENE LOS PUERTOS DISPONIBLES DEL CUADRO COMBINADO Y POSTERIORMENTE LOS MUESTRA EN EL COMBOBOX
        private void comboBox_portList_DropDown(object sender, EventArgs e)
        {
            string[] portLists = SerialPort.GetPortNames();
            comboBox_portList.Items.Clear();
            comboBox_portList.Items.AddRange(portLists);
        }

        //INICIALIZAR EL PROGRAMA Y CONECTAR CON EL SERIAL PORT (BUTTON 3 - CONECTAR)
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //ME PERMITE SELECCIONAR UNO DE LOS PUERTOS COM DISPONIBLES Y ESTABLECER MI CONXIÓN CON EL SERIAL PORT
                serialPort1.PortName = comboBox_portList.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox_baudRated.Text);
                serialPort1.Open();

                button_open.Enabled = false;
                button_close.Enabled = true;

                //LIMPIAR ESTADO ACTUAL DE MEDIDA DE LOS SENSORES
                chart1.Series["TEMP"].Points.Clear();
                chart1.Series["HUM"].Points.Clear();
                hablar.SpeakAsync($"Bienvenido mesa 9");
                //MENSAJE EMERGENTE DE CONEXIÓN ESTABLECIDA
                MessageBox.Show("CONECTADO");
            }

            catch(Exception error)
            {   
                //MENSAJE EMERGENTE DE ERROR
                MessageBox.Show(error.Message);
            }
        }

        //DESCONECTAR EL PROGRAMA DEL SERIAL PORT (BUTTON 4 - ACTUALIZAR PUERTOS)
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();

                button_open.Enabled = true;
                button_close.Enabled = false;

                //LIMPIAR ESTADO ACTUAL DE MEDIDA DE LOS SENSORES
                chart1.Series["TEMP"].Points.Clear();
                chart1.Series["HUM"].Points.Clear();
                hablar.SpeakAsync($"Adiós mesa 9");
                //MENSAJE EMERGENTE DE DESCONEXIÓN
                MessageBox.Show("DESCONECTADO");
            }

            catch (Exception error)
            {
                //MENSAJE EMERGENTE DE ERROR
                MessageBox.Show(error.Message);
            }
        }


        //RECIBIR DATA DEL SERIAL PORT
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //SALTO DE LINEA PARA RECIBIR LOS DATOS DE FORMA ORDENADA
            string dataIn = serialPort1.ReadTo("\n");
            Data_Parsing(dataIn);
            //Ejecución asíncrona del método Show_data en la instancia actual
            this.BeginInvoke(new EventHandler(Show_data));
        }

        private void Show_data(object sender, EventArgs e)
        {
            if (updateData == true)
            {   

                label_temperature.Text = string.Format("{0}°C", temperature.ToString());
                label_humidity.Text = string.Format("{0}%", humidity.ToString());

                chart1.Series["TEMP"].Points.Add(temperature);
                chart1.Series["HUM"].Points.Add(humidity);
            }
        }


        private void Data_Parsing(string data)
        {
            sbyte indexOf_startDataCharacter = (sbyte)data.IndexOf("@");
            sbyte indexOfA = (sbyte)data.IndexOf("A");
            sbyte indexOfB = (sbyte)data.IndexOf("B");

            if (indexOfA != -1 && indexOfB != -1 && indexOf_startDataCharacter != -1)
            {
                try
                {
                    string str_temperature = data.Substring(indexOf_startDataCharacter + 1, (indexOfA - indexOf_startDataCharacter) - 1);
                    string str_humidity = data.Substring(indexOfA + 1, (indexOfB - indexOfA) - 1);

                    temperature = Convert.ToDouble(str_temperature);
                    humidity = Convert.ToDouble(str_humidity);

                    updateData = true;

                }

                catch
                {

                }
            }
            else
            {
                updateData = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serialPort1.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

    }
}
