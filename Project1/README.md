This project is a video game catalog designed for managing video games, their genres, and the platforms they run on.

Stretch goals includes implementing a rating system that allows users to provide reviews and score games.

```mermaid
ERD
    Game {
        int GameId PK
        string Title
        string Developer
        int ReleaseYear
    }
    Genre {
        int GenreId PK
        string Name
    }
    Platform {
        int PlatformId PK
        string Name
    }
    GameGenre {
        int GameId FK
        int GenreId FK
    }
    GamePlatform {
        int GameId FK
        int PlatformId FK
    }

    Game ||--o{ GameGenre : "has"
    Genre ||--o{ GameGenre : "categorizes"
    Game ||--o{ GamePlatform : "runs on"
    Platform ||--o{ GamePlatform : "supports"