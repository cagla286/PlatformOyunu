using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag=="Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag=="Enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            
            
        }
            
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
