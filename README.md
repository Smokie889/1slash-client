# 1slash monorepo (Unity + Backend + DB)

這個 repo 目前以 Unity client 為主，並先加入一個可跑的 backend scaffold，方便前後端同環境開發。

## Recommended structure

- `Assets/`, `Packages/`, `ProjectSettings/`：現有 Unity 專案
- `apps/backend`：暫時 backend（Node + ws）
- `infra/db`：資料庫 migration / seed / init 預留目錄
- `packages/shared-contracts`：前後端共用 DTO/schema 預留目錄

## Quick start

### 1) Unity

1. 用 Unity Hub 打開本專案根目錄。
2. Scene 建議使用：`Assets/_project/Scenes/Duel.unity`。
3. `WsClient` 預設會連 `ws://localhost:8080`。

### 2) Backend scaffold

```bash
cd apps/backend
npm install
npm run dev
```

啟動後：
- Health check: `http://localhost:8080/health`
- WebSocket: `ws://localhost:8080`

## About your existing test server

你可以把自己本機的 `server.js` 內容覆蓋到 `apps/backend/server.js`，保留這個路徑即可。

## Why this setup

- 讓只做 Unity 的同伴不會把 `Library/Temp/Build` 推進 repo。
- 前後端同一工作區好追問題。
- 後續擴充 DB、shared contracts 比較順。
