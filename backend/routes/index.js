const express = require('express');
const booksRoutes = require('./books');
const playersRoutes = require('./players');

const router = express.Router();

router.use('/books', booksRoutes);
router.use('/players', playersRoutes);

module.exports = router;