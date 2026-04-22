public enum AlertState
{
    Clear, // 0     — personne ne fait attention
    Watched, // 1-30  — un PNJ regarde Andy avec insistance
    Suspicious, // 31-60 — un gardien se déplace vers Andy
    Hot, // 61-85 — appel radio en cours, 5s pour fuir la zone
    Caught // 86+   — mission compromise, Andy est arrêté
}
