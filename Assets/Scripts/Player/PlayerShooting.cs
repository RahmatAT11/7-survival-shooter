using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;                      

    public float timer;                                    
    Ray shootRay;                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;                

    void Awake()
    {
        // mengambil layer shootable
        shootableMask = LayerMask.GetMask("Shootable");
        
        // mendapatkan reference komponen
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        /*if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }*/

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // disable line renderer
        gunLine.enabled = false;
        
        // disable light
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;

        // play audio
        gunAudio.Play();

        // hidupkan light
        gunLight.enabled = true;

        // mulai gun particle
        gunParticles.Stop();
        gunParticles.Play();

        // hidupkan line renderer dan set posisinya
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // lakukan raycast jika mendeteksi id enemy apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // lakukan mengambil komponen health dari raycasthit shoothit
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // lakukan take damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // set posisi akhir line ke posisi hit
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // set posisi akhir line ke range maks
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}