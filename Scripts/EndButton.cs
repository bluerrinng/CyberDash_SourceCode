using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 종료 버튼과 다시 시작 버튼을 처리하는 클래스
public class EndButton : MonoBehaviour
{
    // 게임을 다시 시작하는 함수
    public void Restart()
    {
        // "SampleScene" 씬을 다시 로드하여 게임 재시작
        SceneManager.LoadScene("SampleScene");
        // 버튼 클릭 사운드 재생
        AudioManager.instance.PlaySfx(AudioManager.SFX.ButtonPlay);
    }

    // 게임을 종료하는 함수
    public void QuitGame()
    {
        // 애플리케이션을 종료 (에디터에서는 동작하지 않음)
        Application.Quit();
        // 종료 버튼 클릭 사운드 재생
        AudioManager.instance.PlaySfx(AudioManager.SFX.EndClick);
    }
}