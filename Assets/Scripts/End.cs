using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject endGame;

    public void Awake()
    {
        endGame.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            endGame.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
