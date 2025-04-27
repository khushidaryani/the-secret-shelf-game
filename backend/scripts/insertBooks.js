require('dotenv').config();
const mongoose = require('mongoose');
const { Book } = require('../models');

const uri = process.env.MONGODB_URI;

const books = [
  {
    ISBN: "9781405293181",
    title: "A Good Girl's Guide to Murder",
    author: "Holly Jackson",
    genre: "Mystery",
    hints: [
      "This book is full of crime, suspense and mystery.",
      "The main character is a young girl who becomes involved in solving a mysterious murder.",
      "The plot is set in a small town, and the story delves into social dynamics and secrets.",
      "The book's title is a clue - it's about murder."
    ]
  },
  {
    ISBN: "9780385121675",
    title: "The Shining",
    author: "Stephen King",
    genre: "Horror",
    hints: [
      "This book is set in a haunted hotel during the winter.",
      "The protagonist is a man who slowly loses his sanity as the hotel's dark past emerges.",
      "It was adapted into a famous movie directed by Stanley Kubrick.",
      "The story explores themes of isolation, mental illness, and the supernatural."
    ]
  },
  {
    ISBN: "9781501143786",
    title: "Needful Things",
    author: "Stephen King",
    genre: "Horror",
    hints: [
      "The novel is set in a small town and revolves around a mysterious store.",
      "A group of people find themselves trapped inside a store and face dark forces beyond their control.",
      "A store is connected to a sinister force that manipulates people's desires and fears.",
      "Stephen King is the author, known for writing horror and supernatural thrillers."
    ]
  },
  {
    ISBN: "9788408292975",
    title: "Cuando el cielo se vuelva amarillo (When The Sky Turns Yellow)",
    author: "Nerea Pascual",
    genre: "Romance",
    hints: [
      "The title refers to a vivid and symbolic color in the sky.",
      "The story revolves around personal growth and finding oneself.",
      "The book focuses on the emotional and psychological journey of the main character.",
      "The novel is written by a young Spanish author."
    ]
  },
  {
    ISBN: "9788416588435",
    title: "Invisible",
    author: "Eloy Moreno",
    genre: "Contemporary Fiction",
    hints: [
      "This book is often read in high schools to raise awareness of bullying.",
      "It discusses themes like school indifference and peer pressure.",
      "The main character pretends to have superpowers as a coping mechanism.",
      "The protagonist believes becoming invisible is the only way to survive."
    ]
  },
  {
    ISBN: "9781784161101",
    title: "The Girl On The Train",
    author: "Paula Hawkins",
    genre: "Thriller",
    hints: [
      "The protagonist is a woman who travels on a train every day and becomes obsessed with a couple she watches.",
      "The story is told through the perspectives of multiple women, each with their own dark secrets.",
      "The plot is filled with unreliable narrators, making it hard to trust anyone's version of the story.",
      "The main character witnesses something from the train."
    ]
  },
  {
    ISBN: "9781529029581",
    title: "Before The Coffee Gets Cold",
    author: "Toshikazu Kawaguchi",
    genre: "Fantasy",
    hints: [
      "You can go back in time — but only until the coffee gets cold.",
      "The story revolves around a café with a special time-traveling twist.",
      "This book involves time travel, but with a unique rule about how the past can be revisited.",
      "It's originally a Japanese novel."
    ]
  },
  { 
    ISBN: "9781250301697",
    title: "The Silent Patient",
    author: "Alex Michaelides",
    genre: "Psychological Thriller",
    hints: [
      "A woman becomes mute after allegedly killing her husband.",
      "The story focuses on a therapist trying to unravel a woman's silence and uncover the truth.",
      "The story is told by the main character's therapist.",
      "It's a psychological thriller with heavy elements of mystery."
    ]
  },
  {
    ISBN: "9781534467620",
    title: "Better Than The Movies",
    author: "Lynn Painter",
    genre: "Romance",
    hints: [
      "The title compares real life to fiction.",
      "The protagonist is a girl obsessed with romantic movies who begins to fall for a guy who isn't her usual type.",
      "The book plays with romantic tropes and subverts expectations, making you question the idea of 'perfect love.'",
      "The main character wants her life to be like a movie."
    ]
  },
  {
    ISBN: "9780241594933",
    title: "Everyone In My Family Has Killed Someone",
    author: "Benjamin Stevenson",
    genre: "Mystery",
    hints: [
      "The story is about a family where every member has committed a crime.",
      "The protagonist is an investigator who has to figure out which family member is guilty of murder.",
      "The novel is filled with dark humor, family secrets, and a twisted mystery.",
      "The title gives a hint — it's a mystery revolving around a family."
    ]
  },
  {
    ISBN: "9780575094185",
    title: "Do Androids Dream of Electric Sheep?",
    author: "Philip K. Dick",
    genre: "Science Fiction",
    hints: [
      "It explores the blurred lines between humans and artificial beings in a dystopian future.",
      "The story dives into existential questions about humanity, empathy, and artificial intelligence.",
      "This book inspired the movie Blade Runner.",
      "It's a science fiction classic, questioning what it means to be human."
    ]
  },
  {
    ISBN: "9788432242823",
    title: "Tres enigmas para la Organización (Three Enigmas for the Organization)",
    author: "Eduardo Mendoza",
    genre: "Comedy Mystery",
    hints: [
      "The story involves a series of mysteries that need to be solved.",
      "It takes place within a peculiar organization and focuses on its strange inner workings.",
      "The book is filled with humor, satire, and twists that make it hard to predict the outcome.",
      "This Spanish author is known for combining mystery and absurd humor."
    ]
  },
  {
    ISBN: "9788491291916",
    title: "Un cuento perfecto (A Perfect Story)",
    author: "Elisabeth Benavent",
    genre: "Romance",
    hints: [
      "The main character escapes a perfect life and begings a journey of self-discovery.",
      "This is a romantic novel about finding love in unexpected places.",
      "The book deals with complex themes of vulnerability, self-worth, and forgiveness.",
      "Once upon a time there was a perfect story."
    ]
  },
  {
    ISBN: "9788466678865",
    title: "Donde viven las musas (Land of Muses)",
    author: "Marianela dos Santos",
    genre: "Poetry",
    hints: [
      "This book is a collection of poems exploring the beauty of life and art.",
      "The poems delve into themes of creativity, nature and the human experience.",
      "It often reflects on deep philosophical and existential themes.",
      "It's written by a Latin American poet known for her emotive language."
    ]
  }
];

mongoose.connect(uri)
  .then(async () => {
    console.log("Connected to MongoDB");

    // Clear existing books if needed
    await Book.deleteMany({});

    // Insert all books
    await Book.insertMany(books);
    console.log("Books inserted successfully");

    mongoose.disconnect();
  })
  .catch(err => {
    console.error("Connection error:", err);
  });