using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem CrashParticles;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] float TimeDelay = 1f; 
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        respondToDebugKeys();
    }

    void respondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Friendly" : Debug.Log("Friendly");
            break;

         //   case "Obstacle" : Debug.Log("Obstacle");
         //   break;

            case "Fuel" : Debug.Log("Fuel");
            break;

            case "Finish" : FinishingSquence();
            break;

         //   case "Ground" : SceneManager.LoadScene(0);
         //   break;

            default : StartCrashSquence();
            break;
        }
    }

    void FinishingSquence()
    {
        SuccessParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        GetComponent<Movements>().enabled = false;
        Invoke("LoadNextLevel", TimeDelay);
    
    }
    void StartCrashSquence()
    {
        CrashParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(Crash);
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadLevel", TimeDelay);
        
    }

    void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevel + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }


}


