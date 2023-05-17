using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Animate : MonoBehaviour
{
    Animator anim;
    public AudioSource footstepsSound;
    private bool canDoAction = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDoAction) {
            if (!Input.anyKey)
            {
                anim.SetTrigger("Idle");
                footstepsSound.enabled = false;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                anim.SetTrigger("Forward");
                footstepsSound.enabled = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                anim.SetTrigger("Forward");
                footstepsSound.enabled = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                anim.SetTrigger("Forward");
                footstepsSound.enabled = true;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetTrigger("Forward");
                footstepsSound.enabled = true;
            }
            else if (Input.GetKey(KeyCode.Keypad1))
            {
                anim.SetTrigger("Dance");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            canDoAction = false;
            anim.SetTrigger("Dying");
            SceneManager.LoadScene("LoseScreen");
            canDoAction = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            canDoAction = false;
            anim.SetTrigger("Dying");
            SceneManager.LoadScene("LoseScreen");
            canDoAction = true;
        }
    }

    void OnTriggerExit()
    {
        canDoAction = true;
    }

}