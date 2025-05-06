const express = require('express');
const router = express.Router();
const { Book } = require('../models');

// GET all books
router.get('/', async (req, res) => {
  try {
    const books = await Book.find();
    res.json(books);
  } catch (err) {
    res.status(500).send("Error while retrieving books: " + err);
  }
});

// GET book by ISBN
router.get('/:isbn', async (req, res) => {
  try {
    const book = await Book.findOne({ ISBN: req.params.isbn });
    if (!book) return res.status(404).send("Book not found");
    res.json(book);
  } catch (err) {
    res.status(500).send("Error retrieving book: " + err);
  }
});

module.exports = router;