public enum CardTypes {
    TARP,
    ENERGY_BAR,
    FIRELIGHTER,
    UNCOOKED_BEANS,
    COOKED_BEANS,
    TREE,
    CAMPFIRE,
    AXE,
}

public static class CardsData {
    public static CardModel getTarp() {
        CardModel card = new CardModel();
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        card.energyChange = -1;
        return card;
    }

    public static CardModel getEnergyBar() {
        CardModel card = new CardModel();
        card.energyChange = 3;
        return card;
    }

    public static CardModel getFirelighter() {
        CardModel card = new CardModel();
        card.energyChange = -1;
        card.remainingUses = 1;
        return card;
    }

    public static CardModel getUncookedBeans() {
        CardModel card = new CardModel();
        card.energyChange = -1;
        return card;
    }

    public static CardModel getCookedBeans() {
        CardModel card = new CardModel();
        card.energyChange = 3;
        return card;
    }

    public static CardModel getTree() {
        CardModel card = new CardModel();
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        return card;
    }

    public static CardModel getCampfire() {
        CardModel card = new CardModel();
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        return card;
    }

    public static CardModel getAxe() {
        CardModel card = new CardModel();
        card.attack = 1;
        card.energyChange = -1;
        return card;
    }
}
