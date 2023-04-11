using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPot : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    public void destroyPot() {
        animator.SetBool("destroyed", true);
        StartCoroutine(destroyAnimation());
    }

    public IEnumerator destroyAnimation() {
        yield return new WaitForSeconds(0.33f);
        this.gameObject.SetActive(false);
    }
}
