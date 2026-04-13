// Temporary backend bootstrap for local integration tests.
// You can replace this file with your real server implementation later.

const http = require('http');
const { WebSocketServer } = require('ws');

const PORT = Number(process.env.PORT || 8080);

const server = http.createServer((req, res) => {
  if (req.url === '/health') {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({ ok: true }));
    return;
  }

  res.writeHead(404, { 'Content-Type': 'application/json' });
  res.end(JSON.stringify({ error: 'Not found' }));
});

const wss = new WebSocketServer({ server });

wss.on('connection', (socket) => {
  console.log('[ws] client connected');

  socket.on('message', (raw) => {
    const text = raw.toString();
    console.log('[ws] recv:', text);

    // Echo a fake snapshot so Unity client can test receiving pipeline.
    socket.send(JSON.stringify({
      type: 'snapshot',
      tick: Date.now(),
      players: [
        { id: 'p1', x: -3, y: -1.5, facing: 1, state: 'Idle' },
        { id: 'p2', x: 3, y: -1.5, facing: -1, state: 'Idle' }
      ]
    }));
  });

  socket.on('close', () => {
    console.log('[ws] client disconnected');
  });
});

server.listen(PORT, () => {
  console.log(`[backend] listening on http://localhost:${PORT}`);
});
