using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crashNoise;
    [SerializeField] AudioClip successNoise;

    AudioSource playerAudio;

    void Start() 
    {
        playerAudio = GetComponent<AudioSource>();   
    }
    void OnCollisionEnter(Collision other) 
    {

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
        playerAudio.PlayOneShot(crashNoise);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void SuccessSequence(float delayTime)
    {
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
