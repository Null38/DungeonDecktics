using UnityEngine;
using TMPro;               

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float lifetime = 1f;  // 화면에 떠 있는 시간
    public float speed = 1f;  // 화면에 떠 있는 시간

    /// <summary>
    /// 팝업 초기화
    /// </summary>
    public void Init(int dmg)
    {
        damageText.text = $"-{dmg}";
        // 1초 뒤 자신을 파괴
        Destroy(gameObject, lifetime);
    }

    public void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
