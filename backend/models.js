const mongoose = require('mongoose');

const bookSchema = new mongoose.Schema({
  ISBN: {
    type: String,
    required: true,
    unique: true,
  },
  title: {
    type: String,
    required: true,
  },
  author: {
    type: String,
    required: true,
  },
  genre: {
    type: String,
    required: true,
  },
  firstHint: {
    type: String,
    required: true,
  },
  secondHint: {
    type: String,
    required: true,
  },
});

const Book = mongoose.model('Book', bookSchema);

const playerSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  points: {
    type: Number,
    default: 0,
  },
  level: {
    type: Number,
    default: 1,
  },
});

const Player = mongoose.model('Player', playerSchema);

module.exports = { Book, Player };