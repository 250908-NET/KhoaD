A **.NET 9 Web API** for managing a video game catalog, including **Games**, **Platforms**, and their relationships.  

## API Endpoints

### Games
- `GET /games` → list all games  
- `GET /games/{id}` → get game by ID  
- `POST /games` → create a game  
- `PUT /games/{id}` → update a game  
- `DELETE /games/{id}` → delete a game  

### Platforms
- `GET /platforms` → list all platforms  
- `GET /platforms/{id}` → get platform by ID  
- `POST /platforms` → create a platform  
- `PUT /platforms/{id}` → update a platform  
- `DELETE /platforms/{id}` → delete a platform  

### Relationships
- `POST /games/{gameId}/platforms/{platformId}` → link game to platform  
- `GET /games/{gameId}/platforms` → get platforms for a game  
- `GET /platforms/{platformId}/games` → get games for a platform  

---

## Example JSONs

### Create a Game
```json
{
  "title": "Final Fantasy VI",
  "developer": "Square",
  "releaseYear": 1994
}
```

### Create a Platform
```json
{
  "name": "Super Nintendo",
  "manufacturer": "Nintendo",
  "releaseYear": 1994
}
```

### Stretch Goals
- Add a **user rating system** for games  
- Add **genres** table with many-to-many relationships  
- Authentication & authorization  





References:
https://mermaid.js.org/syntax/entityRelationshipDiagram.html

```mermaid
ERD
    GAME {
        int GameId PK
        string Title
        string Developer
        int ReleaseYear
    }

    PLATFORM {
        int PlatformId PK
        string Name
        string Manufacturer
        int ReleaseYear
    }

    GAMEPLATFORM {
        int GameId FK
        int PlatformId FK
    }

    GAME ||--o{ GAMEPLATFORM : has
    PLATFORM ||--o{ GAMEPLATFORM : supports


