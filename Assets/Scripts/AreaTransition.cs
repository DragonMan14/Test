using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaTransition : MonoBehaviour
{
    private CameraController cam;

    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Vector3 playerChange;

    public bool needText;
    public string placeName;
    public TextMeshProUGUI text;
    
    void Start() {
        cam = Camera.main.GetComponent<CameraController>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        // If the colliding object is not a player, exit the function
        if (other.tag != "Player") {
            return;
        }

        cam.minPosition = newMinPos;
        cam.maxPosition = newMaxPos;
        other.transform.position += playerChange;

        // If the area requires a display name
        if (needText) {
            StartCoroutine(displayPlaceName());
        }
    }

    // Display text coroutine
    private IEnumerator displayPlaceName() {
        text.gameObject.SetActive(true);
        text.text = placeName;
        yield return new WaitForSeconds(4f);
        text.gameObject.SetActive(false);
    }
}
