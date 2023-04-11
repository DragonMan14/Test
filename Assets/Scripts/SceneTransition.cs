using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public VectorValue camMin;
    public VectorValue camMax;

    public Vector2 newCamMin;
    public Vector2 newCamMax;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWaitTime;
    
    private void Awake() {
        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        {
            if (!other.CompareTag("Player") || other.isTrigger) {
                return;
            }
            playerStorage.runtimeValue = playerPosition;
            camMin.runtimeValue = newCamMin;
            camMax.runtimeValue = newCamMax;
            StartCoroutine(fadeCoroutine());
        }
    }

    public IEnumerator fadeCoroutine() {
        if (fadeOutPanel != null) {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(fadeWaitTime);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!async.isDone) {
            yield return null;
        }
    }
}
