using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previosRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Player")
        {
            if(collider2D.transform.position.x < transform.position.x)
            {
                cameraController.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previosRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                cameraController.MoveToNewRoom(previosRoom);
                previosRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }

}
