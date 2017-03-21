using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace MathModel
{
    class Shred
    {
        private string path=null;
        public  int width;
        public  int height;
        private byte[] buffer;
        public int index;
        public int[,] data;
        public int blackNuberL;
        public int blackNuberR;
        
        public int maxL;
        public int maxR;
        public Shred(string path)
        {
            this.path=path;
            FileInfo fi = new FileInfo(path);
            long len = fi.Length;
            buffer = new byte[len];
            buffer = this.ReadDataIn();
            width = (int)(buffer[19] * Math.Pow(2, 8) + buffer[18]);
            height = (int)(buffer[23] * Math.Pow(2, 8) + buffer[22]);
        }

        public List<int> leftPoint = new List<int>();

        public List<int> leftBlackPoint = new List<int>();

        public List<int> rightBlackPoint = new List<int>();
        
        public List<int> rightPoint = new List<int>();

        public double[] leftRate = new double[19];

        public double[] rightRate = new double[19];
        
        public void setArray()
        {
                data = new int[(int)height, (int)width];
                int k = 1078;
                

                // set the bmp file data in array;
                for (int m = 0; m < height; m++)
                {
                    for (int n = 0; n < width; n++)
                    {
                        if (buffer[k] > 128)
                        {
                            buffer[k] = 0;
                        }
                        else
                        {
                            buffer[k] = 1;
                        }
                            //Console.WriteLine(buffer[k] + " ");
                        data[(int)height-m-1, n] = buffer[k];
                        if (n == 0)
                        {
                            leftPoint.Add(data[(int)height - m - 1, n]);
 
                        }
                        else if (n == width - 1)
                        {
                            rightPoint.Add(data[(int)height - m - 1, n]);
                        }
                        k++;
                    }
                }
                this.setBlackPoint();
                this.outputTxt();
        }

        private void outputTxt()
        {
            String basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamWriter sw = new StreamWriter(basePath+"data1\\"+this.index+".txt");
            for (int i = 1; i < height; i++)
            {

                for (int j = 0; j < width; j++)
                {
                    sw.Write(data[i, j]);
                    
                }
                sw.WriteLine();
            }
            sw.Close();  
        }

        public void setBlackPoint()
        {

            for(int i=0;i<this.height-1;i++)
            {
                if (leftPoint[i] == 1)
                {
                    blackNuberL++;
 
                }
                if (rightPoint[i] == 1)
                {
                    blackNuberR++;

                }
                
               
            }
            if (blackNuberR == 0)
            {
                this.maxR = -1;
            }
            if (blackNuberL == 0)
            {
                this.maxL = -1;
            }

           
        }

        private byte[] ReadDataIn()
        {
            FileInfo fi = new FileInfo(path);
            long len = fi.Length;
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] buffer = new byte[len];
            fs.Read(buffer, 0, (int)len);
            fs.Close();
            return buffer;
        }

        
    }
}
