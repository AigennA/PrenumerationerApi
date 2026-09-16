# PrenumerationerApi

Backend (ASP.NET Web API) för webbappen [prenumerationer-app](https://github.com/AigennA/prenumerationer-app).
API:et hanterar prenumerationer, till exempel Netflix och Spotify, och om de är aktiva eller avslutade.

> **Viktigt:** Starta API:et först, innan webbappen startas.
> Webbappen hämtar data från API:et direkt när sidan öppnas.
>
> För att köra hela lösningen (backend och frontend), följ instruktionerna i
> [prenumerationer-app](https://github.com/AigennA/prenumerationer-app#kom-igång).

## Förutsättningar
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Kom igång
```
git clone https://github.com/AigennA/PrenumerationerApi.git
cd PrenumerationerApi
dotnet run
```
API:et startar på adressen som visas i terminalen: `Now listening on: http://localhost:5175`.
Swagger finns på http://localhost:5175/swagger.

## Endpoints

| Metod | URL | Beskrivning |
|---|---|---|
| GET | `/api/prenumerationer` | Hämtar alla prenumerationer |
| GET | `/api/prenumerationer/{id}` | Hämtar en prenumeration |
| POST | `/api/prenumerationer` | Skapar en ny prenumeration |
| PUT | `/api/prenumerationer/{id}` | Uppdaterar en prenumeration |
| DELETE | `/api/prenumerationer/{id}` | Tar bort en prenumeration |

Filen `PrenumerationerApi.http` innehåller färdiga exempelanrop för alla endpoints.

## Tekniska val
- **Data sparas i en lista i minnet** i stället för en databas. Det räcker för uppgiften
  och gör att projektet kan startas direkt utan databasinstallation. Datan återställs vid omstart.
- **CORS** är konfigurerat för http://localhost:5173 så att webbappen får anropa API:et
  från en annan port.
- **DateOnly** används för start- och slutdatum eftersom en prenumeration bara behöver
  ett datum, inte en tid.
