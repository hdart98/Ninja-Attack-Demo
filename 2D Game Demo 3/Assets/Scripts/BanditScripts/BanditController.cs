using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float detectDis;
    [SerializeField] private float stopDis;
    [SerializeField] private float timeAtk;
    [SerializeField] private GameObject atkPoint;
    [SerializeField] private float timeActiveAtkPoint;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;

    private Animator animator;
    private PlayerController player;
    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        isAttack = true;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (player)
        {
            Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
            if (Vector2.Distance(transform.position, target) < detectDis &&
                Vector2.Distance(transform.position, target) > stopDis)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                animator.SetFloat("speed", 1);
            }
            else
            {
                animator.SetFloat("speed", 0);
            }

            if (Vector2.Distance(transform.position, target) <= stopDis && isAttack == true)
            {
                animator.SetBool("isAttack", true);
                isAttack = false;
                StartCoroutine(timeAttack());
                atkPoint.SetActive(true);
                StartCoroutine(TimeActiveAttackPoint());
            }
            else
            {
                animator.SetBool("isAttack", false);
            }
        }
        if (healthBar.GetCurHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }


    void Flip()
    {
        float scl;
        if(player && player.transform.position.x >= transform.position.x)
        {
            scl = -1;
        } else
        {
            scl = 1;
        }
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * scl, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator TimeActiveAttackPoint()
    {
        yield return new WaitForSeconds(timeActiveAtkPoint);
        atkPoint.SetActive(false);
    }

    IEnumerator timeAttack()
    {
        yield return new WaitForSeconds(timeAtk);
        isAttack = true;
    }

    public void TakeDamage(float damage)
    {
        healthBar.SetCurrentHealth(healthBar.GetCurHealth() - damage);
    }
}
