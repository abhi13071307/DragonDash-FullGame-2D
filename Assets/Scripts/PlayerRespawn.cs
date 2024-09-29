using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckPoint;
    private Health playerHealth;
    private UiManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UiManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckPoint == null) 
        {
            uiManager.GameOver();
            return;
        }

        playerHealth.Respawn();
        transform.position = currentCheckPoint.position;

        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckPoint.parent);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckPoint = collision.transform;
            SoundMananger.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }

    }
}
