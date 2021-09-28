using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    private void Awake ()
    {
        // cari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag ("Player").transform;

        // mendapatkan reference komponen
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        // memindahkan target enemy ke posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        // hentikan gerakan enemy
        else
        {
            nav.enabled = false;
        }
    }
}
