using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int rotationSpeed = 100;
    public GameObject onCollectEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Instantiate(onCollectEffect, transform.position, Quaternion.identity);
        }
    }
}
