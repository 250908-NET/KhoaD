This project is a video game catalog designed for managing video games, their genres, and the platforms they run on.

Stretch goals includes implementing a rating system that allows users to provide reviews and score games.

```mermaid
erDiagram
    GAME {
        int GameId PK
        string Title
        string Developer
        int ReleaseYear
    }

    GENRE {
        int GenreId PK
        string Name
    }

    PLATFORM {
        int PlatformId PK
        string Name
    }

    GAMEGENRE {
        int GameId FK
        int GenreId FK
    }

    GAMEPLATFORM {
        int GameId FK
        int PlatformId FK
    }

    GAME ||--o{ GAMEGENRE : has
    GENRE ||--o{ GAMEGENRE : categorizes
    GAME ||--o{ GAMEPLATFORM : runs_on
    PLATFORM ||--o{ GAMEPLATFORM : supports