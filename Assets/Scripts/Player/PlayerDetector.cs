using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public GameOverManager gameOverManager;

    private void OnTriggerEnter(Collider other)
    {
        // jika collider yang masuk memiliki tag enemy dan isTrigger false
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            // hitung jarak antara player dengan enemy yang masuk ke collider
            float enemyDistance = Vector3.Distance(transform.position, other.transform.position);
            gameOverManager.ShowWarning(enemyDistance);
        }
    }
}
