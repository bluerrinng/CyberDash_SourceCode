using UnityEngine;

// 오디오 관리 클래스 - 효과음(SFX) 관리
public class AudioManager : MonoBehaviour
{
    // 싱글톤 인스턴스 (어디서든 접근 가능)
    public static AudioManager instance;

    [Header("#SFX Sound")]
    public AudioClip[] SFXSounds; // SFX 오디오 클립 배열 (다양한 효과음)
    public float SFXVolume;       // SFX 볼륨 크기

    public int channels;          // SFX 채널 개수 (동시 재생 가능한 SFX 수)

    int channelIndex;             // 현재 사용할 오디오 채널 인덱스
    AudioSource[] SFXPlayers;     // SFX 재생 오디오 소스 배열

    // SFX 효과음 종류를 Enum으로 정의
    public enum SFX { ButtonPlay, Drink, EndClick, PlayerDead, PlayerHit, PlayerJump }

    // 오브젝트가 생성될 때 호출 - 오디오 매니저 초기화
    void Awake()
    {
        // 싱글톤 설정 (현재 인스턴스를 전역적으로 사용)
        instance = this;
        Init();
    }

    // 오디오 소스 초기화 함수
    void Init()
    {
        // SFX 플레이어를 저장할 오브젝트 생성
        GameObject sfxObjects = new GameObject("SFX Players");
        sfxObjects.transform.parent = transform; // 오디오 매니저의 자식으로 설정
        SFXPlayers = new AudioSource[channels];  // 채널 수만큼 오디오 소스 생성

        // 각 오디오 소스 설정
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            SFXPlayers[i] = sfxObjects.AddComponent<AudioSource>();
            SFXPlayers[i].playOnAwake = false; // 자동 재생 비활성화
            SFXPlayers[i].volume = SFXVolume;  // 볼륨 설정
        }
    }

    // SFX 재생 함수
    public void PlaySfx(SFX sfx)
    {
        // 모든 SFX 플레이어를 탐색하여 사용 가능한 채널 찾기
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % SFXPlayers.Length;

            // 만약 현재 채널이 재생 중이라면 다음으로 넘어감
            if (SFXPlayers[loopIndex].isPlaying)
                continue;

            // SFX를 설정하고 재생
            channelIndex = loopIndex; // 현재 채널 업데이트
            SFXPlayers[loopIndex].clip = SFXSounds[(int)sfx];
            SFXPlayers[loopIndex].Play();
            break;
        }
    }
}