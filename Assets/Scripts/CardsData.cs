public enum CardTypes {
    TARP,
    FOOD,
}

public static class CardsData {
    public static CardModel getTarp() {
        CardModel card = new CardModel();
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        return card;
    }

    public static CardModel getFood() {
        CardModel card = new CardModel();
        card.energyRestored = 3;
        return card;
    }
}
