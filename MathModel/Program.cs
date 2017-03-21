using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MathModel
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] shunXu = new int[19];
            List<Shred> shredList=new List<Shred>();
            String path= System.AppDomain.CurrentDomain.BaseDirectory;
            string[] fileArray = System.IO.Directory.GetFiles(path+@"data"); 
            int fileNumber=fileArray.Length;
            StreamWriter sw = new StreamWriter(path+"data1\\dataAnalys.txt");
            for (int i = 0; i < fileNumber; i++)
            {
                Shred newShred = new Shred(fileArray[i]);
                newShred.index = i;
                newShred.setArray();
                shredList.Add(newShred);
            }
            computeRate(shredList);

            foreach (Shred s in shredList)
            {
                if (s.maxL != -1)
                {
                    s.maxL = Sort(s.leftRate);
                }
                else
                {
                    shunXu[0] = s.index;
                }
                if (s.maxR != -1)
                {
                    s.maxR = Sort(s.rightRate);
                }
                else
                {
                    shunXu[18] = s.index;
                }
                Console.WriteLine(s.index + "的左边是:" + s.maxL);
                Console.WriteLine(s.index + "的右边是:" + s.maxR);
            }
            for(int i=1;i<18;i++)
            {
                shunXu[i] = shredList[shunXu[i-1]].maxR;
            }
            Pic picture = new Pic(0, 1980, 72 * 19, shredList);
            picture.shunXu = shunXu;
            picture.setHead();
            picture.setData();

            picture.Output();


            Console.Write("complete!");
            Console.ReadLine();
        }


        static void computeRate(List<Shred> shredList)
        {
            //StreamWriter sw = new StreamWriter("\\data1\\dataAnalys.txt");
            double sum1 = 0;
            double sum2 = 0;
            double rate1;
            double rate2;

            for (int i = 0; i < shredList.Count; i++)
            {

                foreach (Shred s in shredList)
                {
                    if (s.index != shredList[i].index)
                    {
                        sum1 = 0;
                        sum2 = 0;
                        for (int m = 0; m < 1980; m++)
                        {

                            if (shredList[i].leftPoint[m] == 1 && s.rightPoint[m] == 1)
                            {
                                sum1++;
                            }
                            if (shredList[i].rightPoint[m] == 1 && s.leftPoint[m] == 1)
                            {
                                sum2++;
                            }

                        }
                        rate1 = sum1 / s.blackNuberR;
                        rate2 = sum2 / shredList[i].blackNuberR;

                        shredList[i].leftRate[s.index]=rate1;
                        shredList[i].rightRate[s.index] = rate2;

                        //sw.WriteLine("右： " + i + "左：" + s.index + "=" + rate1);
                        //sw.WriteLine("左： " + i + "右：" + s.index + "=" + rate2);
                    }
                }
            }
            //sw.Close();
 
        }

        static int Sort(double[] source)
        {
            double max = source[0] ;
            int maxIndex = 0;
            for (int i = 0; i < source.Length;i++ )
            {
                if (source[i] > max)
                {
                    max = source[i];
                    maxIndex = i;
                }
            }
            
            return maxIndex;
            
        }




        

    }
}
