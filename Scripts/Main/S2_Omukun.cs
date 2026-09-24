using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Omukun : MonoBehaviour
{
    public static S2_Omukun inst;

    public float Jumphight;
    public bool Nowjimen;
    public int Jumpcount;
    public float Movespeed1;
    public float Movespeed2;
    public bool Isnowgameover;
    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        Isnowgameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&Jumpcount<1&&!S2_GameManager.inst.Gameover)
        {
            Jumpcount++;
            this.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0f,Jumphight,0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Jimen")
        {
            Nowjimen = true;
            Jumpcount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collisione)
    {
        if (collisione.gameObject.tag == "Jimen")
        {
            Nowjimen = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2)
    {
        if(collision2.gameObject.tag == "Folk"&&!Isnowgameover)
        {
            collision2.gameObject.tag = "Hitfolk";
            StartCoroutine("Startgameover");
        }
    }

    IEnumerator Startgameover()
    {
        S2_GameManager.inst.Gameover = true;
        Isnowgameover=true;
        this.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0f, 0f, 0f);
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<CapsuleCollider2D>().isTrigger = true;

        S2_GameManager.inst.Startgameoverhaikei();

        this.transform.eulerAngles -= new Vector3(0f, 0f, Movespeed1);

        while (this.transform.position.y > 0)
        {
            this.transform.position -= new Vector3(0f, Movespeed2, 0f);
            yield return null;
        }
        while (this.transform.position.y < 0)
        {
            this.transform.position += new Vector3(0f, Movespeed2, 0f);
            yield return null;
        }
        this.transform.position = new Vector3(-3f, 0f, 0f);
        while (this.transform.eulerAngles.z > 270f)
        {
            this.transform.eulerAngles -= new Vector3(0f, 0f, Movespeed1);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        while (this.transform.position.x < -2f)
        {
            this.transform.position += new Vector3(Movespeed2, 0f, 0f);
            yield return null;
        }
    }
}
