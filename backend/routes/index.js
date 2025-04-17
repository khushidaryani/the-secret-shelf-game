const express = require('express');
const booksRoutes = require('./books');
const playersRoutes = require('./players');

const router = express.Router();

router.use('/books', booksRoutes);
router.use('/player', playersRoutes);

module.exports = router;