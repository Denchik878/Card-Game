# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Unity 2D turn-based card game. Engine: **Unity 6000.0.25f1** (Unity 6). Gameplay scripts live in `Assets/Scripts/`. Scenes: `Assets/Scenes/` (`EpicCardFight.unity` is the main fight scene; `GoodBoy.unity` / `Sucker.unity` are win/lose screens loaded via `SceneManager.LoadScene`).

Build/run/test is performed through the Unity Editor — there is no standalone CLI build script in the repo. Open the project in Unity Hub and press Play to test.

## Core architecture

The game is structured around a turn-state machine and a component-based card/effect system. Understanding these two axes is essential before editing.

### Turn state machine (`GameManager.cs`)

`GameManager` is a `DontDestroyOnLoad` singleton (`GameManager.Instance`) holding a `GameState` enum: `PlayerTurn`, `EnemyTurn`, `PlayerAnimaton`, `EnemyAnimaton`, `Pause`. State transitions are driven by `ChangeState` and consumed by:

- `Player.FinishTurn()` — awaits every owned `Card.BaseTurn()`, then flips to `EnemyTurn`.
- `EnemyAI.Update()` — polls for `EnemyTurn`, switches to `EnemyAnimaton`, runs all enemy card turns, calls `Spawner.Spawn()`, then flips back to `PlayerTurn`.
- `Weapon.OnMouseDown/OnMouseUp` — gates drag-to-attack on `PlayerTurn` / `PlayerAnimation`.
- `DisposeCard` on both sides — **waits for the opposite side's turn** before destroying a card, so you must not mutate `activeCards` mid-iteration from a death handler. If you add new turn-side logic, preserve this invariant.

Most multi-frame gameplay logic is `async Awaitable` (Unity 6's `Awaitable.NextFrameAsync()`), not coroutines. New animations/effects should follow that pattern.

### Card hierarchy

`Card` (abstract, `Assets/Scripts/Cards/Card.cs`) owns health, damage, crystal counter, effect-icon layout, and the `BaseTurn()` loop that applies all attached `CardEffect` components before calling the virtual `Turn()`.

Two main branches:
- **`Weapon`** (`Cards/Weapon.cs`) — player hand cards. Implements drag-to-target (`OnMouseDown` → spawns `dragPrefab`, `OnMouseUp` → `Physics2D.OverlapPoint` to find a target `Card`). Subclasses override `Damage(Card enemyCard)` (`AxeCard`, `BowCard`, `MaceLightningCard`, `HealCard`, `SpellCard`, `HealAbility`, `AddCrystal`). `SpellCard`-style weapons apply a `CardEffect` to the target rather than directly dealing damage.
- **`TableCard`** (`Cards/TableCards/TableCard.cs`) — board cards. `OnMouseDown` during `PlayerTurn` spawns `weaponPrefab` via `Player.CreateWeapon` and finishes the turn. Subclasses: `CardMinion`, `HerzCard` (player core — its death triggers game over), `BossCard` (enemy core — its death triggers victory).

`Player.activeCards` and `EnemyAI.activeCards` are the authoritative rosters. `Card.OnDeath` fires from `DestroySelf()`; both owners subscribe in `Start`/`OnEnable` and unsubscribe in `OnDisable`. The `HerzCard`/`BossCard` win/lose hooks are wired separately in `Start`.

### Effect system (`Assets/Scripts/GameEffects/`)

`CardEffect` (abstract MonoBehaviour) is attached alongside a `Card`. `Card.BaseTurn()` finds all enabled `CardEffect` components via `GetComponents<CardEffect>()` and calls `TakeEffect()` each turn. Effects track instances in a `List<EffectArgs>` (damage, turnsDura, turnsDelay, icon GameObject). `AddEffect` spawns an icon prefab that `Card.AddEffectIcon`/`RemoveEffectIcon` positions via a precomputed 5×4 grid (`IconPositions`). Concrete effects: `PoisonEffect`, `HealEffect`, `Curse`, `Marked`, `Stun`, `OnDeath`, `UnTouched`.

To add a new effect: subclass `CardEffect`, attach it to the card prefab(s) that should be able to receive it, and have the originating `Weapon.Damage` call `target.GetComponent<NewEffect>().AddEffect(args)` (see `SpellCard.cs`).

### Spawning and points

`Point` (`Point.cs`) is a board slot holding a `currentCard` reference. `Player.points` and `Spawner.points` are both `GetComponentsInChildren<Point>`. `Spawner.Spawn()` fills empty points using a weighted random (`CardWeight[] prefabs`) — bias is a simple integer `weight`.

### Audio

`AudioManager.Instance.PlayClip(clip)` is the single entry point. Cards expose `takeDamageSound`/`healingSound`; weapons expose `grabClip`/`strikeClip`.

## Editing conventions observed in this codebase

- Some identifiers and log strings are in Russian (e.g. `GameState.EnemyAnimaton` — note the misspelling — and `print("Ты лох")` in `Player.GameOver`). Preserve existing names when editing; do not silently rename `EnemyAnimaton` → `EnemyAnimation` as it's referenced across `Player`, `EnemyAI`, and scene-serialized data.
- `.unity` scenes and prefabs reference scripts by GUID; renaming a MonoBehaviour class or file breaks prefab bindings. If a rename is intentional, update the `.cs.meta` GUID mapping or re-link in the Editor.
- Gameplay timing uses `async Awaitable` + `Time.deltaTime`, not `Coroutine`/`IEnumerator`. Match that style in new code.
- `ArrayTest.cs` at `Assets/ArrayTest.cs` is a scratch file (currently untracked).