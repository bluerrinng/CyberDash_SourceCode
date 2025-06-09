using UnityEngine;

// 특정 위치에 도달하면 오브젝트를 제거하는 클래스
public class Destroyer : MonoBehaviour
{
    // 오브젝트 초기화 (필요 시 사용)
    void Start()
    {
    }

    // 매 프레임마다 호출
    void Update()
    {
        // 만약 오브젝트의 X 좌표가 -13.0f 이하로 이동하면
        if (transform.position.x <= -13.0f)
        {
            // 해당 오브젝트를 파괴 (메모리에서 제거)
            Destroy(gameObject);
        }
    }
}