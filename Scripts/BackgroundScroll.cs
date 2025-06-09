using UnityEngine;

// 배경 스크롤링을 관리하는 클래스
public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;            // 배경 스크롤 속도
    private MeshRenderer meshRenderer;   // 메쉬 렌더러 (배경 텍스처 제어)

    // 오브젝트 초기화
    void Start()
    {
        // 메쉬 렌더러 컴포넌트 가져오기
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // 매 프레임마다 호출되는 함수
    void Update()
    {
        // 배경 텍스처의 오프셋을 이동하여 스크롤링 효과 생성
        meshRenderer.material.mainTextureOffset += 
            new Vector2(0.5f * scrollSpeed * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime, 0);
        // X 방향으로만 스크롤 (Y는 고정)
    }
}