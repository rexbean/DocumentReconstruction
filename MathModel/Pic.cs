using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MathModel
{
    class Pic
    {
        private string path=null;
        public byte[] buffer;
        public int[,] data;
        public int[] shunXu=new int[19];
        protected int row;
        protected int column;
        private List<Shred> shredList = new List<Shred>();
        private String basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        public Pic(int i,int row,int column,List<Shred> shredList)
        {
            
            this.path = basePath+@"dataAll\" + i + ".bmp";
            long len = 1078 + row * column;
            buffer = new byte[len];

            this.row = row;
            this.column = column;

            

            this.shredList = shredList;
 
        }

        public void setHead()
        {
            long len = 1078 + row * column;
            string pathIn = basePath+ @"\data\000.bmp";
            FileStream fs = new FileStream(pathIn, FileMode.Open);
            this.buffer = new byte[len];
            fs.Read(buffer, 0, (int)1077);
            byte[] fileSize = BitConverter.GetBytes(len);
            byte[] width = BitConverter.GetBytes(column);
            byte[] height = BitConverter.GetBytes(row);
            byte[] graphSize = BitConverter.GetBytes(row * column);
            data = new int[row, column];

            buffer[2] = fileSize[0];
            buffer[3] = fileSize[1];
            buffer[4] = fileSize[2];
            buffer[5] = fileSize[3];

            buffer[18] = width[0];
            buffer[19] = width[1];
            buffer[20] = width[2];
            buffer[21] = width[3];

            buffer[22] = height[0];
            buffer[23] = height[1];
            buffer[24] = height[2];
            buffer[25] = height[3];

            buffer[34] = graphSize[0];
            buffer[35] = graphSize[1];
            buffer[36] = graphSize[2];
            buffer[37] = graphSize[3];
        }

        public void setData()
        {
            int n = 0;
            for (int m = 0; m < 19; m++)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < 72; j++)
                    {
                        n = m *72+j;
                        data[i, n] = shredList[shunXu[m]].data[1980 - i - 1, j];
                    }
                }
            }

        }


        public void Output()
        {
            int k = 1078;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    
                    byte[] hex;
                    if (data[i, j] == 0)
                    { 
                        hex= BitConverter.GetBytes(255);
                    }
                    else
                    {
                        hex = BitConverter.GetBytes(0);
 
                    }
                    buffer[k] = hex[0];
                    k++;
                }
            }
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();

        }
    }
}
