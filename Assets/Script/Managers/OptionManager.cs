using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("슬라이더")]
    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;


    // 옵션창 열릴 때 슬라이더 값 초기화
    public void Awake()
    {
        if (audioMixer == null)
            throw new System.Exception("audioMixer 연결 안 됨");

        float value;

        audioMixer.GetFloat("MasterVolume", out value);
        masterVolumeSlider.value = value;

        audioMixer.GetFloat("BGMVolume", out value);
        bgmVolumeSlider.value = value;

        audioMixer.GetFloat("SFXVolume", out value);
        sfxVolumeSlider.value = value;

        // 이벤트 리스너 등록
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    // 볼륨 조절 부분 제대로 작동하는지 소리 넣어봐야 할 듯.

    // 옵션창 닫기
    public void CloseOption()
    {
        SceneManager.UnloadSceneAsync("OptionScene");
    }
    
    // 마스터 볼륨 조절
    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }

    // BGM 볼륨 조절
    public void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", value);
    }

    // 효과음 볼륨 조절
    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
    }
}
