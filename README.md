# WriteToOutlook

Tämä koodi on tuotettu copilotilla

# M365 Calendar – Delegated sample (.NET 9, Device Code)

Tämä on pieni konsoliprojekti, joka **luo tapahtuman omaan Outlook-kalenteriisi** Microsoft Graph
in avulla **delegoiduilla oikeuksilla** (Device Code -kirjautuminen).

## Vaatimukset
- .NET SDK 9.0
- Microsoft Entra ID -sovellus (App registration)
  - Delegoitu API-oikeus: `Calendars.ReadWrite`
  - **Authentication → Allow public client flows = Yes** (Device Code)

Docs:
- https://learn.microsoft.com/en-us/graph/sdks/create-client
- https://learn.microsoft.com/en-us/graph/api/calendar-post-events?view=graph-rest-1.0

## Asennus
```bash
cd m365-calendar-delegated-sample
export GRAPH_CLIENT_ID="<YOUR_CLIENT_ID>"
export GRAPH_TENANT_ID="common"

dotnet restore
dotnet build

dotnet run -- "Projektipalaveri" "2025-11-04T08:00:00"
```

## Description

Tämän koodib on copilot kirjoittanut Kajaanin vierailujen synkronoimiseksi outlookkiin, slottiin, näytöille ja kenties portillekin.

## Visuals
Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation

See Vaatimukset above

## Usage

Not targeted to real use. Just test that I can access outlook.

## Support
Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap
Saattaa kehittyä integraatiovälineeksi

## Contributing
Copilot 100%


## License
MIT, see license file.

## Project status
Waiting for entra tenant.
