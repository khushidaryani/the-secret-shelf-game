const express = require('express');
const router = express.Router();
const { Book } = require('../models');

// Middleware to parse the body of requests (necessary for POST requests)
//app.use(express.json());

// Get all books
router.get('/', async (req, res) => {
  try {
    const books = await Book.find();
    res.json(books);
  } catch (err) {
    res.status(500).send("Error while retrieving books: " + err);
  }
});

// Add a new book
router.post('/', async (req, res) => {
  const { ISBN, title, author, genre, hints } = req.body;

  if (!Array.isArray(hints) || hints.length !== 4) {
    return res.status(400).send("The 'hints' field must be an array of exactly 4 strings.");
  }

  const book = new Book({ ISBN, title, author, genre, hints });

  try {
    await book.save();
    res.status(201).send("Book successfully added");
  } catch (err) {
    res.status(500).send("Error while adding the book: " + err);
  }
});

module.exports = router;