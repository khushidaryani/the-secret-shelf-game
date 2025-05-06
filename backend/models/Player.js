const mongoose = require('mongoose');

// Player schema
const playerSchema = new mongoose.Schema({
  name: {
    type: String,
    unique: true,
    required: true,
  },
  coins: {
    type: Number,
    default: 5,
    min: [0, 'Coins cannot be negative'],
  },
});

module.exports = mongoose.model('Player', playerSchema);