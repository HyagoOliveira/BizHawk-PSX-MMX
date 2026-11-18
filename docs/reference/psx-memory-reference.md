# PSX Game Memory Reference

## Scope

- Target: USA releases of Mega Man X4, X5, and X6.
- Memory values are BizHawk PSX `MainRAM` offsets, not executable or process addresses.
- Keep addresses and layouts separate per game. Verified mappings belong in `src/Core/Game.cs` and `src/Core/MemoryAddress.cs`; do not embed game-specific addresses in forms or UI code.
- Reconstructed fork data is a reference, not original source code. Names marked `unk*` and inferred meanings require runtime verification.

## Implementation status

| Game | USA identifier | Base mappings |
|---|---|---|
| MMX4 | Hash `314E06A8` | Player, camera, boss flags, and main-object pool |
| MMX5 | Hash `1C64D6EA` | Player, camera, boss flags, and main-object pool |
| MMX6 | Hash `F063F536` | Player, camera, boss flags, and main-object pool |

MMX4 is the only game with a documented detailed object-name table and reconstructed object-layout reference in this repository. MMX5 and MMX6 currently share only runtime-tested/shared findings, such as the player collision-box pointer offsets; detailed decompilation layouts must be added only after they are confirmed for the corresponding USA build.

## Local extracted data

`ROOMS_Extractions/` is Git-ignored and contains separate `MMX4/`, `MMX5/`, and `MMX6/` folders with disc and executable data. Use it for local investigation of room, asset, and game-specific data, but do not modify or commit its files. Extracted data is not a validated memory map; verify conclusions against save states or live gameplay.

## MMX4 globals and object pools

| Symbol | `MainRAM` offset | Layout | C# model |
|---|---:|---|---|
| `g_Player` | `0x1418C8` | One `PlayerObj`, `0xE4` bytes | `Player` |
| `main_objects` | `0x13BED0` | 48 `MainObj` slots, `0x9C` bytes each | `MainObj` pool |
| `shot_objects` | `0x13F328` | 32 `ShotObj` slots, `0x9C` bytes each | `ShotObj` pool |
| `weapon_objects` | `0x1406F8` | 16 `WeaponObj` slots, `0x9C` bytes each | `WeaponObj` pool |
| `background_objects` | `0x1419B0` | 3 `BackgroundObj` slots, `0x54` bytes each | `BackgroundObj` pool |

These are separate pools. `main_objects` can contain enemies and other objects; it is not enemy-only storage.

In MMX4, `StageObjectRecord.object_type` in `src/main/195B4.c` selects the pool:

| `object_type` | Pool |
|---:|---|
| `0` | `main_objects` |
| `1` | `weapon_objects` |
| `2` | Visual |
| `3` | Effect |
| `4` | Item |
| `5` | Miscellaneous |
| `6` | Quad |
| `7` | Layer |

The record `id` selects behavior within the pool. Main-object IDs index `main_object_update_funcs`; IDs are pool-specific. `0xFF` terminates the stage-object list.

`GameMemoryAddress.Get` supplies `GetMainObjectName` names. Only MMX4 currently has a name table; unknown IDs and unsupported games display `UNKNOWN MAIN OBJECT`.

## Shared MMX4 object fields

| Field | Object offset | Type | C# |
|---|---:|---|---|
| `active` | `+0x00` | `s8` | `sbyte` |
| `id` | `+0x01` | `s8` | `sbyte` |
| `unk2` | `+0x02` | `s8` | `sbyte` |
| `on_screen` | `+0x03` | `s8` | `sbyte` |
| `state` | `+0x04` | `s8` | `sbyte` |
| `unk5`, `unk6`, `unk7` | `+0x05`-`+0x07` | `s8` | `sbyte` |
| `x_pos`, `y_pos` | `+0x08`, `+0x0C` | Signed 16.16 `f32` | `Fixed16Vector2Memory` |
| `backref` | `+0x10` | 32-bit PS1 pointer | `uint` |
| `bg_offset` | `+0x14` | `s8` | `sbyte` |
| `unk15` | `+0x15` | `u8` | `byte` |
| `unk16`, `unk17` | `+0x16`, `+0x17` | `u8` | `byte` |

The `unk15` field is used as horizontal facing/direction by some routines, but its meaning is not established for every object. `f32` is a 32-bit union containing a signed value, not an IEEE-754 `float`; position and player velocity use signed 16.16 fixed-point representation. Divide the raw signed integer by `65,536` to obtain the real value.

Do not assume that the same offset has the same type or meaning in another layout:

| Layout | Field | Object offset | Type |
|---|---|---:|---|
| `PlayerObj` | `x_vel`, `y_vel` | `+0x20`, `+0x24` | Signed 16.16 `f32` |
| `MainObj` | `x_speed`, `y_speed` | `+0x20`, `+0x24` | Signed 32-bit |
| `MainObj` | `x_accel` | `+0x28` | Signed 32-bit |
| `MainObj` | `gravity` | `+0x2C` | Signed 32-bit |
| `PlayerObj`, `MainObj` | `hp` | `+0x5C` | Signed 8-bit |

The current position reader returns a `Vector2` while retaining raw signed values through integer reads. Player velocity and main-object speed use separate mappings.

## Player collision boxes

The MMX4, MMX5, and MMX6 USA builds share hurt-box and terrain-box pointer offsets `+0x54` and `+0x68` in their respective player structures. Their player base addresses remain game-specific.

The analyzer reads the four-byte records and renders hurt and terrain boxes in red and green. A null pointer means the box is inactive. Hurt-box dimensions are interpreted as directional extensions and terrain-box dimensions as half-extents. This geometry comes from the MMX4 reconstruction and is not independently validated for MMX5 or MMX6.

## Player projectiles

Player-fired shots use `weapon_objects`; `shot_objects` is used by other projectiles, including enemy bullets. `player_spawn_weapon` allocates a free `WeaponObj` and stores the shot type in `id`.

Useful MMX4 `PlayerObj` firing fields:

| Field | Offset | Type |
|---|---:|---|
| `attacking`, `shot_fired`, `attack_ended` | `+0x8E`-`+0x90` | `s8` |
| `attack_pose_timer`, `shot_cooldown` | `+0x91`, `+0x92` | `u8` |
| `weapon`, `shot_types[0..1]`, `shot_type`, `last_shot_type` | `+0x93`-`+0x97` | `s8` |
| `shot_count`, `special_shot_count`, `shot_cycle` | `+0x98`-`+0x9A` | `s8` |

## MMX4 Zero state

X and Zero share `g_Player` and `PlayerObj`.

| Field | Offset | Address | Type | Meaning |
|---|---:|---:|---|---|
| `unk2` | `+0x02` | `0x1418CA` | `s8` | Zero/non-Zero routing flag |
| `unk5` | `+0x05` | `0x1418CD` | `s8` | Action state; Zero actions use `0x30`-`0x3B` |
| `unk6` | `+0x06` | `0x1418CE` | `s8` | Action substate |
| `spike_immune` | `+0x7A` | `0x141942` | `s8` | Spike immunity during selected techniques |
| `air_action` | `+0x86` | `0x14194E` | `s8` | Air-move state; double jump uses `6` |
| `weapon_energy[0]` | `+0xA8` | `0x141970` | `s8` | Rakuhouha check/cost |
| `boss_flags` | `+0xB9` | `0x141981` | bitmask | Zero ability flags |
| `combo` | `+0xBB` | `0x141983` | `s8` | Sabre combo progress |

`boss_flags` masks: `0x01` Raijingeki, `0x02` Hyouretsuzan, `0x04` double jump/spin slash, `0x08` Ryuenjin, `0x20` Rakuhouha, `0x80` Shippuuga.

`SetAllWeaponsEnabled` writes `0xFF` or `0x00` to `boss_flags`. Clearing it removes ability flags and does not restore progress or refill weapon energy.

## MMX4 X charge and armor

| Field | Offset | Address | Type | Meaning |
|---|---:|---:|---|---|
| `charge_state[0..1]` | `+0x9B`, `+0x9C` | `0x141963`, `0x141964` | `s8` | Charge state: `0` none, `1` partial, `2` full |
| `charge_timer`, `special_charge_timer` | `+0x9D`, `+0x9E` | `0x141965`, `0x141966` | `u8` | Charge duration in frames |
| `stock_charge` | `+0xA6` | `0x14196E` | `s8` | Stocked buster charge |
| `armor_parts` | `+0xA7` | `0x14196F` | bitmask | Observed armor effects |
| `weapon_energy[0..15]` | `+0xA8`-`+0xB7` | `0x141970`-`0x14197F` | `s8` | Weapon-energy values |
| `arm_type` | `+0xB8` | `0x141980` | `u8` | Charge behavior |
| `nova_strike_active`, `nova_strike_timer` | `+0xE0`, `+0xE1` | `0x1419A8`, `0x1419A9` | `s8` | Nova Strike state |

The 16 weapon-energy slots include slot 0 used by Zero and X logic. The analyzer exposes all slots through `WeaponEnergy` and shows slot 0 by default.

Observed `armor_parts` masks: `0x01` special-weapon energy cost, `0x02` Nova Strike, `0x04` linked special-weapon charging, `0x08` hover. Reliable armor-part names are unavailable. `arm_type` values `0`, `1`, and `2` select charge behavior but do not have reconstructed armor-set names.

Under `arm_type == 1` and `weapon == 0`, stock-charge thresholds `0x5A`, `0x78`, and `0x96` raise `stock_charge` to 2, 3, and 4.

## MMX4 object animation

The `ANIMATED_OBJ_FIELDS` layout is:

| Field | Offset | Type | Meaning |
|---|---:|---|---|
| `unk17` | `+0x17` | `u8` | Current animation index |
| `animation_table` | `+0x30` | 32-bit PS1 pointer | Per-object animation start pointers |
| `animation_cursor` | `+0x34` | 32-bit PS1 pointer | Current animation step |
| `animation_step` | `+0x44` | 4 bytes | Copy of the current step |
| `previous_animation_index` | `+0x48` | `u8` | Set to `0xFF` by `set_animation` |

`AnimationStep` at `+0x44` contains `duration` (`+0x44`, `s8`), `event` (`+0x45`, `s8`), `relative_step` (`+0x46`, `s8`), and `frame_index` (`+0x47`, `u8`). Save-state checks cover `+0x34`, `+0x44`-`+0x48`, and the shared `+0x17` field is reconstructed without save-state validation. Rendering use of `frame_index` is unverified; the analyzer labels it `Sprite`.

`set_animation` points `animation_cursor` at `animation_table[index]` and copies the step. `animate_object` decrements `duration`; at zero, it advances by `relative_step` four-byte steps and copies the next step. A positive `relative_step` advances, a negative value loops back, and zero reloads the current step. The zero and negative cases are inferred and need runtime testing. The analyzer reports `Frame current/total` as `total - duration + 1`.

`event` is interpreted by each object's behavior code. For Trident Mech animation `0xD`, save-state coverage is incomplete:

| `event` | Effect from reconstructed code |
|---:|---|
| `1` | Restore the body box and damage `3`, face the player, set animation `0`, and end the attack |
| `2` | Set `ext.main_16.unk88 = 1`, play a sound, and alternate the attack box each frame |

Event `2` alternates body box `(-11, -17, 19, 47)`, damage `3`, with spear box `(-89, -1, 69, 13)`, damage `6`. The event sequence is reconstructed; step-by-step save-state verification is pending.
