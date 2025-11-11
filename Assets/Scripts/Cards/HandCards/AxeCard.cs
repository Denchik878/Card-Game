using UnityEngine;

public class AxeCard : Weapon
{
    private float progress;
    public float duration;
    public AnimationCurve curve;
    protected override async void Damage(Card enemyCard)
    {
        if (enemyCard.GetComponent<Card>().HPText == null)
        {
            CancelAttack();
            return;
        }

        await Animation(enemyCard.transform);
        enemyCard.ChangeHealth(-damage);
        player.FinishTurn();
        ChangeCrystalAmount(-1);
        if (crystalAmount == 0)
        {
            DestroySelf();
            return;
        }
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
