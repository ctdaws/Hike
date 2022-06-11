using System.Collections.Generic;
using UnityEngine;

public enum CardTypes {
    CAMP,
    TARP,
    ENERGY_BAR,
    FIRELIGHTER,
    UNCOOKED_BEANS,
    COOKED_BEANS,
    TREE,
    CAMPFIRE,
    AXE,
    WOOD,
    WOLF,
}

public static class CardsData {
    public static CardModel getCamp() {
        CardModel card = new CardModel();
        card.type = CardTypes.CAMP;
        card.health = 1;
        return card;
    }

    public static CardModel getTarp() {
        CardModel card = new CardModel();
        card.type = CardTypes.TARP;
        card.health = 3;
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
        card.health = 1;
        card.defense = 0;
        return card;
    }

    public static CardModel getCampfire() {
        CardModel card = new CardModel();
        card.type = CardTypes.CAMPFIRE;
        card.attack = 0;
        card.health = 1;
        card.defense = 0;
        card.lifetime = 6;
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

    public static CardModel getWolf() {
        CardModel card = new CardModel();
        card.type = CardTypes.WOLF;
        card.health = 2;
        card.attack = 2;
        return card;
    }

    public static bool IsCampCard(GameObject card) {
        List<CardTypes> campCards = new List<CardTypes>{
            CardTypes.CAMP
        };

        var cardScript = card.GetComponent<Card>();
        if (campCards.Contains(cardScript.data.type)) {
            return true;
        }

        return false;
    }

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

}
