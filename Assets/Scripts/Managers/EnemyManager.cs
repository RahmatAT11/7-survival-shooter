using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField] 
    private MonoBehaviour factory;
    IFactory Factory {get {return factory as IFactory;}}

    void Start ()
    {
        // mengeksekusi fungsi Spawn setiap bebarapa detik sesuai dengan nilai spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        // Jika player telah mati maka tidak membuat enemy baru
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        // mendapatkan nilai random untuk posisi spawn enemy
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnEnemy = 0;
        
        // menduplikasi enemy
        Factory.FactoryMethod(spawnEnemy);
    }
}
