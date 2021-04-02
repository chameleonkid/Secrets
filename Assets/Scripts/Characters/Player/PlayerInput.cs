using UnityEngine;

public struct PlayerInput {
    public Vector2 direction;
    public bool run;

    public bool attack;
    public bool lamp;

    public bool spellCast1;
    public bool spellCast2;
    public bool spellCast3;

    public void ClearAll() {
        direction = Vector2.zero;
        ClearBools();
    }

    public void ClearBools() {
        run = false;

        attack = false;
        lamp = false;

        spellCast1 = false;
        spellCast2 = false;
        spellCast3 = false;
    }
}
