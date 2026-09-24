using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2_Folk : MonoBehaviour
{
    public float Myspeed;
    public float Myspeed2;
    public float Sizechangespeed;
    // Start is called before the first frame update
    void Start()
    {
        float Mysize = Random.Range(0.3f,0.7f);
        this.transform.localScale = new Vector3(Mysize, Mysize, Mysize);
        Myspeed = S2_GameManager.inst.Folkspeed;
        Sizechangespeed = Random.Range(0.01f, 0.1f);
        StartCoroutine("Gogo");
    }

    // Update is called once per frame
    void Update()
    {
        if (S2_GameManager.inst.Gameover&&this.tag=="Hitfolk")
        {
            StartCoroutine("Imyes");
        }
        else if(S2_GameManager.inst.Gameover&&!(this.tag=="Hitfolk"))
        {
            StartCoroutine("Imno");
        }
    }

    IEnumerator Gogo()
    {
        if (Random.Range(0, 4) == 0)
        {
            bool Gobig = false;
            while (this.transform.position.x > -11f&&!S2_GameManager.inst.Gameover)
            {
                if (Gobig)
                {
                    this.transform.localScale += new Vector3(Sizechangespeed, Sizechangespeed, 0f);
                    if (this.transform.localScale.x >= 0.7f)
                    {
                        Gobig = false;
                    }
                }
                else
                {
                    this.transform.localScale -= new Vector3(Sizechangespeed, Sizechangespeed, 0f);
                    if (this.transform.localScale.x <= 0.3f)
                    {
                        Gobig = true;
                    }
                }
                this.transform.position -= new Vector3(Myspeed, 0f, 0f);
                yield return null;
            }
        }
        else
        {
            while (this.transform.position.x > -11f && !S2_GameManager.inst.Gameover)
            {
                this.transform.position -= new Vector3(Myspeed, 0f, 0f);
                yield return null;
            }
        }
        if (!S2_GameManager.inst.Gameover)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Imno()
    {
        while (this.GetComponent<SpriteRenderer>().color.a > 0f)
        {
            this.GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 0.01f);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    IEnumerator Imyes()
    {
        while (this.transform.position.x < 2f)
        {
            this.transform.position += new Vector3(Myspeed2, 0f, 0f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (this.transform.position.y < 0f)
        {
            this.transform.position += new Vector3(0f, Myspeed2, 0f);
            yield return null;
        }
    }
}
