using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;    // Rigidbody 2D bola
    public float xInitialForce;         // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float yInitialForce;

    private Vector2 trajectoryOrigin;    // Titik asal lintasan bola saat ini

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();        // Mulai game
        trajectoryOrigin = transform.position;
    }
    void RestartGame()
    {
        ResetBall();        // Kembalikan bola ke posisi semula
        Invoke("PushBall", 2);        // Setelah 2 detik, berikan gaya ke bola
    }
    void ResetBall()
    {
        transform.position = Vector2.zero;        // Reset posisi menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;      // Reset kecepatan menjadi (0,0)
    }
    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        if (yRandomInitialForce < 0.1f)
        {
            yRandomInitialForce = -yInitialForce;
        }
        else if (yRandomInitialForce > 0.1f)
        {
            yRandomInitialForce = yInitialForce;
        }

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakkan bola ini.
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }
    //====================================================================================
    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
