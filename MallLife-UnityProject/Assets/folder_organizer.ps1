param(
    [string]$CheminBase = "."
)

# Structure souhaitée : numéro et nom
$structure = @(
    @{Numero = "00"; Nom = "Imports_Packs"},
    @{Numero = "01"; Nom = "Scenes"},
    @{Numero = "02"; Nom = "Prefabs"},
    @{Numero = "03"; Nom = "Scripts"},
    @{Numero = "04"; Nom = "Animations"},
    @{Numero = "05"; Nom = "Behaviors"},
    @{Numero = "06"; Nom = "Rendering"},
    @{Numero = "07"; Nom = "Audio"},
    @{Numero = "08"; Nom = "Settings"}
)

Write-Host "Organisation des dossiers dans : $(Resolve-Path $CheminBase)`n" -ForegroundColor Cyan

# Scanner les dossiers existants
$dossiersExistants = @{}
if (Test-Path $CheminBase) {
    Get-ChildItem -Path $CheminBase -Directory | ForEach-Object {
        $nomSansNumero = if ($_.Name -match "^\d+ - (.+)$") { $matches[1] } else { $_.Name }
        $dossiersExistants[$nomSansNumero] = $_.Name
    }
}

# Traiter chaque dossier de la structure
foreach ($dossier in $structure) {
    $nomCible = "$($dossier.Numero) - $($dossier.Nom)"
    $cheminCible = Join-Path $CheminBase $nomCible
    
    if ($dossiersExistants.ContainsKey($dossier.Nom)) {
        $ancienNom = $dossiersExistants[$dossier.Nom]
        $cheminAncien = Join-Path $CheminBase $ancienNom
        
        if ($ancienNom -ne $nomCible) {
            try {
                Rename-Item -Path $cheminAncien -NewName $nomCible -ErrorAction Stop
                Write-Host "[RENOMME] '$ancienNom' -> '$nomCible'" -ForegroundColor Yellow
            }
            catch {
                Write-Host "[ERREUR] Impossible de renommer '$ancienNom' : $_" -ForegroundColor Red
            }
        }
        else {
            Write-Host "[OK] '$nomCible'" -ForegroundColor Green
        }
    }
    else {
        try {
            New-Item -Path $cheminCible -ItemType Directory -Force | Out-Null
            Write-Host "[CREE] '$nomCible'" -ForegroundColor Green
        }
        catch {
            Write-Host "[ERREUR] Impossible de créer '$nomCible' : $_" -ForegroundColor Red
        }
    }
}

Write-Host "`nOrganisation terminée !" -ForegroundColor Cyan