# Unity Card Match Prototype

This repository contains a functional prototype of a card-matching game developed in **Unity 2021 LTS** as part of a technical assignment.

The focus of this project is **clean gameplay logic, system architecture, and performance**, rather than visual polish.

---

## 🎮 Gameplay Overview

- Grid-based card matching game
- Supports dynamic layouts (e.g. 2x2, 4x4, 5x6)
- Cards flip with animation
- Matching pairs remain open
- Mismatched pairs flip back after a delay
- Game completes only when all cards are matched

---

## ✅ Features Implemented

### Core Gameplay
- Dynamic card grid generation
- Card flip animation (non-blocking)
- Randomized card pair shuffling
- Match / mismatch detection
- Win condition detection

### Systems
- MatchSystem – Handles card comparison and win logic
- ScoreManager – Scoring and combo handling
- AudioManager – Flip, match, mismatch, and win sounds
- UIManager – End-game popup UI
- GameManager – Restart flow and system coordination

### UI & Feedback
- Real-time score display
- End-game popup with final score
- Restart button (no scene reload)
- Sound effects for:
  - Card flip
  - Match
  - Mismatch
  - Game complete

### Technical
- Built from scratch (no purchased assets)
- Clean separation of systems
- No runtime errors or warnings
- Frequent, meaningful git commits

---

## 🛠️ Controls

- Click / Tap: Flip card
- Restart Button: Restart game after completion

---

## How to Run

1. Open the project using **Unity 2021 LTS**
2. Open the main scene
3. Press Play

---

## 📂 Project Structure

Assets/
├── _Scripts/
│ ├── _Gameplay/
│ ├── _Systems/
├── _Audios/
├── _Prefabs/
├── _Scenes/
└── _UI/


---

## 📌 Notes

- Designed to run on Desktop and Mobile
- Visuals kept minimal to emphasize code quality
- Architecture designed to be easily extendable

---

## 👤 Author

Developed by: Rishika Yadav
