using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
        
    [SerializeField] 
    private GameObject sfxPlayerPrefab;

    [Header("SFX Sound")]

    [Header("Player")]
    [SerializeField]
    private AudioClip PlayerAttackClip;
    [SerializeField]
    private AudioClip PlayerShieldClip;

    [Header("Enemy")]
    [SerializeField]
    private AudioClip EnemyAttackClip;
    [SerializeField]
    private AudioClip EnemyDieClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);                
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxPlayerPrefab == null)
            return;
        GameObject go = Instantiate(sfxPlayerPrefab);
        go.name = "SFXPlayer_Instance";
        AudioSource source = go.GetComponent<AudioSource>();
        if (source != null)
        {
            source.PlayOneShot(clip);
            
        }
        else
        {
           Destroy(go);
        }
    }
    //재생편의 레퍼 메서드
    public void PlayPlayerAttack() => PlaySFX(PlayerAttackClip);
    public void PlayEnemyAttack() => PlaySFX(EnemyAttackClip);
    public void PlayEnemyDie() => PlaySFX(EnemyDieClip);
    public void PlayPlayerShield() => PlaySFX(PlayerShieldClip);
    

}
