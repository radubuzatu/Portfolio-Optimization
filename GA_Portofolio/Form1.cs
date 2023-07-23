using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GA_Portofolio
{
    public partial class Form1 : Form
    {
        public String s;
        private Excel.Application excelapp;
        private Excel.Workbooks excelappworkbooks;
        private Excel.Workbook excelappworkbook;
        private Excel.Sheets excelsheets;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        private Populatie pop;
        private Graphics g;
        private int col;

        public Form1()
        {
            InitializeComponent();
            button1.Focus();
        }

        private void distruge_obiect()//distrugem obiectul excel
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelapp);
            excelapp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private bool deschiderea()//deschiderea fisierului indicat
        { 
            String s;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s = openFileDialog1.FileName;

                excelapp = new Excel.Application();
                //excelapp.Visible = true;
                excelappworkbooks = excelapp.Workbooks;
                excelappworkbook = excelapp.Workbooks.Open(s,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing);
               
                excelsheets = excelappworkbook.Worksheets;
                excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);
                
                return true;
            }
            return false;
        }

        private void cantitatea()//numara rindurile
        {
             col = 0;
             //numaram cite rinduri in documentul deschis
             for (int i = 1; true; i++)
             {
                 excelcells = excelworksheet.get_Range("A" + Convert.ToString(i), Type.Missing);
                 if (Convert.ToString(excelcells.Value2).Trim() == "")
                     break;
                 else col++;
             }
             col++;

        }

        private void includerea()//importul datelor din fisier in program
        { 
                string[] svect = new string[6];
                float[] fvect = new float[6];

                int k = 0;

                if (col > 1)//sunt date si denumirile datelor
                {
                    for (int i = 2; i < col; i++)
                    {
                        k = 0;
                        for (char j = 'A'; j <= 'E'; j++)
                        {
                            if (j == 'D') { svect[k] = ""; k++; }
                            excelcells = excelworksheet.get_Range(j + Convert.ToString(i), Type.Missing);
                            svect[k]=Convert.ToString(excelcells.Value2);
                            //fvect[k] =(float)excelcells.Value2;
                            k++;

                        }
                        dataGridView1.Rows.Add(svect);                
                        //dataGridView1.Rows.Add(fvect);
                    }
                }
                excelapp.Quit();
                distruge_obiect();
                button1.Enabled = true;
               // textBox1.Text = Convert.ToString(Containerr.count);
             
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deschiderea())
            {
                cantitatea();
                dataGridView1.Rows.Clear();
                includerea();
            }
            button1.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deschiderea())
            {
                cantitatea();
                includerea();
            }
            button1.Focus();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initializarea datelor in container
            Containerr.CMax = new System.Collections.ArrayList();
            Containerr.CMin = new System.Collections.ArrayList();
            Containerr.Pret = new System.Collections.ArrayList();
            Containerr.Venit = new System.Collections.ArrayList();
            Containerr.Risc = new System.Collections.ArrayList();

            for (int i = 0; i < col - 2; i++)
            {
                Containerr.Pret.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value));
            }
            for (int i = 0; i < col - 2; i++)
            {
                Containerr.Venit.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));
            }
            for (int i = 0; i < col - 2; i++)
            {
               // Containerr.Risc.Add(Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value));
            }
            for (int i = 0; i < col - 2; i++)
            {
                Containerr.CMin.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value));
            }
            for (int i = 0; i < col - 2; i++)
            {
                Containerr.CMax.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value));
            }

          Containerr.count = Containerr.CMin.Count;
          Containerr.summa = (float)Convert.ToDouble(textBox6.Text);
        
          //pregatim populatie
          pop = new Populatie("Populatie de hirtii de valoare", Containerr.count, Convert.ToInt32(textBox1.Text), 
          Convert.ToInt32(textBox2.Text),(float)Convert.ToDouble(textBox3.Text)/100,(float)Convert.ToDouble(textBox4.Text)/100,1);
  
          Cromozom[] c = new Cromozom[50];
          c = pop.GetHighestScoreGenomes(50);
          
            //lista cu fitnesuri initiali
          listBox2.Items.Clear();
          for (int i = 0; i < 50; i++)
          {
              //c[i].CalculateFitness();
              listBox2.Items.Add(c[i].CurrentFitness);
          }

          //generarea generatielor
          for (int i = 0; i < pop.NumberOfGenerations; i++)
          {
              pop.NextGeneration(checkBox1.Checked);
              progressBar1.Value = i * 100 / pop.NumberOfGenerations;
          }
          progressBar1.Value = 100; 
        
          c = pop.GetHighestScoreGenomes(50);

          //lista cu fitnesuri final
          listBox1.Items.Clear();
          for (int i = 0; i < 50; i++)
          {
              //c[i].CalculateFitness();
              listBox1.Items.Add(c[i].CurrentFitness);
          }
          //selectam folia 2 si introducem datele in grid de pe ea 
          tabControl1.SelectedIndex = 1;
          float sumf = 0;
          dataGridView2.Rows.Clear();
          for (int i = 0; i < Containerr.count; i++)
          {
              dataGridView2.Rows.Add();
              dataGridView2.Rows[i].Cells[0].Value = dataGridView1.Rows[i].Cells[0].Value;
              //  dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToInt32(Containerr.CMin[i]) + (Convert.ToInt32(Containerr.CMax[i]) - Convert.ToInt32(Containerr.CMin[i])) * (float)((Cromozom)pop.GetHighestScoreGenome())[i]);
              dataGridView2.Rows[i].Cells[2].Value = Convert.ToInt32(Math.Round(Convert.ToInt32(Containerr.CMin[i]) + (Convert.ToInt32(Containerr.CMax[i]) - Convert.ToInt32(Containerr.CMin[i])) * (float)((Cromozom)pop.GetHighestScoreGenome())[i]));
              
              //dataGridView2.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
              dataGridView2.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
              sumf +=(float)Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);

          }

          for (int i = 0; i < Containerr.count; i++)
          {
              dataGridView2.Rows[i].Cells[1].Value =String.Format("{0:F2}",Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value) / sumf*100);
          }
          textBox5.Text = Convert.ToString(Containerr.summa - sumf);
          textBox7.Text = Convert.ToString(sumf);

          desenare();
          
            button1.Focus();
          //button2.Enabled = true;
        }

        
        private void desenare()
        {   //desenam pe un bitmap
            Bitmap b = new Bitmap(345, 345);
            g = Graphics.FromImage(b);
            //g = pictureBox1.CreateGraphics();//crearea graficii
            SolidBrush Mb1 = new SolidBrush(System.Drawing.Color.White);
            g.FillRectangle(Mb1, 0, 0, 345, 345);
            SolidBrush Mb = new SolidBrush(System.Drawing.Color.Black);
         
            Point[] p = new Point[pop.NumberOfGenerations];
            float min,max,interval;
            min = max = (float)pop.Rezultate[0];

            for (int i = 1; i < pop.Rezultate.Count; i++)
            {
                if ((float)pop.Rezultate[i] < min) min = (float)pop.Rezultate[i];
                else if ((float)pop.Rezultate[i] > max) max = (float)pop.Rezultate[i];
            }
           
            interval = max - min;//calculam interval
            if (interval < 0.001) interval = 0.001f; //control dimensiunea interval
            float y;
            float pas=300f/pop.NumberOfGenerations; //pasul
            for (int i = 0; i < pop.NumberOfGenerations; i++)
            {
                p[i].X = 11 + Convert.ToInt32(Math.Round(i * pas));
                y = (float)pop.Rezultate[i];
                p[i].Y = 300+9-Convert.ToInt32((y - min) * 300f / interval);
            }
            
            int pasy = 300/5;
            float pasx = pop.NumberOfGenerations/5f;

            Font f = new Font("Arial", 8);
            SolidBrush sb = new SolidBrush(Color.Black);
            StringFormat sf=new StringFormat(StringFormatFlags.DirectionVertical);
            for (int i = 0; i <= 5 ; i++)
            {
               //desenarea pe oy
                g.DrawLine(new Pen(Color.Gray,1), 20, 320 - i * pasy, 330, 320 - i * pasy);
                g.DrawLine(new Pen(Color.Black,2), 17, 320 - i * pasy, 23, 320 - i * pasy);
                g.DrawString(String.Format("{0:F2}", (min + interval * i / 5)*100), f, sb, 5, 310 - i * pasy,sf);
               //desenarea pe ox
                g.DrawLine(new Pen(Color.Black, 2), i * pasy + 20, 317, i * pasy + 20, 323);
                g.DrawLine(new Pen(Color.Gray, 1), i * pasy + 20, 13, i * pasy + 20, 320);
                g.DrawString(Convert.ToString(Convert.ToInt32(i*pasx)), f, sb, i * pasy + 14, 323);
                
            }
            f = new Font("Arial", 8, FontStyle.Bold);
            g.DrawString("generații", f, sb, 150,330);
            g.DrawString("profit %", f, sb, 0, 150,sf);
            //desenarea axelor
            g.DrawLine(new Pen(Color.Black, 2), 20, 0, 20, 320);
            g.DrawLine(new Pen(Color.Black, 2), 20, 320, 340, 320);

            g.DrawLine(new Pen(Color.Black, 2), 15, 5, 20, 0);
            g.DrawLine(new Pen(Color.Black, 2), 20, 0, 25, 5);
            g.DrawLine(new Pen(Color.Black, 2), 335, 315, 340, 320);
            g.DrawLine(new Pen(Color.Black, 2), 340, 320, 335, 325);

            //desenarea graficului
            Pen Mp = new Pen(Color.Green, 2);
            for (int i = 1; i < pop.NumberOfGenerations; i++)
            {
                g.DrawLine(Mp, p[i - 1].X + 10, p[i - 1].Y + 10, p[i].X+10,p[i].Y+10);
            }
            pictureBox1.Image = b;
            textBox8.Text = Convert.ToString(String.Format("{0:F2}", max*100));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 2)
            {
               // desenare();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
            //alegem metoda de efectuarea acrosingoverului
        {
            if (checkBox1.Checked == true)
                textBox4.Enabled = true;
            else textBox4.Enabled = false;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About formHelpAb = new About();
            formHelpAb.Location = new Point(this.Location.X+100,this.Location.Y+200);
            formHelpAb.Show();
        }

        //salvarea rezultatelor obtinute
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String s;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s = saveFileDialog1.FileName;
                excelapp = new Excel.Application();
                excelapp.SheetsInNewWorkbook = 1;
                excelapp.Workbooks.Add(Type.Missing);
                excelappworkbooks = excelapp.Workbooks;
                excelappworkbook = excelappworkbooks[1];
                excelappworkbook.Saved = true;
                excelapp.DisplayAlerts = false;
                excelsheets = excelappworkbook.Worksheets;
                excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

                excelcells = excelworksheet.get_Range("A1", Type.Missing);
                excelcells.Value2 = "Denumirea activului";
                excelcells = excelworksheet.get_Range("B1", Type.Missing);
                excelcells.Value2 = "Ponderea în portofoliul";
                excelcells = excelworksheet.get_Range("C1", Type.Missing);
                excelcells.Value2 = "Cantitate";
                excelcells = excelworksheet.get_Range("D1", Type.Missing);
                excelcells.Value2 = "Prețul activului";

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    for (char j1 = 'A', j2 = '0'; j1 <= 'D'; j1++, j2++)
                    {
                        excelcells = excelworksheet.get_Range(j1 + Convert.ToString(i + 2), Type.Missing);
                        excelcells.Value2 = dataGridView2.Rows[i].Cells[Convert.ToInt32(Convert.ToString(j2))].Value;
                    }

                }

                excelcells = excelworksheet.get_Range('A' + Convert.ToString(dataGridView2.RowCount +3), Type.Missing);
                excelcells.Value2 = "Profitabilitatea Portofoliului";

                excelcells = excelworksheet.get_Range('B' + Convert.ToString(dataGridView2.RowCount + 3), Type.Missing);
                excelcells.Value2 = textBox8.Text;

                excelcells = excelworksheet.get_Range('A' + Convert.ToString(dataGridView2.RowCount + 5), Type.Missing);
                excelcells.Value2 = "Capitalul total investit";

                excelcells = excelworksheet.get_Range('B' + Convert.ToString(dataGridView2.RowCount + 5), Type.Missing);
                excelcells.Value2 = textBox7.Text;



                excelapp.Windows[1].Close(true, s, Type.Missing);
                excelapp.Quit();
                distruge_obiect();

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }


    }
}
