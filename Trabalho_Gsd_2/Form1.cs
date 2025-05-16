using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Trabalho_Gsd_2
{
    public partial class Form1 : Form
    {
        private bool isTesting = false;
        private readonly string[] sites = { "google.com", "amazon.com", "stackoverflow.com", "microsoft.com" };
        private Timer testTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            testTimer = new Timer();
            testTimer.Interval = 10000; // 10 segundos
            testTimer.Tick += TestTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Aguardando teste...";
            lab1.Text = ""; // Inicializa como vazio
            lab2.Text = ""; // Inicializa como vazio
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (!isTesting)
            {
                isTesting = true;
                btnOn.Text = "Parar Teste";
                label1.Text = "Testando...";
                testTimer.Start(); // Inicia o timer
                TestPing();        // Executa o primeiro teste imediatamente
            }
            else
            {
                isTesting = false;
                btnOn.Text = "Iniciar Teste";
                label1.Text = "Teste parado";
                testTimer.Stop(); // Para o timer
            }
        }

        private void TestTimer_Tick(object sender, EventArgs e)
        {
            TestPing();
        }

        private void TestPing()
        {
            // Inicializa os textos dos labels para cada novo teste
            lab1.Text = "Testando Sites...";
            lab2.Text = ""; // Limpa os resultados anteriores

            bool anySuccess = false;
            string siteStatus = "";

            // Verifica a conexão com cada site na lista
            foreach (string site in sites)
            {
                string statusMessage = $"{site}: ";
                using (Ping ping = new Ping())
                {
                    try
                    {
                        PingReply reply = ping.Send(site);
                        if (reply.Status == IPStatus.Success)
                        {
                            statusMessage += "Conexão Estável";
                            anySuccess = true;
                        }
                        else
                        {
                            statusMessage += "Falha na Conexão";
                        }
                    }
                    catch
                    {
                        statusMessage += "Erro de Conexão";
                    }

                    // Adiciona o status do site ao label lab2
                    lab2.Text += statusMessage + Environment.NewLine;
                }
            }

            // Atualiza o label principal com o resultado geral
            if (anySuccess)
            {
                label1.Text = "Conectado com sucesso";
            }
            else
            {
                label1.Text = "Falha de conexão";
            }
        }
    }
}
