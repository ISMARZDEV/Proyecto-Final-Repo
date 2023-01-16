using System;
using System.Globalization;
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
        SpeechRecognitionEngine escucha = new SpeechRecognitionEngine(new CultureInfo(Cultura()));

        //GENERADOR DE VOZ
        SpeechSynthesizer hablar = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }
        // Toma la cultura en uso del sistema y la retorna como texto
        static string Cultura()
        {
            var cultura = CultureInfo.CurrentCulture;
            string cultura_actual = Convert.ToString(cultura);
            return cultura_actual;
        }
        //Variable que indica el estado actual del asistente
        bool asistenteactivado = false;

        //INICIALIZA EL RECONOCIMIENTO DE VOZ
        private void button1_Click(object sender, EventArgs e)
        {
            if (asistenteactivado == false)
            {
                asistenteactivado = true;
                escucha.SetInputToDefaultAudioDevice();
                escucha.LoadGrammar(new DictationGrammar());
                escucha.SpeechRecognized += Deteccion;
                escucha.RecognizeAsync(RecognizeMode.Multiple);
                serialPort1.Write("Q"); //BUZZER (SONIDO)
                hablar.SpeakAsync($"Reconocimiento de voz iniciado, Hola soy 42 ¿Comó puedo ayudarte?");
            }
            else
            {
                MessageBox.Show("El asistente de voz ya está habilitado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            asistenteactivado = false;
            escucha.RecognizeAsyncStop();
        }

        //RECONOCIMIENTO DE VOZ
        private void Deteccion(object sender, SpeechRecognizedEventArgs e)
        {
            textBox1.Text = e.Result.Text; //MOSTRAR VOZ A TEXTO (TEXBOX)

            string nombre = "Mesa 9";
            string[] HolaIA = { "hola 42", "42", "o la 42", "horas 42", "ola", "o la", "hola"};
            string[] QuienEresIA = { "quién eres", "a quien eres?", "a quién eres" };
            string[] TemperaturaIA = { "temperatura", "temperatura actual", "cuál es la temperatura", "cuales temperatura", "hace calor", "grados", "hace calor fuera", "calor" };
            string[] HumedadIA = { "humedad", "humedad actual" };
            string[] LuzIA = { "42 enciende la luz", "enciende la luz" };
            string[] FiestaIA = { "42 enciende luz de fiesta", "luz de fiesta", "perenne la luzde fiesta" };
            string[] BuenosDiasIA = { "buenos dias", "buenos días", "buenos vías" };
            string[] EncenderLucesIA = { "42 enciende las luces", "enciende luces", "perenne las luces" };
            string[] ApagarLucesIA = { "42 apagar la luz", "apagar la luz", "a pagar la luz", "apagar luces", "a pagar las luces", "42 apaga las luces", "apaga luces", "a pagar luces", "apagar las luces" };
            string[] BuenasNochesIA = { "buenas noches", "descansa 42", "hasta mañana 42" };
            string[] ApagarLucesFiestaIA = { "42 apaga la luz de fiesta", "apaga la luz de fiesta", "apaga la luz de fiestas" };
            string[] EncenderRojoIA = { "42 enciende rojo", "enciende rojo", "prende rojo", "prende luz roja", "enciende luz roja" };
            string[] EncenderVerdeIA = { "42 perenne ver", "42 perenne verte", "42 perenne verde", "perenne verde", "perenne ver", "perenne verte", "enciende verde", "enciende verte", "enciende ver" };
            string[] EncenderAzulIA = { "42 enciende azul", "enciende azul", "perenne azul", "perenne luz azul", "enciende luz azul" };
            string[] ApagarRojoIA = { "42 apagar rojo", "apagar rojo", "a pagar rojo" };
            string[] ApagarVerdeIA = { "42 apagar verde", "apagar verde", "a pagar verde" };
            string[] ApagarAzulIA = { "42 apagar azul", "apagar azul", "a pagar azul" };
            string[] HoraFiestaIA = { "fiesta", "baile", "bailar", "perenne el músico", "enciende la fiestas", "a fiesta" };
            string[] MusicaTranquilaIA = { "música tranquila", "música tranquila" };
            string[] MusicaClasicaIA = { "música clásica", "clásicos", "música clásica" };
            string[] ControlVentanaIA = { "ángulo de ventana", "ángulo de ventana", "Ajustar ventanas", "ajustar de ventana" };
            //FUNCIONES BASICAS DE LA IA 42 (Saludos,Preguntas y Respuestas)------------------

            foreach (string s in HolaIA)
            { 
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Hola {nombre} ¿Comó puedo ayudarte?");
                    break;
                }
            }

            foreach (string s in QuienEresIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Hola {nombre}, soy tu asistente virtual opiripiropi 42, ¿En que puedo ayudarte?");
                    break;
                }
            }
            foreach (string s in BuenosDiasIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Buenos dias {nombre}, Luces encedidas");
                    break;
                }
            }

            //TEMPERATURA ACTUAL-------------------------------*/
            foreach (string s in TemperaturaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"La temperatura actual es de {temperature} grados centigrados");
                    break;
                }
            }
            /*HUMEDAD ACTUAL-----------------------------------*/

            foreach (string s in HumedadIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"La temperatura actual es de {temperature} grados centigrados");
                    break;
                }
            }

            /*ENCENDER LUCES ----------------------------------*/

            /*RELAY 1 LUZ NORMAL*/
            foreach (string s in LuzIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("7");
                    hablar.SpeakAsync($"Listo {nombre}, luz encendida");
                    break;
                }
            }

            /*RELAY 2 LUCES DE FIESTA*/
            foreach (string s in FiestaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Listo {nombre}, luz de fiesta encendida");
                    break;
                }
            }

            /*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA*/
            foreach (string s in EncenderLucesIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Listo {nombre}, luces encendidas");
                    break;
                }
            }

            /*APAGAR LUCES ------------------------------------ */

            /*RELAY 1 LUZ NORMAL*/
            foreach (string s in ApagarLucesIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("8");
                    hablar.SpeakAsync($"Luz apagada {nombre}");
                    break;
                }
            }
            foreach (string s in BuenasNochesIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Luces apagadas, descansa {nombre}");
                    break;
                }
            }

            /*RELAY 2 LUCES DE FIESTA*/
            foreach (string s in ApagarLucesFiestaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Luz de fiesta apagada {nombre}");
                    break;
                }
            }

            /*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA*/

            /*ENCENDER LEDS ----------------------------------*/


            foreach (string s in EncenderRojoIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("1");
                    hablar.SpeakAsync($"Led rojo encendido {nombre}");
                    break;
                }
            }

            foreach (string s in EncenderVerdeIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("2");
                    hablar.SpeakAsync($"Led verde encendido {nombre}");
                    break;
                }
            }

            foreach (string s in EncenderAzulIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("3");
                    hablar.SpeakAsync($"Led azul encendido {nombre}");
                    break;
                }
            }

            /*APAGAR LEDS ----------------------------------*/

            foreach (string s in ApagarRojoIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("4");
                    hablar.SpeakAsync($"Led rojo apagado {nombre}");
                    break;
                }
            }

            foreach (string s in ApagarVerdeIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("5");
                    hablar.SpeakAsync($"Led verde apagado {nombre}");
                    break;
                }
            }

            foreach (string s in ApagarAzulIA)
            {
                if (e.Result.Text == s)
                {
                    serialPort1.Write("6");
                    hablar.SpeakAsync($"Led azul apagado {nombre}");
                    break;
                }
            }

            /*PONE MUSICA ----------------------------------*/


            foreach (string s in HoraFiestaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Hora de la fiesta {nombre}");
                    string url = "https://www.youtube.com/watch?v=KJ5zaSPjC6w&ab_channel=DjDashPeru";
                    System.Diagnostics.Process.Start(url);
                    break;
                }
            }
            foreach (string s in MusicaTranquilaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Escucha estas canciones mientras te relajas {nombre}");
                    string url = "https://www.youtube.com/watch?v=3-4banibETY&list=PL6W2JmY9MGbV1ff53Vd0MHMpMM7aUHRnL&ab_channel=Meltt";
                    System.Diagnostics.Process.Start(url);
                    break;
                }
            }
            foreach (string s in MusicaClasicaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"Haz escuchado estos temas {nombre}?");
                    string url = "https://www.youtube.com/watch?v=fBJVaJyTMx8&list=PLTW24CNetpwQ-o_Fx5kKKOhGkoYhM0RAJ";
                    System.Diagnostics.Process.Start(url);
                    break;
                }
            }

            /*CONTROL DE VENTANAS ----------------------------------*/
            /*ANGULO DE VENTANAS ----------------------------------*/
            foreach (string s in ControlVentanaIA)
            {
                if (e.Result.Text == s)
                {
                    hablar.SpeakAsync($"{nombre}, En que angulo desea ajustar las ventanas?");
                    SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(); // Inicializar el reconocimiento de voz
                    RecognitionResult result = recognizer.Recognize(); // Capturar la entrada de voz del usuario

                    string input = result.Text; // Convertir la entrada de voz a texto

                    int Palabra;
                    int.TryParse(input, out Palabra);

                    hablar.SpeakAsync($"{nombre} Las ventanas se colocaran en un angulo de {Palabra}");
                    break;
                }
            }
        }

        //CONTROL DE DISPOSITIVOS EN EN LA GUI (Botones...)
        private bool botonPresionado = true;
        #region Boton Activar Led Rojo
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
        #endregion
        #region Boton Activar Led Verde

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
        #endregion

        #region Boton Activar Led Azul
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
        #endregion

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
