const mongoose = require('mongoose');

// Player schema
const playerSchema = new mongoose.Schema({
  name: {
    type: String,
    default: 'Librarian',
    required: true,
  },
  points: {
    type: Number,
    default: 5,
    min: [0, 'Points cannot be negative'],
  },
  level: {
    type: Number,
    default: 1,
    min: [1, 'Level must be at least 1'],
  },
});

module.exports = mongoose.model('Player', playerSchema);