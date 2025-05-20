# The Secret Shelf
**The Secret Shelf** is a 2D puzzle game made as a part of a final project for Development in Multiplatform Applications (DAM).  
You play as a librarian helping customers find the perfect book by interpreting clues and navigating a cosy pixel-art library.

## 🎮 How To Play

1. Start a new game and enter your name.
2. A customer will walk into the library and give you a clue about the book they want (click **SPACE** to continue the dialogue).
3. Approach the bookshelves (using **WASD or arrow keys**) to open a list of available books.
4. Choose a book based on the clue. You only have **3 attempts** per client.
5. Your score will update depending on your performance:
   
   - 🥇 First try: +10 points
   - 🥈 Second try: +5 points
   - 🥉 Third try: +2 points
   - ❌ All wrong: -5 points

---

## 📚 Tech Stack

- **Unity** - Game engine used to build the 2D game
- **C#** - Programming language used for game logic and interactions in Unity
- **Node.js + Express** - Backend API for handling player and book data
- **MongoDB Atlas** - Remote NoSQL database storing player and book info
- **Mongoose** - ODM used in the backend to interact with MongoDB

---

## 📦 Requirements

To play or compile **The Secret Shelf**, here are the recommended **minimum** requirements:  

### 🖥️ Hardware  

- **Processor:** Intel Core i5 or AMD Ryzen 5
- **RAM**: 8GB minimum (16GB recommended)  
- **Storage:** At least 1.5GB of free disk space  
- **Display**: 1280x720 resolution or higher  
- **Operating System**: 64-bit (Windows 10/11)

### 💿 Software 

- **Unity Version:** `Unity 6.0 LTS`
- **Git**: For cloning the repository
- **Web browser:** Chrome, Firefox, Edge or any modern browser (only needed for optional API testing)

> ⚠️ Make sure you have Internet connection — the game fetches data from a hosted API which is linked to a remote database

---

## 💾 Backend Information

This game connects to a Node.js API hosted on [Render](https://render.com), which provides:

- Endpoints to fetch book data
- Endpoints to fetch player data
  
> 🌐 **API URL**: https://the-secret-shelf-api.onrender.com/  
> 🔗 No setup is required — the game connects to the backend automatically

### 📌 Available Endpoints

| Method | Endpoint                 | Description                        |
|--------|--------------------------|------------------------------------|
| GET    | `/books`                 | Returns all books                  |
| GET    | `/books/:isbn`           | Returns a book by ISBN             |
| GET    | `/players`               | Returns all players                |
| GET    | `/players/:name`         | Returns a player by name           |
| POST   | `/players`               | Creates a new player               |
| DELETE | `/players/:name`         | Deletes a player on game exit      |
| PATCH  | `/players/:name/coins`   | Updates a player's coins           |

---

## 🛠️ Compiling and Playing from Source

1. Clone this repository:  
   ```bash
   git clone https://github.com/khushidaryani/the-secret-shelf-game.git
   ```
   
   > 📁 Make sure to open a terminal in the folder where you want to save the project before running the `git clone` command.
   
2. Open the project in Unity Hub:  
   - Launch Unity Hub
   - Click "Open" and select the folder you just cloned
     
3. Let Unity compile the project, then click **Play** button (▶️) in the editor to start testing the game.
   
4. (Optional) To export the game:
   - Go to `File > Build Settings`
   - Select `Windows` and build
  
---
  
## 🎬 Credits  

> 😊 Thank you to these creators for their work!  

- **Pixel Art Assets**
  - [Modern Interiors - RPG Tileset](https://limezu.itch.io/moderninteriors) by LimeZu  
  - [Modern User Interface - RPG asset pack](https://limezu.itch.io/modernuserinterface) by LimeZu  

 - **Fonts**:
   - [Tiny 5](https://fonts.google.com/specimen/Tiny5) by Stefan Schmidt

 - **Tutorials**:
   - [List of tutorials followed](https://github.com/khushidaryani/the-secret-shelf-game/wiki/followed-tutorials)

---

## 🚫 No Contributions

This project was created as an **educational final assignment** and is not open to external contributions.  
Feel free to play, inspect or learn from the code, but **pull requests will not be accepted**. 

---

## 👩‍💻 Author

Developed by **Khushi Daryani** as part of the final project for DAM (2024/2025).

---

## 📥 Contact

If you have questions about the project for educational purposes or want to discuss similar projects, feel free to contact me at:  
📧 khushimdaryani@gmail.com

---

Thanks for checking out **The Secret Shelf**! ☕📖
