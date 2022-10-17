# Notes

Course: https://www.youtube.com/watch?v=hGxr1yAb1gk

## SignalR

- transports
  - websockets
  - if websockets don't work, downgrades to server-sent events (comets)
  - finally will fall back to long polling (eventual poll the server for pending events)
