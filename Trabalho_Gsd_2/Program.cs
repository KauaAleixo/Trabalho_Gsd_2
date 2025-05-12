using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_Gsd_2
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            {
                using (Ping ping = new Ping())
                {
                    string hostName = "stackoverflow.com";
                    try
                    {
                        PingReply reply = ping.Send(hostName); // Versão síncrona

                        Console.WriteLine($"Ping status for ({hostName}): {reply.Status}");

                        if (reply.Status == IPStatus.Success)
                        {
                            Console.WriteLine($"Address: {reply.Address}");
                            Console.WriteLine($"Roundtrip time: {reply.RoundtripTime} ms");
                            Console.WriteLine($"Time to live: {reply.Options?.Ttl}");
                            Console.WriteLine($"Don't fragment: {reply.Options?.DontFragment}");
                        }
                    }
                    catch (PingException ex)
                    {
                        Console.WriteLine($"Ping failed: {ex.Message}");
                    }
                }
            }
        }
    }
}
