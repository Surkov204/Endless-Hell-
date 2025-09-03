using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RBArcBounce2D : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundNormalMinY = 0.5f;
    [SerializeField] float skin = 0.01f;

    [Header("Arc (độ cao đỉnh)")]
    [SerializeField] float startRadius = 4f;
    [SerializeField] float radiusDamping = 0.7f;      // giảm r sau mỗi cú nảy
    [SerializeField] float stopRadiusThreshold = 0.2f;

    [Header("Ngang")]
    [SerializeField] float horizontalFactor = 1.6f;   // vx = factor * vy
    [SerializeField] float horizontalDamping = 0.95f; // giảm đà ngang mỗi lần chạm

    [Header("Launch")]
    [SerializeField] bool autoLaunch = true;
    [SerializeField] bool launchRight = true;

    [Header("Safety")]
    [SerializeField] float postLaunchIgnoreTime = 0.03f; // thời gian bỏ qua va chạm ngay sau khi bật lên
    [SerializeField] float maxSpeed = 50f;

    Rigidbody2D rb;
    float r;
    float g;
    bool airborne = false;
    float ignoreUntil = -999f;
    int dirSign = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        g = Mathf.Abs(Physics2D.gravity.y) * Mathf.Max(0.0001f, rb.gravityScale);
        r = Mathf.Max(0.01f, startRadius);
        dirSign = launchRight ? 1 : -1;

        // Khuyến nghị: material của bóng bounciness=0
        var col = GetComponent<Collider2D>();
        if (col.sharedMaterial && col.sharedMaterial.bounciness > 0f)
            Debug.LogWarning("Ball PhysicsMaterial2D should have bounciness = 0 to avoid double bounce.");

        if (autoLaunch) Launch();
    }

    void Launch()
    {
        if (r < stopRadiusThreshold) { Stop(); return; }

        float vy = Mathf.Sqrt(2f * g * r);
        float vx = horizontalFactor * vy * dirSign;

        // nhấc lên khỏi đất 1 chút
        rb.position = new Vector2(rb.position.x, rb.position.y + skin);
        rb.linearVelocity = Vector2.ClampMagnitude(new Vector2(vx, vy), maxSpeed);

        airborne = true;
        ignoreUntil = Time.time + postLaunchIgnoreTime;
    }

    void Stop()
    {
        airborne = false;
        rb.linearVelocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!airborne) return;
        if (Time.time < ignoreUntil) return; // vừa launch xong, bỏ qua

        // chỉ xử lý khi là ground và đang đi xuống
        if (((1 << c.collider.gameObject.layer) & groundMask) == 0) return;
        if (rb.linearVelocity.y > 0f) return;

        // kiểm tra normal có hướng lên (tránh tường)
        bool onGround = false;
        Vector2 avg = Vector2.zero; int cnt = 0;
        foreach (var ct in c.contacts)
        {
            if (ct.normal.y >= groundNormalMinY) { onGround = true; avg += ct.point; cnt++; }
        }
        if (!onGround) return;
        if (cnt > 0) avg /= cnt;

        // snap lên mặt đất một chút để tách collider
        rb.position = new Vector2(rb.position.x, (cnt > 0 ? avg.y : rb.position.y) + skin);

        // cập nhật thông số cho cú nảy mới
        r *= radiusDamping;                     // giảm "bán kính"
        float prevVX = rb.linearVelocity.x;
        prevVX *= horizontalDamping;            // giảm đà ngang một chút
        dirSign = prevVX > 0 ? 1 : (prevVX < 0 ? -1 : dirSign);

        Launch();
    }
}
