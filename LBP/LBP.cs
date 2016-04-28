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

        public LBP(Image img)
        {
            picture = new Bitmap(img);
            changedPicture = new Bitmap(img);
            createArray();
            changedArrayPixels = methodLBP(picture);
            usedMethodForPicture();
        }


        private void fillZero(int[,] mass)
        {
            for (int i = 0; i < picture.Width; i++)
            {
                for (int j = 0; j < picture.Height; j++)
                {
                    mass[i, j] = 0;
                }
            }
        }

        private void createArray()
        {
            arrayPixels = new int[picture.Width, picture.Height];
            changedArrayPixels = new int[picture.Width, picture.Height];
            fillZero(arrayPixels);
            fillZero(changedArrayPixels);
        }

        public int[,] methodLBP(Bitmap bmp)
        {
            int[,] mass = new int[bmp.Width, bmp.Height];
            fillZero(mass);

            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    mass[i, j] = translationBinaryInDecimal(nearPixel(bmp, i, j));
                }
            }

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
                    if (i != x || j != y)
                    {
                        if (bmp.GetPixel(i , j).R >= bmp.GetPixel(x, y).R)
                            str = str + "1";
                        else
                            str = str + "0";
                    }
                }
            }
            return str;
        }

        //Применяем метод к копии фотографии
        private void usedMethodForPicture()
        {
            for (int i = 1; i < changedPicture.Width - 1; i++)
            {
                for (int j = 1; j < changedPicture.Height - 1; j++)
                {
                    int color = changedArrayPixels[i, j];
                    changedPicture.SetPixel(i, j, Color.FromArgb(color, color, color));
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
