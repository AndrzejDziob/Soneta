using RepositoryInfoAddon;
using RepositoryInfoAddon.UI;
using Soneta.Business.UI;


[assembly: FolderView("Zadanie rekrutacyjne",
        Priority = 0,
        Description = "Autor: Andrzej Dziób", // opcjonalne: opis poniżej tytułu kafla
        BrickColor = FolderViewAttribute.BlueBrick,
        Icon = "TableFolder.ico"
)]


[assembly: FolderView("Zadanie rekrutacyjne/Repozytorium",
    Priority = 10,
    Description = "Dane z repozytorium git",
    ObjectType = typeof(RepositoryPageForm),
    ObjectPage = "RepositoryPageForm.ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]