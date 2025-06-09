using UnityEngine;

// 플레이어 동작을 제어하는 클래스
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float JumpForce; // 점프 힘 설정

    [Header("Unity References")]
    Animator anime;           // 애니메이터 컴포넌트
    Rigidbody2D rigid;        // 물리적 움직임을 위한 Rigidbody2D
    BoxCollider2D boxCollider; // 충돌 체크용 콜라이더
    
    [Header("Player Condition")]
    private bool isGrounded = true;   // 플레이어가 땅에 있는지 확인
    private bool isInvincible = false; // 무적 상태 여부 확인

    // 초기 설정
    void Start()
    {
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();   
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // 매 프레임마다 호출
    void Update()
    {
        // 스페이스바를 눌렀고, 플레이어가 땅에 있을 때
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 점프 실행
            rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anime.SetInteger("state", 0); // 점프 애니메이션 실행
            AudioManager.instance.PlaySfx(AudioManager.SFX.PlayerJump);
        }
    }

    // 바닥과 충돌 체크 (땅에 도착)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            anime.SetInteger("state", 1); // 걷기 애니메이션
            isGrounded = true;
        }
    }

    // 오브젝트와의 충돌 감지 (적, 아이템 등)
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌 시
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isInvincible)
            {
                Destroy(collision.gameObject);
            }
            Hit(); // 피해 처리
            AudioManager.instance.PlaySfx(AudioManager.SFX.PlayerHit);
        }

        // 체력 회복 아이템 (일반 음료)
        if (collision.gameObject.tag == "Drinks")
        {
            Destroy(collision.gameObject);
            Heal();
        }

        // 무적 아이템 (황금 음료)
        else if (collision.gameObject.tag == "GoldenDrinks")
        {
            Destroy(collision.gameObject);
            StartInvincible();
        }
    }

    // 피해 처리 함수
    void Hit()
    {
        GameManager.Instance.health -= 1;
    }

    // 무적 상태 시작
    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f); // 5초 후 무적 종료
    }

    // 무적 상태 종료
    void StopInvincible()
    {
        isInvincible = false;
    }

    // 체력 회복 함수
    void Heal()
    {
        // 체력을 최대 3까지 회복
        GameManager.Instance.health = Mathf.Min(3, GameManager.Instance.health + 1);
        AudioManager.instance.PlaySfx(AudioManager.SFX.Drink);
    }

    // 플레이어 사망 처리
    public void KillPlayer()
    {
        boxCollider.enabled = false; // 충돌 무효화
        anime.enabled = false;       // 애니메이션 중지
        rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // 사망 연출
        AudioManager.instance.PlaySfx(AudioManager.SFX.PlayerDead);
    }
}