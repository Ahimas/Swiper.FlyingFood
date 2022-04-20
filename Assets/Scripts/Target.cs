using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleExplosion;
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float minSpeed = 10f;
    private float maxSpeed = 14f;
    private float maxTorque = 10f;
    private float xSpawnRange = 4.5f;
    private float ySpawnPos = -1f;

    public int scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(new Vector3(RandomTorque(), RandomTorque(), RandomTorque()), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if ( gameManager.isGameActive && Input.GetMouseButton(0) )
        {
            gameManager.UpdateScores(scoreValue);
            Instantiate(particleExplosion, transform.position, particleExplosion.transform.rotation);
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        if (gameManager.isGameActive && !this.gameObject.CompareTag("Bad")) gameManager.DecreaseLives();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos, 0);
    }
}
