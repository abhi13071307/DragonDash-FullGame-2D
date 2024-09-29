using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0;i<enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                initialPosition[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool status)
    {
        for(int i = 0;i<enemies.Length;i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].SetActive(status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
