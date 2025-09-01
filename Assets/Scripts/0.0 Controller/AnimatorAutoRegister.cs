using UnityEngine;

[System.Serializable]
public class TargetEntry
{
    public AnimatorTarget target;
    public Animator animator; 
}

public class AnimatorAutoRegister : MonoBehaviour
{
    [SerializeField] private TargetEntry[] entries;

    private void OnEnable()
    {
        if (AnimationManager.Instance == null)
        {
            Debug.LogWarning($"[AnimatorAutoRegister] AnimationManager chưa sẵn sàng, skip register {gameObject.name}");
            return;
        }

        foreach (var entry in entries)
        {
            var anim = entry.animator ?? GetComponent<Animator>();
            if (anim != null)
                AnimationManager.Instance.RegisterAnimator(entry.target, anim);
        }
    }

    private void OnDisable()
    {
        if (AnimationManager.Instance == null) return;

        foreach (var entry in entries)
        {
            if (entry.animator != null)
                AnimationManager.Instance.UnregisterAnimator(entry.target, entry.animator);
        }
    }
}
