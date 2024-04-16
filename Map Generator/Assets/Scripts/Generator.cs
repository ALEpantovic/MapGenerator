using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public enum Oboji { boje,noiseMapa,Mesh,generatorVode};
    public Oboji oboji;

    const int MapChunkSize = 241;
    [Range(1,6)]
    public int jednostavnostDetalja=1;

    public float skala;

    public float visinaDodavanje;
    public AnimationCurve meshVisina;

    public int oktave;
    [Range (0,1)]
    public float persistance;

    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public bool dodajVoduOkoOstrva;
    float[,] voda;
    public TipTerena[] region;
    public bool Autoupdate;

    public GameObject[] treePrefabs;
    public float treeNoiseScale = .05f;
    public float treeDensity = .5f;

    void Awake()
    {
        voda = generatorVode.GenerisiVodu(MapChunkSize);
    }
    public void Generisi()
    {
        float[,] noiseMapa = Noise.GenerisiNoiseMapu(MapChunkSize, MapChunkSize, skala,seed,oktave,persistance,lacunarity,offset);
        
        Color[] boje = new Color[MapChunkSize * MapChunkSize];
        for(int j=0;j<MapChunkSize;j++)
        {
            for (int i=0;i<MapChunkSize;i++)
            {
                if(dodajVoduOkoOstrva)
                {
                    noiseMapa[i, j] =Mathf.Clamp01( noiseMapa[i, j] - voda[i, j]);
                }
                float trenutnaVisina = noiseMapa[i, j];
                for(int k=0;k<region.Length;k++)
                {
                    if(trenutnaVisina<=region[k].visina)
                    {
                        boje[j * MapChunkSize + i] = region[k].boja;
                        break;
                    }
                }
            }
        }

        PrikazMape prikaz = FindObjectOfType<PrikazMape>();
        if (oboji == Oboji.noiseMapa)
        {
            prikaz.nacrtajTeksture(GeneratorTekstura.bojeVisine(noiseMapa));
        }
        else if (oboji == Oboji.boje)
        {
            prikaz.nacrtajTeksture(GeneratorTekstura.tekstureBoje(boje,MapChunkSize,MapChunkSize));
        }
        else if (oboji == Oboji.Mesh)
        {
            prikaz.DrawMesh(meshGenerator.GenerisiTeren(noiseMapa,visinaDodavanje,meshVisina,jednostavnostDetalja), GeneratorTekstura.tekstureBoje(boje, MapChunkSize, MapChunkSize));
        }
        else if(oboji==Oboji.generatorVode)
        {
            prikaz.nacrtajTeksture(GeneratorTekstura.bojeVisine(generatorVode.GenerisiVodu(MapChunkSize)));
        }

    }
    private void Start()
    {
        GenerateTrees();
    }
    public void GenerateTrees()
    {
        float[,] noiseMapa = Noise.GenerisiNoiseMapu(MapChunkSize, MapChunkSize, skala, seed, oktave, persistance, lacunarity, offset);
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < MapChunkSize; y++)
        {
            for (int x = 0; x < MapChunkSize; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                noiseMapa[x, y] = noiseValue;
            }
        }
        for (int j = 0; j < MapChunkSize; j++)
        {
            for (int i = 0; i < MapChunkSize; i++)
            {
                float trenutnaVisina = noiseMapa[i, j];
                for (int k = 0; k < region.Length; k++)
                {
                    if (region[k].nazivTerena=="Pesak"|| region[k].nazivTerena =="Zemlja")
                    {
                        GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(prefab, transform);
                        tree.transform.position = new Vector3(i, 0, j);
                        tree.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                        tree.transform.localScale = Vector3.one * Random.Range(.8f, 1.2f);
                        break;
                    }
                }
            }
         }
     }
    private void OnValidate()
    {
        if(lacunarity<1)
        {
            lacunarity = 1;
        }
        if(oktave<0)
        {
            oktave = 1;
        }
        voda = generatorVode.GenerisiVodu(MapChunkSize);
    }
    [System.Serializable]
    public struct TipTerena
    {
        public float visina;
        public Color boja;
        public string nazivTerena;
    }
}
