const mongoose = require('mongoose');
const uri = process.env.MONGODB_URI; // URL to connect to MongoDB Atlas

const clientOptions = {
  serverApi: {
    version: '1',
    strict: true,
    deprecationErrors: true
  }
};

const connectDB = async () => {
  try {
    await mongoose.connect(uri, clientOptions);
    console.log("You successfully connected to MongoDB!");
  } catch (error) {
    console.error("Error connecting to MongoDB:", error);
  }
};

module.exports = connectDB;
