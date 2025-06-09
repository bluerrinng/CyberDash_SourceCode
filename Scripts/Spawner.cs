using UnityEngine;

// 오브젝트를 랜덤하게 생성하는 스포너 클래스
public class Spawner : MonoBehaviour
{
    public GameObject[] gameObjects; // 생성할 오브젝트 배열
    public float minSpawnTime;       // 최소 생성 시간 간격
    public float maxSpawnTime;       // 최대 생성 시간 간격

    // 스포너 초기화
    void Start()
    {
        // 일정 시간 후 Spawn 함수 호출 (랜덤 시간으로 설정)
        Invoke("Spawn", Random.Range(minSpawnTime, maxSpawnTime));
    }

    // 오브젝트 생성 함수
    void Spawn()
    {
        // 랜덤한 오브젝트를 선택
        var RandomObjects = gameObjects[Random.Range(0, gameObjects.Length)];
        // 선택된 오브젝트를 현재 위치에 생성
        Instantiate(RandomObjects, transform.position, Quaternion.identity);
        // 다음 스폰 시간 설정 (반복적으로 호출)
        Invoke("Spawn", Random.Range(minSpawnTime, maxSpawnTime));
    }
}