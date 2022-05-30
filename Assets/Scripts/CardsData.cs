using System.Collections.Generic;
using UnityEngine;

public enum CardTypes {
    TARP,
    ENERGY_BAR,
    FIRELIGHTER,
    UNCOOKED_BEANS,
    COOKED_BEANS,
    TREE,
    CAMPFIRE,
    AXE,
    WOOD,
}

public static class CardsData {
    public static bool IsFoodCard(GameObject card) {
        List<CardTypes> foodCards = new List<CardTypes>{
            CardTypes.UNCOOKED_BEANS,
            CardTypes.COOKED_BEANS,
            CardTypes.ENERGY_BAR
        };

        var cardScript = card.GetComponent<Card>();
        if (foodCards.Contains(cardScript.data.type)) {
            return true;
        }

        return false;
    }

    public static bool IsFuelCard(GameObject card) {
        List<CardTypes> fuelCards = new List<CardTypes>{
            CardTypes.WOOD
        };

        var cardScript = card.GetComponent<Card>();
        if (fuelCards.Contains(cardScript.data.type)) {
            return true;
        }

        return false;
    }

    public static bool IsCookableCard(GameObject card) {
        List<CardTypes> fuelCards = new List<CardTypes>{
            CardTypes.UNCOOKED_BEANS
        };

        var cardScript = card.GetComponent<Card>();
        if (fuelCards.Contains(cardScript.data.type)) {
            return true;
        }

        return false;
    }

    public static bool IsFireLightingCard(GameObject card) {
        List<CardTypes> fuelCards = new List<CardTypes>{
            CardTypes.FIRELIGHTER
        };

        var cardScript = card.GetComponent<Card>();
        if (fuelCards.Contains(cardScript.data.type)) {
            return true;
        }

        return false;
    }

    public static bool IsCampfire(GameObject card) {
        var cardScript = card.GetComponent<Card>();
        if (cardScript.data.type == CardTypes.CAMPFIRE) {
            return true;
        }

        return false;
    }

    public static bool IsTree(GameObject card) {
        var cardScript = card.GetComponent<Card>();
        if (cardScript.data.type == CardTypes.TREE) {
            return true;
        }

        return false;
    }

    public static CardModel getTarp() {
        CardModel card = new CardModel();
        card.type = CardTypes.TARP;
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        card.energyChange = -1;
        return card;
    }

    public static CardModel getEnergyBar() {
        CardModel card = new CardModel();
        card.type = CardTypes.ENERGY_BAR;
        card.energyChange = 3;
        return card;
    }

    public static CardModel getFirelighter() {
        CardModel card = new CardModel();
        card.type = CardTypes.FIRELIGHTER;
        card.energyChange = -1;
        card.remainingUses = 1;
        return card;
    }

    public static CardModel getUncookedBeans() {
        CardModel card = new CardModel();
        card.type = CardTypes.UNCOOKED_BEANS;
        card.energyChange = 1;
        return card;
    }

    public static CardModel getCookedBeans() {
        CardModel card = new CardModel();
        card.type = CardTypes.COOKED_BEANS;
        card.energyChange = 3;
        return card;
    }

    public static CardModel getTree() {
        CardModel card = new CardModel();
        card.type = CardTypes.TREE;
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        return card;
    }

    public static CardModel getCampfire() {
        CardModel card = new CardModel();
        card.type = CardTypes.CAMPFIRE;
        card.attack = 0;
        card.health = 3;
        card.defense = 0;
        return card;
    }

    public static CardModel getAxe() {
        CardModel card = new CardModel();
        card.type = CardTypes.AXE;
        card.attack = 1;
        card.energyChange = -1;
        return card;
    }

    public static CardModel getWood() {
        CardModel card = new CardModel();
        card.type = CardTypes.WOOD;
        card.health = 1;
        card.energyChange = -1;
        return card;
    }
}
