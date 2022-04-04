public enum CardTypes {
    TARP,
    ENERGY_BAR,
    FIRELIGHTER,
}

public static class CardsData {
    public static CardModel getTarp() {
        CardModel card = new CardModel();
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        card.energyConsumed = 1;
        return card;
    }

    public static CardModel getFood() {
        CardModel card = new CardModel();
        card.energyRestored = 3;
        return card;
    }

    public static CardModel getFirelighter() {
        CardModel card = new CardModel();
        card.energyConsumed = 1;
        card.remainingUses = 1;
        return card;
    }
}
