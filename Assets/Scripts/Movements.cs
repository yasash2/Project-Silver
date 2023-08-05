using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movements : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float yValue = 100f;
    [SerializeField] float rotation = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startThrust();
        }
        else
        {
            stopThrust();
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            leftRotate();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rightRotate();
        }
        else
        {
            stopRotation();
        }
    }


    private void startThrust()
    {
        rb.AddRelativeForce(Vector3.up * yValue * Time.deltaTime);
        if (!audioSource.isPlaying)
        {

            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void stopThrust()
    {
        mainBooster.Stop();
        audioSource.Stop();
    }


    private void leftRotate()
    {
        rightBooster.Play();
        applyRotation(rotation);
    }

    private void rightRotate()
    {
        leftBooster.Play();
        applyRotation(-rotation);
    }

    private void stopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    void applyRotation(float rotationThrust)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
