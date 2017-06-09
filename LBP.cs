using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace LBP
{
    class LBP
    {
        private Bitmap picture;
        private Bitmap changedPicture;
        private int[,] arrayPixels;
        private int[,] changedArrayPixels;

        private double[] histogram;

        private string kinds = "none";
        private bool transitions;

        private BufferedBitmap bbPicture;
        private BufferedBitmap bbChangedPicture;

        public LBP(Image img, string str, bool trans)
        {
            kinds = str;
            transitions = trans;
            picture = new Bitmap(img);
            changedPicture = new Bitmap(img);

            //createBufferedBitmap();
            createArray();
            createPixelsIntensive();
            changedArrayPixels = methodLBP(picture);
            usedMethodForPicture();
        }

        public void createBufferedBitmap()
        {
            bbPicture = new BufferedBitmap(picture);
            bbChangedPicture = new BufferedBitmap(changedPicture);
        }

        public void createPixelsIntensive()
        {
            for (int i = 0; i < picture.Width; i++)
            {
                for (int j = 0; j < picture.Height; j++)
                {
                    arrayPixels[i, j] = (int)(picture.GetPixel(i, j).R * 0.3 + picture.GetPixel(i, j).G * 0.59 + picture.GetPixel(i, j).B * 0.11);
                }
            }
        }

        public void setUnlock()
        {
            bbPicture.Unlock();
            bbChangedPicture.Unlock();
        }

        private void createArray()
        {
            arrayPixels = new int[picture.Width, picture.Height];
            changedArrayPixels = new int[picture.Width, picture.Height];
            histogram = new double[256];
        }

        public int[,] methodLBP(Bitmap bmp)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();

            int[,] mass = new int[bmp.Width, bmp.Height];

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    mass[i, j] = translationBinaryInDecimal(nearPixel(bmp, i, j));
                }
            }

            myStopwatch.Stop();
            TimeSpan ts = myStopwatch.Elapsed;
            Console.WriteLine(ts.Seconds + "секунд " + ts.Milliseconds + " миллисекунд");
            
            return mass;
        }

        //Перевод бинарного кода в десятичный
        private int translationBinaryInDecimal(String str)
        {
            int number = Int32.Parse(str);
            int dec = 0;
            int degree = 0;

            do{
                int b = number % 10;
                dec = dec + b * (int)Math.Pow(2, degree);
                degree++;
                number = number / 10;
            }while(number != 0);
            return dec;
        }

        //Получение бинарного кода, который описывает окрестность пикселя.
        private String nearPixel(Bitmap bmp, int x, int y)
        {
            string str = "";
            str += checkNearPixel(x - 1, y - 1, x, y, bmp);
            str += checkNearPixel(x - 1, y, x, y, bmp);
            str += checkNearPixel(x - 1, y + 1, x, y, bmp);

            str += checkNearPixel(x, y + 1, x, y, bmp);
            str += checkNearPixel(x + 1, y + 1, x, y, bmp);

            str += checkNearPixel(x + 1, y, x, y, bmp);
            str += checkNearPixel(x + 1, y - 1, x, y, bmp);

            str += checkNearPixel(x, y - 1, x, y, bmp);
            str = allocatedЫignificantPatern(str);
            return str;
        }

        private string allocatedЫignificantPatern(string str)
        {
            char s = str[0];
            int temp = 1;
            for (int i = 1; i < str.Length; i++)
            {
                if (s != str[i]) {
                    s = str[i];
                    temp++;
                }
            }
            if (transitions)
            {
                if (temp <= 2)
                    return str;
                return "11111111";
            }
            return str;
        }

        private string checkNearPixel(int i, int j, int x, int y, Bitmap bmp)
        {
            if (i >= 0 && j >= 0 && i <= bmp.Width - 1 && j <= bmp.Height - 1)
            {
                var intens = arrayPixels[i, j];
                if (kinds == "R")
                {
                    if (bmp.GetPixel(i, j).R >= arrayPixels[x, y])
                        return "1";
                    else
                        return "0";
                }
                if (kinds == "G")
                {
                    if (bmp.GetPixel(i, j).G >= arrayPixels[x, y])
                        return "1";
                    else
                        return "0";
                }
                if (kinds == "B")
                {
                    if (bmp.GetPixel(i, j).B >= arrayPixels[x, y])
                        return "1";
                    else
                        return "0";
                }
                if (kinds == "none")
                {
                    if (intens >= arrayPixels[x, y])
                        return "1";
                    else
                        return "0";
                }
            }
            return "0";
        }

        //Применяем метод к копии фотографии
        private void usedMethodForPicture()
        {
            for (int i = 0; i < changedPicture.Width; i++)
            {
                for (int j = 0; j < changedPicture.Height; j++)
                {
                    int color = changedArrayPixels[i, j];
                    changedPicture.SetPixel(i, j, Color.FromArgb(color, color, color));
                    if(color != 0 && color != 256)
                        histogram[color]++;
                }
            }
        }

        public Bitmap getPicture()
        {
            return picture;
        }

        public Bitmap getChengedPicture()
        {
            return changedPicture;
        }

        public double[] getHistogram()
        {
            return histogram;
        }

        public void setKinds(string str)
        {
            kinds = str;
        }
    }
}
