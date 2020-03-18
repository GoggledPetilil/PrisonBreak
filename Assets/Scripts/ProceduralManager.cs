using UnityEngine;
using UnityEngine.Events;

public class ProceduralManager : MonoBehaviour
{


    public static ProceduralManager instance;
    [SerializeField]private int seed;
    private float perlinSeed;
    public ProceduralWorld world;

    public delegate void regenerate();
    public static regenerate OnRegenerate;


    void Awake()
    {

        if(instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }

        world.Init();

    }

    void OnValidate()
    {
        if(instance != null)
        {
            OnRegenerate.Invoke();
        }
    }

    public void SetSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
        perlinSeed = Random.Range(-100000, 100000);
    }

    public float GetPerlinSeed()
    {
        return perlinSeed;
    }

}
