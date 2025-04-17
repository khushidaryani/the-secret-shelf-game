require('dotenv').config();
const express = require('express');
const connectDB = require('./configuration/database');
const routes = require('./routes');

const app = express();
const port = 3000;

connectDB();
app.use(express.json());
app.use('/', routes);

app.listen(port, () => {
  console.log(`Servidor de API escuchando en http://localhost:${port}`);
});