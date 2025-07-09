using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPoweup = false;
    private float powerUpstrength = 15.0f;
    public GameObject powerupIndicator;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        
        powerupIndicator.transform.position = transform.position - new Vector3(0,0.5f,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            hasPoweup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownPoutine());
        }
    }

    IEnumerator PowerupCountdownPoutine() //IEnumerator을 사용해 update에 영향x
    {
        yield return new WaitForSeconds(7); //7초 기다렸다가 실행
        hasPoweup = false;
        powerupIndicator.gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && hasPoweup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpstrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPoweup); ;
        }
    
    }
}
