using UnityEngine;
using System.Collections; // IMPORTANTE para usar coroutines

public class Move : MonoBehaviour
{
    public int Speed;
    public int Jump;
    private Rigidbody2D rig;
    private bool isChao = true;
    private bool emDash = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += mov * Time.deltaTime * Speed;

        if (Input.GetButtonDown("Jump") && isChao)
        {
            rig.AddForce(new Vector2(0f, Jump), ForceMode2D.Impulse);
            isChao = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && Coletar.trava && !emDash)
        {
            StartCoroutine(DashTemporario());
        }
    }

    IEnumerator DashTemporario()
    {
        emDash = true;
        Speed += 5;
        yield return new WaitForSeconds(2f);
        Speed -= 5;
        Coletar.trava = false;
        emDash = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isChao = true;
        }
    }
}

