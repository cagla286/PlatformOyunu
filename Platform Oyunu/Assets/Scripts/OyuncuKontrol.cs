using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuKontrol : MonoBehaviour
{
    private float mySpeedX;
    public float speed;
    public float jumpPower;
    public GameObject arrow;
    [SerializeField] bool attacked;
    private bool canDoubleJump;
    public bool onGround; // zemin uzerinde mi degil mi ?
    private Vector3 defaultLocalScale;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;
    private Animator myAnimator;
    Rigidbody2D myBody;
    public int arrowNumber;
    public Text arrowNumberText;
    public AudioClip dieMusic;
    public GameObject winPanel, losePanel;



    void Start()
    {
        attacked = false;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        arrowNumberText.text = arrowNumber.ToString();

    }

   
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal"); // -1 ile 1 arasında sag ve sol tusa basılma süresine baglı olarak degerler gelecek.
        myAnimator.SetFloat("speed",Mathf.Abs(mySpeedX));
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y); // karaktere x yonunde hız kazandırılır.

        #region playerın sag ve sol hareket yonune gore yüzünü donmesi

        if (mySpeedX>0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x,defaultLocalScale.y,defaultLocalScale.z);
        }
        else if (mySpeedX<0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion
        #region playerın zıplamasının kontrol edilmesi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump==true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x,jumpPower);
                    canDoubleJump = false;
                    myAnimator.SetTrigger("Jump");

                }
            }
           
        }
        #endregion

        #region playerın ok atmasının kontrolu

        if (Input.GetMouseButtonDown(0) && arrowNumber>0)
        {
            if(attacked==false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("Fire",0.5f);
            }
            
        }

        #endregion
        if (attacked==true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if (currentAttackTimer <= 0)
        {
            attacked = false;

        }
    }
    void Fire ()
    {
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity); // bir obje oluşturmak icin kullanılır.
        okumuz.transform.parent = GameObject.Find("Arrows").transform;

        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        }
        else
        {
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }
        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Enemy")
        {
            Die();
        }
        else if (collision.gameObject.tag=="Finish")
        {
            //winPanel.active = true;
            Destroy(collision.gameObject);
            StartCoroutine(Wait()); // bir süre bekletme için kullanılır.
        }
    }

    public void Die()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieMusic);

        myAnimator.SetFloat("speed", 0);
        myAnimator.SetTrigger("Die");

        myBody.constraints = RigidbodyConstraints2D.FreezePosition;
        enabled = false;
        losePanel.SetActive(true);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        winPanel.SetActive(true);// winPanel.active = true;


    }


}
