using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{

    public Text scoreValueText;
    public float coinRotateSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f,coinRotateSpeed,0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int scoreValue = int.Parse(scoreValueText.text);
            scoreValue += 50;
            scoreValueText.text = scoreValue.ToString();
            Destroy(gameObject);

        }
    }
}
