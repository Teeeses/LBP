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

        private BufferedBitmap bbPicture;
        private BufferedBitmap bbChangedPicture;

        public LBP(Image img)
        {
            picture = new Bitmap(img);
            changedPicture = new Bitmap(img);

            createBufferedBitmap();
            createArray();
            changedArrayPixels = methodLBP(picture);
            usedMethodForPicture();
        }

        public void createBufferedBitmap()
        {
            bbPicture = new BufferedBitmap(picture);
            bbChangedPicture = new BufferedBitmap(changedPicture);
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
        }

        public int[,] methodLBP(Bitmap bmp)
        {
            /*System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();*/

            int[,] mass = new int[bmp.Width, bmp.Height];

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    mass[i, j] = translationBinaryInDecimal(nearPixel(bmp, i, j));
                }
            }

            /*myStopwatch.Stop();
            TimeSpan ts = myStopwatch.Elapsed;
            Console.WriteLine(ts.Seconds + "секунд " + ts.Milliseconds + " миллисекунд");
            */
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
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if(i >= 0 && j >=0 && i <= bmp.Width - 1 && j <= bmp.Height - 1) 
                    {
                        if (i != x || j != y)
                        {
                            var intens = bbPicture.GetPixel(i, j).R * 0.3 + bbPicture.GetPixel(i, j).G * 0.59 + bbPicture.GetPixel(i, j).B * 0.11;
                            if (intens >= bbPicture.GetPixel(x, y).R)
                                str = str + "1";
                            else
                                str = str + "0";
                        }
                    }
                }
            }
            return str;
        }

        //Применяем метод к копии фотографии
        private void usedMethodForPicture()
        {
            for (int i = 0; i < changedPicture.Width; i++)
            {
                for (int j = 0; j < changedPicture.Height; j++)
                {
                    int color = changedArrayPixels[i, j];
                    bbChangedPicture.SetPixel(i, j, Color.FromArgb(color, color, color));
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
    }
}
