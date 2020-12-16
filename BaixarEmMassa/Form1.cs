using AltoHttp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaixarEmMassa
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog openFile = new OpenFileDialog();
        string linha = "";
        int totalLinhas = 0;
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFile.FileName);
                totalLinhas = File.ReadAllLines(openFile.FileName).Length;
                while (linha != null)
                {
                    linha = sr.ReadLine();

                    if (linha != null)
                    {
                        baixarFila(sender, e, linha);
                    }
                    else {
                        lblStatus.Text = "Concluído";
                    }
                }
            }
        }



        HttpDownloader httpDownloader;
        
        private void baixarFila(object sender, EventArgs e, String linha)
        {
            httpDownloader = new HttpDownloader(linha, $"{Application.StartupPath}\\{Path.GetFileName(linha)}");
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            httpDownloader.Start();
        }

        private void HttpDownloader_ProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            progressBar.Value = (int)e.Progress;
        }

        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                //lblStatus.Text = "Pronto!";
                lblNumArq.Text = (int.Parse(lblNumArq.Text) + 1).ToString();

            });
        }

    }
}
