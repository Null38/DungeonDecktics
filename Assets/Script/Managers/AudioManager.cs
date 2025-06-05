using UnityEngine;

/// <summary>
/// AudioManager: 싱글톤으로 동작하며, SFXPlayer 오브젝트의 AudioSource를 통해
/// 언제든지 PlaySFX(clip) 호출만으로 효과음을 재생할 수 있게 해 둡니다.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX Player Prefab")]
    [Tooltip("씬에 하나만 존재해야 합니다. SFX 재생용 AudioSource가 담긴 Prefab.")]
    [SerializeField]
    private GameObject sfxPlayerPrefab;

    private AudioSource sfxSource;

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 씬에 SFXPlayer가 하나도 없으면 새로 생성
        if (sfxPlayerPrefab == null)
        {
            Debug.LogError("[AudioManager] sfxPlayerPrefab이 할당되지 않았습니다!");
            return;
        }

        // 이미 씬 위에 SFXPlayer 오브젝트가 있다면 그걸 사용
        GameObject existing = GameObject.FindWithTag("SFXPlayer");
        if (existing != null)
        {
            sfxSource = existing.GetComponent<AudioSource>();
            if (sfxSource == null)
                Debug.LogError("[AudioManager] SFXPlayer 에 AudioSource가 없습니다!");
        }
        else
        {
            // 씬 위에 없으면, 프리팹으로 Instantiate
            GameObject go = Instantiate(sfxPlayerPrefab);
            go.name = "SFXPlayer";
            go.tag = "SFXPlayer";
            sfxSource = go.GetComponent<AudioSource>();
            if (sfxSource == null)
                Debug.LogError("[AudioManager] Instantiate된 SFXPlayer에 AudioSource가 없습니다!");
        }
    }

    /// <summary>
    /// SFX 재생 (한 번만 틀고 끝). 2D 사운드.
    /// AudioSource.PlayOneShot(clip)을 호출합니다.
    /// </summary>
    /// <param name="clip">재생할 효과음</param>
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }
}
