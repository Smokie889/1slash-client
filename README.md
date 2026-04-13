# 1slash monorepo (Unity + Backend + DB)

這個 repo 現在採用 monorepo：Unity client 在 `apps/unity-client`，backend 在 `apps/backend`。

## Structure

- `apps/unity-client`：Unity 專案
- `apps/backend`：backend（Node + ws）
- `infra/db`：資料庫 migration / seed / init 預留目錄
- `packages/shared-contracts`：前後端共用 DTO/schema 預留目錄

## Quick start

### 1) Unity

1. 用 Unity Hub 打開 `apps/unity-client`。
2. Scene 建議使用：`apps/unity-client/Assets/_project/Scenes/Duel.unity`。
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

- Unity 與 backend 在同一個 repo，開發和追問題更直覺。
- `.gitignore` 已防止 Unity `Library/Temp/Build` 等大型產物被推進 repo。
- 後續擴充 DB、shared contracts 比較順。
