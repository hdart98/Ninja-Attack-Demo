using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private float timeDestroy;

    private PlayerController pctrl;
    private float moveHorizontal;

    // Start is called before the first frame update
    private void Awake()
    {
        pctrl = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        moveHorizontal = pctrl.transform.localScale.x;
        Vector3 scalePos = transform.localScale;
        scalePos.x = Mathf.Abs(scalePos.x) * moveHorizontal;
        transform.localScale = scalePos;
        StartCoroutine(DestroyAfterSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveHorizontal * fireBallSpeed * Time.deltaTime;
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", 50);
            Destroy(gameObject);
        }
    }
}
