using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임의 현재 상태를 나타내는 열거형 (게임 상태 관리)
public enum GameState
{
    Intro,   // 게임 시작 전 상태 (인트로 UI가 활성화됨)
    Playing, // 게임 플레이 중 상태
    Dead     // 플레이어 사망 후 상태 (게임 오버 UI가 활성화됨)
}

// 게임 전체를 관리하는 GameManager 클래스 (싱글톤 방식 사용)
public class GameManager : MonoBehaviour
{
    // GameManager 싱글톤 인스턴스 (전역에서 접근 가능)
    public static GameManager Instance;

    // 현재 게임 상태 (초기 상태는 Intro)
    public GameState state = GameState.Intro;

    // 플레이어 체력 (최대 3)
    public int health = 3; 

    // 게임 플레이 시작 시간 (점수 계산에 사용)
    public float playStartTime;
    
    [Header("References")]
    public GameObject IntroUi;     // 게임 시작 전 보여줄 UI (인트로)
    public GameObject DeadUi;      // 사망 시 보여줄 UI (게임 오버)
    public GameObject[] EnemySpawner; // 적 생성기 오브젝트 배열
    public GameObject DrinkSpawner;   // 음료(회복 아이템) 생성기 오브젝트

    public Player playerScript;    // 플레이어 스크립트 참조
    public TMP_Text scoreText;     // 점수 텍스트 UI (점수 표시용)

    // 초기화 (싱글톤 인스턴스 설정)
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 게임 시작 시 호출
    void Start()
    {
        // 인트로 UI 활성화 (게임 시작 전)
        IntroUi.SetActive(true);
    }

    // 현재 점수를 계산하는 함수 (플레이 시간 기반)
    float CalculateScore()
    {
        // 게임 시작 시간부터 현재 시간까지의 경과 시간 반환
        return Time.time - playStartTime;
    }

    // 최고 점수 저장 함수 (PlayerPrefs 사용)
    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore()); // 현재 점수 계산 (소수점 버림)
        int CurrentHighScore = PlayerPrefs.GetInt("HighScore"); // 저장된 최고 점수 로드
        
        // 현재 점수가 최고 점수보다 높다면
        if (score > CurrentHighScore)
        {
            // 최고 점수 갱신
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // 최고 점수를 불러오는 함수
    int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore"); // 저장된 최고 점수 반환
    }

    // 게임 속도를 계산하는 함수 (시간에 따라 점점 빨라짐)
    public float CalculateGameSpeed()
    {
        // 게임 플레이 중이 아니라면 기본 속도 (3.0f) 유지
        if (state != GameState.Playing)
        {
            return 3f;
        }

        // 게임 플레이 중이라면 경과 시간에 따라 속도 증가 (최대 20)
        float speed = 3f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 20f); // 속도 최대값 제한 (20f)
    }

    // 매 프레임마다 실행 (게임 상태 확인 및 점수 업데이트)
    void Update()
    {
        // 게임이 플레이 중일 때 (점수 UI 업데이트)
        if (state == GameState.Playing)
        {
            // 현재 점수를 소수점 없이 표시
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }

        // 게임 오버 상태일 때 (최고 점수 표시)
        else if (state == GameState.Dead)
        {
            scoreText.text = "High Score: " + GetHighScore();
        }

        // 플레이 중이며 체력이 0이면 (사망 처리)
        if (state == GameState.Playing && health == 0)
        {
            // 플레이어 사망 처리 (점프 연출)
            playerScript.KillPlayer();
            // 음료 생성기 비활성화
            DrinkSpawner.SetActive(false);

            // 모든 적 생성기 비활성화
            for (int i = 0; i < EnemySpawner.Length; i++)
            {
                EnemySpawner[i].SetActive(false);
            }

            // 최고 점수 저장
            SaveHighScore();
            // 게임 오버 UI 활성화
            DeadUi.SetActive(true);
            // 게임 상태를 Dead로 변경
            state = GameState.Dead;
        }
    }

    // 게임 시작 함수 (인트로 UI에서 실행)
    public void GameStart()
    {
        // 버튼 클릭 사운드 재생
        AudioManager.instance.PlaySfx(AudioManager.SFX.ButtonPlay);

        // 게임 상태를 Playing으로 변경
        state = GameState.Playing;
        // 게임 시작 시간 기록 (점수 계산 시작)
        playStartTime = Time.time;

        // 인트로 UI 비활성화
        IntroUi.SetActive(false);
        // 음료 생성기 활성화
        DrinkSpawner.SetActive(true);

        // 모든 적 생성기 활성화
        for (int i = 0; i < EnemySpawner.Length; i++)
        {
            EnemySpawner[i].SetActive(true);
        }
    }
}