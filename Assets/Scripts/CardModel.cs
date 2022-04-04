[System.Serializable]
public class CardModel {
    public int health;
    public int attack;
    public int defense;
    public int energyRestored;
    public int energyConsumed;
    public int remainingUses;
    // TODO: do we need these bools, or do we need a card type system eg fuel cards and food cards etc
    public bool isAttackable;
    public bool isCookable;
}
