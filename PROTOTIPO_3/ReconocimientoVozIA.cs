﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPO_3
{
    public class ReconocimientoVozIA
    {
        SpeechSynthesizer hablar = new SpeechSynthesizer();
        static private string CulturaActual()
        {
            var cultura = CultureInfo.CurrentCulture;
            string cultura_actual = Convert.ToString(cultura);
            return cultura_actual;
        }

        static void InicializarIA(TextBox texto, SerialPort serial)
        {
            SpeechRecognitionEngine escucha = new SpeechRecognitionEngine(new System.Globalization.CultureInfo(CulturaActual()));
            escucha.SetInputToDefaultAudioDevice();
            escucha.LoadGrammar(new DictationGrammar());
            //escucha.SpeechRecognized += Deteccion(texto, serial);
            escucha.RecognizeAsync(RecognizeMode.Multiple);
        }

        /*private void Deteccion(TextBox texto, SerialPort serial, SpeechRecognitionRejectedEventArgs e)
        {
            texto.Text = e.Result.Text; //MOSTRAR VOZ A TEXTO (TEXBOX)

            string nombre = "Mesa 9";

            //FUNCIONES BASICAS DE LA IA 42 (Saludos,Preguntas y Respuestas)------------------

            if (e.Result.Text == "hola 42" || e.Result.Text == "42" || e.Result.Text == "o la 42" || e.Result.Text == "horas 42" || e.Result.Text == "ola" || e.Result.Text == "o la" || e.Result.Text == "hola")
            {
                hablar.SpeakAsync($"Hola {nombre} ¿Comó puedo ayudarte?");
            }

            if (e.Result.Text == "quién eres" || e.Result.Text == "a quien eres?" || e.Result.Text == "a quién eres")
            {
                hablar.SpeakAsync($"Hola {nombre}, soy tu asistente virtual opiripiropi 42, ¿En que puedo ayudarte?");
            }

            //TEMPERATURA ACTUAL-------------------------------/

            if (e.Result.Text == "temperatura" || e.Result.Text == "temperatura actual" || e.Result.Text == "cuál es la temperatura" || e.Result.Text == "Cuales temperatura" || e.Result.Text == "Hace calor" || e.Result.Text == "Grados" || e.Result.Text == "grados" || e.Result.Text == "Hace calor fuera" || e.Result.Text == "calor")
            {
                hablar.SpeakAsync($"La temperatura actual es de {temperature} grados centigrados");
            }
            //*HUMEDAD ACTUAL-----------------------------------/

            if (e.Result.Text == "humedad" || e.Result.Text == "humedad actual")
            {
                hablar.SpeakAsync($"El porcentaje actual de humedad es de {humidity}");
            }

            //*ENCENDER LUCES ----------------------------------/

            //*RELAY 1 LUZ NORMAL/

            if (e.Result.Text == "42 enciende la luz" || e.Result.Text == "enciende la luz")
            {
                serialPort1.Write("7");
                hablar.SpeakAsync($"Listo {nombre}, luz encendida");

            }

            if (e.Result.Text == "Buenos dias" || e.Result.Text == "Buenos días" || e.Result.Text == "Buenos vías")
            {
                hablar.SpeakAsync($"Buenos dias {nombre}, Luces encedidas");

            }

            //*RELAY 2 LUCES DE FIESTA/

            if (e.Result.Text == "42 enciende luz de fiesta" || e.Result.Text == "luz de fiesta")
            {
                hablar.SpeakAsync($"Listo {nombre}, luz de fiesta encendida");
            }

            //*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA/

            if (e.Result.Text == "42 enciende las luces" || e.Result.Text == "enciende luces" || e.Result.Text == "Prende las luces")
            {
                hablar.SpeakAsync($"Listo {nombre}, luces encendidas");
            }

            //*APAGAR LUCES ------------------------------------ /

            //*RELAY 1 LUZ NORMAL/

            if (e.Result.Text == "42 apagar la luz" || e.Result.Text == "apagar la luz" || e.Result.Text == "apagar luces" || e.Result.Text == "Apaga las luces")
            {
                serialPort1.Write("8");
                hablar.SpeakAsync($"Luz apagada {nombre}");
            }

            if (e.Result.Text == "Buenas noches" || e.Result.Text == "Buenas noches" || e.Result.Text == "buenas noches")
            {
                hablar.SpeakAsync($"Luces apagadas, descansa {nombre}");
            }


            //RELAY 2 LUCES DE FIESTA/

            if (e.Result.Text == "42 apaga la luz de fiesta" || e.Result.Text == "apaga la luz de fiesta" || e.Result.Text == "apaga la luz de fiestas")
            {
                hablar.SpeakAsync($"Luz de fiesta apagada {nombre}");
            }

            //*RELAY 1 LUZ NORMAL y RELAY 2 LUCES DE FIESTA/

            if (e.Result.Text == "42 apaga las luces" || e.Result.Text == "apaga luces" || e.Result.Text == "a pagar luces" || e.Result.Text == "apagar las luces")
            {
                hablar.SpeakAsync($"Luces apagas {nombre}");
            }

            //*ENCENDER LEDS ----------------------------------

            if (e.Result.Text == "42 enciende rojo" || e.Result.Text == "enciende rojo" || e.Result.Text == "prende rojo" || e.Result.Text == "prende luz roja" || e.Result.Text == "enciende luz roja")
            {
                serialPort1.Write("1");
                hablar.SpeakAsync($"Led rojo encendido {nombre}");
            }

            if (e.Result.Text == "42 enciende verde" || e.Result.Text == "enciende verde" || e.Result.Text == "prende verde" || e.Result.Text == "prende luz verde" || e.Result.Text == "enciende luz verde")
            {
                serialPort1.Write("2");
                hablar.SpeakAsync($"Led verde encendido {nombre}");
            }

            if (e.Result.Text == "42 enciende azul" || e.Result.Text == "enciende azul" || e.Result.Text == "prende azul" || e.Result.Text == "prende luz azul" || e.Result.Text == "enciende luz azul")
            {
                serialPort1.Write("3");
                hablar.SpeakAsync($"Led azul encendido {nombre}");
            }

            //APAGAR LEDS ----------------------------------/

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

            //PONE MUSICA ----------------------------------//

            if (e.Result.Text == "fiesta" || e.Result.Text == "baile" || e.Result.Text == "bailar" || e.Result.Text == "Prende el musicon" || e.Result.Text == "Enciende la fiestas" || e.Result.Text == "a fiesta")
            {
                hablar.SpeakAsync($"Hora de la fiesta {nombre}");
                string url = "https://www.youtube.com/watch?v=KJ5zaSPjC6w&ab_channel=DjDashPeru";
                System.Diagnostics.Process.Start(url);
            }

            if (e.Result.Text == "Musica tranquila" || e.Result.Text == "Musica tranquila")
            {
                hablar.SpeakAsync($"Escucha estas canciones mientras te relajas {nombre}");
                string url = "https://www.youtube.com/watch?v=3-4banibETY&list=PL6W2JmY9MGbV1ff53Vd0MHMpMM7aUHRnL&ab_channel=Meltt";
                System.Diagnostics.Process.Start(url);
            }
            if (e.Result.Text == "Musica clasica" || e.Result.Text == "Clasicos" || e.Result.Text == "Música clásica")
            {
                hablar.SpeakAsync($"Haz escuchado estos temas {nombre}?");
                string url = "https://www.youtube.com/watch?v=fBJVaJyTMx8&list=PLTW24CNetpwQ-o_Fx5kKKOhGkoYhM0RAJ";
                System.Diagnostics.Process.Start(url);
            }
        }*/
    }


}
