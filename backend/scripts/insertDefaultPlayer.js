require('dotenv').config();
const mongoose = require('mongoose');
const { Player } = require('../models');

const uri = process.env.MONGODB_URI;

const player = [
  {
    name: "Librarian",
    coins: 5,
  }
];

mongoose.connect(uri)
  .then(async () => {
    console.log("Connected to MongoDB");

    // Clear existing player if needed
    await Player.deleteMany({});

    // Insert all players
    await Player.insertMany(player);
    console.log("Player inserted successfully");

    mongoose.disconnect();
  })
  .catch(err => {
    console.error("Connection error:", err);
  });