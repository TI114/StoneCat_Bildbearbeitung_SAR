/* Code by StoneCat */

using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


public class Form1 : Form
{
    private PictureBox pictureArea;

    public          Form1           ()  {  InitializeComponent();  }

    private void InitializeComponent()
    {
            this.pictureArea = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
			this.pictureArea.Location = new System.Drawing.Point(10, 10);
            this.pictureArea.Name = "Picture Area Coffee";
			const string PfadBild = @"./../../Kaffeefleck.jpg";
            StreamReader SR = new StreamReader(PfadBild);
            Bitmap picture = new Bitmap(SR.BaseStream);
            SR.Close();
            
            int minX = picture.Width, maxX = 0, minY = picture.Height, maxY = 0;
            int[] hist = new int[256]; //Default 0
            double darkestXValue = 255, darkestYValue = 255;
            double darkestXPosition = 0, darkestYPosition = 0;
            
            for (int x = 0; x < picture.Width; x++) {
            	double tmp_y_color_value = 0;
            	for (int y = 0; y < picture.Height; y++) {
            		Color tmp_color_value = picture.GetPixel(x,y);
            		int tmp_gray_value = (tmp_color_value.B + tmp_color_value.G + tmp_color_value.R)/3;
            		//Recording grayscale values -> + 1
            		hist[tmp_gray_value] = hist[tmp_gray_value] + 1;
            		tmp_y_color_value = tmp_y_color_value + tmp_gray_value;
            		
            		if (tmp_gray_value <= 245) {
            			//Color to Red, if not white
            			picture.SetPixel(x,y, Color.FromArgb(tmp_gray_value, 40, 40));
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
            	
            	if ((tmp_y_color_value/256) < darkestXValue) {
            		darkestXValue = (tmp_y_color_value/256);
            		darkestXPosition = x;
            	}
            	//TODO: Get darkestYValue
            }
            
            /* Draw a box around the coffee */
            for (int x = minX; x < maxX; x++) {
            	for (int y = (minY-1); y < minY; y++) {
            		picture.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = minX; x < maxX; x++) {
            	for (int y = maxY; y < (maxY+1); y++) {
            		picture.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = (minX-1); x < minX; x++) {
            	for (int y = minY; y < maxY; y++) {
            		picture.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
            
            for (int x = maxX; x < (maxX+1); x++) {
            	for (int y = minY; y < maxY; y++) {
            		picture.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }
                        
            /* Paint grayscale quantities into the image */
            for (int x = 0; x < 256; x++) {
            	/* Round values of grayscale quantities for drawing */
            	hist[x] = (int) Math.Round((double) (hist[x] * 100 / 65536), MidpointRounding.AwayFromZero);
            	for (int y = (picture.Height-1); y > (picture.Height-hist[x]); y--) {
            		picture.SetPixel(x,y, Color.FromArgb(40, 40, 40));
            	}
            }          

            this.pictureArea.Size = new System.Drawing.Size(picture.Size.Width,picture.Size.Height);
            this.pictureArea.Image = picture;

            /* Main Form */
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 600);
            this.Controls.Add(this.pictureArea);
            this.Name = "Picture Screen";
            this.Text = "Picture Screen";
            this.ResumeLayout(false);
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
