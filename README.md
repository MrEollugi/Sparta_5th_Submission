# Sparta_5th_Submission
for submission

현재 코드는 어느정도 작성해두었지만

손목 부상 및 교통 사고 처리 등의 이유로 미완성인 상태입니다.
정상적인 게임 플레이는 현재 불가능한 상태입니다.

작성한 코드가 제대로 작동하는지도 테스트 하지 못했습니다.

죄송합니다.

# RPG Core Systems (Unity)

This repository includes core gameplay systems for a Unity-based RPG prototype featuring inventory management, equipment enhancement, and FSM-based player control logic.

---

## Player Controller

- **States:** `Idle`, `Move`, `AutoMove`, `AutoAttack`
- Supports both **manual** and **auto** battle modes
- Input via Unity Input System + optional `MobileJoystick`
- Auto mode detects enemies and attacks using coroutine-based logic

---


## Inventory System

- **Tabs:** `Consumables` / `Equipments`
- Clicking a slot opens a detailed panel with options to:
  - Equip / Unequip
  - Enhance (if equipment)
  - Sell
- Equipped items show an "Equipped" marker in the inventory
- Quick-use items (e.g., potions) can be equipped via `QuickItemManager`

---

## Equipment Enhancement System

- Accessed through `EnhancePanel`
- Each item has upgrade levels (`UpgradeData`) with:
  - Upgrade cost
  - Success rate
- On success, the item's `upgradeLevel` increases
- UI shows color-coded result feedback (success/failure)

---

## Result Panel

- Appears after dungeon completion via `ResultPanel`
- Displays:
  - Gold and Crystal rewards
  - Item rewards with icons
- Items are dynamically spawned using `ItemSlotUI`

---

## ScriptableObject Structure

| Type                 | Description                                |
|----------------------|--------------------------------------------|
| `ItemSO`             | Base class for all items                   |
| `ConsumableSO`       | Heals or boosts stats temporarily          |
| `EquipmentSO`        | Base for weapons, armor, accessories       |
| `WeaponSO`           | Includes Ultimate & Active skills          |
| `ArmorSO` / `AccessorySO` | Specialized bonuses (e.g., HP, Crit)       |
| `SkillSO`            | Base for active and passive skills         |
| `PassiveEffectSO`    | Used in gear upgrades and skills           |
| `DungeonStageSO`     | Dungeon metadata and enemy spawn layout    |
| `EnemySO`            | Enemy prefab and stat definitions          |

---

## Managers

| Manager             | Role                                      |
|---------------------|-------------------------------------------|
| `InventoryManager`  | Manages adding/removing inventory items   |
| `GoldManager`       | Handles gold amount and spending          |
| `QuickItemManager`  | Controls equipped quick-use items         |
| `EnhanceManager`    | Logic for item enhancement success/fail   |
| `EquipmentManager`  | Applies and removes gear stats to player  |

---

## Dev Notes

- Code organized with `#region` for readability
- All major classes include inline documentation in English
- UI Panels are modular and single-responsibility
- Enhancement system is expandable with protection/fail logic

---

## Future Extensions

- Add experience and level-up system
- Add fail-penalties or enhancement protection items
- Support item sorting and filtering in Inventory UI
- Integrate stage select and story flow

---
