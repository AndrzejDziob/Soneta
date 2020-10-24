using RepositoryInfoAddon;
using RepositoryInfoAddon.UI;
using Soneta.Business.UI;

// Główny folder dodatku, umieszczony w głównym widoku bazy danych
[assembly: FolderView("Zadanie rekrutacyjne Release", // wymagane: to jest tekst na kaflu
        Priority = 0, // opcjonalne: Priority = 0 umieszcza kafel blisko lewej górnej strony widoku kafli
        Description = "Informacje o repozytorium git", // opcjonalne: opis poniżej tytułu kafla
        BrickColor = FolderViewAttribute.BlueBrick, // opcjonalne: Kolor kafla
        Icon = "TableFolder.ico" // opcjonalne: Ikona wyświetlana na kaflu
                                 // Więcej nie ma potrzeby definiować bo jest to kafel "organizacyjny" - przechodzący do widoku innych kafli
)]


[assembly: FolderView("Zadanie rekrutacyjne Release/Repozytorium",
    Priority = 10,
    Description = "Dane z repozytorium git",
    ObjectType = typeof(RepositoryPageForm),
    ObjectPage = "RepositoryPageForm.ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]