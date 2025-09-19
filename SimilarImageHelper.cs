using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowHelp
{
    public class SimilarImageHelper
    {
        public static Image SourceImg;

        public SimilarImageHelper(string filePat)
        {
            SourceImg = Image.FromFile(filePat);
        }

        public SimilarImageHelper(Stream stream)
        {
            SourceImg = Image.FromStream(stream);
        }

        public static String GetHash()
        {
            Image image = ReduceSize();
            Byte[] grayValues = ReduceColor(image);
            Byte average = CalcAverage(grayValues);
            String reslut = ComputeBits(grayValues, average);
            return reslut;
        }

        //压缩图片尺寸
        private static Image ReduceSize(int width = 8,int height = 8)
        {
            Image image = SourceImg.GetThumbnailImage(width, height, () => { return false; },IntPtr.Zero);
            return image;
        }

        //压缩颜色
        private static Byte[] ReduceColor(Image image)
        {
            Bitmap bitMap = new Bitmap(image);
            Byte[] grayValues = new Byte[image.Width * image.Height];

            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    Color color = bitMap.GetPixel(x, y);
                    byte grayValue = (byte)((color.R * 30 + color.G * 59 + color.B * 11) / 100);
                    grayValues[x*image.Width + y] = grayValue;
                }
            }
            return grayValues;
        }

        //平均颜色
        private static Byte CalcAverage(byte[] values)
        {
            int sum = 0;
            for(int i = 0;i<values.Length;i++)
            {
                sum += (int)values[i];
            }
             return Convert.ToByte(sum/values.Length);   
        }

        //计算位数
        private static String ComputeBits(Byte[] values, byte averageValue) 
        {
            char[] result = new char[values.Length];
            for(int i = 0; i<values.Length;i++)
            {
                if (values[i] < averageValue)
                {
                    result[i] = '0';
                }
                else
                {
                    result[i] = '1';
                }
            }
            return new String(result);
        }

        //比较相似度
        public static Int32 CalcSimilarDegree(string a,string b)
        {
            if(a.Length != b.Length)
            {
                throw new ArgumentException();
            }
            int count = 0;
            for (int i = 0;i < a.Length;i++)
            {
                if (a[i] != b[i])
                {
                    count++;
                }
            }
            return count;
        }
    }
}
