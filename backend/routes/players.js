const express = require('express');
const router = express.Router();
const { Player } = require('../models');

// GET all players
router.get('/', async (req, res) => {
  try {
    const players = await Player.find();
    res.json(players);
  } catch (err) {
    res.status(500).send("Error while retrieving players: " + err);
  }
});

// GET player by name
router.get('/:name', async (req, res) => {
  try {
    const player = await Player.findOne({ name: req.params.name });
    if (!player) return res.status(404).send("Player not found");
    res.json(player);
  } catch (err) {
    res.status(500).send("Error retrieving player: " + err);
  }
});

// POST create new player
router.post('/', async (req, res) => {
  const { name, points } = req.body;

  if (!name) {
    return res.status(400).send("Name is required");
  }

  try {
    const existing = await Player.findOne({ name });
    if (existing) {
      return res.status(409).send("Player already exists");
    }

    const newPlayer = new Player({ name, points: points || 0 });
    await newPlayer.save();

    res.status(201).json(newPlayer);
  } catch (err) {
    res.status(500).send("Error creating player: " + err.message);
  }
});

// DELETE player by name
router.delete('/:name', async (req, res) => {
  try {
    const result = await Player.deleteOne({ name: req.params.name });
    if (result.deletedCount === 0) {
      return res.status(404).send("Player not found");
    }
    res.send("Player deleted successfully");
  } catch (err) {
    res.status(500).send("Error deleting player: " + err.message);
  }
});

// PATCH (update) player coins
router.patch('/:name/coins', async (req, res) => {
  const { coinsChange } = req.body;
  try {
    const player = await Player.findOne({ name: req.params.name });
    if (!player) return res.status(404).json({ message: 'Player not found' });

    player.coins = Math.max(0, player.coins + coinsChange);
    await player.save();
    res.json({ message: 'Coins updated', coins: player.coins });
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;