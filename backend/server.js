const mongoose = require('mongoose');
const express = require('express');
const { Book, Player } = require('./models');
const app = express();
const port = 3000;

// URL to connect to MongoDB Atlas
const uri = "mongodb+srv://khushidaryani:<password>@the-secret-shelf-game.qhztmjw.mongodb.net/game?appName=the-secret-shelf-game";

// Option settings for MongoDB (for a stable API)
const clientOptions = { serverApi: { version: '1', strict: true, deprecationErrors: true } };

// Connection to MongoDB Atlas
mongoose.connect(uri, clientOptions)
    .then(() => {
        console.log("You successfully connected to MongoDB!");
    })
    .catch((err) => {
        console.error("Error while connecting to MongoDB:", err);
    });

// Middleware to parse the body of requests (necessary for POST requests)
app.use(express.json());

// Route to get all books
app.get('/books', async (req, res) => {
    try {
        const books = await Book.find();
        res.json(books);
    } catch (err) {
        res.status(500).send("Error while retrieving books: " + err);
    }
});

// Route to add a new book (POST request)
app.post('/books', async (req, res) => {
    const { ISBN, title, author, genre, firstHint, secondHint } = req.body;
    const book = new Book({ ISBN, title, author, genre, firstHint, secondHint });

    try {
        await book.save();
        res.status(201).send("Book successfully added");
    } catch (err) {
        res.status(500).send("Error while adding the book: " + err);
    }
});

// Route to get a player by their name
app.get('/player/:name', async (req, res) => {
    try {
        const player = await Player.findOne({ name: req.params.name });
        if (!player) {
            return res.status(404).send("Player not found");
        }
        res.json(player);
    } catch (err) {
        res.status(500).send("Error while retrieving the player: " + err);
    }
});

/// Route to update the player's points
app.post('/player/:name/points', async (req, res) => {
    try {
        const player = await Player.findOne({ name: req.params.name });
        if (!player) {
            return res.status(404).send("Player not found");
        }
        player.points += req.body.points;
        await player.save();
        res.send("Points successfully updated");
    } catch (err) {
        res.status(500).send("Error while updating points: " + err);
    }
});

// Start the server on port 3000
app.listen(port, () => {
    console.log(`Servidor de API escuchando en http://localhost:${port}`);
});

// async function run() {
//   try {
//     // Create a Mongoose client with a MongoClientOptions object to set the Stable API version
//     await mongoose.connect(uri, clientOptions);
//     await mongoose.connection.db.admin().command({ ping: 1 });
//     console.log("Pinged your deployment. You successfully connected to MongoDB!");
//   } finally {
//     // Ensures that the client will close when you finish/error
//     await mongoose.disconnect();
//   }
// }
// run().catch(console.dir);