const WebSocket = require("ws");

const wss = new WebSocket.Server({ port: 8080 });

const state = {
  players: {
    p1: { id: "p1", x: -3, y: -1.5, facing: 1, state: "Idle" },
    p2: { id: "p2", x: 3, y: -1.5, facing: -1, state: "Idle" },
  },
};

console.log("WebSocket server running on ws://localhost:8080");

wss.on("connection", (ws) => {
  console.log("Client connected");

  sendSnapshot(ws);

  ws.on("message", (raw) => {
    const text = raw.toString();
    console.log("Received:", text);

    let msg;
    try {
      msg = JSON.parse(text);
    } catch (err) {
      console.error("Invalid JSON");
      return;
    }

    if (msg.type === "input") {
      applyInputToP1(msg);
      sendSnapshot(ws);
    }
  });

  ws.on("close", () => {
    console.log("Client disconnected");
  });
});

function applyInputToP1(input) {
  const p1 = state.players.p1;
  const speed = 0.15;

  p1.x += input.moveX * speed;

  if (input.moveX > 0) {
    p1.facing = 1;
    p1.state = "Move";
  } else if (input.moveX < 0) {
    p1.facing = -1;
    p1.state = "Move";
  } else {
    p1.state = "Idle";
  }

  const p2 = state.players.p2;
  p2.facing = p1.x < p2.x ? -1 : 1;
}

function sendSnapshot(ws) {
  const payload = {
    type: "snapshot",
    players: [state.players.p1, state.players.p2],
  };

  ws.send(JSON.stringify(payload));
}
