# Todo API - CI/CD Showcase

Dit project dient als een demonstratie van een volledige **CI/CD pipeline** voor een .NET 10 Web API. Het combineert geautomatiseerd testen, code-kwaliteitscontroles en continue deployment naar een cloud-omgeving.

## CI/CD Pipeline Overzicht

De automatisering is opgedeeld in twee hoofdonderdelen: Continuous Integration (GitHub Actions) en Continuous Deployment (Render).

### 1. Continuous Integration (GitHub)
Bij elke push of pull request naar de `main` branch wordt de **GitHub Actions** workflow geactiveerd (`.github/workflows/ci.yml`). Deze voert de volgende stappen uit:

- **Build**: Controleert of de code succesvol compileert.
- **Linting**: Gebruikt `dotnet format` om te verifiëren of de code voldoet aan de geconfigureerde stijlregels.
- **Testing**: Voert automatische unit tests uit (XUnit) om de logica van de `ToDoController` te valideren.
- **Artifact Generation**: Bij een succesvolle build op de `main` branch wordt een release-artifact aangemaakt.

### 2. Continuous Deployment (Render)
De applicatie is live gekoppeld aan **Render**. Zodra de CI-tests op de `main` branch succesvol zijn afgerond, wordt de deployment automatisch gestart:

- **Docker-gebaseerd**: Render gebruikt de `Dockerfile` in de root van het project om een container te bouwen.
- **Automatische Migraties**: Bij het opstarten van de container voert de API automatisch de nieuwste database-migraties uit op de SQLite-database.
- **Web-service**: De API wordt beschikbaar gesteld via een publieke URL verzorgd door Render.

## Projectstructuur

- **`Program.cs`**: De kern van de Web API met OpenAPI/Swagger ondersteuning en automatische database-migratie logica.
- **`todo.Tests`**: Bevat de unit tests die essentieel zijn voor de betrouwbaarheid van de CI-pipeline.
- **`Dockerfile`**: Definieert hoe de applicatie in een container wordt verpakt voor deployment.
- **`Migrations`**: Bevat de database-evolutie geschiedenis.

## Lokale Ontwikkeling

Hoewel de focus ligt op CI/CD, kan het project ook lokaal gedraaid worden:

```bash
dotnet restore
dotnet run
```

De Swagger UI is dan beschikbaar voor handmatig testen van de eindpunten.

## Testen Handmatig Uitvoeren

Om dezelfde tests te draaien die in de GitHub CI draaien:

```bash
dotnet test
```
