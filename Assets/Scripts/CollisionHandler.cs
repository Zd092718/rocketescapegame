using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crashNoise;
    [SerializeField] AudioClip successNoise;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource playerAudio;
    BoxCollider playerCollider;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start() 
    {
        playerAudio = GetComponent<AudioSource>();
        playerCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
            
        } else if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled){return;}

        switch(other.gameObject.tag)
        {
            case "Friendly":
                print("Hit friendly target");
                break;
            case "Finish":
                SuccessSequence(loadDelay);
                break;
            default:
                CrashSequence(loadDelay);
                break;
        }
    }


    void CrashSequence(float delayTime)
    {
        isTransitioning = true;
        playerAudio.Stop();
        explosionParticles.Play();
        playerAudio.PlayOneShot(crashNoise);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void SuccessSequence(float delayTime)
    {
        isTransitioning = true;
        successParticles.Play();
        playerAudio.Stop();
        playerAudio.PlayOneShot(successNoise);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
