using UnityEngine;

public class TestBulletController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] Vector2 _bulletspeed = default;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(_bulletspeed, ForceMode2D.Impulse);
    }
}
