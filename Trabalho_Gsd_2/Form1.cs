using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Trabalho_Gsd_2
{
    public partial class Form1 : Form
    {
        private bool isTesting = false;
        private readonly string[] sites = { "google.com", "amazon.com", "stackoverflow.com", "microsoft.com" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Aguardando teste...";
            progressBar1.Value = 0;
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (!isTesting)
            {
                isTesting = true;
                btnOn.Text = "Parar Teste";
                label1.Text = "Testando...";
                progressBar1.Style = ProgressBarStyle.Marquee;
                TestPing();
            }
            else
            {
                isTesting = false;
                btnOn.Text = "Iniciar Teste";
                label1.Text = "Teste parado";
                progressBar1.Style = ProgressBarStyle.Blocks;
            }
        }

        private void TestPing()
        {
            foreach (string site in sites)
            {
                using (Ping ping = new Ping())
                {
                    try
                    {
                        PingReply reply = ping.Send(site);
                        if (reply.Status == IPStatus.Success)
                        {
                            MessageBox.Show($"Ping bem-sucedido!\nSite: {site}\nEndereço: {reply.Address}\nTempo de resposta: {reply.RoundtripTime} ms",
                                            "Teste de Conectividade", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Falha no Ping para {site}: {reply.Status}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (PingException ex)
                    {
                        MessageBox.Show($"Erro ao realizar Ping para {site}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
