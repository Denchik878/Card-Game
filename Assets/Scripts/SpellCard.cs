using UnityEngine;

public class SpellCard : Weapon
{
    public async override Awaitable Turn()
    {
        await DestroySelf();
    }
}
