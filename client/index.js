const express = require('express');
const app = express();
const path = require('path');

app.use(express.static(path.join(__dirname, 'dist/client')));

app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'dist/client/index.html'));
});

const port = process.env.PORT || 5001;
app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});