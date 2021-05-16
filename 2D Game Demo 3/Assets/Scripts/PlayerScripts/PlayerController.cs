using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject skillPoint;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float timeActiveAttackPoint;
    [SerializeField] private float timeRecoverySkill;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth;

    private float curHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isSkillReady = true;
    private bool isDead;

    //Animations parameters
    const string SPEED = "speed";
    const string IS_GROUNDED = "isGrounded";
    const string IS_ATTACK = "isAttack";
    const string IS_SKILL = "isSkill";

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(curHealth);
    }

    private void Update()
    {
        if (healthBar.GetCurHealth() <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public  bool IsDead()
    {
        return isDead;
    }

    public void run(float moveHorizontal)
    {
        transform.position += Vector3.right * speed * moveHorizontal * Time.deltaTime;
        if (moveHorizontal != 0)
        {
            transform.localScale = new Vector3(moveHorizontal * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat(SPEED, Mathf.Abs(moveHorizontal));
    }

    public void jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        //rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        isGrounded = false;
    }

    public bool getGrounded()
    {
        return isGrounded;
    }

    public void attack(bool isAttack)
    {
        animator.SetBool(IS_ATTACK, isAttack);
        attackPoint.SetActive(true);
        if(!isAttack)
        StartCoroutine(TimeActiveAttackPoint());
    }
    public void skill(bool isSkill)
    {
        if(!isSkill) animator.SetBool(IS_SKILL, isSkill);
        if (isSkillReady&&isSkill)
        {
            animator.SetBool(IS_SKILL, isSkill);
            Instantiate(fireBall, skillPoint.transform.position, Quaternion.identity);
            isSkillReady = false;
            StartCoroutine(RecoverySkill());
        }
    }

    IEnumerator TimeActiveAttackPoint()
    {
        yield return new WaitForSeconds (timeActiveAttackPoint);
        attackPoint.SetActive(false);
    }

    IEnumerator RecoverySkill()
    {
        yield return new WaitForSeconds(timeRecoverySkill);
        isSkillReady = true;
    }

    IEnumerator TimeAnimationSkill()
    {
        yield return new WaitForSeconds(0.25f);

    }


    // Handle Collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
        animator.SetBool(IS_GROUNDED, isGrounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
        animator.SetBool(IS_GROUNDED, isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPointEnemy"))
        {
            healthBar.SetCurrentHealth(healthBar.GetCurHealth() - 5);
        }
    }
}
