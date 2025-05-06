const mongoose = require('mongoose');

function arrayLimit(val) {
  return val.length === 3;
}

// Book schema with an array of 3 hints
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
  hints: {
    type: [String], // Array of strings
    validate: [arrayLimit, '{PATH} must have exactly 3 hints'],
    required: true,
  }
});

module.exports = mongoose.model('Book', bookSchema);