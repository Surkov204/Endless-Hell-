using UnityEngine;
using JS.Utils;
using System.Collections.Generic;

public class AnimationManager : ManualSingletonMono<AnimationManager>
{
    private Dictionary<AnimatorTarget, List<Animator>> animatorDict = new();

    public void RegisterAnimator(AnimatorTarget target, Animator animator)
    {
        if (animator == null) return;

        if (!animatorDict.ContainsKey(target))
            animatorDict[target] = new List<Animator>();

        if (!animatorDict[target].Contains(animator))
            animatorDict[target].Add(animator);

        Debug.Log($"[AnimationManager] Registered {target} -> {animator.gameObject.name}");
    }
    public void UnregisterAnimator(AnimatorTarget target, Animator animator)
    {
        if (animator == null) return;

        if (animatorDict.ContainsKey(target))
        {
            animatorDict[target].Remove(animator);
            if (animatorDict[target].Count == 0)
                animatorDict.Remove(target);
        }
    }

    public void Play(AnimationClipName clip, AnimatorParamType paramType, object value, AnimatorTarget target)
    {
        if (!animatorDict.TryGetValue(target, out var animators)) return;

        string paramName = clip.ToString();

        foreach (var animator in animators)
        {
            if (animator == null) continue;

            switch (paramType)
            {
                case AnimatorParamType.Trigger:
                    animator.ResetTrigger(paramName);
                    animator.SetTrigger(paramName);
                    break;

                case AnimatorParamType.Bool:
                    if (value is bool b) animator.SetBool(paramName, b);
                    break;

                case AnimatorParamType.Int:
                    if (value is int i) animator.SetInteger(paramName, i);
                    break;

                case AnimatorParamType.Float:
                    if (value is float f) animator.SetFloat(paramName, f);
                    break;
            }
        }
    }

    public void Play(AnimationClipName clip, AnimatorTarget target)
    {
        Play(clip, AnimatorParamType.Trigger, null, target);
    }
}

public enum AnimatorParamType { Trigger, Bool, Int, Float }

public enum AnimatorTarget
{
    CameraShaking,
    Canon,
    PlayerAttack,
    Enemy,
    Boss
}

public enum AnimationClipName
{
    ShakingByBullet,
    isCanonFire,
    Attack,
    Die,
    Speed,
    IsRunning
}