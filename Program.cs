// from Program.cs
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


public class Form1 : Form
{
    private Button button1, button2, button3;
    private PictureBox pictureBox1;
    private System.DirectoryServices.DirectorySearcher directorySearcher1;

    public          Form1           ()  {  InitializeComponent();  }
	
    #region Vom Windows Form-Designer generierter Code

    private void InitializeComponent()
    {
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 522);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 200);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
       
			this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
			const string PfadBild = @"./../../Kaffeefleck.jpg";
            StreamReader SR = new StreamReader(PfadBild);
            Bitmap Bild = new Bitmap(SR.BaseStream);
            SR.Close();
            
            int minX = Bild.Width, maxX = 0, minY = Bild.Height, maxY = 0;
            
            for (int x = 0; x < Bild.Width; x++) {
            	for (int y = 0; y < Bild.Height; y++) {
            		if ((Bild.GetPixel(x,y).ToArgb() * 100 / -16777216) >=2) {
            			//Färbe Rot ein, wenn nicht weiß
            			Bild.SetPixel(x,y, Color.FromArgb(Bild.GetPixel(x,y).R, 40, 40));
            			if (x < minX)
            				minX = x;
            			if (x > maxX)
            				maxX = x;
            			if (y < minY)
            				minY = y;
            			if (y > maxY)
            				maxY = y;
            		}
            	}
            }
            
            /* TODO: Male Kasten um Fleck */
            for (int x = minX; x < maxX; x++) {
            	for (int y = (minY-1); y < minY; y++) {
            		Bild.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = minX; x < maxX; x++) {
            	for (int y = maxY; y < (maxY+1); y++) {
            		Bild.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = (minX-1); x < minX; x++) {
            	for (int y = minY; y < maxY; y++) {
            		Bild.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = maxX; x < (maxX+1); x++) {
            	for (int y = minY; y < maxY; y++) {
            		Bild.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
           

            this.pictureBox1.Size = new System.Drawing.Size(Bild.Size.Width,Bild.Size.Height);
            this.pictureBox1.Image = Bild;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 600);
            //this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

    }

    #endregion		

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }
    private void button2_Click(object sender, EventArgs e)
    {

    }
}



static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}
