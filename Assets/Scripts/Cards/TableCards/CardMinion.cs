using UnityEngine;

public class CardMinion : Card
{
    private float progress;
    public AnimationCurve curve;
    public float duration;
    private bool isKilled = true;
    protected override async Awaitable Turn()
    {
        if(crystalAmount <= 1)
        {
            var target = FindAnyObjectByType<HerzCard>();
            await Animation(target.transform);
            target.ChangeHealth(-damage);
            isKilled = false;
            await DestroySelf();
        }
        else
        {
            ChangeCrystalAmount(-1);
        }
    }

    protected async override Awaitable DestroySelf()
    {
        if (isKilled)
        {
            GameManager.Instance.valuta += valutaErned;
        }
        base.DestroySelf();
    }

    private async Awaitable Animation(Transform target)
    {
        if (progress > 0)
            return;
        Vector3 current = Vector3.zero;
        Vector3 start = transform.position;
        while (progress < 1)
        {
            progress += Time.deltaTime / duration;
            progress = Mathf.Clamp01(progress);
            current = Vector3.LerpUnclamped(start, target.position, curve.Evaluate(progress));
            transform.position = current;
            await Awaitable.NextFrameAsync();
        }
        progress = 0;
    }
}
