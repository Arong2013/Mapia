
[System.Serializable]
public class HealthStats : IStatComponent
{
    public Stat maxHp = new Stat(100);
    public Stat curHp = new Stat(100);
    public Stat hpRegenRate = new Stat(0.2f);
    public void RemoveModifiersFromSource(object source)
    {
        maxHp.RemoveAllModifiersFromSource(source);
        curHp.RemoveAllModifiersFromSource(source);
        hpRegenRate.RemoveAllModifiersFromSource(source);
    }
}

[System.Serializable]
public class CombatStats : IStatComponent
{
    public Stat attack = new Stat(10);          // 공격력
    public Stat defense = new Stat(5);          // 방어력
    public Stat physicalResistance = new Stat(5); // 물리 저항력
    public Stat magicResistance = new Stat(5);  // 마법 저항력
    public Stat criticalChance = new Stat(5);   // 치명타 확률
    public Stat accuracy = new Stat(80);        // 명중률
    public Stat evasion = new Stat(5);          // 회피율

    public void RemoveModifiersFromSource(object source)
    {
        attack.RemoveAllModifiersFromSource(source);
        defense.RemoveAllModifiersFromSource(source);
        physicalResistance.RemoveAllModifiersFromSource(source);
        magicResistance.RemoveAllModifiersFromSource(source);
        criticalChance.RemoveAllModifiersFromSource(source);
        accuracy.RemoveAllModifiersFromSource(source);
        evasion.RemoveAllModifiersFromSource(source);
    }
}

[System.Serializable]
public class SurvivalStats : IStatComponent
{
    public Stat satiety = new Stat(100);
    public Stat hungerRate = new Stat(1);
    public Stat experience = new Stat(0);
    public Stat level = new Stat(1);

    public void RemoveModifiersFromSource(object source)
    {
        satiety.RemoveAllModifiersFromSource(source);
        hungerRate.RemoveAllModifiersFromSource(source);
        experience.RemoveAllModifiersFromSource(source);
        level.RemoveAllModifiersFromSource(source);
    }
}

[System.Serializable]
public class MovementStats : IStatComponent
{
    public float smoothTime = 0.3f; // 기본값 설정 (필요에 따라 조정 가능)
    public Stat speed = new Stat(5f);  // 이동 속도
    public Stat range = new Stat(5);  // 공격 범위

    public void RemoveModifiersFromSource(object source)
    {
        speed.RemoveAllModifiersFromSource(source);
        range.RemoveAllModifiersFromSource(source);
    }
}

[System.Serializable]

public class Attributes : IStatComponent
{
    public Stat strengths = new Stat(10);     // 힘
    public Stat agility = new Stat(10);       // 민첩성
    public Stat intelligence = new Stat(10);  // 지능
    public Stat dexterity = new Stat(10);     // 손재주
    public Stat stamina = new Stat(10);       // 지구력

    public void RemoveModifiersFromSource(object source)
    {
        strengths.RemoveAllModifiersFromSource(source);
        agility.RemoveAllModifiersFromSource(source);
        intelligence.RemoveAllModifiersFromSource(source);
        dexterity.RemoveAllModifiersFromSource(source);
        stamina.RemoveAllModifiersFromSource(source);
    }
}

public class InventoryStats : IStatComponent
{
    public Stat maxInventory = new Stat(10);  // 최대 인벤토리 크기

    public void RemoveModifiersFromSource(object source)
    {
        maxInventory.RemoveAllModifiersFromSource(source);
    }
}