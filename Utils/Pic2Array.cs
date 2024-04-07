using System;
using System.Collections.Generic;
using System.Drawing;

namespace EmptySet.Utils
{
    internal class Pic2Array
    {
        public static int[,] GetArray(String path, Dictionary<int, Color> color)
        {
            Bitmap myBitmap = new Bitmap(path);
            int w = myBitmap.Width;
            int h = myBitmap.Height;
            Color pixelColor;
            int[,] worldArray = new int[w,h];
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++)
                {
                    pixelColor = myBitmap.GetPixel(i, j);

                    if (pixelColor.IsEmpty)
                    {
                        worldArray[i, j] = 0;
                    } else{
                        foreach (KeyValuePair<int, Color> kvp in color){
                            if (kvp.Value == pixelColor) 
                            {
                                worldArray[i, j] = kvp.Key;
                                break;
                            }

                        }
                    }
                }
            }
            return worldArray;
        }
    }
}
