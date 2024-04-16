using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneratorTekstura
{
    public static Texture2D tekstureBoje(Color[] boje,int sirina,int visina)
    {
        Texture2D tekstura = new Texture2D(sirina, visina);
        tekstura.filterMode = FilterMode.Point;
        tekstura.wrapMode = TextureWrapMode.Clamp;
        tekstura.SetPixels(boje);
        tekstura.Apply();
        return tekstura;
    }
    
    public static Texture2D bojeVisine( float[,] mapeVisine)
    {
        int sirina = mapeVisine.GetLength(0);
        int visina = mapeVisine.GetLength(1);

        Color[] generisiBoje = new Color[sirina * visina];
        for (int j = 0; j < visina; j++)
        {
            for (int i = 0; i < sirina; i++)
            {
                generisiBoje[j * sirina + i] = Color.Lerp(Color.black, Color.white, mapeVisine[i, j]);

            }
        }
        return tekstureBoje(generisiBoje,sirina,visina);

    }
}
