This project is a video game catalog designed for managing video games and the platforms they run on.

Stretch goals includes implementing a rating system that allows users to provide reviews and score games and adding a genre table.

References:
https://mermaid.js.org/syntax/entityRelationshipDiagram.html

```mermaid
erDiagram
    GAME {
        int GameId PK
        string Title
        string Developer
        int ReleaseYear
    }

    PLATFORM {
        int PlatformId PK
        string Name
    }

    GAMEPLATFORM {
        int GameId FK
        int PlatformId FK
    }

    GAME ||--o{ GAMEPLATFORM : runs_on
    PLATFORM ||--o{ GAMEPLATFORM : supports

