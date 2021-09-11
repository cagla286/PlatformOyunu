using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger girdi");
        player.GetComponent<OyuncuKontrol>().onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Trigger cikti");
        player.GetComponent<OyuncuKontrol>().onGround = false;
    }
}
