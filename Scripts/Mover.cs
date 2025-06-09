using UnityEngine;

// 오브젝트를 왼쪽으로 이동시키는 클래스 (장애물이나 배경 이동)
public class Mover : MonoBehaviour
{
    public float moveSpeed; // 오브젝트 이동 속도

    // 오브젝트 초기화 (필요 시 사용)
    void Start()
    {
    }

    // 매 프레임마다 호출되는 함수
    void Update()
    {
        // 오브젝트를 왼쪽으로 이동 (Vector3.left)
        // GameManager의 게임 속도와 Time.deltaTime을 곱하여 일정한 속도로 이동
        transform.position += 
            Vector3.left * moveSpeed * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime;
    }
}