using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        // mendapatkan reference komponen
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        // set currentHealth
        currentHealth = startingHealth;
    }


    void Update ()
    {
        // cek enemy jika sinking
        if (isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // cek jika enemy dead
        if (isDead)
            return;

        // play audio
        enemyAudio.Play ();

        // kurangi health enemy
        currentHealth -= amount;

        // ganti posisi particle
        hitParticles.transform.position = hitPoint;
        
        // mulai particle system
        hitParticles.Play();

        // dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        // set isDead
        isDead = true;

        // Set capsuleCollider ke trigger
        capsuleCollider.isTrigger = true;

        // trigger animasi dead
        anim.SetTrigger ("Dead");

        // mainkan suara dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

        ScoreManager.score += scoreValue;
    }


    public void StartSinking ()
    {
        // disable komponen navmesh
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        // set rigidbody menjadi kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
