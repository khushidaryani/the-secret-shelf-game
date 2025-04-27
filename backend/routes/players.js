const express = require('express');
const router = express.Router();
const { Player } = require('../models');

// Get player by name
router.get('/:name', async (req, res) => {
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

// Update player points
router.post(':name/points', async (req, res) => {
  try {
    const player = await Player.findOne({ name: req.params.name });
    if (!player) return res.status(404).send("Player not found");

    player.points += req.body.points;
    await player.save();
    res.send("Points successfully updated", player); // Respond with updated player data
  } catch (err) {
    res.status(500).send("Error while updating points: " + err);
  }
});

// Update player progress
router.post('/:name/progress', async (req, res) => {
  try {
    const player = await Player.findOne({ name: req.params.name });
    if (!player) {
        return res.status(404).send("Player not found");
    }

    // Update level and points
    player.level = req.body.level;
    player.points = req.body.points;

    await player.save();
    res.send("Progress successfully updated", player); // Respond with updated player data
  } catch (err) {
    res.status(500).send("Error while updating progress: " + err);
  }
});

// Get player progress
router.get('/:name/progress', async (req, res) => {
  try {
    const player = await Player.findOne({ name: req.params.name });
    if (!player) {
        return res.status(404).send("Player not found");
    }
    res.json({ 
        points: player.points, 
        level: player.level 
    });
  } catch (err) {
    res.status(500).send("Error while retrieving the player's progress: " + err);
  }
});

module.exports = router;