using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class generatorVode
{
   public static float[,] GenerisiVodu(int velicina)
    {
        float[,] mapa = new float[velicina, velicina];
        for(int i = 0;i<velicina;i++)
        {
            for(int j=0;j<velicina;j++)
            {
                float x = i / (float)velicina * 2 - 1; 
                float y = j / (float)velicina * 2 - 1;

                float vrednost = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                mapa[i, j] = razmera(vrednost);
            }
        }
        return mapa;
    }
    static float razmera(float vrednost)
    {
        float a = 3;
        float b = 2.2f;
        return Mathf.Pow(vrednost, a) / (Mathf.Pow(vrednost, a) + Mathf.Pow(b - b * vrednost, a));
    }
}
