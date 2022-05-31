[System.Serializable]
public class CardModel {
    public CardTypes type;
    public int health;
    public int attack;
    public int defense;
    // TODO: split this out into energy restored and energy cost, cooking beans should cost energy but consuming should restore energy
    public int energyChange;
    public int remainingUses;
    // TODO: do we need these bools, or do we need a card type system eg fuel cards and food cards etc
    public bool isAttackable;
    public bool isCookable;
    public int lifetime;
}
