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

module.exports = router;