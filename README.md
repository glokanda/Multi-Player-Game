


# 🎮 Multiplayer Game – Netcode Assignment

A multiplayer game made with **Unity 6+** and **Netcode for GameObjects**. Players can connect, move, and interact in a shared 3D space.

---

## 🎯 Game Purpose

The purpose of this game is to **learn how to make a multiplayer game**.
Players can **see each other, move around, and shoot fireballs**.
This project teaches **networking, synchronization, and multiplayer interaction**.

---

## ✨ Game Features

* **Multiplayer Connectivity**: Host or join games using an IP address
* **Player Movement**: Smooth WASD controls with animations
* **Player Customization**: Each player has a unique name and color
* **Shooting Mechanics**: Fireballs to interact with other players
* **Network Sync**: Player positions, names, and colors are the same for everyone

---

## 📋 Requirements

* Unity **6000.0.41f1** or newer
* **Netcode for GameObjects** package (included)

---

## 🚀 How to Play

### Host a Game

1. Open the project in Unity
2. Open the `NetcodeMultiplayerTest` scene
3. Press **Play**
4. Click **Host**
5. Share your IP with friends

### Join a Game

1. Build the game (File → Build Settings → Build)
2. Run the game
3. Click **Client**
4. Enter the host’s IP
5. Click **Connect**

### Controls

* **WASD** → Move
* **Mouse** → Aim
* **Left Click** → Shoot fireballs

---

## 🔧 Technical Details

### Network Features

* **NetworkObjects** → Players and fireballs are networked
* **NetworkVariables** → Stores player names and colors
* **RPCs** → Client-server communication for updates
* **Scene Sync** → All clients see the same scene

### Key Scripts

| Script              | Function                          |
| ------------------- | --------------------------------- |
| `PlayerMovement.cs` | Moves player and plays animations |
| `PlayerSettings.cs` | Sets player names and colors      |
| `ShootFireBall.cs`  | Handles shooting mechanics        |
| `MoveProjectile.cs` | Moves fireballs over network      |
| `NetworkUI.cs`      | UI for hosting/joining games      |

---

## 📦 Building the Game

1. Open **File → Build Settings**
2. Add `NetcodeMultiplayerTest` scene to "Scenes in Build"
3. Choose your target platform
4. Click **Build**
5. Select an output folder

---

## 🧪 Testing Multiplayer

### Test on One Computer

1. Run the game in Unity as **Host**
2. Build and run the game as **Client**
3. Both should see each other in the game

### Test on Multiple Computers

1. Host on one computer (find local IP with `ipconfig` or `ifconfig`)
2. Connect from other computers on the same network
3. Ensure all players are visible

---

## 📁 Project Structure

* **Scenes** → `NetcodeMultiplayerTest`
* **Scripts** → `PlayerMovement`, `PlayerSettings`, `ShootFireBall`, `MoveProjectile`, `NetworkUI`
* **Prefabs** → Player and Fireball objects


