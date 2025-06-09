using UnityEngine;

// 플레이어 체력을 시각적으로 표시하는 하트 시스템
public class Heart : MonoBehaviour
{
    public Sprite OnHeart; // 체력이 있는 상태의 하트 이미지
    public Sprite OffHeart; // 체력이 없는 상태의 하트 이미지

    SpriteRenderer spriteRenderer; // 스프라이트 렌더러 (이미지 변경용)
    public int LiveNumber;         // 하트의 순서 (1, 2, 3)

    // 초기 설정
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 매 프레임마다 체력 상태 확인
    void Update()
    {
        // 현재 체력이 해당 하트의 순서보다 크거나 같다면 OnHeart
        if (GameManager.Instance.health >= LiveNumber)
        {
            spriteRenderer.sprite = OnHeart;
        }
        else
        {
            spriteRenderer.sprite = OffHeart;
        }
    }
}