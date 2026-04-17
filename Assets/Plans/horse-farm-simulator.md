# Horse Farm Simulator - Implementation Plan

## Project Overview
- **Game Title:** Horse Farm Simulator
- **High-Level Concept:** A 3D simulation game where players manage a horse farm, care for horses (feeding/washing), train them on obstacle courses, and compete in timed races.
- **Players:** Single player.
- **Inspiration:** Star Stable, Disney animated features.
- **Tone / Art Direction:** Cartoon "Disney Style" (stylized 3D, vibrant colors, expressive animations).
- **Target Platform:** StandaloneOSX (PC).
- **Screen Orientation:** Landscape (Desktop).
- **Render Pipeline:** Universal Render Pipeline (URP).

## Game Mechanics
### Core Gameplay Loop
1. **Management Phase:** Purchase and upgrade stables to house more horses. Buy new horse breeds/colors using points/currency earned from races.
2. **Care Phase:** Maintain horse health and happiness by feeding and washing them.
3. **Training Phase:** Practice on the track and field area, jumping over obstacles (planks, puddles) to improve horse stats.
4. **Racing Phase:** Participate in timed races. Points are awarded based on completion time and jump accuracy.

### Controls and Input Methods (New Input System)
- **Movement (WASD/Arrows):** Controls horse direction and speed.
- **Jump (Space):** Triggers the jump animation and physics-based leap over obstacles.
- **Interact (E):** Triggers care activities (feeding, washing) when near a horse or stable.
- **UI Navigation (Mouse):** Manage stables and shop menus.

## UI
- **Management HUD:** Currency display, stable capacity, and current horse stats.
- **Stable Menu (UI Toolkit):** List of owned horses, upgrade buttons, and shop interface.
- **Care Interface:** Interactive buttons for "Wash" and "Feed".
- **Racing HUD:** Timer, points counter, and speed indicator.

## Key Asset & Context
- **Scripts:**
    - `HorseData (ScriptableObject)`: Stores breed, color, speed, jump height, and care status.
    - `HorseController`: Handles movement, jumping, and animation state switching.
    - `StableManager`: Manages stable upgrades and horse assignments.
    - `RaceSystem`: Manages checkpoints, timers, and scoring.
- **Prefabs:**
    - `Horse_Base`: Stylized 3D horse with Animator.
    - `Stable_Module`: Modular stable building.
    - `Obstacle_Plank`, `Obstacle_Puddle`: Physics-enabled obstacles.
- **Cinemachine:**
    - `VirtualCamera_Follow`: Follows the horse during movement.

## Implementation Steps

### Phase 1: Foundation & Data
1.  **Install Cinemachine 3.1:** Ensure the package is present in the project.
2.  **Define Data Structures:**
    - Create `HorseData` ScriptableObject.
    - Create `StableData` ScriptableObject.
3.  **Setup Input Actions:** Configure "Move", "Jump", and "Interact" in `InputActions.inputactions`.

### Phase 2: Horse & Environment
1.  **Generate Assets (Disney Style):**
    - Generate stylized 3D Horse models with multiple texture variations (using `GenerateAsset`).
    - Generate Stable and Obstacle models.
2.  **Horse Controller Implementation:**
    - Implement `HorseController` using `CharacterController` or `Rigidbody`.
    - Set up `AnimatorController` with Idle, Walk, Run, and Jump states.
3.  **Camera Setup:** Configure Cinemachine Virtual Camera to follow the player horse.

### Phase 3: Care & Management Systems
1.  **Stable Management:** Implement the logic for buying and upgrading stables.
2.  **Care Activities:** Create simple triggers and UI for feeding and washing.
3.  **UI Development (UI Toolkit):** Build the Management Menu and HUD.

### Phase 4: Training & Racing
1.  **Obstacle Course:** Design a track with jump triggers.
2.  **Race Logic:** Implement `RaceSystem` for timing laps and calculating points.
3.  **Testing & Balancing:** Tune movement speed and jump physics for a "cartoon" feel.

## Verification & Testing
- **Movement Test:** Verify horse moves smoothly and animations sync with speed.
- **Jump Test:** Ensure the horse can clear planks but "fails" (slows down) if hitting a puddle or plank.
- **UI Test:** Check that buying a stable correctly updates the capacity.
- **Race Test:** Complete a lap and verify the timer stops and points are awarded.
