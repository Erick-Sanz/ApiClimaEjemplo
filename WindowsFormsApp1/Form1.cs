using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        HttpClient ClienteHttp = new HttpClient();
        Weather Cl = new Weather("https://www.metaweather.com/");
        private void Clear()
        {
            listBox1.Items.Clear(); 
            listBox2.Items.Clear(); 
            listBox3.Items.Clear(); 
            listBox4.Items.Clear();
            listBox5.Items.Clear();
        }
        private void Llena(List<Country> Lista=null)
        {
            if (Lista != null)
            {
                foreach (var g in Lista)
                {
                    listBox1.Items.Add(g.title);
                    listBox2.Items.Add(g.location_type);
                    listBox3.Items.Add(g.latt_long);
                    listBox4.Items.Add(g.woeid);
                    listBox5.Items.Add(g.distance);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            List<Country> L = Cl.LocationSearch(true, textBox1.Text);
            if (L != null)
            {
                Llena(L);
                textBox3.Text = L[0].woeid.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label10.Text = "Fecha Pronostico:"; 
            label11.Text = "Humedad %";
            label12.Text = "Descripcion clima:";
            label13.Text = "Abreviatura \n del estado";
            label14.Text = "Direccion del\n viento grados:";
            label15.Text = "Temperatura \n actual:";
            label16.Text = "Temperatura \n minima:";
            label17.Text = "Temperatura \n maxima:";
            label18.Text = "Visibilidad millas";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
            Llena(Cl.LocationSearch(false,textBox2.Text));
        }
        private void Image(Country C)
        {           
            pictureBox1.ImageLocation = Cl.Images(C.consolidated_weather[0].weather_state_abbr);
            pictureBox2.ImageLocation = Cl.Images(C.consolidated_weather[1].weather_state_abbr);
            pictureBox3.ImageLocation = Cl.Images(C.consolidated_weather[2].weather_state_abbr);
            pictureBox4.ImageLocation = Cl.Images(C.consolidated_weather[3].weather_state_abbr);
            pictureBox5.ImageLocation = Cl.Images(C.consolidated_weather[4].weather_state_abbr);
            pictureBox6.ImageLocation = Cl.Images(C.consolidated_weather[5].weather_state_abbr);           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Country C = Cl.WeatherLocation(textBox3.Text);
            foreach(var P in C.consolidated_weather)
            {
                listBox6.Items.Add(P.applicable_date);
                listBox6.Items.Add("");
                listBox6.Items.Add("____________");
                listBox7.Items.Add(P.humidity);
                listBox7.Items.Add("");
                listBox7.Items.Add("____________");
                listBox8.Items.Add(P.weather_state_name);
                listBox8.Items.Add("");
                listBox8.Items.Add("____________");
                listBox9.Items.Add(P.weather_state_abbr);
                listBox9.Items.Add("");
                listBox9.Items.Add("____________");
                listBox10.Items.Add(Math.Round(P.wind_direction,2));
                listBox10.Items.Add("");
                listBox10.Items.Add("____________");
                listBox11.Items.Add($"{Math.Round(P.the_temp,2)} C°");
                listBox11.Items.Add("");
                listBox11.Items.Add("____________");
                listBox12.Items.Add($"{Math.Round(P.min_temp,2)} C°");
                listBox12.Items.Add("");
                listBox12.Items.Add("____________");
                listBox13.Items.Add($"{Math.Round(P.max_temp,2)} C°");
                listBox13.Items.Add("");
                listBox13.Items.Add("____________");
                listBox14.Items.Add(Math.Round(P.visibility, 2));
                listBox14.Items.Add("");
                listBox14.Items.Add("____________");
            }
            Image(C);
        }
    }
}
