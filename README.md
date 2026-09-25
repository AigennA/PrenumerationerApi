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
| POST | `/api/prenumerationer/{id}/logo` | Laddar upp en logga till en prenumeration |
| POST | `/api/prenumerationer/{id}/document` | Laddar upp en fil, till exempel ett kvitto eller avtal |

Filen `PrenumerationerApi.http` innehåller färdiga exempelanrop för alla endpoints utom filuppladdning,
som enklast testas i Swagger.

## Filuppladdning
- **Logga:** JPG, PNG, WebP eller GIF
- **Fil:** PDF, JPG, PNG eller WebP
- **Max storlek:** 25 MB per fil
- Filerna sparas i mappen `uploads/`, som skapas automatiskt när API:et startar och inte finns i repot.
  De visas via `http://localhost:5175/uploads/...`.

## Tekniska val
- **Data sparas i en lista i minnet** i stället för en databas. Det räcker för uppgiften
  och gör att projektet kan startas direkt utan databasinstallation. Datan återställs vid omstart.
- **CORS** är konfigurerat för http://localhost:5173 (webbappen) och http://localhost:8081 (mobilappen när den körs i webbläsaren med Expo)
  från en annan port.
- **DateOnly** används för start- och slutdatum eftersom en prenumeration bara behöver
  ett datum, inte en tid.
- **Filhanteringen ligger i en egen klass (`Services/FileStorage.cs`)** så att controllern
  bara hanterar HTTP-anrop och båda uppladdnings-endpoints kan använda samma kod.
- **Uppladdade filer får unika namn** så att två filer med samma namn inte skriver över varandra.
  Originalnamnet sparas separat och visas i listan.
- **Filtyp och storlek kontrolleras i API:et** så att till exempel körbara filer eller
  mycket stora filer stoppas.
